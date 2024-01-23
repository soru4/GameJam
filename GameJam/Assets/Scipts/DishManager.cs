using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public AnimationCurve bellCurve;
    // Start is called before the first frame update
    void Start()
    {
        Dish.bellCurve = bellCurve;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
