using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 30f;
    private int battleMode = 0;
    public GameObject syncChar1;
    public GameObject syncChar2;
    public GameObject player;
    bool mirrorMode = false;
    public float maxDistance;
    SyncCharMovement sync;
    Rigidbody2D rb;
    Vector2 moveDistance;
    public CinemachineVirtualCamera vcam;

    // Use this for initialization
    void Start () {
        syncChar2.SetActive(false);
        syncChar1.SetActive(true);
        sync = GetComponent<SyncCharMovement>();
        rb = GetComponent<Rigidbody2D>();
        Vector2 moveDistance = new Vector2(0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        moveDistance = new Vector2(0, 0);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.forward, transform.position);
            float dist = 0;
            if(plane.Raycast(ray, out dist))
            {
                sync.UseAbility(ray.GetPoint(dist), mirrorMode);
            }
        } 

        if (Input.GetKey("w"))
        {             
                Vector2 upMove = Vector2.up * speed * Time.deltaTime;
                moveDistance = moveDistance + upMove;
        }
        if (Input.GetKey("s"))
        {
            Vector2 downMove = Vector2.down * speed * Time.deltaTime;
            moveDistance = moveDistance + downMove;            
        }
        if (Input.GetKey("d"))
        {
            Vector2 rightMove = Vector2.right * speed * Time.deltaTime;
                moveDistance = moveDistance + rightMove;
        }
        if (Input.GetKey("a"))
        {
            Vector2 leftMove = Vector2.left * speed * Time.deltaTime;    
                moveDistance = moveDistance + leftMove;             
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            battleMode++;
            if (battleMode > 3)
            {
                battleMode = 0;
            }
            switch (battleMode)
            {
                case 0:
                    syncChar1.transform.position = new Vector2(syncChar1.transform.position.x + syncChar1.GetComponentInChildren<BoxCollider2D>().bounds.size.x / 2, syncChar1.transform.position.y);
                    syncChar2.transform.position = syncChar1.transform.position;                    
                    syncChar1.SetActive(true);
                    syncChar2.SetActive(false);
                    mirrorMode = false;
                    break;
                case 1:
                    syncChar1.SetActive(false);
                    syncChar2.SetActive(true);
                    break;
                case 2:
                    syncChar1.SetActive(true);               
                    syncChar2.transform.position = new Vector2(syncChar1.transform.position.x + syncChar1.GetComponentInChildren<BoxCollider2D>().bounds.size.x/2, syncChar1.transform.position.y);
                    syncChar1.transform.position = new Vector2(syncChar1.transform.position.x - syncChar1.GetComponentInChildren<BoxCollider2D>().bounds.size.x / 2, syncChar1.transform.position.y);
                    break;
                case 3:
                    mirrorMode = true;
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!sync.GetActiveAbility())
        {
            if (!mirrorMode)
            {
                rb.MovePosition(rb.position + moveDistance);
            }
            else
            {
                //todo set rb
                float distanceX = syncChar1.transform.position.x - rb.position.x;
                float distanceY = syncChar1.transform.position.y - rb.position.y;
                distanceX = Math.Abs(distanceX);
                distanceY = Math.Abs(distanceY);             
                if (distanceX < maxDistance && distanceY < maxDistance)
                {
                    syncChar1.transform.Translate(moveDistance);
                    syncChar2.transform.Translate(moveDistance * -1f);
                }
                else
                {
                    //todo
                    rb.MovePosition(rb.position+moveDistance);
                    if (distanceX > maxDistance)
                    {
                        moveDistance.x = 0;
                    }
                    else
                    {
                        moveDistance.y = 0;
                    }
                    syncChar2.transform.Translate(moveDistance);
                }        
            }
        }
    }

    public bool GetMirrorMode()
    {
        return mirrorMode;
    }
}
