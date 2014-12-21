using UnityEngine;
using System.Collections;

public class MonkeyAnimationScript : MonoBehaviour {
	public Texture2D[] monkeyRunAnimation = new Texture2D[7];
	public Texture2D[] monkeyDieAnimation = new Texture2D[7];
	public Texture2D[] monkeyJumpAnimation = new Texture2D[7];
	public Texture2D[] monkeyBananaThrowAnimation = new Texture2D[7];
	int monkeyPresentState,monkeyRunCount,monkeyJumpCount,monkeyDieCount,monkeyBananaThrowCount;
	// Use this for initialization
	void Start () 
	{
		monkeyBananaThrowCount = monkeyRunCount = monkeyJumpCount = monkeyDieCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		}

	public void AnimationStateSetter(int temp)
	{
		monkeyPresentState = temp;
		if (monkeyPresentState == 0) 
		{
			if(monkeyRunCount == 0)
			{
				StartCoroutine ("MonkeyRunAnimation");
				StopCoroutine("MonkeyJumpAnimation");	
				StopCoroutine("MonkeyDieAnimation");
				StopCoroutine("MonkeyBananaThrowAnimation");
			}
		}
		if (monkeyPresentState == 1) 
		{
			StartCoroutine ("MonkeyJumpAnimation");
			StopCoroutine("MonkeyRunAnimation");	
			StopCoroutine("MonkeyDieAnimation");
			StopCoroutine("MonkeyBananaThrowAnimation");
		}
		if (monkeyPresentState == 2) 
		{
			if(monkeyDieCount == 0)
			{
				StartCoroutine ("MonkeyDieAnimation");
				StopCoroutine("MonkeyJumpAnimation");	
				StopCoroutine("MonkeyRunAnimation");
				StopCoroutine("MonkeyBananaThrowAnimation");
			}
		}

		if (monkeyPresentState == 3) 
		{
			monkeyBananaThrowCount = 0;
			StopCoroutine("MonkeyBananaThrowAnimation");
			StartCoroutine ("MonkeyBananaThrowAnimation");
			StopCoroutine("MonkeyJumpAnimation");	
			StopCoroutine("MonkeyRunAnimation");	
			StopCoroutine("MonkeyDieAnimation");
		}
	}

	IEnumerator MonkeyRunAnimation()
	{
		while(monkeyPresentState == 0)
		{
			renderer.material.mainTexture = monkeyRunAnimation[monkeyRunCount];
			monkeyBananaThrowCount = monkeyDieCount = monkeyJumpCount = 0;
			monkeyRunCount++;
		yield return new WaitForSeconds(0.05f);
		if(monkeyRunCount == monkeyRunAnimation.GetLength(0))
			monkeyRunCount = 0;
		}
	}

	IEnumerator MonkeyBananaThrowAnimation()
	{
		while(monkeyPresentState == 3)
		{
			renderer.material.mainTexture = monkeyBananaThrowAnimation[monkeyBananaThrowCount];
			monkeyRunCount = monkeyDieCount = monkeyJumpCount = 0;
			monkeyBananaThrowCount++;
			yield return new WaitForSeconds(0.05f);
			if(monkeyBananaThrowCount == monkeyRunAnimation.GetLength(0))
			{
				monkeyBananaThrowCount = 0;
				monkeyPresentState = 0;	
			}
		}
	}

	IEnumerator MonkeyJumpAnimation()
	{
		while(monkeyPresentState == 1)
		{
			yield return new WaitForSeconds(0.07f);
			renderer.material.mainTexture = monkeyJumpAnimation[monkeyJumpCount];
			monkeyBananaThrowCount = monkeyDieCount = monkeyRunCount = 0;
			monkeyJumpCount++;
			if(monkeyJumpCount == monkeyJumpAnimation.GetLength(0))
				break;
		}
	}

	IEnumerator MonkeyDieAnimation()
	{
		while(monkeyPresentState == 2)
		{
			renderer.material.mainTexture = monkeyDieAnimation[monkeyDieCount];
			monkeyBananaThrowCount = monkeyJumpCount = monkeyRunCount = 0;
			monkeyDieCount++;
			yield return new WaitForSeconds(0.2f);
			if(monkeyDieCount >= monkeyDieAnimation.GetLength(0))
				break;
		}
		renderer.material.mainTexture = monkeyDieAnimation[monkeyDieAnimation.GetLength(0)-1];
	}

	public int MonkeyPresentStateGetter()
	{
		return monkeyPresentState;
	}
}
