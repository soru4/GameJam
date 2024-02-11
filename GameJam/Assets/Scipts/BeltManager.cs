using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
 
public class BeltManager : MonoBehaviour
{
	public ConveyerItems[] items; 
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		
	}
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
		z = z.OrderBy(x => x.transform.position.x).ToArray();
		
		
		return z;
	}
	public void CheckSpacing(){
		List<GameObject> ordered = OrderObjectsByPos().ToList();
		
		GameObject previous = ordered[0];
		for(int i = 1; i < ordered.Count; i++){
		
			
				// first assume that previous is in correct spot and actually the current is wrong
				// move the current so that it is + 20 away from previous
				float x = -(ordered[i].transform.position.x - previous.transform.position.x);
			
				
				
				if(Mathf.Abs(x) >= 20.7 || Mathf.Abs(x) <= 18.9){
					ordered[i].transform.position = new Vector3(( previous.transform.position.x + 20f), ordered[i].transform.position.y, ordered[i].transform.position.z);
				}
				
				
					
			previous = ordered[i];	
			}
			
		}
	}

