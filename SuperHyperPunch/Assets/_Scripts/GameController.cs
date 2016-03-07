using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text timerText;
    public PlayerStats p1stats;
    public PlayerStats p2stats;
    public Text endText;
    public float gameTime = 90.0f;
    public Color p1WinColor, p2WinColor, drawColor;


    private float displayTime;
    private bool isPlaying;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        isPlaying = true;
        displayTime = 0;
        timerText.text = "" + gameTime;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(Time.timeScale);
        if (isPlaying)
        {
            gameTime -= Time.deltaTime;
            displayTime = Mathf.Round(gameTime);
            timerText.text = "" + displayTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.R))
            {
                Application.LoadLevel("Main");
            }
        }
        if(gameTime <= 0)
        {
            isPlaying = false;
            Time.timeScale = 0.25f;
            if(p1stats.score > p2stats.score)
            {
                endText.enabled = true;
                endText.text = "P1 Wins!";
                endText.color = p1WinColor;
            }
            else if(p2stats.score > p1stats.score)
            {
                endText.enabled = true;
                endText.text = "P2 Wins!";
                endText.color = p2WinColor;
            }
            else
            {
                endText.enabled = true;
                endText.text = "Draw!";
                endText.color = drawColor;
            }
        }
	}
}
