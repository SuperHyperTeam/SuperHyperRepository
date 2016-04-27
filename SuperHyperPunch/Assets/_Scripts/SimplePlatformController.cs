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

	[HideInInspector] public bool dead;
	private bool isPunching;
	private int punchCount = 0;
	public float sPunchCost, hPunchCost, shPunchCost;

	private int playerForward;

	public Transform upTarget, downTarget, forwardTarget;
	
	// Use this for initialization
	public void Init () 
	{
		isPunching = false;
		audioSource = GetComponent<AudioSource> ();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		if (gameObject.tag == "Player1"){
			energyBarSlider = GameObject.Find ("p1Energy").GetComponent<Slider> ();
			horInputName = "Horizontal";
			vertInputName = "Vertical";
			punchInputName = "Jump";
		}
		else if (gameObject.tag == "Player2"){
			energyBarSlider = GameObject.Find ("p2Energy").GetComponent<Slider> ();
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
		anim.SetBool ("Walking", false);
		punchCount = 0;
	}

	void FixedUpdate()
	{
		float h = Input.GetAxis (horInputName);
		float v = Input.GetAxis (vertInputName);
		if (inControl && !isPunching) {
			if (grounded) {
				if (h != 0) {
					anim.SetBool ("Walking", true);
				}else anim.SetBool ("Walking", false);

				if (h * rb2d.velocity.x < maxSpeed)
					rb2d.AddForce (Vector2.right * h * moveForce);

				if (Mathf.Abs (rb2d.velocity.x) > maxSpeed)
					rb2d.velocity = new Vector2 (Mathf.Sign (rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
			} else {
				anim.SetBool ("Walking", false);
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
			if (punchTimer >= punchTime){
				punchCount = 0;
				runPunchTimer = false;
			}
		}
	}

	void LateUpdate(){
		float h = Input.GetAxis (horInputName);
		float v = Input.GetAxis (vertInputName);
		bool p = Input.GetButtonDown (punchInputName);

		if (p && inControl){
			
			if (punchCount >= 5) {
				if (energyBarSlider.value > shPunchCost) {
					anim.SetTrigger ("ExitPunch");
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
			if (punchCount >= 3 && punchCount < 5) {
				if (energyBarSlider.value > hPunchCost) {
					anim.SetTrigger ("ExitPunch");
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
			if (punchCount < 3) {
				if (energyBarSlider.value > sPunchCost) {
					anim.SetTrigger ("ExitPunch");
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
		if (gameObject.tag == "Player1"){
			gameManager.isAliveP1 = false;
		}
		if (gameObject.tag == "Player2"){
			gameManager.isAliveP2 = false;
		}
		gameManager.DoPointScoring ();
		gameObject.SetActive (false);
	}



	public void SetTimers(){
		punchTimer = 0f;
		runPunchTimer = true;
	}

	public void StartSuperPunch(string direction){
		isPunching = true;
		StartCoroutine (SuperPunch (direction));
			if (direction == "up"){
				upHitbox.enabled = true;
				downHitbox.enabled = false;
				horHitbox.enabled = false;
			energyBarSlider.value -= sPunchCost;
				anim.SetInteger ("PunchType",0);
				anim.SetTrigger ("UpPunch");
			}
			if(direction == "down"){
				downHitbox.enabled = true;
				upHitbox.enabled = false;
				horHitbox.enabled = false;
			energyBarSlider.value -=  sPunchCost;
				anim.SetInteger ("PunchType",0);
				anim.SetTrigger ("DownPunch");
			}
			if(direction == "right"){
				horHitbox.enabled = true;
				downHitbox.enabled = false;
				upHitbox.enabled = false;
			energyBarSlider.value -=  sPunchCost;
				anim.SetInteger ("PunchType",0);
				anim.SetTrigger ("RightPunch");
			}
			if(direction == "left"){
				horHitbox.enabled = true;
				downHitbox.enabled = false;
				upHitbox.enabled = false;
			energyBarSlider.value -=  sPunchCost;
				anim.SetInteger ("PunchType",0);
				anim.SetTrigger ("RightPunch");
			}
			punchCount += 1;
	}
		
	private IEnumerator SuperPunch(string direction){
		audioSource.PlayOneShot (punchSound);
		float time = 0f;
		if (inControl) {
			while (punchDistance > time){
				inControl = false;
				time += Time.deltaTime;
				if (direction == "up"){
					rb2d.velocity = (Vector2)upTarget.localPosition * punchForce;
				}
				if (direction == "down"){
					if (facingRight) {
						rb2d.velocity = (Vector2)downTarget.localPosition * punchForce;
					}else
						rb2d.velocity = (new Vector2 (downTarget.localPosition.y, -downTarget.localPosition.x) * punchForce);
				}
				if (direction == "right"){
					rb2d.velocity = (Vector2)forwardTarget.localPosition * punchForce;
				}
				if (direction == "left"){
					rb2d.velocity = -((Vector2)forwardTarget.localPosition * punchForce);
				}
				yield return 0;
			}
			anim.SetTrigger ("ExitPunch");
			upHitbox.enabled = downHitbox.enabled = horHitbox.enabled = false;
			rb2d.velocity = Vector2.zero;
			isPunching = false;
			inControl = true;
		}
	}

	public void StartHyperPunch(string direction){
		isPunching = true;
		StartCoroutine (HyperPunch (direction));
		if (direction == "up"){
			upHitbox.enabled = true;
			downHitbox.enabled = false;
			horHitbox.enabled = false;
			energyBarSlider.value -= hPunchCost;
			anim.SetInteger ("PunchType",1);
			anim.SetTrigger ("UpPunch");
		}
		if(direction == "down"){
			downHitbox.enabled = true;
			upHitbox.enabled = false;
			horHitbox.enabled = false;
			energyBarSlider.value -= hPunchCost;
			anim.SetInteger ("PunchType",1);
			anim.SetTrigger ("DownPunch");
		}
		if(direction == "right"){
			horHitbox.enabled = true;
			downHitbox.enabled = false;
			upHitbox.enabled = false;
			energyBarSlider.value -= hPunchCost;
			anim.SetInteger ("PunchType",1);
			anim.SetTrigger ("RightPunch");
		}
		if(direction == "left"){
			horHitbox.enabled = true;
			downHitbox.enabled = false;
			upHitbox.enabled = false;
			energyBarSlider.value -= hPunchCost;
			anim.SetInteger ("PunchType",1);
			anim.SetTrigger ("RightPunch");
		}
		punchCount += 1;
	}

	private IEnumerator HyperPunch(string direction){
		audioSource.PlayOneShot (punchSound);
		float time = 0f;
		if (inControl) {
			while (punchDistance > time){
				inControl = false;
				time += Time.deltaTime;
				if (direction == "up"){
					rb2d.velocity = (Vector2)upTarget.localPosition * punchForce * 2;
				}
				if (direction == "down"){
					if (facingRight) {
						rb2d.velocity = (Vector2)downTarget.localPosition * punchForce * 2;
					}else
						rb2d.velocity = (new Vector2 (downTarget.localPosition.y, -downTarget.localPosition.x) * punchForce * 2);
				}
				if (direction == "right"){
					rb2d.velocity = (Vector2)forwardTarget.localPosition * punchForce * 2;
				}
				if (direction == "left"){
					rb2d.velocity = -((Vector2)forwardTarget.localPosition * punchForce * 2);
				}
				yield return 0;
			}
			anim.SetTrigger ("ExitPunch");
			upHitbox.enabled = downHitbox.enabled = horHitbox.enabled = false;
			rb2d.velocity = Vector2.zero;
			isPunching = false;
			inControl = true;
		}
	}

	public void StartSuperHyperPunch(string direction){
		isPunching = true;
		StartCoroutine (SuperHyperPunch (direction));
		if (direction == "up"){
			upHitbox.enabled = true;
			downHitbox.enabled = false;
			horHitbox.enabled = false;
			energyBarSlider.value -= shPunchCost;
			anim.SetInteger ("PunchType",2);
			anim.SetTrigger ("UpPunch");
		}
		if(direction == "down"){
			downHitbox.enabled = true;
			upHitbox.enabled = false;
			horHitbox.enabled = false;
			energyBarSlider.value -= shPunchCost;
			anim.SetInteger ("PunchType",2);
			anim.SetTrigger ("DownPunch");
		}
		if(direction == "right"){
			horHitbox.enabled = true;
			downHitbox.enabled = false;
			upHitbox.enabled = false;
			energyBarSlider.value -= shPunchCost;
			anim.SetInteger ("PunchType",2);
			anim.SetTrigger ("RightPunch");
		}
		if(direction == "left"){
			horHitbox.enabled = true;
			downHitbox.enabled = false;
			upHitbox.enabled = false;
			energyBarSlider.value -= shPunchCost;
			anim.SetInteger ("PunchType",2);
			anim.SetTrigger ("RightPunch");
		}
		punchCount = 0;
	}

	private IEnumerator SuperHyperPunch(string direction){
		audioSource.PlayOneShot (punchSound);
		float time = 0f;
		if (inControl) {
			while (punchDistance > time){
				inControl = false;
				time += Time.deltaTime;
				if (direction == "up"){
					rb2d.velocity = (Vector2)upTarget.localPosition * punchForce * 3;
				}
				if (direction == "down"){
					if (facingRight) {
						rb2d.velocity = (Vector2)downTarget.localPosition * punchForce * 3;
					}else
						rb2d.velocity = (new Vector2 (downTarget.localPosition.y, -downTarget.localPosition.x) * punchForce * 3);
				}
				if (direction == "right"){
					rb2d.velocity = (Vector2)forwardTarget.localPosition * punchForce * 3;
				}
				if (direction == "left"){
					rb2d.velocity = -((Vector2)forwardTarget.localPosition * punchForce * 3);
				}
				yield return 0;
			}
			anim.SetTrigger ("ExitPunch");
			upHitbox.enabled = downHitbox.enabled = horHitbox.enabled = false;
			rb2d.velocity = Vector2.zero;
			isPunching = false;
			inControl = true;
		}
	}
		
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