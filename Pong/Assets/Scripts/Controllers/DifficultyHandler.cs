using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyHandlerActions
{
    public Action<DIFFICULTY> onSetDifficulty = null;
    public Func<DifficultySO[]> onGetDifficulties = null;
    public Func<DIFFICULTY> onGetDifficultySelected = null;
}

public class DifficultyHandler : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("SO"), Space]
    [SerializeField] private DifficultySO[] difficulties = null;
    #endregion

    #region PRIVATE_FIELDS
    private DIFFICULTY difficultySelected = default;

    private DifficultyHandlerActions difficultyHandlerActions = new();
    #endregion

    #region PROPERTIES
    public DifficultyHandlerActions DifficultyHandlerActions { get => difficultyHandlerActions; }
    #endregion

    #region INIT
    public void Init()
    {
        difficultyHandlerActions.onGetDifficulties += GetDifficulties;
        difficultyHandlerActions.onGetDifficultySelected += GetDifficultySelected;
        difficultyHandlerActions.onSetDifficulty += SetDifficulty;
    }
    #endregion

    #region PUBLIC_METHODS
    public DifficultySO[] GetDifficulties()
    {
        return difficulties;
    }

    public DIFFICULTY GetDifficultySelected()
    {
        return difficultySelected;
    }
    #endregion

    #region PRIVATE_METHODS
    private void SetDifficulty(DIFFICULTY difficulty)
    {
        difficultySelected = difficulty;
    }
    #endregion
}
