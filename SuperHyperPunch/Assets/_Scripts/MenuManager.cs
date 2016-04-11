using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	public string sceneToLoad;
	public GameManager gameManager;
	public GameObject startPanel;
	public GameObject characterPanel;
	public GameObject levelPanel;
	public int activePanel = 0;
	public GameObject player1selector, player2selector;
	public Vector2 selectorOffset;
	public GameObject[] character;
	public GameObject[] characterObj;

	private bool player1pick;

	private int selectorPosition;
	// Update is called once per frame
	void Start(){
		player1pick = true;
		SwitchPanel (activePanel);
		selectorPosition = 0;
		player2selector.SetActive (false);
		player1selector.transform.position = (Vector2)character [selectorPosition].transform.position + selectorOffset;
	}


	void Update () {
		if (activePanel == 0) {
			if (Input.anyKeyDown) {
				SwitchPanel (1);
			}
		}
		if (activePanel == 1){
			if (player1pick) {
				if (Input.GetKeyDown (KeyCode.D)) {
					if (selectorPosition < character.Length - 1) {
						selectorPosition += 1;
					}
				}
				if (Input.GetKeyDown (KeyCode.A)) {
					if (selectorPosition > 0) {
						selectorPosition -= 1;
					}
				}
				if (Input.GetKeyDown (KeyCode.LeftShift)){
					player1pick = false;
					player1selector.SetActive (false);
					player2selector.SetActive (true);

					gameManager.player1Prefab = characterObj [selectorPosition];
					//gameManager.player1 = gameManager.player1obj.GetComponent<SimplePlatformController>();
					gameManager.player1Prefab.tag = "Player1";
					selectorPosition = 0;
				}
				player1selector.transform.position = (Vector2)character [selectorPosition].transform.position + selectorOffset;
			}
			if (!player1pick) {
				if (Input.GetKeyDown (KeyCode.RightArrow)) {
					if (selectorPosition < character.Length - 1) {
						selectorPosition += 1;
					}
				}
				if (Input.GetKeyDown (KeyCode.LeftArrow)) {
					if (selectorPosition > 0) {
						selectorPosition -= 1;
					}
				}
				if (Input.GetKeyDown (KeyCode.RightShift)){
					player1pick = true;
					SwitchPanel (2);

					gameManager.player2Prefab = characterObj [selectorPosition];
					//gameManager.player2 = gameManager.player1obj.GetComponent<SimplePlatformController>();
					gameManager.player2Prefab.tag = "Player2";
					SceneManager.LoadScene ("GageMain");
				}
				player2selector.transform.position = (Vector2)character [selectorPosition].transform.position + selectorOffset;
			}
		}
	}

	void SwitchPanel(int panel){
		if (panel == 0){
			activePanel = 0;
			startPanel.SetActive (true);
			characterPanel.SetActive (false);
			levelPanel.SetActive (false);
		}
		else if (panel == 1){
			
			StartCoroutine (WaitTime (1));
			startPanel.SetActive (false);
			characterPanel.SetActive (true);
			levelPanel.SetActive (false);
		}
		else if (panel == 2){
			StartCoroutine (WaitTime (2));
			startPanel.SetActive (false);
			characterPanel.SetActive (false);
			levelPanel.SetActive (true);
		}
	}

	IEnumerator WaitTime(int i){
		yield return new WaitForSeconds(.25f);
		activePanel = i;
	}
}
