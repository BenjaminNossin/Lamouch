using UnityEngine;
using UnityEngine.SceneManagement;

public class EndButtons : MonoBehaviour
{
    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

}
