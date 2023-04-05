package server_game_sea_battle

import (
	"context"
	"database/sql"
	"encoding/json"
	"github.com/heroiclabs/nakama-common/runtime"
	"math/rand"
)

const SendClickCoordinateOpCode = 1 // Клиент прислал по какой точке хочет атаковать
const SendAllGridsOpCode = 2        // Рассылка состояния полотна
const SendChangeState = 3           // Смена состояния

type clientSendClickCoordinateData struct {
	X int
	Y int
}

// ClientSendClickCoordinate Клиент атакует бота
func ClientSendClickCoordinate(ctx context.Context, logger runtime.Logger, db *sql.DB, nk runtime.NakamaModule, dispatcher runtime.MatchDispatcher, tick int64, state interface{}, messages []runtime.MatchData, idMessage int) {

	var m clientSendClickCoordinateData
	stateGame := state.(*MatchState)
	err := json.Unmarshal(messages[idMessage].GetData(), &m)
	if err != nil {
		panic(err)
	}

	logger.Info("Click %v", m.X)

	// Атакуем бота
	hitClient := stateGame.AttackTheBot(m.X, m.Y)

	// Если не попали, делает ход бот, пока не промажет
	if !hitClient {
		for {
			hitBot := stateGame.AttackTheClient(rand.Intn(10), rand.Intn(10))
			if !hitBot {
				break
			}
		}

	}

	stateGame.SendStateClient(dispatcher, []runtime.Presence{messages[idMessage]})

}
