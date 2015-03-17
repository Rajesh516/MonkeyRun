using UnityEngine;
using System.Collections;

public class ObstacleScript : MonoBehaviour {
	Rigidbody rigidBodyObstacle;
	BoxCollider boxCollider;
	public bool flyingObstacle,isSkunk;
	bool startMovingAnimals;
	public Vector3 initialPos;
	void OnEnable(){
		if (flyingObstacle) {
			transform.localPosition = initialPos;	
			//startMovingAnimals = false;
		}
	}
	// Use this for initialization
	void Start () {
		boxCollider = GetComponent<BoxCollider> ();
		if(isSkunk)
			boxCollider.size = new Vector3 (1.0f, 1.0f, 22.8f);
		else
			boxCollider.size = new Vector3 (0.75f, 0.7f, 22.8f);
		boxCollider.isTrigger = true;
		rigidBodyObstacle = gameObject.GetComponent<Rigidbody> ();
		if(rigidBodyObstacle == null)
			rigidBodyObstacle = gameObject.AddComponent<Rigidbody> ();
		//if (flyingObstacle == false) {
			rigidBodyObstacle.isKinematic = true;	
			rigidBodyObstacle.useGravity = false;
		//}
	}
	
	// Update is called once per frame
	void Update () {
		if (flyingObstacle) {
			if(startMovingAnimals)
			transform.localPosition  += new Vector3(5.0f*Time.deltaTime,0,0); 
		}
	
	}

	void OnTriggerEnter(Collider colli)
	{
		if (colli.gameObject.tag.Equals ("Player")) {
			if(!IsTesting.instance.isTesting)
			{
				if(flyingObstacle)
				{
					foreach (Transform childTransform in transform)
					{
						if(childTransform.GetComponent<MovingAnimals>()!=null)
						{
							print ("FlyingDead");
							childTransform.GetComponent<MeshRenderer>().enabled = false;
						}
					}
				}
				PlayerManager.Instance.ObstacleCollided(gameObject.collider);	
			}
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
				if(flyingObstacle)
				{
					foreach (Transform childTransform in transform)
					{
						if(childTransform.GetComponent<MovingAnimals>()!=null)
						{
							print ("FlyingDead");
							childTransform.GetComponent<MeshRenderer>().enabled = false;
						}
					}
				}
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

	public void AnimalMovementSetter(bool temp)
	{
		startMovingAnimals = temp;
		foreach (Transform childTransform in transform)
		{
			if(childTransform.GetComponent<MovingAnimals>()!=null)
			{
				print ("FlyingDead");
				childTransform.GetComponent<MeshRenderer>().enabled = true;
			}
		}
	}
}
