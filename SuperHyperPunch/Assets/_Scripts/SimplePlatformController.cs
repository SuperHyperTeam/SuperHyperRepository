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
	public float horKnockbackForce;
	public float vertKnockbackForce;

	private bool punch;
	public bool punchLeft, punchRight, punchUp, punchDown; //if punching and what direction

	//Gravity Delay
	public float gravModifier; //What to make the gravity while punching
	private float currentGrav;
	private bool runGravTimer;
	public float gravTime;
	private float gravTimer;
	public bool knockback;
	private bool runKnockbackTimer;
	public float knockbackTime;
	private float knockbackTimer;

	//Punch Duration
	private bool runPunchTimer;
	public float punchTime;
	private float punchTimer;

	private bool inControl;

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

		inControl = true;

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
		if (runPunchTimer) {
			punchTimer += Time.deltaTime;
			if (punchTimer >= punchTime) {
				rb2d.velocity = Vector2.zero;
				runPunchTimer = false;
				punch = false;
				punchUp = punchDown = punchRight = punchLeft = false;
				punchTimer = 0f;
			}
		}
		if (runKnockbackTimer){
			knockbackTimer += Time.deltaTime;
			if (knockbackTimer >= knockbackTime){
				rb2d.velocity = Vector2.zero;
				runKnockbackTimer = false;
				inControl = true;
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

	public void Knockback(Vector2 direction, bool isHorizontal){
		inControl = false;
		runPunchTimer = false;
		punch = false;
		knockbackTimer = 0f;
		runKnockbackTimer = true;
		punchDown = punchUp = punchRight = punchLeft = false;
		rb2d.velocity = direction;
		if (isHorizontal) {
			rb2d.AddForce (direction * horKnockbackForce);
		} else
			rb2d.AddForce (direction * vertKnockbackForce);
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis(horInputName);
		float v = Input.GetAxis (vertInputName);
		bool p = Input.GetButtonDown (punchInputName);
		
		//anim.SetFloat("Speed", Mathf.Abs(h));
		if (inControl) {
			if (h > 0 && p) {
				punch = true; //You are punching
				punchRight = true; //You are punching right...
				punchUp = punchDown = punchLeft = false; //...and not any other direction
				punchTimer = 0f; //reset the punch timer...
				runPunchTimer = true; //...and run it
				gravTimer = 0f; //reset the gravity damping timer...
				runGravTimer = true; //...and run it
				currentGrav = gravModifier; //make gravity damping to set value
			}

			if (h < 0 && p) {
				punch = true;
				punchLeft = true;
				punchUp = punchDown = punchRight = false;
				punchTimer = 0f;
				runPunchTimer = true;
				gravTimer = 0f;
				runGravTimer = true;
				currentGrav = gravModifier;
			}

			if (v > 0 && p) {
				punch = true;
				punchUp = true;
				punchRight = punchDown = punchLeft = false;
				punchTimer = 0f;
				runPunchTimer = true;
				gravTimer = 0f;
				runGravTimer = true;
				currentGrav = gravModifier;
			}

			if (v < 0 && p) {
				punch = true;
				punchDown = true;
				punchRight = punchUp = punchLeft = false;
				punchTimer = 0f;
				runPunchTimer = true;
				gravTimer = 0f;
				runGravTimer = true;
				currentGrav = gravModifier;
			}
		

			if (punch) {
				if (punchRight) {
					rb2d.AddForce (Vector2.right * horPunchForce);
				}
				if (punchLeft) {
					rb2d.AddForce (Vector2.left * horPunchForce);
	
				}
				if (punchUp) {
					rb2d.AddForce (Vector2.up * vertPunchForce);
				}
				if (punchDown) {
					rb2d.AddForce (Vector2.down * vertPunchForce);
				}
			}
		
			if (grounded) {
				if (h * rb2d.velocity.x < maxSpeed)
					rb2d.AddForce (Vector2.right * h * moveForce);
		
				if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
					rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
			} else {
				if (h * rb2d.velocity.x < maxAirSpeed)
					rb2d.AddForce (Vector2.right * h * moveForce);

				if (Mathf.Abs (rb2d.velocity.x) > maxAirSpeed)
					rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxAirSpeed, rb2d.velocity.y);
			}
		}

		
		/*if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();*/
	}
	
	
	/*void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}*/
}