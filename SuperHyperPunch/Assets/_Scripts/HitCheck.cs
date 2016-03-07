using UnityEngine;
using System.Collections;

public class HitCheck : MonoBehaviour {

	public string direction;
	private SimplePlatformController player;

	// Use this for initialization
	void Start () {
		player = GetComponentInParent<SimplePlatformController> ();
	}
	
	void OnCollisionEnter2D(Collision2D other){
		Debug.Log ("collision step 1");
		if (other.gameObject.CompareTag("PunchHitbox")){
			string hitDirection = other.gameObject.GetComponent<HitCheck> ().direction;
			Debug.Log ("collision step 2");
			if (direction == "right" && hitDirection == "left"){
				player.Knockback(Vector2.left, true);
				Debug.Log ("collision step 3");
			}
		}else{
			Debug.Log ("Hit Player");
		}
	}
}
