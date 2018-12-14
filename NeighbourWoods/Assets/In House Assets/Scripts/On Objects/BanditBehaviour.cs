using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BanditBehaviour : MonoBehaviour
{
    public GameObject animal;
    public GameObject animal1;
    public GameObject animal2;
    public GameObject animal3;
    public GameObject animal4;
    public GameObject animal5;
    public GameObject animal6;
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
        yield return new WaitForSeconds(6);
        animal.SetActive(false);
        animal1.SetActive(false);
        animal2.SetActive(false);
        animal3.SetActive(false);
        animal4.SetActive(false);
        animal5.SetActive(false);
        animal6.SetActive(false);
    }
}
