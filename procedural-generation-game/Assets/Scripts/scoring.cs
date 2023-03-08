using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class scoring : MonoBehaviour
{
    public int score;
    public float timer = 0;
    public TextMeshProUGUI UIscore;
    public TextMeshProUGUI UItimer;
    private int tier = 0;
    private int nextTier = 100;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        UIscore.SetText("Score:" + score.ToString());
        UItimer.SetText("Timer:" + (60 - ((int) timer % 60)).ToString());
        if (score > nextTier)
        {
            tier++;
            nextTier += nextTier;
            //grow arm
        }

        if (((int)timer % 60) == 59) {
            PublicVars.score = score;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Scored(int newScore)
    {
        score += newScore;
    }
}
