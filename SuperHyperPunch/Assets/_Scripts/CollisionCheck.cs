using UnityEngine;
using System.Collections;


public class CollisionCheck : MonoBehaviour {

	private SimplePlatformController player;
	private SimplePlatformController otherPlayer;

	private Rigidbody2D rb2d;

	void Start (){
		rb2d = GetComponent<Rigidbody2D> ();
		player = GetComponent<SimplePlatformController> ();

	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player"){
			otherPlayer = other.gameObject.GetComponent<SimplePlatformController> ();
			if (player.punchRight){
				if (otherPlayer.punchLeft) {
					player.Knockback (Vector2.left, true);
					otherPlayer.Knockback (Vector2.right, true);
				} else
					otherPlayer.Knockout ();
			}
			if (player.punchLeft){
				if (otherPlayer.punchRight){
					player.Knockback (Vector2.right, true);
					otherPlayer.Knockback (Vector2.left, true);
				} else
					otherPlayer.Knockout ();
				
			}
			if (player.punchUp){
				if(otherPlayer.punchDown){
					player.Knockback (Vector2.down, false);
					otherPlayer.Knockback (Vector2.up, false);
				} else
					otherPlayer.Knockout ();
			}
			if (player.punchDown){
				if(otherPlayer.punchUp){
					player.Knockback (Vector2.up, false);
					otherPlayer.Knockback (Vector2.down, false);
				} else
					otherPlayer.Knockout ();
				
			}
		}
	}
}
