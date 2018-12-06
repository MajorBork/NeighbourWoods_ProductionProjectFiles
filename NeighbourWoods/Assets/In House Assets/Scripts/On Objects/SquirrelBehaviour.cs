using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class SquirrelBehaviour : MonoBehaviour
{
    public GameObject squirrel;
    public 
	void Start () // Use this for initialization
    {
		
	}
	void Update () // Update is called once per frame
    {
		
	}
    void RunAwaySquirrel()
    {
        squirrel.SetActive(false);
    }
}
