using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornBehaviour : MonoBehaviour
{
    public GameObject acorn;
	void Start () // Use this for initialization
    {
		
	}
	void Update () // Update is called once per frame
    {
		
	}
    public void OnTriggerEnter(Collider other)
    {
        acorn.SetActive(false);
    }
}
