using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedStart : MonoBehaviour
{
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;
    public GameObject Star4;
    public GameObject Star5;



    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnDelay", 1);
    }

    private void SpawnDelay()
    {
        Star1.SetActive(false);
        Star2.SetActive(true);
    }

}
