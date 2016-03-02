/*
Project: Super Hyper Punch
Created By: Trevor Tomasic
Description:
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Combos : MonoBehaviour {

	#region Variables
	private bool inputAvailable = true;
	//private bool joystickInput = false;
	private Animator anim;
	//private AnimationClip [] animClip;
	//public Animation animation;

	[SerializeField]
	private float timer = 0;
	[SerializeField]
	private float comboTime = 0;
	private float comboRate = .25f;
	private string comboCode;
	private string comboCodeDefault;

	[SerializeField]
	private int combo = 0;
	[SerializeField]
	private bool up = false;
	[SerializeField]
	private bool down = false;
	[SerializeField]
	private bool left = false;
	[SerializeField]
	private bool right = false;
	[SerializeField]
	private bool punch = false;

	Queue<string> ComboQueue = new Queue<string>(); 

	GameManager gm;
	//Movement movement = gameObject.GetComponent<Movement>();

	#endregion

	#region Unity Event Functions
	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Update () 
	{
		if(inputAvailable)
		{
//			if (Input.GetAxis("Vertical")==0 && Input.GetAxis("Horizontal")==0)
//				joystickInput = false;
//			else
//				joystickInput = true;
//
//			if(joystickInput)
//			{
//				if (Input.GetAxis("Vertical")>.3f && Input.GetAxis("Horizontal")>-.3f && Input.GetAxis("Horizontal")<.3f)
//				{
//					if(!up)
//						AddCombo("up");
//					up = true;
//				}
//				if (Input.GetAxis("Vertical")<-.3f && Input.GetAxis("Horizontal")>-.3f && Input.GetAxis("Horizontal")<.3f)
//				{
//					if(!down)
//						AddCombo("down");
//					down = true;
//				}
//				if (Input.GetAxis("Horizontal")<-.3f && Input.GetAxis("Vertical")>-.3f && Input.GetAxis("Vertical")<.3f)
//				{
//					if(!left)
//						AddCombo("left");
//					left = true;
//				}
//				if (Input.GetAxis("Horizontal")>.3f && Input.GetAxis("Vertical")>-.3f && Input.GetAxis("Vertical")<.3f)
//				{
//					if(!right)
//						AddCombo("right");
//					right = true;
		//		if(Input.GetButtonDown("Punch"))
		//		{
		//			if(!punch)
		//				AddCombo("punch");
		//			punch = true;
		//		}
//				}
		//	}

			if (Input.GetKeyDown(KeyCode.W))
			{
					if(!up)
						AddCombo("up");
					up = true;
			}
			if (Input.GetKeyDown(KeyCode.S))
			{
					if(!down)
						AddCombo("down");
					down = true;
			}
			if (Input.GetKeyDown(KeyCode.A))
			{
					if(!left)
						AddCombo("left");	
					left = true;
			}
			if (Input.GetKeyDown(KeyCode.D))
			{
					if(!right)
						AddCombo("right");		
					right = true;
			}
			
			if(Input.GetKeyDown(KeyCode.Space))
			{
				if(!punch)
					AddCombo("punch");
				punch = true;
			}

			if(combo > 0)
			{
				timer += Time.deltaTime;
				if(timer > comboTime)
				{
					timer = 0;
					ResetCombo();
				}
			}
		}
	}
	#endregion

	#region Public Functions
	public void ExecuteCombo(string input)
	{
	inputAvailable = false;


	inputAvailable = true; //after move animation is complete enable input again
	}
	#endregion

	#region Private Functions
	private void AddCombo(string input)
	{
		combo++;
		comboTime += comboRate;

		if(ComboQueue.Count >= 5)
		{
			ComboQueue.Dequeue();
			ComboQueue.Enqueue(input);
		}
		else
			ComboQueue.Enqueue(input);
		comboCode = "";
		for(int i = 0; i < ComboQueue.Count; i++)	
			comboCode = comboCode + ComboQueue.ToArray().GetValue(i).ToString();

		comboCodeDefault = ComboQueue.ToArray().GetValue(0).ToString();
		punch = false;
	}

	private void ResetCombo()
	{
		SetCombo(comboCode);
		comboTime = 0;
		combo = 0;
		ComboQueue.Clear();

		up = false;
		down = false;
		left = false;
		right = false;
		punch = false;
	}

	private void SetCombo(string input)
	{
		switch(input)
		{	
		case "up":
			Debug.Log("up");
			break;

		case "upright":
			Debug.Log("up right");
			break;

		case "upleft":
			Debug.Log("upleft");
			break;

		case "down":
			Debug.Log("down");
			break;

		case "left":
			Debug.Log("left");
			break;

		case "right":
			Debug.Log("right");
			break;

		case "punch":
			Debug.Log("punch");
			break;

		case "uppunch":
			Debug.Log("up punch");
			break;

		case "updownpunch":
			Debug.Log("up down punch");
			break;

		case "punchupdownpunch":
			Debug.Log("punch up down punch");
			break;

		default:
			Debug.Log(comboCodeDefault);
			break;

			//ExecuteCombo(input);
		}
	}
	#endregion
}
