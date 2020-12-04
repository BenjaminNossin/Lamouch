using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Text score, lives;
    public static int scoreInt;
    public int livesInt;
    public bool isInEndScene = false;

    private void OnEnable()
    {
        EnemyAI.OnTouchingPlayer += LoseLIfe; 
    }

    private void Start()
    {
        if (!isInEndScene)
        {
            scoreInt = 0;
        }        
        livesInt = 3;
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
            SceneManager.LoadScene("BestScoreScene");
        }
    }

    public void AddScore()
    {
        scoreInt ++;
    }

    public void LoseLIfe()
    {
        livesInt--;
    }

    private void OnDisable()
    {
        EnemyAI.OnTouchingPlayer -= LoseLIfe;
    }
}
