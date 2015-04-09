using UnityEngine;
using System.Collections;

public class PowerUpScript : MonoBehaviour 
{
	public bool isMagnet,isFastLegs,isSheild;
	// Use this for initialization
	void Start () 
	{
		GetComponent<BoxCollider> ().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter(Collider colli)
	{
		if (colli.tag.Equals ("Player")) {
			#if UNITY_5
			PlayerManager.Instance.PowerUpsCollected (gameObject.GetComponent<Collider>());
			#else
			PlayerManager.Instance.PowerUpsCollected (gameObject.collider);
			#endif

				}
		/*if (isMagnet)
						;
		if (isFastLegs)
			;
		if (isSheild)
			;*/
	}
}
