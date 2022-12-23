using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
public void onPlay(){
    UnityEngine.SceneManagement.SceneManager.LoadScene("GamePlay");
}

public void onQuit(){
    Application.Quit();
    Debug.Log("Quitting Application");
}}
