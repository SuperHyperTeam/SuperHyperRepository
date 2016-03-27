using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {
	private static DontDestroy instance = null;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);    

		DontDestroyOnLoad(gameObject);
	}
}
