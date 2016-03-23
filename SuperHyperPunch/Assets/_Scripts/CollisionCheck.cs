using UnityEngine;
using System.Collections;


public class CollisionCheck : MonoBehaviour {

	private SimplePlatformController player;

	void Start (){
		player = GetComponent<SimplePlatformController> ();
	}

	void OnCollisionEnter2D(Collision2D collision){
		foreach(ContactPoint2D c in collision.contacts){
			if (c.collider.tag == "Hitbox" && c.otherCollider.tag == "Hitbox"){
				SimplePlatformController other = c.otherCollider.GetComponentInParent<SimplePlatformController> ();

				Vector2 playerDirection = player.rb2d.velocity;
				Vector2 otherDirection = other.rb2d.velocity;

				player.Knockback ();
				other.Knockback ();

//				player.rb2d.AddForce(Vector2.Reflect((c.point-playerPos).normalized, 
//					collision.contacts[0].normal) * player.knockbackForce,   
//					ForceMode2D.Impulse);
				
//				other.rb2d.AddForce(Vector2.Reflect((c.point-otherPos).normalized, 
//					-collision.contacts[0].normal) * other.knockbackForce,   
//					ForceMode2D.Impulse);
			}
			else if (c.collider.tag == "Hitbox" && c.otherCollider.tag == "Player"){
				c.otherCollider.GetComponentInParent<SimplePlatformController> ().Knockout ();
			}
		}
	}
}
