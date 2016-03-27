using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimateCanvasSprite : MonoBehaviour {

	private SpriteRenderer renderer;
	private Image image;

	// Use this for initialization
	void Start () {
		renderer = GetComponent<SpriteRenderer> ();
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		image.sprite = renderer.sprite;
	}
}
