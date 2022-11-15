using UnityEngine;

[CreateAssetMenu(fileName = "Difficulty_", menuName = "ScriptableObjects/Difficulties", order = 1)]
public class DifficultySO : ScriptableObject
{
    #region SERIALIZED_FIELDS
    [SerializeField] private DIFFICULTY difficulty = default;
    [SerializeField] private float speedMultiplier = 1f;
    #endregion

    #region PPROPERTIES
    public DIFFICULTY Difficulty { get => difficulty; }
    public float SpeedMultiplier { get => speedMultiplier; }
    #endregion
}

public enum DIFFICULTY
{
    EASY,
    MEDIUM,
    HARD
}
