using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBehaviour : MonoBehaviour
{
    public GameObject Bandit;
	void Start () // Use this for initialization
    {
		
	}
	void Update () // Update is called once per frame
    {
		
	}
    void BanditRetreat()
    {
        Bandit.SetActive(false);
    }
}
