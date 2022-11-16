using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameControllerActions
{
    public Action onPlay = null;
    public Action onPlayerOneScoreGoal = null;
    public Action onPlayerTwoScoreGoal = null;
    public Action onExit = null;
    public Action<int, bool> onCheckScore = null;
    public Action onPowerUp = null;
    public Action onPowerUpFinish = null;
}

public class GameController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("Handlers"), Space]
    [SerializeField] private LevelController levelController = null;
    [SerializeField] private DifficultyHandler difficultyHandler = null;
    [SerializeField] private ScoreController scoreController = null;
    [SerializeField] private InputHandler inputHandler = null;
    [SerializeField] private AudioHandler audioHandler = null;

    [Header("Views"), Space]
    [SerializeField] private MenuView menuView = null;

    [Header("Players"), Space]
    [SerializeField] private int totalGoals = 10;
    [SerializeField] private Paddle playerOne = null;
    [SerializeField] private Paddle playerTwo = null;
    [SerializeField] private Ball ball = null;
    [SerializeField] private Camera camera = null;
    #endregion

    #region PRIVATE_FIELDS
    private GameControllerActions gameControllerActions = new();
    private AudioHandlerActions audioHandlerActions = null;
    #endregion

    #region UNITY_CALLS
    private void Awake()
    {
        gameControllerActions.onPlay += Play;
        gameControllerActions.onExit += Exit;
        gameControllerActions.onCheckScore += CheckScore;

        levelController.Init(gameControllerActions);
        scoreController.Init(gameControllerActions);

        difficultyHandler.Init();
        inputHandler.Init();
        audioHandler.Init(gameControllerActions);

        audioHandlerActions = audioHandler.AudioHandlerActions;

        menuView.Init(gameControllerActions, difficultyHandler.DifficultyHandlerActions, inputHandler.InputHandlerActions, audioHandlerActions);

        Vector3 cameraBounds = GetBounds();

        playerOne.Init(gameControllerActions, camera, cameraBounds);
        playerTwo.Init(gameControllerActions, camera, cameraBounds);
        playerTwo.SetBall(ball);

        ball.Init(gameControllerActions, audioHandlerActions, camera, cameraBounds);
    }
    #endregion

    #region PRIVATE_METHODS
    private void Play()
    {
        StartCoroutine(IStart());

        IEnumerator IStart()
        {
            playerOne.SetInput((INPUT)inputHandler.InputHandlerActions.onGetSelectedInput?.Invoke());

            DifficultySO difficulty = difficultyHandler.DifficultyHandlerActions.onGetDifficultySelected?.Invoke();
            playerTwo.SetDifficultySpeed(difficulty.SpeedMultiplier);            

            yield return new WaitForSeconds(2f);

            playerOne.TogglePlaying(true);
            playerTwo.TogglePlaying(true);
            ball.TogglePlaying(true);
        }
    }

    private void Exit()
    {
        playerOne.TogglePlaying(false);
        playerTwo.TogglePlaying(false);
        ball.TogglePlaying(false);
    }

    private void CheckScore(int amount, bool status)
    {
        if(amount >= totalGoals)
        {
            audioHandlerActions.onGameOver?.Invoke(status);
            gameControllerActions.onExit?.Invoke();
        }
    }

    private Vector3 GetBounds()
    {
        return camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, camera.transform.position.z));
    }
    #endregion
}
