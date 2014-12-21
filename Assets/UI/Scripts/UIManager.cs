using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	public static UIManager Instance;

	public GameObject MainMenuScreen;
	public GameObject ShopScreen;
	public GameObject PauseScreen;
	public GameObject GameOverScreen;
	public GameObject InGameUIScreen;
	public GameObject LeaderBoardScreen;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}

	// Update is called once per frame
	public void ShowMainMenuScreen () {
		MainMenuScreen.SetActive (true);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
	}

	public void ShowShopScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (true);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
	}

	public void ShowPauseScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (true);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (false);
	}

	public void ShowResumeScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (true);
	}

	public void ShowGameOverScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (true);
		InGameUIScreen.SetActive (false);
	}

	public void ShowInGameUIScreen () {
		MainMenuScreen.SetActive (false);
		ShopScreen.SetActive (false);
		PauseScreen.SetActive (false);
		GameOverScreen.SetActive (false);
		InGameUIScreen.SetActive (true);
	}
}
