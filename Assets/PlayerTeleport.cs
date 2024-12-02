using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] Transform teleportPos;
    [SerializeField] Vector2 teleportOffset;
    [SerializeField] int howManyToActivate;
    int activatedCount = 0;

    bool isEnabled = true;


    // seamless player teleportation to different location
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnabled)
        {
            activatedCount++;
            if (activatedCount == howManyToActivate)
            {
                GameObject player = GameManager.instance.player;

                Transform newTeleportPos = teleportPos;
                newTeleportPos.position = new Vector2(player.transform.position.x + teleportOffset.x, player.transform.position.y + teleportOffset.y);

                Vector2 delta = newTeleportPos.position - player.transform.position;
                player.transform.position = newTeleportPos.position;
                int numVcams = CinemachineCore.VirtualCameraCount;
                for (int i = 0; i < numVcams; ++i)
                    CinemachineCore.GetVirtualCamera(i).OnTargetObjectWarped(player.transform, delta);
            }
        }
    }
}
