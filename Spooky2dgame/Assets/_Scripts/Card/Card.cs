using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer dSpriteRenderer;
    [SerializeField] Animator animator;


    [SerializeField] CardBlueprint lightSide;
    [SerializeField] CardBlueprint darkSide;

    public bool Flipped()
    {
        return flipped;
    }

    bool flipped;

    Slot currentSlot;

    [SerializeField] GameObject particles;


    private void Awake()
    {
        currentSlot = null;


        if (lightSide != null && darkSide != null)
        {
            SetData(lightSide, darkSide);
        }
    }


    private void OnEnable()
    {
        if (flipped)
        {
            animator.SetBool("Darker", true);
        }
    }

    public void SetData(CardBlueprint _blueprintL, CardBlueprint _blueprintD)
    {
        lightSide = _blueprintL;
        darkSide = _blueprintD;
        spriteRenderer.sprite = lightSide.GetSprite();
        dSpriteRenderer.sprite = darkSide.GetSprite();
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
            GameObject particlesI = Instantiate(particles, transform.position, transform.rotation);

            AudioManager.instance.Play("Flip");

            if(TutorialScript.Instance != null)
            {
                TutorialScript.Instance.flip++;
            }

            Destroy(particlesI, 5);
            flipped = true;
            animator.SetBool("Dark", true);
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
        if (!flipped)
        {
            return lightSide.GetSprite();
        }
        else
        {
            return darkSide.GetSprite();
        }

    }

    internal void HighLight()
    {
        currentSlot.particles2.SetActive(true);
    }

    internal void DisableHighlight()
    {
        currentSlot.particles2.SetActive(false);
    }
}
