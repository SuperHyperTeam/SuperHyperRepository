using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MovementandHealthScript : MonoBehaviour {
	
	public Slider healthBarSlider;  //reference for slider
	public Text gameOverText;   //reference for text
	private bool isGameOver = false; //flag to see if game is over
	
	//Energy Variables
	public Slider energyBarSlider;  //reference for slider
	//public float max_Energy = 1.0f;
	//public float cur_Energy = 0f;
	public float powerAmount = 0.3f;
	
	void Start(){
		gameOverText.enabled = false; //disable GameOver text on start
	}
	
	// Update is called once per frame
	void Update () {
		//check if game is over i.e., health is greater than 0
		if(!isGameOver)
			transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime*10f, 0, 0); //get input
			
			IncreaseEnergy();
			DecreaseEnergy();
	}
	
	//Check if player enters/stays on the fire
	void OnTriggerStay(Collider other){
		//if player triggers fire object and health is greater than 0
		if(other.gameObject.name=="OtherPlayer" && healthBarSlider.value>0){
			healthBarSlider.value -=1.0f;  //reduce health
		}
		else{
			isGameOver = true;    //set game over to true
			gameOverText.enabled = true; //enable GameOver text
		}
	}
	
	void IncreaseEnergy(){
		/*
		if(cur_Energy < 1.0f)
		{
			cur_Energy += powerAmount;
			float calc_Health = cur_Energy / max_Energy;
			
			if(cur_Energy > max_Energy){
				cur_Energy = max_Energy;
			}
		}*/
		
		
		if(Input.GetKeyDown("i"))
		{
			energyBarSlider.value += powerAmount;
		}
	}
	
	void DecreaseEnergy(){
		//just for testing, need to replace this with a energy system.
		if(Input.GetKeyDown("o"))
		{
			energyBarSlider.value -= powerAmount;
		}
	}
}