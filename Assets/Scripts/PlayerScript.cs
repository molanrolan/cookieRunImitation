using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool isGround = true, isAxisUp = false, isAxisDown = false;
    public float speed = 6f,jumpForce = 10f;
    private Vector2 tempVelocity=new Vector2(0f,0f);
    private string jumpAnimation = "isJump",walkAnimation="isWalk";
    private Vector3 tempPos;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        
        PauseManager.instance.onPaused();
    }
    private void OnEnable() {
        PauseManager.instance.onGameStateChanged+=onGameStateChanged;   //unsubscride is at PauseManager
    }

    // Update is called once per frame
    void Update()
    {
        //suspend character if isPaused
        if(PauseManager.instance.isPaused){
            transform.position=tempPos;
            if(Input.GetButtonDown("Jump"))PauseManager.instance.onPaused();
            return;}

        //move function
        transform.localScale = new Vector2(Input.GetAxis("Horizontal")<0 ? -1f:1f,1f);
        rb.velocity = new Vector2(Input.GetAxis("Horizontal")*speed,rb.velocity.y);
        animator.SetBool(walkAnimation,Mathf.Abs(Input.GetAxis("Horizontal"))>0.2);

        //jump function
        if(isGround && (Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical")==1)){
            if(Input.GetAxisRaw("Vertical")==1) isAxisUp=true;
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
            animator.SetBool(jumpAnimation,true);
            isGround=false;
        }
        else if(Input.GetButtonUp("Jump") || (Input.GetAxisRaw("Vertical")==0&&isAxisUp)){
            rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y/2);
            isAxisUp=false;
        }

        //duck function
        if(isGround&&Input.GetAxisRaw("Vertical")==-1){
            isAxisDown=true;
            transform.rotation=Quaternion.Euler(0f,0f,90f);
        }else if(isAxisDown && Input.GetAxisRaw("Vertical")==0){
            isAxisDown=false;
            transform.rotation=Quaternion.Euler(0f,0f,0f);
        }
    }

    private void onGameStateChanged(){
        if(PauseManager.instance.isPaused){
            tempPos=transform.position;
            tempVelocity = new Vector2(rb.velocity.x,rb.velocity.y);
            rb.velocity = new Vector2(0,0);
        } else{
            rb.velocity = tempVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
            isGround=true;
            animator.SetBool(jumpAnimation,false);
        }
    }
}
