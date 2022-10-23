using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardManager : MonoBehaviour
{
    List<Card> deck = new List<Card>();


    [SerializeField] List<CardBlueprint> lCards;
    [SerializeField] List<CardBlueprint> dCards;

    [SerializeField] Slot[] cardSlots;

    [SerializeField] GameObject cardObject;

    [SerializeField] Text deckCountText;
    [SerializeField] Text winCountText;
    int deckCount;
    [SerializeField]int winCount;

    public static CardManager Instance;


    private void Awake()
    {
        Instance = this;
        GenerateCards();

        winCountText.text = winCount.ToString();
        deckCountText.text = deckCount.ToString();
    }

    void GenerateCards()
    {

        while(dCards.Count > 0)
        {
            int r = Random.Range(0, lCards.Count);
            CardBlueprint _lCard = lCards[Random.Range(0, lCards.Count)];
            CardBlueprint _dCard = dCards[Random.Range(r, dCards.Count)];
            lCards.Remove(_lCard);
            dCards.Remove(_dCard);

            Card _newCard = Instantiate(cardObject, this.transform).GetComponent<Card>();
            _newCard.SetData(_lCard, _dCard);
            _newCard.gameObject.SetActive(false);

            deck.Add(_newCard);
        }
        deckCount = deck.Count;
    }

    public void DrawCard()
    {
        if(deck.Count >= 1)
        {
            Card _nextCard = deck[deck.Count - 1]; //vaihda randomiksi jos ei smart shufflea

            for (int i = 0; i < cardSlots.Length; i++)
            {
                if (cardSlots[i].avaible)
                {
                    cardSlots[i].Activate(_nextCard);
                    deck.Remove(_nextCard);
                    UpdateDeckCount();
                    return;
                }
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DrawCard();
        }
        
    }
    public void UpdateDeckCount()
    {
        AudioManager.instance.Play("Deck");
        deckCount = deck.Count;
        deckCountText.text = deckCount.ToString();
    }
    public void UpdateWinCount()
    {
        winCount--;
        if(winCount == 0)
        {
            CanvasManager.Instance.Invoke("Win", 2.5f);
        }
        winCountText.text = winCount.ToString();
    }
}