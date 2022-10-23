using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] string sceneName;

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName);
    }

}