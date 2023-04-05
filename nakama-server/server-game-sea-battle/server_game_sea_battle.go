// Copyright 2020 The Nakama Authors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

package server_game_sea_battle

import (
	"context"
	"database/sql"
	"github.com/heroiclabs/nakama-common/api"
	"github.com/heroiclabs/nakama-common/runtime"
)

func InitModule(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, initializer runtime.Initializer) error {

	if err := initializer.RegisterRpc("go_echo_sample", rpcEcho); err != nil {
		return err
	}
	if err := initializer.RegisterRpc("rpc_create_match", rpcCreateMatch); err != nil {
		return err
	}
	if err := initializer.RegisterMatch("match", func(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule) (runtime.Match, error) {
		return &Match{}, nil
	}); err != nil {
		return err
	}
	if err := initializer.RegisterEventSessionStart(eventSessionStart); err != nil {
		return err
	}
	if err := initializer.RegisterEventSessionEnd(eventSessionEnd); err != nil {
		return err
	}
	if err := initializer.RegisterEvent(func(ctx context.Context, logger runtime.Logger, evt *api.Event) {
		logger.Info("Received event: %+v", evt)
	}); err != nil {
		return err
	}

	return nil
}

func rpcEcho(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {
	logger.Info("RUNNING IN GO")
	return payload, nil
}

func rpcCreateMatch(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, payload string) (string, error) {

	matchID, err := nk.MatchCreate(ctx, "match", map[string]interface{}{})

	if err != nil {
		return "", err
	}

	return matchID, nil
}

type Match struct{}

func (m *Match) MatchInit(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, params map[string]interface{}) (interface{}, int, string) {
	var debug bool
	if d, ok := params["debug"]; ok {
		if dv, ok := d.(bool); ok {
			debug = dv
		}
	}

	// Создаем состояние
	state := &MatchState{
		Debug:  debug,
		Status: StatusStateClientMove,
	}

	state.PlayersGame.PlayerClient = Player{Name: "c1", Board: NewBoard(10, []int{4, 3, 3, 2, 2, 2, 1, 1, 1, 1})}
	state.PlayersGame.PlayerBot = Player{Name: "c2", Board: NewBoard(10, []int{4, 3, 3, 2, 2, 2, 1, 1, 1, 1})}

	//for i := 0; i < len(state.ClientGrid); i++ {
	//	state.ClientGrid[i] = 0
	//	state.BotGrid[i] = 0
	//}

	if state.Debug {
		logger.Info("match init, starting with debug: %v", state.Debug)
	}
	tickRate := 20
	label := "skill=100-150"

	return state, tickRate, label
}

func (m *Match) MatchJoinAttempt(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, dispatcher runtime.MatchDispatcher, tick int64, state interface{}, presence runtime.Presence, metadata map[string]string) (interface{}, bool, string) {
	if state.(*MatchState).Debug {
		logger.Info("match join attempt username %v user_id %v session_id %v node %v with metadata %v", presence.GetUsername(), presence.GetUserId(), presence.GetSessionId(), presence.GetNodeId(), metadata)
	}

	return state, true, ""
}

func (m *Match) MatchJoin(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, dispatcher runtime.MatchDispatcher, tick int64, state interface{}, presences []runtime.Presence) interface{} {
	if state.(*MatchState).Debug {
		for _, presence := range presences {
			logger.Info("match join username %v user_id %v session_id %v node %v", presence.GetUsername(), presence.GetUserId(), presence.GetSessionId(), presence.GetNodeId())
		}
	}

	// Посылаем всем состояние
	stateGame := state.(*MatchState)
	err := dispatcher.BroadcastMessage(SendAllGridsOpCode, stateGame.GenerationData(), nil, nil, true)
	if err != nil {
		return nil
	}

	return state
}

func (m *Match) MatchLeave(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, dispatcher runtime.MatchDispatcher, tick int64, state interface{}, presences []runtime.Presence) interface{} {
	if state.(*MatchState).Debug {
		for _, presence := range presences {
			logger.Info("match leave username %v user_id %v session_id %v node %v", presence.GetUsername(), presence.GetUserId(), presence.GetSessionId(), presence.GetNodeId())
		}
	}

	return state
}

func (m *Match) MatchLoop(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, dispatcher runtime.MatchDispatcher, tick int64, state interface{}, messages []runtime.MatchData) interface{} {

	// Получаем state
	stateGame := state.(*MatchState)

	if stateGame.Debug {
		logger.Info("match loop match_id %v tick %v", ctx.Value(runtime.RUNTIME_CTX_MATCH_ID), tick)
		logger.Info("match loop match_id %v message count %v", ctx.Value(runtime.RUNTIME_CTX_MATCH_ID), len(messages))
	}

	// Обрабатываем входящии сообщения
	for i := 0; i < len(messages); i++ {
		if messages[i].GetOpCode() == SendClickCoordinateOpCode {
			ClientSendClickCoordinate(ctx, logger, db, nk, dispatcher, tick, state, messages, i)
		}

		/*if stateGame.Status == StatusStateBotMove {
			BotAttack(ctx, logger, db, nk, dispatcher, tick, state, messages, i)
		}*/

	}

	// Начинаем заново, у клиента или у бота все корабли потоплены
	if !stateGame.PlayersGame.PlayerClient.Board.HasShipsLeft() || !stateGame.PlayersGame.PlayerBot.Board.HasShipsLeft() {
		stateGame.PlayersGame.PlayerClient = Player{Name: "c1", Board: NewBoard(10, []int{4, 3, 3, 2, 2, 2, 1, 1, 1, 1})}
		stateGame.PlayersGame.PlayerBot = Player{Name: "c2", Board: NewBoard(10, []int{4, 3, 3, 2, 2, 2, 1, 1, 1, 1})}
		stateGame.SendStateClient(dispatcher, nil)
	}

	/*if tick >= 200 {
		return nil
	}*/
	return state
}

func (m *Match) MatchTerminate(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, dispatcher runtime.MatchDispatcher, tick int64, state interface{}, graceSeconds int) interface{} {
	if state.(*MatchState).Debug {
		logger.Info("match terminate match_id %v tick %v", ctx.Value(runtime.RUNTIME_CTX_MATCH_ID), tick)
		logger.Info("match terminate match_id %v grace seconds %v", ctx.Value(runtime.RUNTIME_CTX_MATCH_ID), graceSeconds)
	}

	return state
}

func (m *Match) MatchSignal(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, dispatcher runtime.MatchDispatcher, tick int64, state interface{}, data string) (interface{}, string) {
	if state.(*MatchState).Debug {
		logger.Info("match signal match_id %v tick %v", ctx.Value(runtime.RUNTIME_CTX_MATCH_ID), tick)
		logger.Info("match signal match_id %v data %v", ctx.Value(runtime.RUNTIME_CTX_MATCH_ID), data)
	}
	return state, "signal received: " + data
}

func eventSessionStart(ctx context.Context, logger runtime.Logger, evt *api.Event) {
	logger.Info("session start %v %v", ctx, evt)
}

func eventSessionEnd(ctx context.Context, logger runtime.Logger, evt *api.Event) {
	logger.Info("session end %v %v", ctx, evt)
}
