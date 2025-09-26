using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    Rigidbody2D rb;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;
    //Referencias 
    PlayerStats player;
    void Start()
    {
        player=GetComponent<PlayerStats>();
        rb=GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); 
    }

    // Update is called once per frame
    void Update()
    {
        InputManament();
        
    }
    void FixedUpdate(){
        Move();
    }
    void InputManament(){
        float moveX=Input.GetAxis("Horizontal");
        float moveY=Input.GetAxis("Vertical");
        moveDir	= new Vector2(moveX, moveY).normalized;
        if(moveDir.x !=0){
            lastHorizontalVector=moveDir.x;
                       lastMovedVector= new Vector2(lastHorizontalVector, 0f); //ultimo x
        }
        if(moveDir.y !=0){
            lastVerticalVector=moveDir.y;
            lastMovedVector= new Vector2(0f, lastVerticalVector);
        }
        if(moveDir.x !=0 && moveDir.y !=0){
            lastMovedVector= new Vector2(lastHorizontalVector, lastVerticalVector);
        }
    }
    void Move(){
        rb.velocity = new Vector2(moveDir.x * player.currentMoveSpeed, moveDir.y * player.currentMoveSpeed);
    }
}
