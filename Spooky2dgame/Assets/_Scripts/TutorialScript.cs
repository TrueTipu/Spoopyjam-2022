using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] GameObject object1;
    [SerializeField] GameObject object2;
    [SerializeField] GameObject object3;
    [SerializeField] GameObject object4;
    [SerializeField] GameObject object5;
    [SerializeField] GameObject object6;


    public static TutorialScript Instance;

    public int flip = 0;
    public int destroyed = 0;

    private void Awake()
    {
        Instance = this;
    }
        private void Start()
    {
        StartCoroutine(Tutorial());
    }

    IEnumerator Tutorial()
    {
        object1.SetActive(true); //nosta 3 korttii klikkaamal left klick tai spacel
        while(CardManager.Instance.cardsDrawn < 3)
        {
            yield return null;
        }
        object1.SetActive(false);
        object2.SetActive(true); //voit siirtää kortteja leftclick ja drag tyhjään kohtaan
        while (DragAndDropManager.Instance.cardsSiirtynyt < 1)
        {
            yield return null;
        }
        object2.SetActive(false);
        object3.SetActive(true); //voit kääntää kortteja right klick niitä
        while (flip < 1)
        {
            yield return null;
        }
        object3.SetActive(false);
        object4.SetActive(true); //Näet vasemmasta yläkulmasta mitä kortteja tarvitaan mihinkin viholliseen, tapa vihollinen vetämällä oikea kortti sen päälle
        while (CardManager.Instance.cardsWon < 1)
        {
            yield return null;
        }
        object4.SetActive(false);
        object5.SetActive(true); //voit myös tuhota kortteja vetämällä ne oikeaan alakulmaan
        while (destroyed < 1)
        {
            yield return null;
        }
        object5.SetActive(false);
        object6.SetActive(true); //Tavoitteesi on voittaa tarpeeksi monta viholliskorttia(yläkeskellä, jos epäonnistut paina oikeasta yläkulmasta aloittaaksesi alusta
    }
}
