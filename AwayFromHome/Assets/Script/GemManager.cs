using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GemManager : MonoBehaviour
{
    public static GemManager instance;
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

    public void GemScore(int gemValue)
    {
        //Μakes an addition
        //text will be as much as the score
        score += gemValue;
        text.text = "X" + score.ToString();
    }
}