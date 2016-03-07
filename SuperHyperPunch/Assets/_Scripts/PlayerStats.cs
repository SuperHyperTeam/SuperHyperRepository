using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    public int score;
    public int deaths;
    public Text scoreText;

    void Start()
    {
        score = 0;
        deaths = 0;
        scoreText.text = "" + score;
    }

    public void UpdateScore()
    {
        scoreText.text = "" + score;
    }

}
