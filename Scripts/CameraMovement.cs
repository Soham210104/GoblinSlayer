using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public moveByTouch playerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerScript.playerIsDead == false)
        {
        transform.position = new Vector3(target.transform.position.x,target.transform.position.y,transform.position.z);
        }
    }
} 
