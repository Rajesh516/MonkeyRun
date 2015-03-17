using UnityEngine;
using System.Collections;

public class ShootScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		PlayerManager.Instance.ShootWeapon();
	}
}
