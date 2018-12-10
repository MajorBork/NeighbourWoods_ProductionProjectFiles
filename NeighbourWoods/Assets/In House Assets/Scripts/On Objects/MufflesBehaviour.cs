using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MufflesBehaviour : MonoBehaviour
{
    public GameObject Muffle;
	void Start () // Use this for initialization
    {

    }
	void Update () // Update is called once per frame
    {

    }
    public void MufflesRetreat()
    {
        Muffle.SetActive(false);
    }
}
