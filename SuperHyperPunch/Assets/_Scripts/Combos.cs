//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;
//
//public class Combos : MonoBehaviour {
//
//	public Slider energyBarSlider;  //reference for slider
//
//	#region Variables
//	private bool inputAvailable = true;
//	private Animator anim;
//	private SimplePlatformController player;
//
//	[SerializeField]
//	private float timer = 0;
//	[SerializeField]
//	private float comboTime = 0;
//	public float comboRate = .25f;
//	private string comboCode;
//	private string comboCodeDefault;
//
//	[SerializeField]
//	private int combo = 0;
//	[SerializeField]
//	private bool up = false;
//	[SerializeField]
//	private bool down = false;
//	[SerializeField]
//	private bool left = false;
//	[SerializeField]
//	private bool right = false;
//	[SerializeField]
//	private bool punch = false;
//
//	Queue<string> ComboQueue = new Queue<string>(); 
//
//	public string hyperPunch, superHyperPunch, hyperPunchAlt, superHyperPunchAlt;
//	public string hyperPunchDirection, superHyperPunchDirection;
//
//	GameManager gm;
//
//	#endregion
//
//	#region Unity Event Functions
//	void Awake()
//	{
//		anim = GetComponent<Animator>();
//		player = GetComponent<SimplePlatformController> ();
//	}
//
//	void FixedUpdate () 
//	{
//		if(inputAvailable)
//		{
//			if (Input.GetAxis(player.vertInputName) > 0f)
//			{
//					if(!up)
//						AddCombo("up");
//					up = true;
//			}
//			if (Input.GetAxis(player.vertInputName) < 0f)
//			{
//					if(!down)
//						AddCombo("down");
//					down = true;
//			}
//			if (Input.GetAxis(player.horInputName) < 0f)
//			{
//					if(!left)
//						AddCombo("left");	
//					left = true;
//			}
//			if (Input.GetAxis(player.horInputName) > 0f)
//			{
//					if(!right)
//						AddCombo("right");		
//					right = true;
//			}
//			
//			if(Input.GetButtonDown(player.punchInputName))
//			{
//				if(!punch)
//					AddCombo("punch");
//				punch = true;
//			}
//
//			if(combo > 0)
//			{
//				timer += Time.deltaTime;
//				if(timer > comboTime)
//				{
//					timer = 0;
//					ResetCombo();
//				}
//			}
//		}
//	}
//	#endregion
//
//	#region Public Functions
//	public void ExecuteCombo(string input)
//	{
//	inputAvailable = false;
//
//
//	inputAvailable = true; //after move animation is complete enable input again
//	}
//	#endregion
//
//	#region Private Functions
//	private void AddCombo(string input)
//	{
//		combo++;
//		comboTime += comboRate;
//
//		if(ComboQueue.Count >= 5)
//		{
//			ComboQueue.Dequeue();
//			ComboQueue.Enqueue(input);
//		}
//		else
//			ComboQueue.Enqueue(input);
//		comboCode = "";
//		for(int i = 0; i < ComboQueue.Count; i++)	
//			comboCode = comboCode + ComboQueue.ToArray().GetValue(i).ToString();
//
//		comboCodeDefault = ComboQueue.ToArray().GetValue(0).ToString();
//		punch = false;
//	}
//
//	private void ResetCombo()
//	{
//		SetCombo(comboCode);
//		comboTime = 0;
//		combo = 0;
//		ComboQueue.Clear();
//
//		up = false;
//		down = false;
//		left = false;
//		right = false;
//		punch = false;
//	}
//
//	public void ExternalSetCombo(){
//		SetCombo (comboCode);
//	}
//
//	private void SetCombo(string input)
//	{
//		if (energyBarSlider.value != 0.0f) {
//			//Combo Punches
//			if (input == hyperPunch){
//				//Debug.Log (hyperPunch);
//				player.SetTimers ();
////				player.HyperPunch (hyperPunchDirection);
//			}
//			else if (input == hyperPunchAlt){
//				//Debug.Log (hyperPunchAlt);
//				player.SetTimers ();
////				player.HyperPunch (hyperPunchDirection);
//			}
//			else if (input == superHyperPunch){
//				//Debug.Log (superHyperPunch);
//				player.SetTimers ();
////				player.SuperHyperPunch (superHyperPunchDirection);
//			}
//			else if (input == superHyperPunchAlt){
//				//Debug.Log (superHyperPunchAlt);
//				player.SetTimers ();
////				player.SuperHyperPunch (superHyperPunchDirection);
//			}
//			//Standard Punches
//			else if (input == "rightpunch"){
//				player.SetTimers ();
//				player.StartSuperPunch ("right");
//			}
//			else if (input == "leftpunch"){
//				player.SetTimers ();
//				player.StartSuperPunch ("left");
//			}
//			else if (input == "uppunch"){
//				player.SetTimers ();
//				player.StartSuperPunch ("up");
//			}
//			else if (input == "downpunch"){
//				player.SetTimers ();
//				player.StartSuperPunch ("down");
//			}
//		}//end if
//	}//end setcombo
//	#endregion
//}
