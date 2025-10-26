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
        go = playerInputManager.playerPrefab;
        go.transform.root.position = spawnPositions[_playerCount] ? spawnPositions[_playerCount].position : Vector3.zero;

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

        if (model)
            model.gameObject.SetActive(false);

        _playerCount++;
    }
}
