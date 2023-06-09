package server_game_sea_battle

import (
	"encoding/json"
	"fmt"
	"strconv"
	"strings"
)

type Players struct {
	PlayerClient Player `json:"PlayerClient"`
	PlayerBot    Player `json:"PlayerBot"`
}

func Play(player1Name, player2Name string, boardSize int, shipCounts []int) {
	// Инициализация игроков и досок
	player1 := Player{Name: player1Name, Board: NewBoard(boardSize, shipCounts)}
	player2 := Player{Name: player2Name, Board: NewBoard(boardSize, shipCounts)}

	var data, _ = json.Marshal(player1)
	fmt.Println(string(data))

	currentPlayer := &player1
	otherPlayer := &player2

	// Цикл игры
	for otherPlayer.Board.HasShipsLeft() {

		fmt.Printf("%s's turn\n\n", currentPlayer.Name)
		otherPlayer.Board.Print()

		fmt.Println("\nEnter coordinates to shoot (ex. A1): ")
		var coords string
		fmt.Scanln(&coords)

		// Парсинг координат выстрела
		x, y := parseCoordinate(coords)

		// Совершение выстрела
		if otherPlayer.Board.Shoot(x, y) {
			fmt.Println("Hit!")
			currentPlayer.Hits++
		} else {
			fmt.Println("Miss!")
			currentPlayer.Misses++
			// Переключение хода к другому игроку
			currentPlayer, otherPlayer = otherPlayer, currentPlayer
		}

		// Переключение хода к другому игроку
		//currentPlayer, otherPlayer = otherPlayer, currentPlayer
	}

	// Конец игры

	fmt.Printf("%s's board:\n\n", player1.Name)
	player1.Board.Print()
	fmt.Printf("\n%s's board:\n\n", player2.Name)
	player2.Board.Print()

	if player1.Board.HasShipsLeft() {
		fmt.Printf("\n%s wins! Congratulations!\n", player1.Name)
	} else {
		fmt.Printf("\n%s wins! Congratulations!\n", player1.Name)
	}
}

func parseCoordinate(text string) (int, int) {
	text = strings.TrimSpace(text)
	if len(text) < 2 {
		return -1, -1
	}

	x := int(text[0] - 'A')
	y, err := strconv.Atoi(text[1:])
	if err != nil {
		return -1, -1
	}
	y--

	return x, y
}
