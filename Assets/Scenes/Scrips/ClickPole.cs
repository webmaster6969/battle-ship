using UnityEngine;


public class ClickPole : MonoBehaviour
{
    public GameObject EventManager;
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
        
        if (EventManager != null)
        {
            EventManager.GetComponent<EventManager>().Notify("GameEvents", new DataObserver(DataObserver.WHO_CLICK, new Vector2Int(CoordY, CoordY)));
        }
    }
}