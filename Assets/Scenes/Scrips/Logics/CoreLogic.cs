using UnityEngine;

// ������ �������� �� ������ � ���������� ���������� ����
public struct GameState
{

    // ��������� ������
    public GameState(int Status, Vector2Int Position) {
        this.Position = Position;
        this.Status = Status;
    }

    public int GetStatus() {
        return this.Status;
    }

    public void SetStatus(int Status)
    {
        this.Status = Status;
    }

    // ������� ������
    public Vector2Int Position;

    // ������ ������
    public int Status;
}

// ���� ����� �����
public struct GameData
{
    public GameData(int Width, int Height)
    {
        StateClient = new GameState[Width, Height];
        StateAI = new GameState[Width, Height];
    }


    // ���� ������
    public GameState[,] StateClient;

    // ���� ����
    public GameState[,] StateAI;
}

public class CoreLogic
{

    // ����� ����� � �� ���������
    protected GameData stateGame;

    public GameData GetGameData()
    {
        return this.stateGame;
    }

    // ������������� ��������� ������
    public CoreLogic()
    {
        // ������� ��������� �����
        stateGame = new GameData(10, 10);
        GenerationShip generationShipClient = new GenerationShip();
        GenerationShip generationShipAI = new GenerationShip();
        // ��������� ��������
        int[,] shipsClient = generationShipClient.Generation(10);
        int[,] shipsAI = generationShipAI.Generation(10);

        // ��������� �����
        for (int y = 0; y < 10; y++) 
        {
            for (int x = 0; x < 10; x++)
            {
                int readX = x;
                int readY = y;

                // ��������� ������ ��� �������
                if (shipsClient[readX, readY] == Cell.CELL_SHIP)
                {
                    stateGame.StateClient[readX, readY].Status = Cell.CELL_SHIP;
                } else
                {
                    stateGame.StateClient[readX, readY].Status = Cell.CELL_EMPTY;
                }


                // ��������� ������ ��� AI
                if (shipsAI[readX, readY] == Cell.CELL_SHIP)
                {
                    stateGame.StateAI[readX, readY].Status = Cell.CELL_SHIP;
                }
                else
                {
                    stateGame.StateAI[readX, readY].Status = Cell.CELL_EMPTY;
                }

                stateGame.StateClient[readX, readY].Position = new Vector2Int(readX, readY);
                stateGame.StateAI[readX, readY].Position = new Vector2Int(readX, readY);

            }
        }
    }

    // ��� �������
    public virtual void MoveAI()
    {
        int x = Random.Range(0, 10);
        int y = Random.Range(0, 10);

        switch (stateGame.StateClient[x, y].GetStatus())
        {
            case Cell.CELL_EMPTY:
                stateGame.StateClient[x, y].SetStatus(Cell.CELL_MISS);
                break;
            case Cell.CELL_MISS:
                this.MoveAI();
                break;

            case Cell.CELL_HIT:
                this.MoveAI();
                break;
            case Cell.CELL_SHIP:
                stateGame.StateClient[x, y].SetStatus(Cell.CELL_HIT);
                this.MoveAI();
                break;
        }
    }

    // ���� �� ����
    public virtual void WhoClick(int x, int y)
    {
        switch (stateGame.StateAI[x, y].GetStatus())
        {
            case Cell.CELL_EMPTY:
                stateGame.StateAI[x, y].SetStatus(Cell.CELL_MISS);
                this.MoveAI();
                break;
            case Cell.CELL_SHIP:
                stateGame.StateAI[x, y].SetStatus(Cell.CELL_HIT);
                break;
        }
    }
}
