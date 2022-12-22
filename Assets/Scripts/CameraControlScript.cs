using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript : MonoBehaviour
{
    private PlayerScript _Player;
    // Start is called before the first frame update
    void Start()
    {
        _Player = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() {
        transform.position= new Vector3(Mathf.Max(0,_Player.transform.position.x),
            Mathf.Max(-10,_Player.transform.position.y),transform.position.z);
    }
}
