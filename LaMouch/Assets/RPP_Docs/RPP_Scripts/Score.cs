using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;  

public class Score : MonoBehaviour
{
    [Range(1, 40)] public int HP = 20; 
    public Text score, lives;
    public static int scoreInt;
    public static int livesInt;
    public bool isInEndScene = false;
    public Sound impactSound;
    public Sound gameOverSound;

    private bool newSceneLoaded; 

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "TestScene1")
        {
            LevelManager.OnReachingNewDifficulty += AddHP;
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

        if (livesInt <= 0 && SceneManager.GetActiveScene().name == "TestScene1" && !newSceneLoaded)
        {
            newSceneLoaded = true; 
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

    public void AddHP(int amount)
    {
        livesInt += amount; 
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == "TestScene1")
        {
            LevelManager.OnReachingNewDifficulty -= AddHP; 
            EnemyAI.OnTouchingPlayer -= LoseLIfe;
            Bullet.OnKillingFly -= AddScore;
        }
    }
}
