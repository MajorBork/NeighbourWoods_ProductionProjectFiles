using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxBehaviour : MonoBehaviour
{
    public GameObject animal;
    public BoxCollider maxCollider;
	void Start () // Use this for initialization
    {
        maxCollider = GetComponent<BoxCollider>();
	}
	void Update () // Update is called once per frame
    {
		
	}
    public void TurnOnConversation()
    {
        maxCollider.isTrigger = true;
    }
    public void RunAway()
    {
        StartCoroutine(DelayedRunAway());
    }
    IEnumerator DelayedRunAway()
    {
        yield return new WaitForSeconds(5);
        animal.SetActive(false);
    }
}
