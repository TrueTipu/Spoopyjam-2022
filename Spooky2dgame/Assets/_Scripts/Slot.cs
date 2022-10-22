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
            CardBlueprint.Suit _attackStatus = _card.Data().GetSuit();
            if (((_currentStatus.bishop != 0 && _attackStatus.bishop != 0) ||
                (_currentStatus.doctor != 0 && _attackStatus.doctor != 0) ||
                (_currentStatus.hunter != 0 && _attackStatus.hunter != 0))
                && card.Data().IsDarkSide() && !_card.Data().IsDarkSide()
                && attackers.Contains(_card) == false)
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

    private void Attack(CardBlueprint.Suit _currentStatus, Card _mainAttacker)
    {
        CardBlueprint.Suit _defendStatus = new CardBlueprint.Suit(_currentStatus);
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
            card.gameObject.SetActive(false);
            Clear();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear()
    {
        card = null;
        avaible = true;
        attackers = new List<Card>();
    }
}
