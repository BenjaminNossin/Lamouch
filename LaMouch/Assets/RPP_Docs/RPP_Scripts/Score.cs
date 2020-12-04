using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;  

public class Score : MonoBehaviour
{
    [Range(1, 30)] public int HP = 20; 
    public Text score, lives;
    public static int scoreInt;
    public static int livesInt;
    public bool isInEndScene = false;
    public Sound impactSound;
    public Sound gameOverSound;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "TestScene1")
        {
            EnemyAI.OnTouchingPlayer += LoseLIfe;
            Bullet.OnKillingFly += AddScore;
        }
    }

    private void Start()
    {
        livesInt = HP; 
        if (SceneManager.GetActiveScene().name == "TestScene1")
        {
            impactSound.source.outputAudioMixerGroup = impactSound.group;
            gameOverSound.source.outputAudioMixerGroup = gameOverSound.group;
        }

        if (!isInEndScene)
        {
            scoreInt = 0;
        }        
    }

    void Update()
    {
        if (!isInEndScene)
        {
            score.text = ": " + scoreInt;
            lives.text = ": " + livesInt;
        }        
        else
        {
            score.text = "" + scoreInt;
        }

        if (livesInt <= 0 && SceneManager.GetActiveScene().name == "TestScene1")
        {
            gameOverSound.source.PlayOneShot(gameOverSound.clip);
            StartCoroutine(LoadNewScene()); 
        }
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadSceneAsync("BestScoreScene", LoadSceneMode.Single);
    }
    

    public void AddScore()
    {
        scoreInt++;
    }

    public void LoseLIfe()
    {
        if (livesInt > 0)
        {
            livesInt--;
            impactSound.source.PlayOneShot(impactSound.clip);
        }
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == "TestScene1")
        {
            EnemyAI.OnTouchingPlayer -= LoseLIfe;
            Bullet.OnKillingFly -= AddScore;
        }
    }
}
