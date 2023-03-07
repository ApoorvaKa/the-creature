using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoring : MonoBehaviour
{
    public int score;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer -= (int) (1 * Time.deltaTime);
    }

    public void Scored(int newScore)
    {
        score += newScore;
    }
}
