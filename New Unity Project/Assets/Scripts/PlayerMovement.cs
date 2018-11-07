using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 30f;
    private int battleMode = 0;
    public GameObject syncChar1;
    public GameObject syncChar2;
    public GameObject player;
    BoxCollider2D box;
    bool mirrorMode = false;
    public float maxDistance;

	// Use this for initialization
	void Start () {
        syncChar2.SetActive(false);
        syncChar1.SetActive(true);
        box = GetComponent<BoxCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 moveDistance = new Vector3(0,0,0); ;
        Vector3 syncMoveDistane = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(0))
        {
            if (mirrorMode)
            {

            }
        }

        if (Input.GetKey("w"))
        {             
                Vector3 upMove = Vector3.up * speed * Time.deltaTime;
            if (mirrorMode)
            {
                float distance = syncChar1.transform.position.y - transform.position.y;
                Debug.Log(distance);
                if (distance < maxDistance)
                {
                    syncMoveDistane = syncMoveDistane + upMove;
                }
                else
                {
                    moveDistance = moveDistance + upMove;
                }
            }
            else
            {
                moveDistance = moveDistance + upMove;
            }
        }
        if (Input.GetKey("s"))
        {
            Vector3 downMove = Vector3.down * speed * Time.deltaTime;
            if (mirrorMode)
            {
                float distance = syncChar1.transform.position.y - transform.position.y;
                if (distance > -maxDistance)
                {
                    syncMoveDistane = syncMoveDistane + downMove;
                }
                else
                {
                    moveDistance = moveDistance + downMove;
                }
            }
            else
            {
                moveDistance = moveDistance + downMove;
            }
        }
        if (Input.GetKey("d"))
        {
            Vector3 rightMove = Vector3.right * speed * Time.deltaTime;
            if (mirrorMode)
                {
                float distance = syncChar1.transform.position.x - transform.position.x;
                if (distance < maxDistance)
                {
                    syncMoveDistane = syncMoveDistane + rightMove;
                }
                else
                {
                    moveDistance = moveDistance + rightMove;
                }
            }
                else
                {
                    moveDistance = moveDistance + rightMove;
            }
        }
        if (Input.GetKey("a"))
        {
            Vector3 leftMove = Vector3.left * speed * Time.deltaTime;
            if (mirrorMode)
            {
                float distance = syncChar1.transform.position.x - transform.position.x;
                if (distance > -maxDistance)
                {
                    syncMoveDistane = syncMoveDistane + leftMove;
                }
                else
                {
                    moveDistance = moveDistance + leftMove;
                }
            }
            else
            {
                moveDistance = moveDistance + leftMove;
            }
        }


            syncChar1.transform.Translate(syncMoveDistane, Space.World);
            syncChar2.transform.Translate(syncMoveDistane * -1, Space.World);
            transform.Translate(moveDistance, Space.World);




        if (Input.GetKeyDown(KeyCode.Space)){
            battleMode++;
            if (battleMode > 3)
            {
                battleMode = 0;
            }
            switch (battleMode)
            {
                case 0:
                    syncChar1.SetActive(true);
                    syncChar2.SetActive(false);
                    syncChar1.transform.position = transform.position;
                    syncChar2.transform.position = syncChar1.transform.position;
                    mirrorMode = false;
                    break;
                case 1:
                    syncChar1.SetActive(false);
                    syncChar2.SetActive(true);
                    break;
                case 2:
                    syncChar1.SetActive(true);
                    syncChar2.transform.position = new Vector2(syncChar1.transform.position.x + syncChar1.GetComponentInChildren<BoxCollider2D>().bounds.size.x, syncChar1.transform.position.y);
                    break;
                case 3:
                    mirrorMode = true;
                    break;
            }
        }
    }
}
