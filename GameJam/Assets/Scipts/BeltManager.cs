using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
 
public class BeltManager : MonoBehaviour
{
	public static BeltManager inst;
	public ConveyerItems[] items; 
	public float speed;
	public float targetSpeed;
	[SerializeField] float interp;
	
	// Awake is called when the script instance is being loaded.
	protected void Awake()
	{
		inst = this;
	}
	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	protected void Start()
	{
		
	}
	public void SetSpeedColective(float speed, float interps){
		
		targetSpeed = speed;
		interp = interps;
	}
	
	// Update is called every frame, if the MonoBehaviour is enabled.
	protected void Update()
	{
		
		speed = Mathf.Lerp(speed, targetSpeed, interp);
			
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
		float startTime = Time.time;
		List<GameObject> ordered = OrderObjectsByPos().ToList();
		
		GameObject previous = ordered[0];
		for(int i = 1; i < ordered.Count; i++){
		
			
			// first assume that previous is in correct spot and actually the current is wrong
			// move the current so that it is + 20 away from previous
			float x = -(ordered[i].transform.position.x - previous.transform.position.x);
			
				
				
			if(Mathf.Abs(x) >= 20.1 || Mathf.Abs(x) <= 19.8){
				ordered[i].transform.position = new Vector3(( previous.transform.position.x + 20f ), ordered[i].transform.position.y, ordered[i].transform.position.z);
			}
				
				
					
			previous = ordered[i];	
		}
			
	}
}
