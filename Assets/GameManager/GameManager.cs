using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

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
    public void AddPlayer(GameObject newPlayer)
    {
        players.Add(newPlayer);
        Debug.Log("Player added");
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