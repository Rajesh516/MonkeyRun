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
		#if UNITY_5
		if (!GetComponent<Renderer>().isVisible)
			count++;
		#else
		if (!renderer.isVisible)
			count++;
		#endif

		if(count > 1)
			Destroy (gameObject);
	}
}
