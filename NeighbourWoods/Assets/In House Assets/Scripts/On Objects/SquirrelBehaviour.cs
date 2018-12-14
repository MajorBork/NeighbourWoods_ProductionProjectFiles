using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class SquirrelBehaviour : MonoBehaviour
{
    public GameObject animal;
    public 
	void Start () // Use this for initialization
    {
		
	}
	void Update () // Update is called once per frame
    {
		
	}
    void RunAway()
    {
        StartCoroutine(DelayedRunAway());
    }
    IEnumerator DelayedRunAway()
    {
        yield return new WaitForSeconds(5);
        animal.SetActive(false);
    }
}
