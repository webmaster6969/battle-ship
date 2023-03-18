using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPole : MonoBehaviour
{

    public GameObject whoPerent = null;
    public int coordX, CoordY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(whoPerent != null)
        {
            whoPerent.GetComponent<GamePole>().WhoClick(coordX, CoordY);
        }
    }
}
