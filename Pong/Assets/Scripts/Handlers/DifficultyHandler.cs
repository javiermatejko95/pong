using System;

using UnityEngine;

public class DifficultyHandlerActions
{
    public Action<DifficultySO> onSetDifficulty = null;
    public Func<DifficultySO[]> onGetDifficulties = null;
    public Func<DifficultySO> onGetDifficultySelected = null;
}

public class DifficultyHandler : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [Header("SO"), Space]
    [SerializeField] private DifficultySO[] difficulties = null;
    #endregion

    #region PRIVATE_FIELDS
    private DifficultySO difficultySelected = null;

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

        difficultySelected = difficulties[0];
    }
    #endregion

    #region PUBLIC_METHODS
    public DifficultySO[] GetDifficulties()
    {
        return difficulties;
    }

    public DifficultySO GetDifficultySelected()
    {
        return difficultySelected;
    }
    #endregion

    #region PRIVATE_METHODS
    private void SetDifficulty(DifficultySO difficulty)
    {
        difficultySelected = difficulty;
    }
    #endregion
}
