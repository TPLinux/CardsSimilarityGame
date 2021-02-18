using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float delayBeforeFlip = 1.5f;
    public Sprite cardBackFaceSprite;

    [Header("Cards")]
    public GameObject mainCardUIObject;
    public List<Card> cards;
    [Header("Ui Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    bool flipBack;
    bool pause;

    void Start()
    {
        SetupCards();
    }

    void Update()
    {
        if (Time.time > delayBeforeFlip)
        {
            FlipCards();
        }
    }

    void SetupCards()
    {
        // setup main card
        mainCardUIObject.GetComponent<Image>().sprite = GetMainCard().cardSprite;
        foreach (Card card in cards)
        {
            card.Init();
        }
    }

    void FlipCards()
    {
        foreach (Card card in cards)
        {
            card.Flip(flipBack);
        }
    }

    public Card GetCardByName(string name)
    {
        return cards.Find(c => c.name == name);
    }

    public Card GetMainCard()
    {
        return cards.Find(c => c.isMain);
    }

    public void ChooseCard(string cardName)
    {
        if (!pause)
        {
            flipBack = true;
            if (cardName == GetMainCard().name)
            {
                Invoke("Win", 2.5f);
            }
            else
            {
                Invoke("Lose", 2.5f);
            }
        }
    }

    public void Win()
    {
        winPanel.SetActive(true);
    }
    public void Lose()
    {
        losePanel.SetActive(true);
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(0);
    }
}
