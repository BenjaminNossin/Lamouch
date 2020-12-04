using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScoreScript : MonoBehaviour
{
    public int scoreCheat;
    void Update()
    {
        Score.scoreInt = scoreCheat;
        if(Input.GetKey(KeyCode.P))
        {
            SceneManager.LoadScene("BestScoreScene");
        }
    }
}
