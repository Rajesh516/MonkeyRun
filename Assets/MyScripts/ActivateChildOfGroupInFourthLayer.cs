﻿using UnityEngine;
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
			}
		}
	}
}
