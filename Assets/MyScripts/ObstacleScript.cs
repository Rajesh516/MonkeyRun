﻿using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {
	Rigidbody rigidBodyObstacle;
	// Use this for initialization
	void Start () {
		GetComponent<BoxCollider> ().isTrigger = true;
		rigidBodyObstacle = gameObject.GetComponent<Rigidbody> ();
		if(rigidBodyObstacle == null)
			rigidBodyObstacle = gameObject.AddComponent<Rigidbody> ();
		rigidBodyObstacle.isKinematic = true;
		rigidBodyObstacle.useGravity = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider colli)
	{
		if (colli.gameObject.tag.Equals ("Player")) {
			;//PlayerManager.Instance.ObstacleCollided(gameObject.collider);	
		}
		if (colli.gameObject.tag.Equals ("Weapon")) {
			Transform expParent = transform;
			Transform weaponTransform = colli.transform;
			if (expParent.name == "Torpedo")
			{
				//Notify torpedo manager
				expParent.transform.parent.gameObject.GetComponent<Torpedo>().TargetHit(true);
				LevelManager.Instance.TorpedoExplodedSetter();
			}
			//If the sub collided with something else
			else
			{

				//Find the particle child, and play it
				ParticleSystem explosion = expParent.FindChild("ExplosionParticle").gameObject.GetComponent("ParticleSystem") as ParticleSystem;
				explosion.Play();
				//Disable the object's renderer and collider
				expParent.renderer.enabled = false;
				expParent.collider.enabled = false;
			}
			weaponTransform.renderer.enabled = false;
			weaponTransform.collider.enabled = false;
		}
	}
}
