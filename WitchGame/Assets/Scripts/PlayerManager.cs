using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    public Transform[] spawnPositions;
    public GameObject playerPrefab;

    private int _playerCount = 0;

    private void Awake()
    {
        playerInputManager = FindAnyObjectByType<PlayerInputManager>();
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += AddPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= AddPlayer;
    }

    private void AddPlayer(PlayerInput input)
    {
        GameObject go;
        Transform model;
        go = input.transform.gameObject;

        if (spawnPositions != null && spawnPositions.Length != 0)
        {
            go.transform.root.position = spawnPositions[input.playerIndex].position;
        }
        else
        {
            go.transform.root.position = Vector3.zero;
        }

        if (input.playerIndex == 0)
        {
            playerInputManager.playerPrefab = playerPrefab;
        }

        switch (_playerCount)
        {
            case (0):
                model = go.transform.Find("brother");
                break;
            case (1):
                model = go.transform.Find("sister");
                break;
            default:
                Debug.Log("Not allowed to spawn more players");
                return;
        }

        _playerCount++;
    }
}
