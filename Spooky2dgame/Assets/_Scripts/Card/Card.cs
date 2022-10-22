using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Card : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] CardBlueprint lightSide;
    [SerializeField] CardBlueprint darkSide;

    bool flipped;

    Slot currentSlot;

    private void Awake()
    {
        currentSlot = null;
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if(lightSide != null && darkSide != null)
        {
            SetData(lightSide, darkSide);
        }
    }

    public void SetData(CardBlueprint _blueprintL, CardBlueprint _blueprintD)
    {
        lightSide = _blueprintL;
        darkSide = _blueprintD;
        spriteRenderer.sprite = lightSide.GetSprite();
    }

    public CardBlueprint Data()
    {
        if(flipped == false)
        {
            return lightSide;
        }
        else
        {
            return darkSide;
        }
    }
    private void OnMouseDown()
    {
        print(Data().IsDarkSide());
    }
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Flip();
        }
    }

    public void Flip()
    {
        if (!flipped)
        {
            flipped = true;
            spriteRenderer.sprite = darkSide.GetSprite();
        }
    }

    public void SetSlot(Slot _slot)
    {
        if(currentSlot != null) currentSlot.Clear();
        currentSlot = _slot;
    }

    public void DestroyCard()
    {
        currentSlot.Clear();
        currentSlot = null;
        Destroy(gameObject);
    }

    internal Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }
}
