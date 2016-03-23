using UnityEngine;
using System.Collections;

public class PhysicsMaterialChanger : MonoBehaviour {

	public PhysicsMaterial2D bounce;
	
	void OnTriggerEnter2D(){
		bounce.bounciness = 2;
	}

	void OnTriggerExit2D(){
		bounce.bounciness = 0;
	}

}
