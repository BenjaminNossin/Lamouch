using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BestScoresCounter : MonoBehaviour
{
    public Score score;
    public static int previousScore1, previousScore2, previousScore3;
    public Text firstScore, secondScore, thirdScore;
    public GameObject congrats;

    void Awake()
    {
        score.isInEndScene = true;
        UpdadePreviousScores();
    }

    void Update()
    {
        firstScore.text = "1st - " + previousScore1;
        secondScore.text = "2nd - " + previousScore2;
        thirdScore.text = "3rd - " + previousScore3;
    }

    void UpdadePreviousScores()
    {
        if (Score.scoreInt >= previousScore1)
        {
            previousScore3 = previousScore2;
            previousScore2 = previousScore1;
            previousScore1 = Score.scoreInt;
            congrats.SetActive(true);
        }
        else if (Score.scoreInt >= previousScore2)
        {
            previousScore3 = previousScore2;
            previousScore2 = Score.scoreInt;
            congrats.SetActive(false);
        }
        else if (Score.scoreInt >= previousScore3)
        {
            previousScore3 = Score.scoreInt;
            congrats.SetActive(false);
        }
    }
}
