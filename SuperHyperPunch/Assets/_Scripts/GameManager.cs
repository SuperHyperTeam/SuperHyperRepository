/*
Project: Super Hyper Punch
Created By: Trevor Tomasic
Description:
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	private int [] characters = new int[7];
	private int [] levels = new int[7];
	private int charactersUnlocked = 3;
	private int levelsUnlocked = 3;
	private int numberOfRounds = 5;
	private int matchTime = 100;

	public int characterP1 {get; set;}
	public float energyP1 {get; set;}
	public int roundsWonP1;// {get; set;}
	public bool isAliveP1; //{get; set;}

	public int characterP2 {get; set;}
	public float energyP2 {get; set;}
	public int roundsWonP2;// {get; set;}
	public bool isAliveP2;//{get; set;}

	public int level {get; set;}
	public float gameTimer {get; set;}
	public float gameRound;// {get; set;}

	public bool isPaused {get; set;}

	public GameObject player1obj, player2obj;
	private SimplePlatformController player1, player2;

	//Scoring Variables
	public float waitTime;
	public bool roundWinnerPlayer1;
	public bool roundWinnerPlayer2;
	public GameObject player1RespawnPoint;
	public GameObject player2RespawnPoint;
	public GameObject player1Prefab;
	public GameObject player2Prefab;

	public GameObject[] p1ScoreFists;
	public GameObject[] p2ScoreFists;

	public Text winText;
	public CountdownTimer countdownTimer;
	void Awake()
	{
		winText.text = "";
		//Scoring initialization
		roundWinnerPlayer1 = false;
		roundWinnerPlayer2 = false;
		isAliveP1 = true;
		isAliveP2 = true;
		roundsWonP1 = 4;
		roundsWonP2 = 4;
		gameRound = 1;

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    

		DontDestroyOnLoad(gameObject);

		player1 = player1obj.GetComponent<SimplePlatformController> ();
		player2 = player2obj.GetComponent<SimplePlatformController> ();
		player1.inControl = false;
		player2.inControl = false;

		DrawScoreboard ();
	}

	void Update()
	{
		gameTimer += Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Alpha3)){
			Application.LoadLevel (Application.loadedLevel);
		}

		if(gameTimer > matchTime || !isAliveP1 || !isAliveP2)
		{
//			if (isAliveP1) {
//				Debug.Log ("player1 is alive");
//				roundWinnerPlayer1 = true;
//				StartCoroutine(PointScoring(roundWinnerPlayer1,gameRound));
//			} 
//
//			else if (isAliveP2) {
//				Debug.Log ("player2 is alive");
//				roundWinnerPlayer2 = true;
//				StartCoroutine(PointScoring(roundWinnerPlayer2,gameRound));
//			}
			gameTimer = 0;
			NewRound();
		}
			
	}

	public void UnlockNewCharacter()
	{
		if(charactersUnlocked < characters.Length)
			charactersUnlocked++;
	}

	public void UnlockNewLevel()
	{
		if(levelsUnlocked < levels.Length)
			levelsUnlocked++;
	}

	public void NewRound()
	{
		roundWinnerPlayer1 = false;
		roundWinnerPlayer2 = false;

		if(roundsWonP1 > numberOfRounds/2 || roundsWonP2 > numberOfRounds/2){}
			//Debug.Log("Game Complete, go to winner state");
		else
		{
			//Debug.Log("reset timer, go to new round etc.");
			gameRound++;//duplicate round number
		}
	}

	private void DrawScoreboard(){
		foreach(GameObject fist in p1ScoreFists){
			fist.SetActive (false);
		}
		foreach(GameObject fist in p2ScoreFists){
			fist.SetActive (false);
		}

		for(int i = 0; i < roundsWonP1; i++){
			p1ScoreFists [i].SetActive (true);
		}
		for(int j = 0; j < roundsWonP2; j++){
			p2ScoreFists [j].SetActive (true);
		}
	}

	public void DoPointScoring(){
		if (isAliveP1) {
			Debug.Log ("player1 is alive");
			roundsWonP1 += 1;
			roundsWonP2 -= 1;
			roundWinnerPlayer1 = true;
			StartCoroutine(PointScoring(roundWinnerPlayer1,gameRound));
		} 

		if (isAliveP2) {
			Debug.Log ("player2 is alive");
			roundsWonP1 -= 1;
			roundsWonP2 += 1;
			roundWinnerPlayer2 = true;
			StartCoroutine(PointScoring(roundWinnerPlayer2,gameRound));
		}
	}

	public IEnumerator PointScoring(bool roundWinner, float roundNumber)
	{
		gameRound += 1;
		Debug.Log ("ienumerator");
		if (isAliveP1){
			winText.text = player1obj.name + " Wins!";
		}
		if (isAliveP2){
			winText.text = player2obj.name + " Wins!";
		}
		yield return new WaitForSeconds(waitTime);
		winText.text = "";
		isAliveP1 = true;
		isAliveP2 = true;
		player1obj.transform.position = player1RespawnPoint.transform.position;
		player2obj.transform.position = player2RespawnPoint.transform.position;
		player1obj.SetActive (true);
		player2obj.SetActive (true);
		player1.inControl = false;
		player2.inControl = false;
		DrawScoreboard ();
		countdownTimer.Reset ();
//		player1obj = Instantiate (player1Prefab, player1RespawnPoint.transform.position, Quaternion.identity) as GameObject;
//		player1 = player1obj.GetComponent<SimplePlatformController> ();
//		player1.energyBarSlider = GameObject.Find ("p1Energy").GetComponent<Slider> ();
//		player2obj = Instantiate (player2Prefab, player2RespawnPoint.transform.position, Quaternion.identity) as GameObject;
//		player2 = player2obj.GetComponent<SimplePlatformController> ();
//		player2.energyBarSlider = GameObject.Find ("p2Energy").GetComponent<Slider> ();

		//needs to respawn the losing player

	}
}
