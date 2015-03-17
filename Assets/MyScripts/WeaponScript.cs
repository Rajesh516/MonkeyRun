using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	int count = 0;
	// Use this for initialization
	void Start () {
		count = 0;
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.left * Time.deltaTime * 30);
		if (!renderer.isVisible)
						count++;
		if(count > 1)
			Destroy (gameObject);
	}
}
