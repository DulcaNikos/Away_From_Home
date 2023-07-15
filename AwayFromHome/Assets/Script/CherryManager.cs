using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CherryManager : MonoBehaviour
{ 
    public static CherryManager instance;
    public TextMeshProUGUI text;
    int score;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void CherryScore(int cherryValue)
    {
        //Μakes an addition
        //text will be as much as the score
        score += cherryValue;
        text.text = "X" + score.ToString();
    }
}
