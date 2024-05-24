using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    public ObjectViewUI objectViewUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private List<GameObject> players = new List<GameObject>();
    public GameObject player;
    public GameObject uiCanvas;
    public void AddPlayer(GameObject newPlayer)
    {
        players.Add(newPlayer);
        Debug.Log("Player added");
    }

    public ObjectViewUI GetViewedObject()
    {
        return objectViewUI;
    }

    public void RemovePlayer(GameObject playerToRemove)
    {
        if (players.Contains(playerToRemove))
        {
            players.Remove(playerToRemove);
            Debug.Log("Player removed");
        }
        else
        {
            Debug.LogWarning("Trying to remove a player that doesn't exist in the list.");
        }
    }
}