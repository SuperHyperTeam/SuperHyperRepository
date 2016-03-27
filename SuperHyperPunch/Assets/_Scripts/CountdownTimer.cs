using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

	private float countdownTimer;
	public float roundStartTime;
	private Text StartTimeText;
	public SimplePlatformController player1, player2;


	// Use this for initialization
	void Start () {
		StartTimeText = GetComponent<Text> ();
		countdownTimer = roundStartTime + 1f;
	}
	
	// Update is called once per frame
	void Update () {
		countdownTimer -= Time.deltaTime;

		if (countdownTimer >= 1f)
			StartTimeText.text = "" + (int)countdownTimer;

		else if ((int)countdownTimer < 1f && countdownTimer > 0f){
			player1.inControl = true;
			player2.inControl = true;
			StartTimeText.text = "GO!";
		}
		else if (countdownTimer <= 0f){
			StartTimeText.text = "";
		}
	}
}
