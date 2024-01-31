using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightManager : MonoBehaviour
{
	[SerializeField] GameObject spotLight; 
	[Range(0.1f, 1f)]
	[SerializeField] float lerpAmount = 0.13f; 
	private Vector3 point;
	private Vector3 spotLightPoint;
	Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	protected void FixedUpdate()
	{
		RaycastHit hit; 
			
		if(Physics.Raycast( Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000f))
		{
			point = hit.point;
			spotLightPoint = point;

			if(hit.collider.gameObject.layer == LayerMask.NameToLayer("FoodItem")){
				spotLightPoint = hit.transform.position;
			}
		}

		target = Vector3.Lerp(target, spotLightPoint, lerpAmount);
		spotLight.transform.LookAt(target, -Vector3.up);
	}
	
}
