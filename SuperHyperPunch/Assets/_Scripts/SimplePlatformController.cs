using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimplePlatformController : MonoBehaviour {
	
	[HideInInspector] public bool facingRight = true;
	[HideInInspector] public bool jump = false;

	[Header ("Energy")]
	//energy variables
	public Slider energyBarSlider;  //reference for slider
	public float energyGain = 0.002f;

	[Header ("Movement")]
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float maxAirSpeed = 1f;
	public Transform groundCheck;

	[Header ("Input Names")]
	public string punchInputName;
	public string horInputName;
	public string vertInputName;

	//Punch Duration
	private bool runPunchTimer;
	public float punchTime;
	private float punchTimer;
	public float punchDistance;

	public bool inControl;

	public float punchForce;

	private bool grounded = false;
	private Animator anim;
	[HideInInspector]public Rigidbody2D rb2d;

	public BoxCollider2D upHitbox, downHitbox, horHitbox;
	public GameManager gameManager;

	[HideInInspector]public AudioSource audioSource;
	public AudioClip punchSound;
	public AudioClip deathSound;
	public AudioClip hitSound;

	//private Vector2 velocityZero = Vector2(0,0);

	[HideInInspector] public bool dead;
	private bool isPunching;
	private int punchCount = 0;
	public float sPunchCost, hPunchCost, shPunchCost;

	private int playerForward;
	
	// Use this for initialization
	public void Init () 
	{
		isPunching = false;
		audioSource = GetComponent<AudioSource> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		if (gameObject.tag == "Player1"){
			energyBarSlider = GameObject.Find ("p1Energy").GetComponent<Slider> ();
//			GetComponent<Combos> ().energyBarSlider = energyBarSlider;
			horInputName = "Horizontal";
			vertInputName = "Vertical";
			punchInputName = "Jump";
		}
		else if (gameObject.tag == "Player2"){
			energyBarSlider = GameObject.Find ("p2Energy").GetComponent<Slider> ();
//			GetComponent<Combos> ().energyBarSlider = energyBarSlider;
			horInputName = "Horizontal2";
			vertInputName = "Vertical2";
			punchInputName = "Jump2";
		}
		playerForward = 1;
		upHitbox.enabled = false;
		downHitbox.enabled = false;
		horHitbox.enabled = false;

		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();

		inControl = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		if (grounded && energyBarSlider.value != 1.0f) 
		{
			energyBarSlider.value += energyGain;
		}

		if (runPunchTimer){
			punchTimer += Time.deltaTime;
			Debug.Log (punchCount);
			if (punchTimer >= punchTime){
				punchCount = 0;
				runPunchTimer = false;
			}
		}
			
	}

	public void Knockback(){
		rb2d.velocity = Vector2.zero;
		inControl = false;
		runPunchTimer = false;
	}

	public void Knockout(){
		if (deathSound != null){
			audioSource.PlayOneShot (deathSound);
		}
		//Debug.Log ("Knocked out");
		if (gameObject.tag == "Player1"){
			//Debug.Log (gameObject.tag);
			gameManager.isAliveP1 = false;
		}
		if (gameObject.tag == "Player2"){
			//Debug.Log (gameObject.tag);
			gameManager.isAliveP2 = false;
		}
		gameManager.DoPointScoring ();
		gameObject.SetActive (false);
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis (horInputName);
		float v = Input.GetAxis (vertInputName);
		bool p = Input.GetButtonDown (punchInputName);

		if (p){
			if (punchCount >= 5 && energyBarSlider.value > 0.15f) {
				if (energyBarSlider.value > shPunchCost) {
					//Debug.Log ("super hyper punch");
					punchTimer = 0;
					runPunchTimer = true;
					if (v == 1) {
						StartSuperHyperPunch ("up");
					}
					if (v == -1) {
						StartSuperHyperPunch ("down");
					}
					if (h == 1) {
						StartSuperHyperPunch ("right");
					}
					if (h == -1) {
						StartSuperHyperPunch ("left");
					}
				}
			}
			if (punchCount >= 3 && punchCount < 5 && energyBarSlider.value > 0.1f) {
					if (energyBarSlider.value > hPunchCost) {
						//Debug.Log ("hyper punch");
					punchTimer = 0;
					runPunchTimer = true;
						if (v == 1) {
							StartHyperPunch ("up");
						}
						if (v == -1) {
							StartHyperPunch ("down");
						}
						if (h == 1) {
							StartHyperPunch ("right");
						}
						if (h == -1) {
							StartHyperPunch ("left");
						}

				}
			}
			if (punchCount < 3 && energyBarSlider.value > 0.05f) {
				if (energyBarSlider.value > sPunchCost) {
//					Debug.Log ("super punch");
//					Debug.Log ("v: " + v);
//					Debug.Log ("h: " +h);
					punchTimer = 0;
					runPunchTimer = true;
					if (v == 1) {
						StartSuperPunch ("up");
					}
					if (v == -1) {
						StartSuperPunch ("down");
					}
					if (h == 1) {
						StartSuperPunch ("right");
					}
					if (h == -1) {
						StartSuperPunch ("left");
					}
				}
			}
		}
			
		if (inControl && !isPunching) {
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
		if (inControl) {
			if (h > 0 && !facingRight)
				Flip ();
			else if (h < 0 && facingRight)
				Flip ();
		}
	}

	public void SetTimers(){
		punchTimer = 0f;
		runPunchTimer = true;
	}

	public void StartSuperPunch(string direction){
		isPunching = true;
		StartCoroutine (SuperPunch (direction));
			if (direction == "up"){
				energyBarSlider.value -= 0.05f;
				anim.SetInteger ("PunchType",0);
				anim.SetTrigger ("UpPunch");
			}
			if(direction == "down"){
				energyBarSlider.value -= 0.05f;
				anim.SetInteger ("PunchType",0);
				anim.SetTrigger ("DownPunch");
			}
			if(direction == "right"){
				energyBarSlider.value -= 0.05f;
				anim.SetInteger ("PunchType",0);
				anim.SetTrigger ("RightPunch");
			}
			if(direction == "left"){
				energyBarSlider.value -= 0.05f;
				anim.SetInteger ("PunchType",0);
				anim.SetTrigger ("RightPunch");
			}
			punchCount += 1;
			Debug.Log (punchCount);
	}
		
	private IEnumerator SuperPunch(string direction){
		audioSource.PlayOneShot (punchSound);
		float time = 0f;
		if (inControl) {
			while (punchDistance > time){
				inControl = false;
				time += Time.deltaTime;
				if (direction == "up"){
					rb2d.velocity = Vector2.up * punchForce;
				}
				if (direction == "down"){
					rb2d.velocity = Vector2.down * punchForce;
				}
				if (direction == "right"){
					rb2d.velocity = Vector2.right * punchForce;
				}
				if (direction == "left"){
					rb2d.velocity = Vector2.left * punchForce;
				}
				yield return 0;
			}
			rb2d.velocity = Vector2.zero;
			isPunching = false;
			inControl = true;
		}
	}

	public void StartHyperPunch(string direction){
		isPunching = true;
		StartCoroutine (HyperPunch (direction));
		if (direction == "up"){
			energyBarSlider.value -= 0.1f;
			anim.SetInteger ("PunchType",1);
			anim.SetTrigger ("UpPunch");
		}
		if(direction == "down"){
			energyBarSlider.value -= 0.1f;
			anim.SetInteger ("PunchType",1);
			anim.SetTrigger ("DownPunch");
		}
		if(direction == "right"){
			energyBarSlider.value -= 0.1f;
			anim.SetInteger ("PunchType",1);
			anim.SetTrigger ("RightPunch");
		}
		if(direction == "left"){
			energyBarSlider.value -= 0.1f;
			anim.SetInteger ("PunchType",1);
			anim.SetTrigger ("RightPunch");
		}
		punchCount += 1;
		Debug.Log (punchCount);
	}

	private IEnumerator HyperPunch(string direction){
		audioSource.PlayOneShot (punchSound);
		float time = 0f;
		if (inControl) {
			while (punchDistance > time){
				inControl = false;
				time += Time.deltaTime;
				if (direction == "up"){
					rb2d.velocity = Vector2.up * punchForce * 2;
				}
				if (direction == "down"){
					rb2d.velocity = Vector2.down * punchForce * 2;
				}
				if (direction == "right"){
					rb2d.velocity = Vector2.right * punchForce * 2;
				}
				if (direction == "left"){
					rb2d.velocity = Vector2.left * punchForce * 2;
				}
				yield return 0;
			}
			rb2d.velocity = Vector2.zero;
			isPunching = false;
			inControl = true;
		}
	}

	public void StartSuperHyperPunch(string direction){
		isPunching = true;
		StartCoroutine (SuperHyperPunch (direction));
		if (direction == "up"){
			energyBarSlider.value -= .15f;
			anim.SetInteger ("PunchType",2);
			anim.SetTrigger ("UpPunch");
		}
		if(direction == "down"){
			energyBarSlider.value -= .15f;
			anim.SetInteger ("PunchType",2);
			anim.SetTrigger ("DownPunch");
		}
		if(direction == "right"){
			energyBarSlider.value -= .15f;
			anim.SetInteger ("PunchType",2);
			anim.SetTrigger ("RightPunch");
		}
		if(direction == "left"){
			energyBarSlider.value -= .15f;
			anim.SetInteger ("PunchType",2);
			anim.SetTrigger ("RightPunch");
		}
		punchCount = 0;
		Debug.Log (punchCount);
	}

	private IEnumerator SuperHyperPunch(string direction){
		audioSource.PlayOneShot (punchSound);
		float time = 0f;
		if (inControl) {
			while (punchDistance > time){
				inControl = false;
				time += Time.deltaTime;
				if (direction == "up"){
					rb2d.velocity = Vector2.up * punchForce * 3;
				}
				if (direction == "down"){
					rb2d.velocity = Vector2.down * punchForce * 3;
				}
				if (direction == "right"){
					rb2d.velocity = Vector2.right * punchForce * 3;
				}
				if (direction == "left"){
					rb2d.velocity = Vector2.left * punchForce * 3;
				}
				yield return 0;
			}
			rb2d.velocity = Vector2.zero;
			isPunching = false;
			inControl = true;
		}
	}

	//SUPER PUNCH
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
		
	void OnTriggerEnter2D(Collider2D other){
		if (other.name == "KillBox"){
			Knockout ();
		}
	}

	IEnumerator SetHitbox(float resetTime){
		yield return new WaitForSeconds (resetTime);
		upHitbox.enabled = false;
		downHitbox.enabled = false;
		horHitbox.enabled = false;
	}
	
	void Flip()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		playerForward *= -1;
		transform.localScale = theScale;
	}
}