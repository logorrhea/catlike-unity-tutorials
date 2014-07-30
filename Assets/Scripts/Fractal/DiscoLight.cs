using UnityEngine;
using System.Collections;

public class DiscoLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float r = Random.Range (0.0f, 1.0f);	
		float g = Random.Range (0.0f, 1.0f);
		float b = Random.Range (0.0f, 1.0f);
		this.light.color = new Color(r, g, b);
	}
}
