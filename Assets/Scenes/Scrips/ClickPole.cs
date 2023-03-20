using UnityEngine;


public class ClickPole : MonoBehaviour
{
    public GameObject whoPerent = null;
    public int coordX, CoordY;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    // Делать ход по полю в зависемости от нажати¤ мыши
    private void OnMouseDown()
    {
        if (whoPerent != null)
        {
            whoPerent.GetComponent<GamePole>().WhoClick(coordX, CoordY);
        }
    }
}