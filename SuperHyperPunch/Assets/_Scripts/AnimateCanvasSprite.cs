using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimateCanvasSprite : MonoBehaviour {

	private SpriteRenderer sprite;
	private Image image;

	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer> ();
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		image.sprite = sprite.sprite;
	}
}
