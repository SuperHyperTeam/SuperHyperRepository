using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour {

	public GameObject[] items;
	public GameObject selector;
	private int selectorPosition;
	public Vector2 selectorOffset;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager> ();
		selectorPosition = 0;
		selector.transform.position =(Vector2) items [selectorPosition].transform.position + selectorOffset;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)){
			if (selectorPosition > 0){
				selectorPosition -= 1;
				selector.transform.position =(Vector2) items [selectorPosition].transform.position + selectorOffset;
			}
		}
		if (Input.GetKeyDown(KeyCode.D)){
			if (selectorPosition < items.Length-1){
				selectorPosition += 1;
				selector.transform.position =(Vector2) items [selectorPosition].transform.position + selectorOffset;
			}
		}
		if (Input.GetKeyDown(KeyCode.LeftShift)){
			if (selectorPosition == 0){
				gameManager.Rematch ();
			}
			else if (selectorPosition == 1){
				gameManager.QuitToMenu ();
			}
		}
	}
}
