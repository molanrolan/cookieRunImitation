using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Animator _Anim;
    private Rigidbody2D _Rb;
    private bool _IsGround = true, _IsAxisUp = false, _IsAxisDown = false;
    public float Speed = 6f,JumpForce = 10f;
    private Vector2 _TempVelocity=new Vector2(0f,0f);
    private string _JumpAnimation = "isJump",_WalkAnimation="isWalk";
    private Vector3 _TempPos;
    [SerializeField] private TMPro.TMP_Text _ScoreText;
    [SerializeField] private GameObject _DeadScreen;
    private int _Score;
    public int Health=5;
    // Start is called before the first frame update
    void Start()
    {
        _Rb = GetComponent<Rigidbody2D>();
        _Anim=GetComponent<Animator>();
        _Score=0;
        _ScoreText.text=_Score.ToString();
        _DeadScreen.SetActive(false);
        PauseManager.Instance.onPaused();
    }
    void OnEnable(){PauseManager.Instance.onGameStateChanged+=onGameStateChanged;}  //unsubscribe is at pausemanager
    void OnDisable(){PauseManager.Instance.onGameStateChanged-=onGameStateChanged;} //return error on scenedestroy
    
    // Update is called once per frame
    void Update()
    {
        //suspend character if isPaused
        if(PauseManager.Instance.IsPaused){
            transform.position=_TempPos;
            if(Input.GetButtonDown("Jump"))PauseManager.Instance.onPaused();
            return;}

        //move function
        _Rb.velocity = new Vector2(Input.GetAxis("Horizontal")*Speed,_Rb.velocity.y);
        if(_Rb.velocity.x>0)transform.localScale = new Vector3(1f,1f,1f);
        else if(_Rb.velocity.x<0)transform.localScale = new Vector3(-1f,1f,1f);
        _Anim.SetBool(_WalkAnimation,Mathf.Abs(Input.GetAxis("Horizontal"))>0.2);

        //jump function
        if(_IsGround && (Input.GetButtonDown("Jump") || Input.GetAxisRaw("Vertical")==1)){
            if(Input.GetAxisRaw("Vertical")==1) _IsAxisUp=true;
            _Rb.velocity = new Vector2(_Rb.velocity.x,JumpForce);
            _Anim.SetBool(_JumpAnimation,true);
            _IsGround=false;
        }
        else if(Input.GetButtonUp("Jump") || (Input.GetAxisRaw("Vertical")==0&&_IsAxisUp)){
            _Rb.velocity = new Vector2(_Rb.velocity.x,_Rb.velocity.y/2);
            _IsAxisUp=false;
        }

        //duck function
        if(_IsGround&&Input.GetAxisRaw("Vertical")==-1){
            _IsAxisDown=true;
            transform.rotation=Quaternion.Euler(0f,0f,90f);
        }else if(_IsAxisDown && Input.GetAxisRaw("Vertical")==0){
            _IsAxisDown=false;
            transform.rotation=Quaternion.Euler(0f,0f,0f);
        }
    }

    private void onGameStateChanged(){
        if(PauseManager.Instance.IsPaused){
            _TempPos=transform.position;
            _TempVelocity = new Vector2(_Rb.velocity.x,_Rb.velocity.y);
            _Rb.velocity = new Vector2(0,0);
        } else{
            _Rb.velocity = _TempVelocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Ground"){
            _IsGround=true;
            _Anim.SetBool(_JumpAnimation,false);
        } else if (other.gameObject.tag == "Enemy"){
            Health-=1;
            Debug.Log("Got hit by enemy : " + other.gameObject.name);
            Destroy(other.gameObject);
            if(Health<=0){
                _DeadScreen.SetActive(true);
                PauseManager.Instance.onPaused();
                Debug.Log("Player have died");}
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy"){
            _Score+=1;
            _ScoreText.text=_Score.ToString();
            Destroy(other.gameObject);
            Debug.Log("Killed the enemy : " + other.gameObject.name);
        }
    }
}
