using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;  

public class Score : MonoBehaviour
{
    public Text score, lives;
    public static int scoreInt;
    public int livesInt;
    public bool isInEndScene = false;
    public Sound impactSound;
    public Sound gameOverSound;

    private void OnEnable()
    {
        EnemyAI.OnTouchingPlayer += LoseLIfe;
        Bullet.OnKillingFly += AddScore;
    }

    private void Start()
    {
        impactSound.source.outputAudioMixerGroup = impactSound.group;
        gameOverSound.source.outputAudioMixerGroup = gameOverSound.group;

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

        if (livesInt <= 0)
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
        livesInt--;
        impactSound.source.PlayOneShot(impactSound.clip);
    }

    private void OnDisable()
    {
        EnemyAI.OnTouchingPlayer -= LoseLIfe;
        Bullet.OnKillingFly -= AddScore; 
    }
}
