using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _Spawner;
    [SerializeField] private GameObject[] _EnemyPrefabs;
    private int _EnemyIndex,_SpawnerIndex;
    void OnEnable(){PauseManager.Instance.onGameStateChanged+=onGameStateChanged;}  //unsubscribe is at pausemanager
    void OnDisable(){PauseManager.Instance.onGameStateChanged-=onGameStateChanged;} //return error on scenedestroy
    private void onGameStateChanged(){
        if(PauseManager.Instance.IsPaused)StopCoroutine("Spawning");
        else StartCoroutine("Spawning");
    }

    IEnumerator Spawning(){
        while(true){
            yield return new WaitForSeconds(Random.Range(3f,6f));
            _EnemyIndex=Random.Range(0,_EnemyPrefabs.Length);
            _SpawnerIndex=Random.Range(0,_Spawner.Length);
            Instantiate(_EnemyPrefabs[_EnemyIndex],
                _Spawner[_SpawnerIndex].transform.position,Quaternion.identity);
        }
    }
}
