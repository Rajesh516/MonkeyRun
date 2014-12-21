using UnityEngine;
using System.Collections;

public class CoinsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<BoxCollider> ().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider colli)
	{
		if (colli.gameObject.tag.Equals ("Player")) {
			PlayerManager.Instance.CoinCollected(gameObject.collider);	
		}
	}
}
