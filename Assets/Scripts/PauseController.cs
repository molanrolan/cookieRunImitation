using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject _PausePanel;
    void OnEnable(){PauseManager.Instance.onGameStateChanged+=onGameStateChanged;}  //unsubscribe is at pausemanager
    void OnDisable(){PauseManager.Instance.onGameStateChanged-=onGameStateChanged;} //return error on scenedestroy

    private void onGameStateChanged(){
       _PausePanel.SetActive(PauseManager.Instance.IsPaused);
    //    Debug.Log("set paused panel is "+PauseManager.Instance.IsPaused);
    }
}
