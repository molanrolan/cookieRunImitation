using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static PauseManager _Instance;
    public static PauseManager Instance{
        get{ 
            if(_Instance == null){      //LazyObject init
            _Instance= FindObjectOfType<PauseManager>();
            Debug.Log("instance was called while null : " + _Instance?.name);
            }
            return _Instance;
        }
        set{_Instance = value;}
    }
    public bool IsPaused=false;
    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    // static void init(){      //PauseManager awake is later than PlayerScript, hence event is null
    //     instance = FindObjectOfType<PauseManager>();
    //     Debug.Log("PauseManager is init" + instance.name);
    // }
    private void Awake() {
        if(Instance==null)Instance = this;  //singleton... but this awake is slower than other script awake
        // Debug.Log("PauseManager is awake : " + _Instance.name);
    }

    private void OnDisable() {
        onGameStateChanged = null;  //unsubscribe all listener as their OnDisable is sometime slower
    }
    public event Action onGameStateChanged;
    private void SetState(bool newGameState)
    {
        if (newGameState == IsPaused)
            return;
 
        IsPaused = newGameState;
        if(onGameStateChanged!=null)
            onGameStateChanged();
    }

    public void onPaused(){
        // Debug.Log("onPause function is Called, current isPaused = " + !IsPaused);
        SetState(!IsPaused);
    }
    public void onHome(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        //  Debug.Log("onHome function is Called");
    }
    public void onRetry(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
        //  Debug.Log("onRetry function is Called");
    }

}
