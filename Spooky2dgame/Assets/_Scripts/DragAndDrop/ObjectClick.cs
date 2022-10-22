using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectClick : MonoBehaviour
{
    private void OnMouseDown()
    {
        DragAndDropManager.Instance.OnMouseDownItem(GetComponent<Card>());
    }
}
