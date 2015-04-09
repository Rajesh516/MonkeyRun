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
			#if UNITY_5
			GetComponent<Renderer>().material.mainTexture = monkeyRunAnimation[monkeyRunCount];
			#else
			renderer.material.mainTexture = monkeyRunAnimation[monkeyRunCount];
			#endif

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
			#if UNITY_5
			GetComponent<Renderer>().material.mainTexture = monkeyBananaThrowAnimation[monkeyBananaThrowCount];
			#else
			renderer.material.mainTexture = monkeyBananaThrowAnimation[monkeyBananaThrowCount];
			#endif

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
			#if UNITY_5
			GetComponent<Renderer>().material.mainTexture = monkeyJumpAnimation[monkeyJumpCount];
			#else
			renderer.material.mainTexture = monkeyJumpAnimation[monkeyJumpCount];
			#endif

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
			#if UNITY_5
			GetComponent<Renderer>().material.mainTexture = monkeyDieAnimation[monkeyDieCount];
			#else
			renderer.material.mainTexture = monkeyDieAnimation[monkeyDieCount];
			#endif

			monkeyBananaThrowCount = monkeyJumpCount = monkeyRunCount = 0;
			monkeyDieCount++;
			yield return new WaitForSeconds(0.2f);
			if(monkeyDieCount >= monkeyDieAnimation.GetLength(0))
				break;
		}
		#if UNITY_5
		GetComponent<Renderer>().material.mainTexture = monkeyDieAnimation[monkeyDieAnimation.GetLength(0)-1];
		#else
		renderer.material.mainTexture = monkeyDieAnimation[monkeyDieAnimation.GetLength(0)-1];
		#endif

	}

	public int MonkeyPresentStateGetter()
	{
		return monkeyPresentState;
	}
}
