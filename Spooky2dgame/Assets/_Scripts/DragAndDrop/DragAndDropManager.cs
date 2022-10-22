using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using System;

public class DragAndDropManager : MonoBehaviour
{
    Card currentCard;
    [SerializeField] Image customCursor;

    List<IDroppable> slots;

    [SerializeField]
    float minDist;

    public Action<Card, IDroppable> dragNewCard = (a, b) => { return; };

    public static DragAndDropManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        slots = new List<IDroppable>();
        var ss = FindObjectsOfType<MonoBehaviour>().OfType<IDroppable>();
        foreach (IDroppable s in ss)
        {
            slots.Add(s);
        }
    }
    
    public void ReLoad()
    {
        slots = new List<IDroppable>();
        var ss = FindObjectsOfType<MonoBehaviour>().OfType<IDroppable>();
        foreach (IDroppable s in ss)
        {
            slots.Add(s);
        }
    }

    public void OnMouseDownItem(Card _card)
    {
        if (currentCard == null)
        {
            currentCard = _card;
            _card.gameObject.SetActive(false);

            customCursor.gameObject.SetActive(true);
            customCursor.sprite = currentCard.GetSprite();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (currentCard != null)
            {
                customCursor.gameObject.SetActive(false);
                IDroppable _currentSlot = null;
                float _shortestDis = float.MaxValue;

                foreach (IDroppable slot in slots)
                {
                    if (slot.Active == true)
                    {
                        Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                        float dist = Vector2.Distance(mouseWorldPosition, slot.Position);

                        if (dist < _shortestDis && dist < minDist)
                        {
                            _shortestDis = dist;
                            _currentSlot = slot;
                        }
                    }
                }

                currentCard.gameObject.SetActive(true);

                Debug.Log(dragNewCard + " " + currentCard + " " + _currentSlot);
                dragNewCard.Invoke(currentCard, _currentSlot);
                dragNewCard = (a, b) => { return; };
                if (_currentSlot != null)
                {
                    
                    _currentSlot.Activate(currentCard);
                }

                currentCard = null;
                
            }
        }
    }

}
