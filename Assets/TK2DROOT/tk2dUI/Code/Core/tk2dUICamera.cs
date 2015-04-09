using UnityEngine;
using System.Collections;

[AddComponentMenu("2D Toolkit/UI/Core/tk2dUICamera")]
public class tk2dUICamera : MonoBehaviour {

	// This is multiplied with the cameras layermask
	[SerializeField]
	private LayerMask raycastLayerMask = -1;

	// This is used for backwards compatiblity only
	public void AssignRaycastLayerMask( LayerMask mask ) {
		raycastLayerMask = mask;
	}

	// The actual layermask, i.e. allowedMasks & layerMask
	public LayerMask FilteredMask {
		get {
			#if UNITY_5
			return raycastLayerMask & GetComponent<Camera>().cullingMask;
			#else
			return raycastLayerMask & camera.cullingMask;
			#endif

		}
	}

	public Camera HostCamera {
		get {
			#if UNITY_5
			return GetComponent<Camera>();
			#else
				return camera;
			#endif
			
		}
	}

	void OnEnable() {
		#if UNITY_5
		if (GetComponent<Camera>() == null) 
		#else
		if (camera == null) 
		#endif
		{
			Debug.LogError("tk2dUICamera should only be attached to a camera.");
			enabled = false;
			return;
		}

		tk2dUIManager.RegisterCamera( this );
	}

	void OnDisable() {
		tk2dUIManager.UnregisterCamera( this );
	}
}
