using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] List<Card> deck = new List<Card>();

    [SerializeField] Slot[] cardSlots;

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
}