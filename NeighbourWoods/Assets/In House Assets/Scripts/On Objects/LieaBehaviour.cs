using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LieaBehaviour : MonoBehaviour
{
    public GameObject Liea;
	void Start () // Use this for initialization
    {
		
	}
	void Update () // Update is called once per frame
    {
		
	}
    public void LieaRetreat()
    {
        Liea.SetActive(false);
    }
}
