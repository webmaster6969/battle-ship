using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chanks : MonoBehaviour
{
    public Sprite[] imgs;
    public int index = 0;

    public void ChangeImgs()
    {
        if(imgs.Length > index)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[index];
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ChangeImgs();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeImgs();
    }
}
