//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;
//
//public class SimplePlatformControllerBACKUP : MonoBehaviour {
//	
//	[HideInInspector] public bool facingRight = true;
//	[HideInInspector] public bool jump = false;
//
//	[Header ("Energy")]
//	//energy variables
//	public Slider energyBarSlider;  //reference for slider
//	public float energyGain = 0.002f;
//
//	[Header ("Movement")]
//	public float moveForce = 365f;
//	public float maxSpeed = 5f;
//	public float maxAirSpeed = 1f;
//	public Transform groundCheck;
//
//	[Header ("Input Names")]
//	public string punchInputName;
//	public string horInputName;
//	public string vertInputName;
//	[Header ("Punching")]
//	public float horPunchForce;
//	public float vertPunchForce;
//	public float knockbackForce;
//	public float superPunchTime;
//	public float hyperPunchTime;
//	public float superHyperPunchTime;
//
//	private bool punch;
//	[HideInInspector]public bool punchLeft, punchRight, punchUp, punchDown; //if punching and what direction
//
//	[Header ("Timing")]
//	//Gravity Delay
//	public float gravModifier; //What to make the gravity while punching
//	private float currentGrav;
//	private bool runGravTimer;
//	public float gravTime;
//	private float gravTimer;
//	public bool knockback;
//	private bool runKnockbackTimer;
//	public float knockbackTime;
//	private float knockbackTimer;
//
//	//Punch Duration
//	private bool runPunchTimer;
//	public float punchTime;
//	private float punchTimer;
//
//	public bool inControl;
//
//	private bool grounded = false;
//	private Animator anim;
//	[HideInInspector]public Rigidbody2D rb2d;
//
//	public BoxCollider2D upHitbox, downHitbox, horHitbox;
//	public GameManager gameManager;
//	public GameObject forwardTarget;
//	public GameObject upTarget;
//	public GameObject downTarget;
//	public GameObject forwardTargetHyper, forwardTargetSH, upTargetHyper, upTargetSH, downTargetHyper, downTargetSH;
//
//	[HideInInspector]public AudioSource audioSource;
//	public AudioClip punchSound;
//	public AudioClip deathSound;
//	public AudioClip hitSound;
//
//	//private Vector2 velocityZero = Vector2(0,0);
//
//	[HideInInspector] public bool dead;
//
//	private int playerForward;
//	
//	// Use this for initialization
//	public void Init () 
//	{
//		audioSource = GetComponent<AudioSource> ();
//		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
//		if (gameObject.tag == "Player1"){
//			energyBarSlider = GameObject.Find ("p1Energy").GetComponent<Slider> ();
//			GetComponent<Combos> ().energyBarSlider = energyBarSlider;
//			horInputName = "Horizontal";
//			vertInputName = "Vertical";
//			punchInputName = "Jump";
//		}
//		else if (gameObject.tag == "Player2"){
//			energyBarSlider = GameObject.Find ("p2Energy").GetComponent<Slider> ();
//			GetComponent<Combos> ().energyBarSlider = energyBarSlider;
//			horInputName = "Horizontal2";
//			vertInputName = "Vertical2";
//			punchInputName = "Jump2";
//		}
//		playerForward = 1;
//		upHitbox.enabled = false;
//		downHitbox.enabled = false;
//		horHitbox.enabled = false;
////		if (energyBarSlider == null){
////			if (gameObject.name == "Player1") {
////				energyBarSlider = GameObject.Find ("p1Energy").GetComponent<Slider> ();
////			}
////			if (gameObject.name == "Player2"){
////				energyBarSlider = GameObject.Find ("p2Energy").GetComponent<Slider> ();
////			}
////		}
//		anim = GetComponent<Animator>();
//		rb2d = GetComponent<Rigidbody2D>();
//		currentGrav = rb2d.gravityScale;
//
//		inControl = true;
//
//		//Initialize bools
//		runGravTimer = runPunchTimer = false;
//		punch = punchDown = punchLeft = punchRight = punchUp = false;
//	}
//	
//	// Update is called once per frame
//	void Update () 
//	{
//		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
//
//		if (grounded && energyBarSlider.value != 1.0f) 
//		{
//			energyBarSlider.value += energyGain;
//		}
//
//		if(runGravTimer){
//			gravTimer += Time.deltaTime;
//			if(gravTimer >= gravTime){
//				runGravTimer = false;
//				currentGrav = 1f;
//				gravTimer = 0f;
//			}
//		}
//		if (runPunchTimer) {
//			punchTimer += Time.deltaTime;
//			if (punchTimer >= punchTime) {
//				rb2d.velocity = Vector2.zero;
//				runPunchTimer = false;
//				punch = false;
//				punchUp = punchDown = punchRight = punchLeft = false;
//				punchTimer = 0f;
//			}
//		}
//		if (runKnockbackTimer){
//			knockbackTimer += Time.deltaTime;
//			if (knockbackTimer >= knockbackTime){
//				rb2d.velocity = Vector2.zero;
//				runKnockbackTimer = false;
//				inControl = true;
//			}
//		}
//			
//		rb2d.gravityScale = currentGrav;
//	}
//
//	public void Knockback(){
//		rb2d.velocity = Vector2.zero;
//		inControl = false;
//		runPunchTimer = false;
//		punch = false;
//		knockbackTimer = 0f;
//		runKnockbackTimer = true;
//	}
//
//	public void Knockout(){
//		if (deathSound != null){
//			audioSource.PlayOneShot (deathSound);
//		}
//		Debug.Log ("Knocked out");
//		if (gameObject.tag == "Player1"){
//			Debug.Log (gameObject.tag);
//			gameManager.isAliveP1 = false;
//		}
//		if (gameObject.tag == "Player2"){
//			Debug.Log (gameObject.tag);
//			gameManager.isAliveP2 = false;
//		}
//		gameManager.DoPointScoring ();
//		gameObject.SetActive (false);
//	}
//
//	void FixedUpdate()
//	{
//		float h = Input.GetAxis (horInputName);
//		float v = Input.GetAxis (vertInputName);
//		bool p = Input.GetButtonDown (punchInputName);
//
//		if (p){
//			GetComponent<Combos> ().ExternalSetCombo ();
//		}
//
//		//anim.SetFloat("Speed", Mathf.Abs(h));
//
////		if (punch) {
////			if (punchRight) {
////				SuperPunch (Vector2.right, horPunchForce, horHitbox);
////			}
////			if (punchLeft) {
////				SuperPunch (Vector2.left, horPunchForce, horHitbox);
////			}
////			if (punchUp) {
////				SuperPunch (Vector2.up, vertPunchForce, upHitbox);
////			}
////			if (punchDown) {
////				SuperPunch (Vector2.down, vertPunchForce, downHitbox);
////			}
////		}
//		if (inControl) {
//			if (grounded) {
//				if (h * rb2d.velocity.x < maxSpeed)
//					rb2d.AddForce (Vector2.right * h * moveForce);
//		
//				if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
//					rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
//			} else {
//				if (h * rb2d.velocity.x < maxAirSpeed)
//					rb2d.AddForce (Vector2.right * h * moveForce);
//
//				if (Mathf.Abs (rb2d.velocity.x) > maxAirSpeed)
//					rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxAirSpeed, rb2d.velocity.y);
//			}
//		}
//		if (inControl) {
//			if (h > 0 && !facingRight)
//				Flip ();
//			else if (h < 0 && facingRight)
//				Flip ();
//		}
//	}
//
//	public void SetTimers(){
//		punch = true;
//		punchTimer = 0f;
//		runPunchTimer = true;
//		gravTimer = 0f;
//		runGravTimer = true;
//		currentGrav = gravModifier;
//	}
//
////	public void SuperPunch(Vector2 direction, float force, BoxCollider2D hitbox){
////		if (energyBarSlider.value > 0.05f) 
////		{
////			energyBarSlider.value -= 0.05f;
////			rb2d.AddForce (direction * force, ForceMode2D.Impulse);
////			hitbox.enabled = true;
////		}
////	}
//
//	//SUPER PUNCH
//	public void SuperPunch (string direction){
//		audioSource.PlayOneShot (punchSound);
//		if (direction == "up" && inControl){
//			if (energyBarSlider.value > 0.2f)
//			{
//				energyBarSlider.value -= 0.2f;
//				iTween.MoveTo (gameObject, upTarget.transform.position, hyperPunchTime);
//				upHitbox.enabled = true;
//				anim.SetInteger ("PunchType",0);
//				anim.SetTrigger ("UpPunch");
//				StartCoroutine (SetHitbox (superPunchTime));
//			}
//		}
//		if (direction == "down" && inControl){
//			if (energyBarSlider.value > 0.2f) 
//			{
//				energyBarSlider.value -= 0.2f;
//				iTween.MoveTo (gameObject, downTarget.transform.position, hyperPunchTime);
//				downHitbox.enabled = true;
//				anim.SetInteger ("PunchType",0);
//				anim.SetTrigger ("DownPunch");
//				StartCoroutine (SetHitbox (superPunchTime));
//			}
//		}
//		if (direction == "forward" && inControl){
//			if (energyBarSlider.value > 0.2f)
//			{
//				energyBarSlider.value -= 0.2f;
//				iTween.MoveTo (gameObject, forwardTarget.transform.position, hyperPunchTime);
//				horHitbox.enabled = true;
//				anim.SetInteger ("PunchType",0);
//				anim.SetTrigger ("RightPunch");
//				StartCoroutine (SetHitbox (superPunchTime));
//			}
//		}
//	}
//
//	//HYPER PUNCH
//	public void HyperPunch (string direction){
//		audioSource.PlayOneShot (punchSound);
//		if (direction == "up" && inControl){
//			if (energyBarSlider.value > 0.2f) 
//			{
//				energyBarSlider.value -= 0.2f;
//				iTween.MoveTo (gameObject, upTargetHyper.transform.position, hyperPunchTime);
//				upHitbox.enabled = true;
//				anim.SetInteger ("PunchType",1);
//				anim.SetTrigger ("UpPunch");
//				StartCoroutine (SetHitbox (hyperPunchTime));
//			}
//		}
//		if (direction == "down" && inControl){
//			if (energyBarSlider.value > 0.2f) 
//			{
//				energyBarSlider.value -= 0.2f;
//				iTween.MoveTo (gameObject, downTargetHyper.transform.position, hyperPunchTime);
//				downHitbox.enabled = true;
//				anim.SetInteger ("PunchType",1);
//				anim.SetTrigger ("DownPunch");
//				StartCoroutine (SetHitbox (hyperPunchTime));
//			}
//		}
//		if (direction == "forward" && inControl){
//			if (energyBarSlider.value > 0.2f) {
//				energyBarSlider.value -= 0.2f;
//				iTween.MoveTo (gameObject, forwardTargetHyper.transform.position, hyperPunchTime);
//				horHitbox.enabled = true;
//				anim.SetInteger ("PunchType",1);
//				anim.SetTrigger ("RightPunch");
//				StartCoroutine (SetHitbox (superPunchTime));
//			}
//		}
//	}
//
//	//SUPERHYPER PUNCH
//	public void SuperHyperPunch (string direction){
//		audioSource.PlayOneShot (punchSound);
//		if (direction == "up" && inControl){
//			if (energyBarSlider.value > 0.2f) 
//			{
//				energyBarSlider.value -= 0.2f;
//				iTween.MoveTo (gameObject, upTargetSH.transform.position, hyperPunchTime);
//				upHitbox.enabled = true;
//				anim.SetInteger ("PunchType",2);
//				anim.SetTrigger ("UpPunch");
//				StartCoroutine (SetHitbox (superHyperPunchTime));
//
//			}
//		}
//		if (direction == "down" && inControl){
//			if (energyBarSlider.value > 0.2f) 
//			{
//				energyBarSlider.value -= 0.2f;
//				iTween.MoveTo (gameObject, downTargetSH.transform.position, hyperPunchTime);
//				downHitbox.enabled = true;
//				anim.SetInteger ("PunchType",2);
//				anim.SetTrigger ("DownPunch");
//				StartCoroutine (SetHitbox (superHyperPunchTime));
//			}
//		}
//		if (direction == "forward" && inControl){
//			if (energyBarSlider.value > 0.2f) 
//			{
//				energyBarSlider.value -= 0.2f;
//				iTween.MoveTo (gameObject, forwardTargetSH.transform.position, hyperPunchTime);
//				horHitbox.enabled = true;
//				anim.SetInteger ("PunchType",2);
//				anim.SetTrigger ("RightPunch");
//				StartCoroutine (SetHitbox (superPunchTime));
//			}
//		}
//	}
//		
//	void OnTriggerEnter2D(Collider2D other){
//		if (other.name == "KillBox"){
//			Knockout ();
////			if (gameObject.tag == "Player1"){
////				gameManager.isAliveP1 = false;
////			}
////			if (gameObject.tag == "Player2"){
////				gameManager.isAliveP2 = false;
////			}
////			gameManager.DoPointScoring ();
////			gameObject.SetActive (false);
//			//Destroy (gameObject);
//		}
//	}
//
//	IEnumerator SetHitbox(float resetTime){
//		yield return new WaitForSeconds (resetTime);
//		upHitbox.enabled = false;
//		downHitbox.enabled = false;
//		horHitbox.enabled = false;
//	}
//	
//	void Flip()
//	{
//		facingRight = !facingRight;
//		Vector3 theScale = transform.localScale;
//		theScale.x *= -1;
//		playerForward *= -1;
//		transform.localScale = theScale;
//	}
//
//	void OnCollisionEnter2D(){
//		iTween.Stop ();
//	}
//}