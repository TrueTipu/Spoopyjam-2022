using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour, IDroppable
{

    public bool avaible = true;

    Card card;

    List<Card> attackers = new List<Card>();


    public Vector3 Position { get { return transform.position; } set { return; } }

    public bool Active { get { return true; } set { return; } }


    [SerializeField] List<SpriteRenderer> images;

    [SerializeField] List<Sprite> suitImages;

    public void Activate(Card _card)
    {
        if (avaible)
        {
            card = _card;
            card.gameObject.SetActive(true);
            card.transform.position = transform.position;
            card.SetSlot(this);

            avaible = false;
        }
        else
        {
            CardBlueprint.Suit _currentStatus = card.Data().GetSuit();
            if(CanAttack(_currentStatus, _card))
            {
                Attack(_currentStatus, _card);
            }

            else
            {
                _card.gameObject.SetActive(true);
                return;
            }
        }

    }
    bool CanAttack(CardBlueprint.Suit _currentStatus, Card _card)
    {
        CardBlueprint.Suit _attackStatus = _card.Data().GetSuit();
        return (((_currentStatus.bishop != 0 && _attackStatus.bishop != 0) ||
            (_currentStatus.doctor != 0 && _attackStatus.doctor != 0) ||
            (_currentStatus.hunter != 0 && _attackStatus.hunter != 0))
            && card.Data().IsDarkSide() && !_card.Data().IsDarkSide()
            && attackers.Contains(_card) == false);
    }

    private void Update()
    {
        if(attackers.Count == 0)
        {
            for (int j = 0; j < images.Count; j++)
            {
                images[j].gameObject.SetActive(false);
            }
        }
        List<SuitForImages> _suits = new List<SuitForImages>();
        foreach (Card _attacker in attackers)
        {
            if (_attacker.Data().GetSuit().doctor != 0)
            {
                _suits.Add(SuitForImages.doctor);
            }
            if (_attacker.Data().GetSuit().hunter != 0)
            {
                _suits.Add(SuitForImages.hunter);
            }
            if (_attacker.Data().GetSuit().bishop != 0)
            {
               _suits.Add(SuitForImages.bishop);
            }
        }
        int i = 0;
        foreach (SuitForImages _suit in _suits)
        {
            images[i].gameObject.SetActive(true);
            images[i].sprite = suitImages[(int)_suit];
            i++;
            if(i >= images.Count)
            {
                break;
            }
        }
    }
    enum SuitForImages
    {
        bishop,
        doctor,
        hunter
    }

    private void Attack(CardBlueprint.Suit _currentStatus, Card _mainAttacker)
    {
        CardBlueprint.Suit _defendStatus = new CardBlueprint.Suit(_currentStatus);


        DragAndDropManager.Instance.dragNewCard += (_card, _slot) => {
            if (!(_slot == this && CanAttack(card.Data().GetSuit(), _card))){
                ClearAttackers(false);
            }
        };


        attackers.Add(_mainAttacker);
        foreach (Card _attacker in attackers)
        {
            if(_attacker.Data().GetSuit().doctor != 0)
            {
                _defendStatus.doctor -= _attacker.Data().GetSuit().doctor;
            }
            if (_attacker.Data().GetSuit().hunter != 0)
            {
                _defendStatus.hunter -= _attacker.Data().GetSuit().hunter;
            }
            if (_attacker.Data().GetSuit().bishop != 0)
            {
                _defendStatus.bishop -= _attacker.Data().GetSuit().bishop;
            }
        }

        if (_defendStatus.doctor <= 0 && _defendStatus.hunter <= 0 && _defendStatus.bishop <= 0)
        {
            ClearAttackers(true);
            card.DestroyCard();
            CardManager.Instance.UpdateWinCount();
        }
    }



    public void Clear()
    {
        card = null;
        avaible = true;
    }
    private void ClearAttackers(bool delete)
    {
        foreach(Card _attacker in attackers)
        {
            //poista efekti
            if (delete)
            {
                _attacker.DestroyCard();
            }
        }
        attackers = new List<Card>();
    }
}
