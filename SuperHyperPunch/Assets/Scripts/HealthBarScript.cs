using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour {
	
	public float max_Health = 100f;
	public float cur_Health = 0f;
	public float healAmount = 10f;
	public GameObject healthBar;
	public Text healthText;
	
	// Use this for initialization
	void Start () {
		cur_Health = max_Health;
		//just for testing, need to replace this with a call to the damage system.
		//InvokeRepeating ("DecreaseHealth", 1f, 5f);
		//InvokeRepeating ("IncreaseHealth", 2f, 10f);
	}
	
	// Update is called once per frame
	void Update () {
		healthText.text = "Health: " + cur_Health;
	}
	
	void IncreaseHealth(){
		
		if(cur_Health < 100f)
		{
			cur_Health += healAmount;
			float calc_Health = cur_Health / max_Health;
			SetHealthBar (calc_Health);
			
			if(cur_Health > max_Health){
				cur_Health = max_Health;
			}
		}
	}
	
	void DecreaseHealth(){
		//just for testing, need to replace this with a damage system.
		cur_Health -= 50f;
		float calc_Health = cur_Health / max_Health;
		SetHealthBar (calc_Health);
	}
	
	public void SetHealthBar(float myHealth){
		healthBar.transform.localScale = new Vector3(Mathf.Clamp(myHealth,0f ,1f), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
	}
	
}
