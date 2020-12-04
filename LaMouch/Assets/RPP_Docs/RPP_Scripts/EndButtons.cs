using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButtons : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadScene("RPP_MainScene");
    }

    public void BackToMenu()
    {
        //Go Back to the Menu
    }

}
