using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SlidingDoorBehaviour : MonoBehaviour
{
    public Animator slidingDoor;
	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other == CompareTag("Player"))
        {
            slidingDoor.SetBool("Door Open", true);
            slidingDoor.SetBool("Door Close", false);
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other == CompareTag("Player"))
        {
            slidingDoor.SetBool("Door Open", true);
            slidingDoor.SetBool("Door Close", false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other == CompareTag("Player"))
        {
            slidingDoor.SetBool("Door Open", false);
            slidingDoor.SetBool("Door Close", true);
        }
    }
}
