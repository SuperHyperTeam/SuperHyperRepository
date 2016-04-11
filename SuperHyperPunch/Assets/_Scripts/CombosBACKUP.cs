///*
//Project: Super Hyper Punch
//Created By: Trevor Tomasic
//Description:
// */
//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//using System.Collections.Generic;
//
//public class CombosBACKUP : MonoBehaviour {
//
//	public Slider energyBarSlider;  //reference for slider
//
//	#region Variables
//	private bool inputAvailable = true;
//	//private bool joystickInput = false;
//	private Animator anim;
//	private SimplePlatformController player;
//	//private AnimationClip [] animClip;
//	//public Animation animation;
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
//	//Movement movement = gameObject.GetComponent<Movement>();
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
////	public void GetEnergyBar(){
////		if (gameObject.tag == "Player1"){
////			Debug.Log ("Finding p1");
////			energyBarSlider = GameObject.Find ("p1Energy").GetComponent<Slider> ();
////		}
////		else if (gameObject.tag == "Player2"){
////			Debug.Log ("Finding p2");
////			energyBarSlider = GameObject.Find ("p2Energy").GetComponent<Slider> ();
////		}
////	}
//
//	void FixedUpdate () 
//	{
//		if(inputAvailable)
//		{
////			if (Input.GetAxis("Vertical")==0 && Input.GetAxis("Horizontal")==0)
////				joystickInput = false;
////			else
////				joystickInput = true;
////
////			if(joystickInput)
////			{
////				if (Input.GetAxis("Vertical")>.3f && Input.GetAxis("Horizontal")>-.3f && Input.GetAxis("Horizontal")<.3f)
////				{
////					if(!up)
////						AddCombo("up");
////					up = true;
////				}
////				if (Input.GetAxis("Vertical")<-.3f && Input.GetAxis("Horizontal")>-.3f && Input.GetAxis("Horizontal")<.3f)
////				{
////					if(!down)
////						AddCombo("down");
////					down = true;
////				}
////				if (Input.GetAxis("Horizontal")<-.3f && Input.GetAxis("Vertical")>-.3f && Input.GetAxis("Vertical")<.3f)
////				{
////					if(!left)
////						AddCombo("left");
////					left = true;
////				}
////				if (Input.GetAxis("Horizontal")>.3f && Input.GetAxis("Vertical")>-.3f && Input.GetAxis("Vertical")<.3f)
////				{
////					if(!right)
////						AddCombo("right");
////					right = true;
//		//		if(Input.GetButtonDown("Punch"))
//		//		{
//		//			if(!punch)
//		//				AddCombo("punch");
//		//			punch = true;
//		//		}
////				}
//		//	}
//
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
//
//		if (energyBarSlider.value != 0.0f) {
//			//Combo Punches
//			if (input == hyperPunch){
//				Debug.Log (hyperPunch);
//				player.SetTimers ();
////				player.HyperPunch (hyperPunchDirection);
//			}
//			else if (input == hyperPunchAlt){
//				Debug.Log (hyperPunchAlt);
//				player.SetTimers ();
////				player.HyperPunch (hyperPunchDirection);
//			}
//			else if (input == superHyperPunch){
//				Debug.Log (superHyperPunch);
//				player.SetTimers ();
////				player.SuperHyperPunch (superHyperPunchDirection);
//			}
//			else if (input == superHyperPunchAlt){
//				Debug.Log (superHyperPunchAlt);
//				player.SetTimers ();
////				player.SuperHyperPunch (superHyperPunchDirection);
//			}
//			//Standard Punches
//			else if (input == "rightpunch"){
//				player.SetTimers ();
//				player.StartSuperPunch ("forward");
//			}
//			else if (input == "leftpunch"){
//				player.SetTimers ();
//				player.StartSuperPunch ("forward");
//			}
//			else if (input == "uppunch"){
//				player.SetTimers ();
//				player.StartSuperPunch ("up");
//			}
//			else if (input == "downpunch"){
//				player.SetTimers ();
//				player.StartSuperPunch ("down");
//			}
//
//
////			switch (input) {	
////			case "punchup":
////				Debug.Log ("punchup");
////				break;
////
////			case "punchright":
////				Debug.Log ("up right");
////				break;
////
////			case "punchleft":
////				Debug.Log ("up left");
////				break;
////
////			case "punchdown":
////				Debug.Log ("down");
////				break;
////
////			case "left":
////				Debug.Log ("left");
////				break;
////
////			case "right":
////				Debug.Log ("right");
////				break;
////
////			case "punch":
////				Debug.Log ("punch");
////				break;
////
////			case "uppunch":
////				Debug.Log ("up punch");
////				break;
////
////			case "downpunch":
////				Debug.Log ("up down punch");
////				break;
////
////			case "rightpunch":
////				Debug.Log ("punch up down punch");
////				break;
////
////			case "leftpunch":
////				Debug.Log ("punch up down punch");
////				break;
////
////			case "punchpunchright":
////				Debug.Log ("punch up down punch");
////				break;
////
////			case "punchpunchleft":
////				Debug.Log ("punch up down punch");
////				break;
////
////			case "punchpunchup":
////				Debug.Log ("punch up down punch");
////				break;
////
////			case "punchpunchdown":
////				Debug.Log ("punch up down punch");
////				break;
////
////			default:
////				Debug.Log (comboCodeDefault);
////				break;
//
//			//ExecuteCombo(input);
//			//}//end switch
//		}//end if
//	}//end setcombo
//	#endregion
//}
