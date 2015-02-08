using UnityEngine;
using System.Collections;

public class IsTesting : MonoBehaviour {
	public static IsTesting instance; 
	// Use this for initialization
	public bool isTesting;
	void Awake()
	{
		instance = this;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
