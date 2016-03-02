using UnityEngine;
using System.Collections;

//This is derived primarily from the original Acrocatic PlayerDash script

namespace Acrocatic
{
    public class CustomDash : MonoBehaviour
    {
        // Public variables.
        [Header("Dash settings")]
        [Tooltip("Set the force of the dash.")]
        public float dashForce = 600f;
        [Tooltip("Set the duration of the dash. The player can't move during this duration.")]
        public float dashTime = 0.2f;
        [Tooltip("Set the cooldown duration after performing a dash. The player can't dash again while the cooldown is active.")]
        public float cooldownTime = 0.5f;
        [Header("Vertical movement")]
        [Tooltip("Enable or disable gravity while performing a dash.")]
        public bool dashGravity = false;
        [Tooltip("When gravity is disabled, you can set the player's Y velocity to make sure the vertical position doesn't change. Or you can use it to add vertical movement to the dash.")]
        public float dashVelocityY = 0.4f;
        [Header("Air dashing")]
        [Tooltip("Enable or disable dashing while in the air.")]
        public bool airDash = true;
        [Tooltip("You can set a limit for the amount of dashes in the air by enabling this variable and changing the variable below.")]
        //public bool airDashLimit = true;
        //[Tooltip("When there is an air dashing limit, you can set the amount of air dashes here.")]
        //public int airDashTotal = 1;
        public string dashInput = "Dash";
        public string dashHorInput = "Horizontal";
        public string dashVertInput = "Vertical";

        // Private variables.
        private bool dash = false;

        private bool dashLeft = false;               // Boolean that determines if a dash should be performed.
        private bool dashRight = false;
        private bool dashUp = false;               // Boolean that determines if a dash should be performed.
        private bool dashDown = false;
        private bool dashAllowed = false;           // Boolean that determines if a dash is allowed.
        private float dashTimer;                    // Timer used to count down the dashTime.
        private float cooldownTimer;                // Timer used to count down the cooldownTime.
        private bool runCooldownTimer = false;      // Boolean that determines if the cooldown timer should run.
		public float punchTime;						//Punch frame count
		private float punchTimer;						//Punch frame counter
		public bool punching;
        //private int totalAirDashes;                 // Determines how many air dashes are currently allowed.
        private Player player;						// Get the Player class.

        // Use this for initialization
        void Start()
        {
            //Assign player component
            player = GetComponent<Player>();
			punching = false;
        }

        // Update is called once per frame
        void Update()
        {
            SetDashAllowed();

            //If dashing make sure dashing isn't allowed
            if (player.dashing)
            {
                dashAllowed = false;

                //Reset dash timer is x velocity is 0
                if(player.rigidbody.velocity.x == 0)
                {
                    dashTimer = 0;
                }

                //Run Dash Timer
                if (dashTimer > 0)
                {
                    dashTimer -= Time.deltaTime;
                }
                else
                {
                    //run cooldown timer
                    runCooldownTimer = true;

                    player.Dash(false);
                }

            }

            if (runCooldownTimer)
            {
                dashAllowed = false;

                if (cooldownTimer > 0)
                {
                    cooldownTimer -= Time.deltaTime;
                }
                else
                {
                    runCooldownTimer = false;
                }
            }

            if (Input.GetAxis(dashHorInput) > 0 && Input.GetButtonDown(dashInput)){
                dashRight = true;
                dash = true;
				punching = true;
				punchTimer = 0f;
            }
            else if (Input.GetAxis(dashHorInput) < 0 && Input.GetButtonDown(dashInput))
            {
                dash = true;
                dashLeft = true;
				punching = true;
				punchTimer = 0f;
            }
            else if (Input.GetAxis(dashVertInput) > 0 && Input.GetButtonDown(dashInput))
            {
                dash = true;
                dashUp = true;
				punching = true;
				punchTimer = 0f;
            }
            else if (Input.GetAxis(dashVertInput) < 0 && Input.GetButtonDown(dashInput))
            {
                dash = true;
                dashDown = true;
				punching = true;
				punchTimer = 0f;
            }
        }

		void LateUpdate(){
			if (punching){
				punchTimer += Time.deltaTime;
				if (punchTimer >= punchTime){
					punchTimer = 0f;
				}
			}
		}

        void FixedUpdate()
        {

			if (dash){
				dash = false;

				player.UnstickFromPlatform();
				player.Dash(true);
				player.SetXVelocity(0);

				if (dashRight){
					player.rigidbody.AddForce(Vector2.right * dashForce);
					dashRight = false;
				}

				else if (dashLeft){
					player.rigidbody.AddForce(Vector2.left * dashForce);
					dashLeft = false;
				}

				else if (dashUp){
					player.rigidbody.AddForce(Vector2.up * dashForce);
					dashUp = false;
				}

				else if (dashDown){
					player.rigidbody.AddForce(Vector2.down * dashForce);
				}

				dashTimer = dashTime;
				cooldownTimer = cooldownTime;

			}

			if (player.dashing){
				if(!dashGravity){
					float speed = dashVelocityY;
					player.SetYVelocity(speed);
				}
			}
        }

        void SetDashAllowed()
        {
            dashAllowed = true;
        }
    }

}
