using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public string sceneToLoad;

	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown){
			SceneManager.LoadScene (sceneToLoad);
		}
	}
}
