using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSlot : MonoBehaviour, IDroppable
{

    public Vector3 Position { get { return transform.position; } set { return; } }

    public bool Active { get { return true; } set { return; } }

    public void Activate(Card _card)
    {
        _card.gameObject.SetActive(false);
        _card.DestroyCard();

    }
}
