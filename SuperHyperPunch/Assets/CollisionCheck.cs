using UnityEngine;
using System.Collections;


namespace Acrocatic{
	public class CollisionCheck : MonoBehaviour {

		private Player player;
		private CustomDash playerDash;

		void Start (){
			player = GetComponent<Player> ();
			playerDash = GetComponent<CustomDash> ();
		}

		void OnCollisionEnter2D(Collision2D other){
			if (other.gameObject.tag == "Player"){
				Player otherPlayer = other.gameObject.GetComponent<Player> ();
				Debug.Log (playerDash.punching);
				if (playerDash.punching) {
					Debug.Log ("Dashing Hit");
					Destroy (other.gameObject);
				}
			}
		}
	}
}
