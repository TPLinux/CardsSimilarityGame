using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card
{
    public string name;
    public Sprite cardSprite;
    public GameObject UIObject;
    public bool isMain;
    public float flipSpeed = 10f;

    public void Init()
    {
        // set up sprite (card image)
        UIObject.transform.GetComponent<Image>().sprite = cardSprite;
        UIObject.GetComponent<Button>().onClick.AddListener(delegate { GameObject.FindObjectOfType<GameController>().ChooseCard(name); });
    }

    public void Flip(bool flipBack)
    {
        // get current rotation of the card
        Quaternion currentRotation = UIObject.transform.transform.rotation;

        float angle = -180f;
        bool changeSprite = currentRotation.eulerAngles.y >= 90;
        // set target rotation of the card
        if (flipBack)
        {
            angle = 0f;
            changeSprite = currentRotation.eulerAngles.y <= 90;
        }

        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.up);
        // if card rotation is 45 change the current sprite;
        if (changeSprite)
        {
            if (!flipBack)
            {
                UIObject.transform.GetComponent<Image>().sprite = GameObject.FindObjectOfType<GameController>().cardBackFaceSprite;
            }
            else
            {
                UIObject.transform.GetComponent<Image>().sprite = cardSprite;
            }
        }
        // rotate the card to 180;
        UIObject.transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, Time.deltaTime * flipSpeed);
    }
}
