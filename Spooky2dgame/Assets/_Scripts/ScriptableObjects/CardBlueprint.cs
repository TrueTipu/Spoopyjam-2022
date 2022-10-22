using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Card", menuName = "Cards")]
public class CardBlueprint : ScriptableObject
{
    [SerializeField] Sprite sprite;
    public Sprite GetSprite() { return sprite; }

    [SerializeField] Suit suit;
    public Suit GetSuit() { return suit; }

    [SerializeField] bool darkSide;
    public bool IsDarkSide() { return darkSide; }

    [System.Serializable]
    public class Suit
    {
        [SerializeField]
        public int bishop;
        [SerializeField]
        public int doctor;
        [SerializeField]
        public int hunter;

        public Suit(Suit _suit)
        {
            bishop = _suit.bishop;
            doctor = _suit.doctor;
            hunter = _suit.hunter;
        }
    }
}

