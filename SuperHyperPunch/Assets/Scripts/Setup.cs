using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Setup : MonoBehaviour {
	public Text winText;
	public CountdownTimer countdownTimer;
	public GameObject[] p1ScoreFists;
	public GameObject[] p2ScoreFists;
	public GameObject player1RespawnPoint;
	public GameObject player2RespawnPoint;
	private GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		gameManager.winText = winText;
		gameManager.countdownTimer = countdownTimer;
		gameManager.p1ScoreFists = p1ScoreFists;
		gameManager.p2ScoreFists = p2ScoreFists;
		gameManager.player1RespawnPoint = player1RespawnPoint;
		gameManager.player2RespawnPoint = player2RespawnPoint;
		gameManager.inGame = true;
		gameManager.GameInit ();
	}
}
