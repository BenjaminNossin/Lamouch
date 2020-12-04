using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager: MonoBehaviour
{
    public Sound startGameSound;
    public Sound quitGameSound; 

    public void StartButton()
    {
        PlaySound(startGameSound);
        StartCoroutine(StartGame()); 
    }

    public void QuitButton()
    {
        PlaySound(quitGameSound); 
        StartCoroutine(QuitGame()); 
    }

    private void PlaySound(Sound sound)
    {
        sound.source.outputAudioMixerGroup = sound.group;
        sound.source.PlayOneShot(sound.clip);
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }
}
