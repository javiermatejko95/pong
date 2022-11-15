using UnityEngine;

[CreateAssetMenu(fileName = "Input_", menuName = "ScriptableObjects/Inputs", order = 1)]
public class InputSO : ScriptableObject
{
    #region SERIALIZED_FIELDS
    [SerializeField] private INPUT input = default;
    #endregion

    #region PROPERTIES
    public INPUT Input { get => input; }
    #endregion
}

public enum INPUT
{
    KEYBOARD,
    MOUSE
}
