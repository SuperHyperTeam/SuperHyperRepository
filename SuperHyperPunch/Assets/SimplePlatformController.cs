using UnityEngine;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float maxAirSpeed = 1f;
	public Transform groundCheck;
	public string punchInputName;
	public string horInputName;
	public string vertInputName;
	public float horPunchForce;
	public float vertPunchForce;

	private bool punch, punchLeft, punchRight, punchUp, punchDown; //if punching and what direction

	//Gravity Delay
	public float gravModifier; //What to make the gravity while punching
	private float currentGrav;
	private bool runGravTimer;
	public float gravTime;
	private float gravTimer;

	//Punch Duration
	private bool runPunchTimer;
	public float punchTime;
	private float punchTimer;

	private bool grounded = false;
	private Animator anim;
	private Rigidbody2D rb2d;

	//private Vector2 velocityZero = Vector2(0,0);

	[HideInInspector] public bool dead;
	
	// Use this for initialization
	void Awake () 
	{
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		currentGrav = rb2d.gravityScale;

		//Initialize bools
		runGravTimer = runPunchTimer = false;
		punch = punchDown = punchLeft = punchRight = punchUp = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if(runGravTimer){
			gravTimer += Time.deltaTime;
			if(gravTimer >= gravTime){
				runGravTimer = false;
				currentGrav = 1f;
				gravTimer = 0f;
			}
		}

		if(runPunchTimer){
			punchTimer += Time.deltaTime;
			if(punchTimer >= punchTime){
				rb2d.velocity = Vector2.zero;
				runPunchTimer = false;
				punch = false;
				punchTimer = 0f;
			}
				

		}
			
		rb2d.gravityScale = currentGrav;

		/*if (Input.GetButtonDown (punchInputName)) 
		{
			if (grounded) 
			{
				Debug.Log ("Grounded");
				jump = true;
			}
		}*/
	}
	
	void FixedUpdate()
	{
		float h = Input.GetAxis(horInputName);
		float v = Input.GetAxis (vertInputName);
		bool p = Input.GetButtonDown (punchInputName);
		
		//anim.SetFloat("Speed", Mathf.Abs(h));

		if (h > 0 && p){
			punch = true;
			punchRight = true;
			punchUp = punchDown = punchLeft = false;
			punchTimer = 0f;
			runPunchTimer = true;
			gravTimer = 0f;
			runGravTimer = true;
			currentGrav = gravModifier;
		}

		if (h < 0 && p){
			punch = true;
			punchLeft = true;
			punchUp = punchDown = punchRight = false;
			punchTimer = 0f;
			runPunchTimer = true;
			gravTimer = 0f;
			runGravTimer = true;
			currentGrav = gravModifier;
		}

		if (v > 0 && p){
			punch = true;
			punchUp = true;
			punchRight = punchDown = punchLeft = false;
			punchTimer = 0f;
			runPunchTimer = true;
			gravTimer = 0f;
			runGravTimer = true;
			currentGrav = gravModifier;
		}

		if (v < 0 && p){
			punch = true;
			punchDown = true;
			punchRight = punchUp = punchLeft = false;
			punchTimer = 0f;
			runPunchTimer = true;
			gravTimer = 0f;
			runGravTimer = true;
			currentGrav = gravModifier;
		}

		if (punch){
			if (punchRight){
				rb2d.AddForce (Vector2.right * horPunchForce);
			}
			if (punchLeft){
				rb2d.AddForce (Vector2.left * horPunchForce);
			}
			if (punchUp){
				rb2d.AddForce (Vector2.up * vertPunchForce);
			}
			if (punchDown){
				rb2d.AddForce (Vector2.down * vertPunchForce);
			}
		}
		if (grounded) {
			if (h * rb2d.velocity.x < maxSpeed)
				rb2d.AddForce (Vector2.right * h * moveForce);
		
			if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
		}
		else{
			if (h * rb2d.velocity.x < maxAirSpeed)
				rb2d.AddForce (Vector2.right * h * moveForce);

			if (Mathf.Abs (rb2d.velocity.x) > maxAirSpeed)
				rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxAirSpeed, rb2d.velocity.y);
		}

		
		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();
	}
	
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}