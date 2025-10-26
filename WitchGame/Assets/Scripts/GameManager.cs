using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    private enum GameState
    {
        WaitingToStart,
        CountdownToStart,
        GameInProgress,
        GameOver,
    }

    private GameState _state;

    [SerializeField] private float _countdownToStartTimer = 3f;
    [SerializeField] private float _gameplayTimerDuration = 120f;
    private float _gameplayTimer;

    public static event Action OnGameStarted;
    public static event Action OnGameOver;

    private bool startedGame = false;

    private void Awake()
    {
        instance = this;
        _state = GameState.WaitingToStart;
        startedGame = false;
    }

    private void Update()
    {
        switch (_state)
        {
            case GameState.WaitingToStart: //add conditions to start game if needed
                if (startedGame)
                {
                    _state = GameState.CountdownToStart;
                }
                break;
            case GameState.CountdownToStart:
                _countdownToStartTimer -= Time.deltaTime;
                if (_countdownToStartTimer < 0f)
                {
                    OnGameStarted?.Invoke();
                    _gameplayTimer = _gameplayTimerDuration;
                    _state = GameState.GameInProgress;
                }
                
                break;
            case GameState.GameInProgress:
                _gameplayTimer -= Time.deltaTime;
                if (_gameplayTimer < 0f)
                {
                    _state = GameState.GameOver;
                }
                
                break;
            case GameState.GameOver:
                OnGameOver?.Invoke();
                break;
        }
    }

    public bool IsCountdownToStart()
    {
        return _state == GameState.CountdownToStart;
    }

    public bool IsGameInProgress()
    {
        return _state == GameState.GameInProgress;
    }

    public bool IsGameOver()
    {
        return _state == GameState.GameOver;
    }

    public float GetCountdownTimer()
    {
        return _countdownToStartTimer;
    }

    public float GetGameplayTimer()
    {
        return _gameplayTimer;
    }

    public void StartGame()
    {
        startedGame = true;
    }
}
