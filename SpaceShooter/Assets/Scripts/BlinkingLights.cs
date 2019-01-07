using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlinkingLights : MonoBehaviour
{
    public Material LightsON;
    public Material LightsOFF;
    public float offsetTime;
    public List<Transform> theLightObjects;
    
	void Start ()
	{
		theLightObjects = new List<Transform>();
	    foreach (Transform child in transform)
	    {
	        theLightObjects.Add(child);
	    }

	    theLightObjects = theLightObjects.OrderBy(x => x.name).ToList();

	    StartCoroutine(Blink());
	}

    IEnumerator Blink()
    {
        while (true)
        {
            foreach (var t in theLightObjects)
            {
                t.GetComponent<Renderer>().material = LightsON;
                yield return new WaitForSeconds(offsetTime);
                t.GetComponent<Renderer>().material = LightsOFF;
            }
        }
    }
}
