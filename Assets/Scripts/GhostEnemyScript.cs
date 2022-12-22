using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEnemyScript : MonoBehaviour
{
    private Animator _Anim;
    private Rigidbody2D _Rb;
    private PlayerScript _Player;
    private string _WalkAnimation="isWalk";
    public float Speed=5f, speedy=3f;
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
            _Rb.velocity=new Vector2(0,0);
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
        _Rb.velocity=new Vector2(0,0);
        yield return new WaitForSeconds(Random.Range(1f,3f));
        teleport();
        yield return new WaitForSeconds(1f);
        }
    }

    private void move(){
        _Rb.velocity=new Vector2(Random.Range(0.5f,1.2f)*Speed*
        (_Player.transform.position.x>transform.position.x?1:-1),
        _Player.transform.position.y-transform.position.y);
    }

    private void teleport(){
        var randomOnSurface = Random.insideUnitCircle * 4;
        transform.position=new Vector3(randomOnSurface.x+(randomOnSurface.x>=0?+2:-2),randomOnSurface.y,0) + _Player.transform.position;
    }
}
