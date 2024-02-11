using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
 
public class BeltManager : MonoBehaviour
{
	public ConveyerItems[] items; 
	
	public void SetSpeedColective(float speed){
		
		foreach (ConveyerItems i in items){
			i.SetSpeed( speed);
		}
		CheckSpacing();
	}
	public GameObject[] OrderObjectsByPos(){
		List<GameObject> s = new List<GameObject>();
		foreach(ConveyerItems y in items){
			if(y != null){
				s.Add(y.gameObject);
			}
		}
		GameObject[] z = s.ToArray();
		z = z.OrderByDescending(x => x.transform.position.x).ToArray();
		
		
		return z;
	}
	public void CheckSpacing(){
		List<GameObject> ordered = OrderObjectsByPos().ToList();
		GameObject previous = ordered[0];
		for(int i = 1; i < ordered.Count; i++){
			if( !InRange( System.Math.Round(ordered[i].transform.position.x - previous.transform.position.x , 1), 19f, 21f )){
				// first assume that previous is in correct spot and actually the current is wrong
				// move the current so that it is + 20 away from previous
				float x = -(ordered[i].transform.position.x - previous.transform.position.x);
				float moveDist = x + 20f;
				ordered[i].transform.position = new Vector3(-(moveDist - ordered[i].transform.position.x), ordered[i].transform.position.y, ordered[i].transform.position.z);
				
			}
		}
	}
	
	public bool InRange(double x, float lower, float higher){
		if(x >=lower && x <= higher)
			return true;
		return false;
	}
}
