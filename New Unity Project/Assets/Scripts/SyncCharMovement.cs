using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCharMovement : MonoBehaviour {

    // Use this for initialization
 
    private bool mode = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeMode()
    {
        mode = !mode;
    }
}
