using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayFinalScore : MonoBehaviour
{
    public TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        score.SetText(PublicVars.score.ToString());
    }
}
