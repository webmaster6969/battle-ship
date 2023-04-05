package server_game_sea_battle

type Cell struct {
	Location Location `json:"Location"`
	Status   int      `json:"Status"`
}

func (c *Cell) SetStatus(Status int) {
	c.Status = Status
}
