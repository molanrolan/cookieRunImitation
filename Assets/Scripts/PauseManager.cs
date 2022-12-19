using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static PauseManager _instance;
    public static PauseManager instance{
        get{ 
            if(_instance == null){      //LazyObject init
            _instance= FindObjectOfType<PauseManager>();
            Debug.Log("instance was called while null : " + _instance?.name);
            }
            return _instance;
        }
        set{_instance = value;}
    }
    public bool isPaused=false;
    // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    // static void init(){      //PauseManager awake is later than PlayerScript, hence event is null
    //     instance = FindObjectOfType<PauseManager>();
    //     Debug.Log("PauseManager is init" + instance.name);
    // }
    private void Awake() {
        instance = this;            // singleton... but this awake is sometimes slower than other awake
        Debug.Log("PauseManager is awake : " + _instance.name);
    }

    private void OnDisable() {
        onGameStateChanged = null;  //unsubscribe all listener as their OnDisable is sometime slower
    }
    public event Action onGameStateChanged;
    private void SetState(bool newGameState)
    {
        if (newGameState == isPaused)
            return;
 
        isPaused = newGameState;
        if(onGameStateChanged!=null)
            onGameStateChanged();
    }

    public void onPaused(){
        Debug.Log("onPause function is Called, current isPaused = " + !isPaused);
        SetState(!isPaused);
    }
    public void onHome(){
         Debug.Log("onHome function is Called");
    }
    public void onRetry(){
         Debug.Log("onRetry function is Called");
         UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
