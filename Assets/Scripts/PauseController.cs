using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    [SerializeField] GameObject PausePanel;
    private void OnEnable() {
        PauseManager.instance.onGameStateChanged+=onGameStateChanged;   //unsubscride is at PauseManager
    }

    private void onGameStateChanged(){
       PausePanel.SetActive(PauseManager.instance.isPaused);
       Debug.Log("set paused panel is "+PauseManager.instance.isPaused);
    }
}
