using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    [SerializeField] Transform teleportPos;
    [SerializeField] CinemachineVirtualCamera cmVCam;
    [SerializeField] Vector2 teleportOffset;
    [SerializeField] int howManyToActivate;
    [SerializeField] bool deactivateAfterTeleport;
    int activatedCount = 0;


    // seamless player teleportation to different location
    public void OnTriggerEnter2D(Collider2D other)
    {
        activatedCount++;
        if (activatedCount == howManyToActivate)
        {
            GameObject player = GameManager.instance.player;

            Transform newTeleportPos = teleportPos;
            newTeleportPos.position = new Vector2(player.transform.position.x + teleportOffset.x, player.transform.position.y + teleportOffset.y);

            Vector2 delta = newTeleportPos.position - player.transform.position;
            player.transform.position = newTeleportPos.position;
            int numVcams = CinemachineCore.Instance.VirtualCameraCount;
            for (int i = 0; i < numVcams; ++i)
                CinemachineCore.Instance.GetVirtualCamera(i).OnTargetObjectWarped(player.transform, delta);
        }

    }
}
