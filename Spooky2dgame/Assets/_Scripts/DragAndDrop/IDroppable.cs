
using UnityEngine;

public interface IDroppable
{
    Vector3 Position { get; set; }

    bool Active { get; set; }

    void Activate(Card card);
}