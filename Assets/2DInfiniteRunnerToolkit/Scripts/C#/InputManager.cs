using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour 
{
	public bool useTouch = false;				//Use touch based controls
	
	public LayerMask mask = -1;					//Set input layer mask
	
	Ray ray;									//The hit ray
	RaycastHit hit;								//The hit raycast
	
	Transform button;							//The triggered button

	private Vector2 touchStartPosition;
	public float swipeSensitivty = 2;
	private bool acceptInput; // ensure that only one action is performed per touch gesture
	public Vector2 swipeDistance = new Vector2(40, 40);

	void Start(){
		acceptInput = true;
	}
	//Called at every frame
	void Update () 
	{
		if (useTouch)
						GetTouches();
		else {
						GetClicks ();
						//GetTouchInput ();
				}
	}

	//If playing with mouse
	void GetClicks()
	{
		//If we pressed the mouse
		if(Input.GetMouseButtonDown(0))
		{
			//Cast a ray
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			//If the ray hit something in the set layer
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
			{
				//Register it, and send it to the GUI manager
				button = hit.transform;
				GUIManager.Instance.ButtonDown(button);
			}
			//If the ray didn't hit a GUI object
			else
			{
				//Set the button to null, and move the sub up
				button = null;
				Vector3 vec;
				vec = Camera.main.ScreenToViewportPoint(Input.mousePosition);
				/*if(vec.x < 0.5)
				{
					PlayerManager.Instance.MoveUp();
				}
				else
					PlayerManager.Instance.ShootWeapon();*/
			}
		}
		//If the click was released
		else if (Input.GetMouseButtonUp(0))
		{
			//If there is no button registered previousely
			if (button == null)
				//Move the sub down
                PlayerManager.Instance.MoveDown();
			//If there is a button registered
			else
				//Send it to the GUI manager
                GUIManager.Instance.ButtonUp(button);
		}
		
		//Used in testing to reset the status
		/*if (Input.GetKey(KeyCode.P))
		{
			SaveManager.CreateData();
			missionManager.ResetDataString();
		}*/
	}
	//If playing with touch screen
	void GetTouches()
	{
		//Loop through the touches
		foreach (Touch touch in Input.touches) 
		{
			//If a touch has happened
            if (touch.phase == TouchPhase.Began && touch.phase != TouchPhase.Canceled)
			{
				//Cast a ray
				ray = Camera.main.ScreenPointToRay(touch.position);
				
				//If the ray hit something in the set layer
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
				{
					//Register it, and send it to the GUI manager
					button = hit.transform;
                    GUIManager.Instance.ButtonDown(button);
				}
				//If the ray didn't hit a GUI object
				else
				{
					//Set the button to null, and move the sub up
					button = null;
					Vector3 vec;
					vec = Camera.main.ScreenToViewportPoint(touch.position);
					/*if(vec.x < 0.5)
                    	PlayerManager.Instance.MoveUp();
					else
						PlayerManager.Instance.ShootWeapon();*/
				}
			}
			//If a touch has ended
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
				//If there is no button registered previousely
				if (button == null)
					//Move the sub down
                    PlayerManager.Instance.MoveDown();
				//If there is a button registered
				else
					//Send it to the GUI manager
                    GUIManager.Instance.ButtonUp(button);
			}
		}
	}

	void GetTouches1()
	{
		//Loop through the touches
		//foreach (Touch touch in Input.touches) 
		Touch touch = Input.GetTouch(0);
		{
			//If a touch has happened
			if (touch.phase == TouchPhase.Began)// && touch.phase != TouchPhase.Canceled)
			{
				touchStartPosition = touch.position;
			}
			else if (touch.phase == TouchPhase.Stationary) 
			{
				//Cast a ray
				ray = Camera.main.ScreenPointToRay(touch.position);
				
				//If the ray hit something in the set layer
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask))
				{
					//Register it, and send it to the GUI manager
					button = hit.transform;
					GUIManager.Instance.ButtonDown(button);
				}
				//If the ray didn't hit a GUI object
				else
				{
					//Set the button to null, and move the sub up
					button = null;
					Vector3 vec;
					vec = Camera.main.ScreenToViewportPoint(touch.position);
					//if(vec.x < 0.5)
						PlayerManager.Instance.MoveUp();
					//else
					//	PlayerManager.Instance.ShootWeapon();
				}
			}
			else if (touch.phase == TouchPhase.Moved)
			{
				Vector2 diff = touch.position - touchStartPosition;
				if(diff.x >  swipeDistance.x)
				{
					touchStartPosition = touch.position ;
					PlayerManager.Instance.ShootWeapon();
				}
			}
			//If a touch has ended
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
				//If there is no button registered previousely
				if (button == null)
					//Move the sub down
					PlayerManager.Instance.MoveDown();
				//If there is a button registered
				else
					//Send it to the GUI manager
					GUIManager.Instance.ButtonUp(button);
			}
		}
	}

	/*void GetTouchInput(){
		//#if !UNITY_EDITOR && (UNITY_IPHONE || UNITY_ANDROID || UNITY_BLACKBERRY || UNITY_WP8)
		if (Input.touchCount == 1) {
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began) {
				touchStartPosition = touch.position;
			} else if (touch.phase == TouchPhase.Moved && acceptInput) {
				Vector2 diff = touch.position - touchStartPosition;
				if (diff.x == 0f)
					diff.x = 1f; // avoid divide by zero
				float verticalPercent = Mathf.Abs(diff.y / diff.x);
				
				if (verticalPercent > swipeSensitivty && Mathf.Abs(diff.y) > swipeDistance.y) {
					;
					/*if(!playerController.IsZipLining())
					{
						if (diff.y > 0) {
							playerController.jump(false);
							acceptInput = false;
							//acceptInput = true;
						} else if (diff.y < 0) {
							playerController.slide();
							acceptInput = false;
							//acceptInput = true;
						}
					}
					acceptInput = false;
					touchStartPosition = touch.position;
				} else if (verticalPercent < (1 / swipeSensitivty) && Mathf.Abs(diff.x) > swipeDistance.x) {
					// turn if above a turn, otherwise change slots
					if(diff.x >  0)
						PlayerManager.Instance.ShootWeapon();
					/*if(!playerController.IsZipLining())
					{
						if (swipeToChangeSlots) {
							if (playerController.abovePlatform(true)) {
								if(!powerUpManager.isPowerUpActive(PowerUpTypes.Invincibility) && !powerUpManager.isPowerUpActive(PowerUpTypes.BoltPack) && !powerUpManager.isPowerUpActive(PowerUpTypes.SuperPogoStick) && !powerUpManager.isPowerUpActive(PowerUpTypes.SpeedIncrease)  && !playerController.autoTurn && !hasTurned)
								{
									hasreceivedDirection = diff.x > 0 ? false : true;
									if(hasEnteredInTurnTrigger)
									{
										print ("------Not Entered In Coroutine");
										StopCoroutine("EnteredInTurnTrigger");
										playerController.turn(hasreceivedDirection, true);
									}
									else
										StartCoroutine("EnteredInTurnTrigger");
									hasTurned = true;
								}
							} else {
								playerController.changeSlots(diff.x > 0 ? true : false);
								hasTurned = false;
							}
						} else {
							if(!powerUpManager.isPowerUpActive(PowerUpTypes.Invincibility) && !powerUpManager.isPowerUpActive(PowerUpTypes.BoltPack) && !powerUpManager.isPowerUpActive(PowerUpTypes.SpeedIncrease)  && !playerController.autoTurn)
								playerController.turn(diff.x > 0 ? false : true, true);
						}
					}
					acceptInput = false;
					//acceptInput = true;
				}
			} else if (touch.phase == TouchPhase.Stationary) {
				acceptInput = true;
			} else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) {
				if ((touch.position - touchStartPosition).sqrMagnitude < 100 && acceptInput) {
					PlayerManager.Instance.MoveUp();//playerController.attack();
				}
				acceptInput = true;
			}
		}
		/*for (var i = 0; i < Input.touchCount; ++i)
		{
			if (Input.GetTouch(i).phase == TouchPhase.Began) 
			{
				if(Input.GetTouch(i).tapCount >= 2)
				{
					if(!powerUpManager.isPowerUpActive(PowerUpTypes.BoltPack))
						if(!powerUpManager.isPowerUpActive(PowerUpTypes.SuperPogoStick))
							if(!powerUpManager.isPowerUpActive(PowerUpTypes.SpeedIncrease))
						{
							if(DataManager.instance.getPowerUpLevel(PowerUpTypes.Invincibility) > 0)
							{
								ActivatePowerUp(PowerUpTypes.Invincibility);
								//GameManager.instance.decreaseSingleUse(PowerUpTypes.Invincibility);
							}
						}
				}
			}
		}        
		
		if (!swipeToChangeSlots)
			checkSlotPosition(Input.acceleration.x);
		//#endif
	}*/
	
}
