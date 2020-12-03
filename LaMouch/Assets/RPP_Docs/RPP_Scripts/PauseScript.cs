using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] GameObject pauseButton, pausePanel;

    private void Start()
    {
        pausePanel.SetActive(false);
    }

    public void StartPause()
    {
        Time.timeScale = 0f;        
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void StopPause()
    {
        Time.timeScale = 1f;        
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void ReturnToMenu()
    {
        //Returns to the Menu Scene
        Debug.Log("Has returned to menu");
    }
}