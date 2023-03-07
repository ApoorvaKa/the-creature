using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class scoring : MonoBehaviour
{
    public int score;
    public float timer;
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
        timer -= (int) (1 * Time.deltaTime);
        UIscore.SetText("Score:" + score.ToString());
        UItimer.SetText("Timer:" + timer.ToString());
        if (score > nextTier)
        {
            tier++;
            nextTier += nextTier;
            //grow arm
        }
    }

    public void Scored(int newScore)
    {
        score += newScore;
    }
}
