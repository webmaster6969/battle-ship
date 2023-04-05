package server_game_sea_battle

import (
	"encoding/json"
	"github.com/heroiclabs/nakama-common/runtime"
)

type MatchState struct {
	Debug       bool    `json:"Debug"`
	PlayersGame Players `json:"PlayersGame"`
	Status      int     `json:"Status"`
}

const TypeGridGameCell = 1      // Тип клетки, что она уничтожена
const TypeGridDestroyedCell = 2 // Тип клетки, что она уничтожена

const StatusStateClientMove = 1 // Ход клиента
const StatusStateBotMove = 2    // Ход бота

// AttackTheBot Атакуем бота
func (m *MatchState) AttackTheBot(x int, y int) bool {
	cell := m.PlayersGame.PlayerBot.Board.Grid.FindCell(Location{x, y})
	if cell != nil && cell.Status != BoardCellTypeEmpty {
		return true
	}
	return m.PlayersGame.PlayerBot.Board.Shoot(x, y)
}

// AttackTheClient Атакуем клиента
func (m *MatchState) AttackTheClient(x int, y int) bool {
	return m.PlayersGame.PlayerClient.Board.Shoot(x, y)
}

// GenerationData Генерируем данные JSON
func (m *MatchState) GenerationData() []byte {
	var data, _ = json.Marshal(m)

	return data
}

func (m *MatchState) SendStateClient(dispatcher runtime.MatchDispatcher, Msg []runtime.Presence) {
	var data = m.GenerationData()
	err := dispatcher.BroadcastMessage(SendAllGridsOpCode, data, Msg, nil, true)
	if err != nil {
		panic(err)
	}
}
