using UnityEngine;
using System.Collections;

public class CustomController : MonoBehaviour {

	[HideInInspector]
	public Transform groundCheck;

	private Animator animator;
	private Rigidbody2D rigidbody;
	[HideInInspector]
	public bool grounded = true;
	[HideInInspector]
	public bool punching = false;
	[HideInInspector]
	public Collider2D groundCollider;
	public float groundCheckRadius = 0.12f;
	public LayerMask groundLayer;

	public string horInput;//Name of horizontal input
	public string vertInput; //Name of vertical input
	private float hor;
	private float vert;

	//PUNCHING
	public float punchForce = 600f; //Force to apply when punching
	public float punchTime = 0.2f; 
	public float cooldownTime = 0.5f; //Cooldown between punches
	public bool punchGravity = false; //Whether different gravity should be applied while punching
	public float punchVelocityY = 0.4f; //Gravity to apply when punching if punch gravity is true
	public bool airPunch = true; //Can punch in the air
	public string punchInput = "Dash"; // Input for punching

	private bool punch = false;

	private bool punchLeft = false; //If player should punch left
	private bool punchRight = false; //If player should punch right
	private bool punchUp = false;  //If player should punch up
	private bool punchDown = false; // If player should punch down
	private bool punchAllowed = false;  // If player can punch
	private float punchTimer;                    // Timer used to count down the dashTime.
	private float cooldownTimer; // Timer for dash cooldown
	private bool runCooldownTimer = false; //Whether the cooldown timer should run
	public float punchFrames; //Frame count for punches
	private float punchFrameTimer;	//Timer for tracking punch frames

	// Use this for initialization
	void Start () {
		
		groundCheck = transform.Find ("GroundCheck");
	}

	// Update is called once per frame
	void Update () {

		animator.SetBool ("Grounded", grounded);
		animator.SetFloat ("Horizontal", hor);
		animator.SetBool ("PunchRight", punchRight);
		animator.SetBool ("PunchLeft", punchLeft);
		animator.SetBool ("PunchUp", punchUp);
		animator.SetBool ("PunchDown", punchDown);
		animator.SetFloat ("xSpeed", rigidbody.velocity.x);
		animator.SetFloat ("ySpeed", rigidbody.velocity.y);

		groundCollider = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, groundLayer);

		if (groundCollider){
			grounded = true;
		}

		if (grounded){
			
		}
	}


}
