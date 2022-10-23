using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject loseScreen;


    public void Win()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }
}
