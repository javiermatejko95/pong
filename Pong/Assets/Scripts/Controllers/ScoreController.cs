using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControllerActions
{
    public Action<int> onSetScorePlayerOne = null;
    public Action<int> onSetScorePlayerTwo = null;
}

public class ScoreController : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private ScoreView scoreView = null;
    #endregion

    #region PRIVATE_FIELDS
    private int scorePlayerOne = 0;
    private int scorePlayerTwo = 0;

    private ScoreControllerActions scoreControllerActions = new();
    #endregion

    #region PROPERTIES
    public ScoreControllerActions ScoreControllerActions { get => scoreControllerActions; }
    #endregion

    #region INIT
    public void Init(GameControllerActions gameControllerActions)
    {
        scoreControllerActions.onSetScorePlayerOne += scoreView.SetScorePlayerOne;
        scoreControllerActions.onSetScorePlayerTwo += scoreView.SetScorePlayerTwo;

        gameControllerActions.onPlayerOneScoreGoal += () => scoreControllerActions.onSetScorePlayerOne?.Invoke(1);
        gameControllerActions.onPlayerTwoScoreGoal += () => scoreControllerActions.onSetScorePlayerTwo?.Invoke(1);

        ResetGame();
    }
    #endregion

    #region PRIVATE_METHODS
    private void ResetGame()
    {
        scorePlayerOne = 0;
        scorePlayerTwo = 0;

        scoreView.SetScorePlayerOne(scorePlayerOne);
        scoreView.SetScorePlayerTwo(scorePlayerTwo);
    }
    #endregion
}
