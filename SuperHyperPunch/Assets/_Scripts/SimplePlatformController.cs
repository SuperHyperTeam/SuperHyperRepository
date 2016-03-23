using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	//energy variables
	public Slider energyBarSlider;  //reference for slider
	public float energyGain = 0.002f;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float maxAirSpeed = 1f;
	public Transform groundCheck;
	public string punchInputName;
	public string horInputName;
	public string vertInputName;
	public float horPunchForce;
	public float vertPunchForce;
	public float knockbackForce;

	private bool punch;
	[HideInInspector]public bool punchLeft, punchRight, punchUp, punchDown; //if punching and what direction

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

	[HideInInspector]public bool inControl;

	private bool grounded = false;
	private Animator anim;
	[HideInInspector]public Rigidbody2D rb2d;

	public BoxCollider2D upHitbox, downHitbox, horHitbox;

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

		if (grounded && energyBarSlider.value != 1.0f) 
		{
			energyBarSlider.value += energyGain;
		}

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
				//rb2d.velocity = Vector2.zero;
				runKnockbackTimer = false;
				inControl = true;
			}
		}
			
		rb2d.gravityScale = currentGrav;
	}

	public void Knockback(){
		//rb2d.velocity = Vector2.zero;
		inControl = false;
		runPunchTimer = false;
		punch = false;
		knockbackTimer = 0f;
		runKnockbackTimer = true;

	}

	public void Knockout(){
		dead = true;
		GetComponent<SpriteRenderer> ().enabled = false;
		GetComponent<BoxCollider2D> ().enabled = false;
	}

	void FixedUpdate()
	{
		upHitbox.enabled = false;
		downHitbox.enabled = false;
		horHitbox.enabled = false;
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
					SuperPunch (Vector2.right, horPunchForce, horHitbox);
				}
				if (punchLeft) {
					SuperPunch (Vector2.left, horPunchForce, horHitbox);
				}
				if (punchUp) {
					SuperPunch (Vector2.up, vertPunchForce, upHitbox);
				}
				if (punchDown) {
					SuperPunch (Vector2.down, vertPunchForce, downHitbox);
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

		
		if (h > 0 && !facingRight)
			Flip ();
		else if (h < 0 && facingRight)
			Flip ();
	}

	public void SuperPunch(Vector2 direction, float force, BoxCollider2D hitbox){
		if (energyBarSlider.value > 0.05f) 
		{
			energyBarSlider.value -= 0.05f;
			rb2d.AddForce (direction * force);
			hitbox.enabled = true;
		}
	}

	public void HyperPunch(Vector2 direction, float force, BoxCollider2D hitbox){
		if (energyBarSlider.value > 0.2f) 
		{
			energyBarSlider.value -= 0.2f;
			rb2d.AddForce ((direction * force)*1.5f);
			hitbox.enabled = true;
		}
	}

	public void SuperHyperPunch(Vector2 direction, float force, BoxCollider2D hitbox){
		if (energyBarSlider.value > 0.6f) 
		{
			energyBarSlider.value -= 0.6f;
			rb2d.AddForce ((direction * force)*1.75f);
			hitbox.enabled = true;
		}
	}

	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}