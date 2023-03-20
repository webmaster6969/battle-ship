using UnityEngine;

public class Chanks : MonoBehaviour
{
    public Sprite[] imgs;
    public int index = 0;

    //  аждый раз при отрисовки сцены, присваиваем правильное изоброжение пол€
    public void ChangeImgs()
    {
        if (imgs.Length > index)
        {
            GetComponent<SpriteRenderer>().sprite = imgs[index];
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        ChangeImgs();
    }

    // Update is called once per frame
    private void Update()
    {
        ChangeImgs();
    }
}