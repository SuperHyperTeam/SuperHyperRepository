/*
Project: Super Hyper Punch
Created By: Trevor Tomasic
Description:
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	#region Variables
	public static GameManager instance = null;
	private int [] characters = new int[7];
	private int [] levels = new int[7];
	private int charactersUnlocked = 3;
	private int levelsUnlocked = 3;
	private int numberOfRounds = 5;
	private int matchTime = 100;

	public int characterP1 {get; set;}
	public float energyP1 {get; set;}
	public int roundsWonP1 {get; set;}
	public bool isAliveP1 {get; set;}

	public int characterP2 {get; set;}
	public float energyP2 {get; set;}
	public int roundsWonP2 {get; set;}
	public bool isAliveP2 {get; set;}

	public int level {get; set;}
	public float gameTimer {get; set;}
	public float gameRound {get; set;}

	public bool isPaused {get; set;}
	#endregion

	#region Unity Event Functions
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    

		DontDestroyOnLoad(gameObject);
	}

	void Update()
	{
		gameTimer += Time.deltaTime;
		if(gameTimer > matchTime || !isAliveP1 || !isAliveP2)
		{
			gameTimer = 0;
			NewRound();
		}
	}
	#endregion

	#region Public Functions
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
		if(roundsWonP1 > numberOfRounds/2 || roundsWonP2 > numberOfRounds/2)
			Debug.Log("Game Complete, go to winner state");
		else
		{
			Debug.Log("reset timer, go to new round etc.");
			gameRound++;
		}
	}
	#endregion

	#region Private Functions
	#endregion
}
