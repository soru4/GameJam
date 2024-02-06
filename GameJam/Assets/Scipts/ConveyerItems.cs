using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerItems : MonoBehaviour
{
	public Vector3 startPosition;
	public Vector3 endPosition;
	public int distanceModify = 20;
	// Start is called before the first frame update
	void Start()
	{
		startPosition = transform.position;
		endPosition = startPosition;
		endPosition.x += distanceModify;
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.x >= endPosition.x)
		{
			transform.position = startPosition;
		}

		transform.position += new Vector3(distanceModify * RoundManager.inst.beltSpeed * Time.deltaTime, 0, 0);
	}
}
