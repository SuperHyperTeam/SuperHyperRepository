using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

	private float countdownTimer;
	public float roundStartTime;
	private Text StartTimeText;
	public SimplePlatformController player1, player2;
	public Text winText;
	public GameManager gameManager;

	// Use this for initialization
	void Start () {
		winText.text = "Round 1";
		StartTimeText = GetComponent<Text> ();
		countdownTimer = roundStartTime;
		StartCoroutine (Countdown ());
	}

	public void Reset(){
		countdownTimer = roundStartTime;
		StartCoroutine (Countdown ());
	}

	// Update is called once per frame
	void Update () {
//		countdownTimer -= Time.deltaTime;
//
//		if (countdownTimer >= 1f)
//			StartTimeText.text = "" + (int)countdownTimer;
//
//		else if ((int)countdownTimer < 1f && countdownTimer > 0f){
//			player1.inControl = true;
//			player2.inControl = true;
//			winText.text = "";
//			StartTimeText.text = "GO!";
//		}
//		else if (countdownTimer <= 0f){
//			StartTimeText.text = "";
//		}
	}

	public IEnumerator Countdown(){
		winText.text = "Round "+gameManager.gameRound;
		StartTimeText.text = ""+countdownTimer;
		yield return new WaitForSeconds(1f);
		countdownTimer -= 1;
		StartTimeText.text = ""+countdownTimer;
		yield return new WaitForSeconds(1f);
		countdownTimer -= 1;
		StartTimeText.text = ""+countdownTimer;
		yield return new WaitForSeconds(1f);
		countdownTimer -= 1;
		StartTimeText.text = "GO!";
		winText.text = "";
		player1.inControl = true;
		player2.inControl = true;
		yield return new WaitForSeconds(.5f);
		StartTimeText.text = "";
	}
}
