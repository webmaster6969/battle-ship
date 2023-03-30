using Nakama.TinyJson;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Tilemaps;


public class PositionState
{
    public float X;
}



public class EventTiles : MonoBehaviour
{
    private GameConnection _connection;

    void Start()
    {
        //This is probably not the best way to get references to
        //these objects but it works for this example
        //gd = gameObject.GetComponentInParent<Grid>();
        //tm = gameObject.GetComponent<Tilemap>();
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            /*_connection = Scene01MainMenuController.getConnect();
            _managerTiles = Scene01MainMenuController.getManagerTiles();

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int posInt = _managerTiles.mainGrid.LocalToCell(pos);

            posInt.z = 0;
            TileBase t = _managerTiles.gdEnemy.GetTile(posInt);

            int posClick = posInt.x + ((12 - posInt.y) * 10) - 1;


            if (posClick >= 0 && posClick < 101 && t != null)
            {
                Debug.Log(t.name);
                var state = new PositionState
                {
                    X = posClick
                };

                _connection.Socket.SendMatchStateAsync(_connection.match.Payload, 1, JsonWriter.ToJson(state));
                //_managerTiles.gdEnemy.SetTile(posInt, null);
            } else
            {
                Debug.Log(t);
            }*/
            
        }
    }
}
