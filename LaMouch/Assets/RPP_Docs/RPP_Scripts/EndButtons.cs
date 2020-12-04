using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButtons : MonoBehaviour
{
    public Sound buttonSelection;
    public Sound quitGame; 

    public void RestartGame()
    {
        PlaySound();
        StartCoroutine(ButtonInput(1));
    }

    public void BackToMenu()
    {
        PlaySound();
        StartCoroutine(ButtonInput(0)); 
    }

    private void PlaySound()
    {
        buttonSelection.source.outputAudioMixerGroup = buttonSelection.group;
        buttonSelection.source.PlayOneShot(buttonSelection.clip);
    }

    

    IEnumerator ButtonInput(int index)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
    }

}
