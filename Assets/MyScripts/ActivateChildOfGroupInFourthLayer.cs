using UnityEngine;
using System.Collections;

public class ActivateChildOfGroupInFourthLayer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ActivateChildren()
	{
		foreach (Transform childTransform in transform) 
		{
			if(childTransform.tag.Equals("Obstacles"))
			{
				childTransform.GetComponent<MeshRenderer>().enabled = true;
				childTransform.GetComponent<BoxCollider>().enabled = true;
				if(childTransform.GetComponent<ObstacleScript>() != null)
				if(childTransform.GetComponent<ObstacleScript>().flyingObstacle)
				{
					childTransform.GetComponent<ObstacleScript>().AnimalMovementSetter(true);
				}
			}
		}
	}

	public void DeActivateChildren()
	{
		foreach (Transform childTransform in transform) 
		{
			if(childTransform.tag.Equals("Obstacles"))
			{
				childTransform.GetComponent<MeshRenderer>().enabled = false;
				childTransform.GetComponent<BoxCollider>().enabled = false;
				if(childTransform.GetComponent<ObstacleScript>() != null)
				if(childTransform.GetComponent<ObstacleScript>().flyingObstacle)
				{
					childTransform.GetComponent<ObstacleScript>().AnimalMovementSetter(false);
				}
			}
		}
	}
}
