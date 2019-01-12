using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncCharMovement : MonoBehaviour {

    // Use this for initialization
    public float abilitySpeed = 30;
    bool activeAbility = false;
    bool mirrormode = false;
    Vector3 mousePos;
    Vector2 dir;
    public GameObject sync1;
    public GameObject sync2;
    public float lerpPrescicion = 0.1f;
    Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (activeAbility)
        {
            // transform.position = Vector3.Lerp(transform.position, mousePos, abilitySpeed * Time.deltaTime);
            rb.MovePosition(rb.position + dir.normalized / abilitySpeed);

            if (mirrormode)
            {//todo
                sync1.transform.position = Vector3.Lerp(sync1.transform.position, new Vector3(rb.position.x, rb.position.y, 0) - sync1.GetComponent<BoxCollider2D>().bounds.size / 2, abilitySpeed * Time.fixedDeltaTime);
                sync2.transform.position = Vector3.Lerp(sync2.transform.position, new Vector3(rb.position.x, rb.position.y, 0) + sync2.GetComponent<BoxCollider2D>().bounds.size / 2, abilitySpeed * Time.fixedDeltaTime);
            }
            //todo
            Debug.Log(Mathf.Abs(dir.x - rb.position.x)< lerpPrescicion);
            Debug.Log(Mathf.Abs(dir.y - rb.position.y));
            if (Mathf.Abs(dir.x - rb.position.x) < lerpPrescicion && Mathf.Abs(dir.y + rb.position.y) < lerpPrescicion)
            {
                Debug.Log("REEE");
                activeAbility = false;
            }        
        }
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
          activeAbility = false;
    }

    public void UseAbility(Vector3 mousePosition, bool mirror)
    {
        mirrormode = mirror;
        mousePos = mousePosition;
        activeAbility = true;
        dir = mousePos;
        dir = dir - rb.position;
    }

    public bool GetActiveAbility()
    {
        return activeAbility;
    }
}
