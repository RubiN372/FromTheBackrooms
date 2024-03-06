using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] Transform teleportPos;
    [SerializeField] CinemachineVirtualCamera cmVCam;
    

    public void OnTriggerEnter2D()
    {
        GameObject player = GameManager.instance.player;
        
        // position difference from middle of green box 
        float playerXDifference = gameObject.transform.position.x - player.transform.position.x; 
        float playerYDifference = gameObject.transform.position.y - player.transform.position.y; 

        // new position hehe based on where do you walk into that box  
        Transform newTeleportPos = teleportPos; 
        newTeleportPos.position = new Vector2(teleportPos.position.x - playerXDifference, teleportPos.position.y - playerYDifference);
        newTeleportPos.position = new Vector2(newTeleportPos.position.x , newTeleportPos.position.y );

        // thing that somehow prevents smooth camera transition when teleported
        Vector2 delta = newTeleportPos.position - player.transform.position;
        cmVCam.OnTargetObjectWarped(newTeleportPos, delta);
        cmVCam.PreviousStateIsValid = false;


        // actual teleporting
        player.transform.position = newTeleportPos.position;
    }
}
