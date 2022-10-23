using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSlot : MonoBehaviour, IDroppable
{

    [SerializeField] GameObject particles;

    public Vector3 Position { get { return transform.position; } set { return; } }

    public bool Active { get { return true; } set { return; } }

    public void Activate(Card _card)
    {
        if (!_card.Flipped())
        {
            _card.gameObject.SetActive(false);
            _card.DestroyCard();
            GameObject particlesI = Instantiate(particles, transform.position, transform.rotation);
            AudioManager.instance.Play("Destroy");
            Destroy(particlesI, 5);
            if (TutorialScript.Instance != null)
            {
                TutorialScript.Instance.destroyed++;
            }
        }
        else
        {
            _card.gameObject.SetActive(true);
            return;
        }
    }
}
