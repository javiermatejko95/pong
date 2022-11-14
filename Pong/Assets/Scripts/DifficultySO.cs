using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty_", menuName = "ScriptableObjects/Difficulties", order = 1)]
public class DifficultySO : ScriptableObject
{
    #region SERIALIZED_FIELDS
    [SerializeField] private DIFFICULTIES difficulty = default;
    [SerializeField] private float speedMultiplier = 1f;
    #endregion

    #region PPROPERTIES
    public DIFFICULTIES Difficulty { get => difficulty; }
    public float SpeedMultiplier { get => speedMultiplier; }
    #endregion
}

public enum DIFFICULTIES
{
    EASY,
    MEDIUM,
    HARD
}
