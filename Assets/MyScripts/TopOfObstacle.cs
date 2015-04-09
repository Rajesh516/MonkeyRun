using UnityEngine;
using System.Collections;

public class TopOfObstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider colli){
		if (colli.gameObject.tag.Equals ("Player")) {
			transform.parent.GetComponent<ObstacleScript>().PlayerCollidedWithTop();
			print("Calling------------");
		}
	}
}
