using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieEnemyScript : MonoBehaviour
{
    private Animator _Anim;
    private Rigidbody2D _Rb;
    private PlayerScript _Player;
    private string _WalkAnimation="isWalk";
    public float Speed=5f, Jump=4f;
    public bool IsDead = false;
    // Start is called before the first frame update
    void Start()
    {
        _Anim=GetComponent<Animator>();
        _Rb=GetComponent<Rigidbody2D>();
        _Player= FindObjectOfType<PlayerScript>();
        StartCoroutine("MovementScript");
    }
    void OnEnable(){PauseManager.Instance.onGameStateChanged+=onGameStateChanged;}  //unsubscribe is at pausemanager
    void OnDisable(){PauseManager.Instance.onGameStateChanged-=onGameStateChanged;} //return error on scenedestroy

    // Update is called once per frame
    void Update()
    {
        if(PauseManager.Instance.IsPaused){
            StopCoroutine("MovementScript");
            _Rb.velocity=new Vector2(0,_Rb.velocity.y);
            transform.position=transform.position;
            return;}

        if(IsDead){
            Debug.Log(gameObject.name + "is killed");
            Destroy(gameObject);
            return;
        }
        
        _Anim.SetBool(_WalkAnimation,(_Rb.velocity.x!=0));
        if(_Rb.velocity.x>0)transform.localScale=new Vector3(1f,1f,1f);
        else if(_Rb.velocity.x<0)transform.localScale=new Vector3(-1f,1f,1f);
    }

    private void onGameStateChanged(){
        if(PauseManager.Instance.IsPaused) StopCoroutine("MovementScript");
        else StartCoroutine("MovementScript");
    }
    private IEnumerator MovementScript(){
        yield return new WaitForSeconds(3f);
        while(true){
        move();
        yield return new WaitForSeconds(Random.Range(2f,5f));
        _Rb.velocity=new Vector2(0,_Rb.velocity.y);
        yield return new WaitForSeconds(Random.Range(1f,3f));
        jump();
        yield return new WaitForSeconds(1f);
        }
    }

    private void move(){
        _Rb.velocity=new Vector2(Random.Range(0.5f,1.5f)*Speed*
        (_Player.transform.position.x>transform.position.x?1:-1),_Rb.velocity.y);
    }

    private void jump(){
        _Rb.velocity=new Vector2(Random.Range(-Speed,Speed),Speed);
    }
}
