using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PhysicalEquipment : MonoBehaviour
{
	public string equipName = null;
	public Equipment equipment;

	// Start is called before the first frame update
	void Start()
	{
		equipName = gameObject.name;
		equipment = (Equipment)Enum.Parse(typeof(Equipment), equipName);
	}




	private void OnMouseOver()
	{
		if (Input.GetMouseButtonDown(0))
		{
			GameManager.inst.UpdateEquipmentAmount(this, +1);
		}
		else if (Input.GetMouseButtonDown(1))
		{
			GameManager.inst.UpdateEquipmentAmount(this, -1);
		}
	}

}
