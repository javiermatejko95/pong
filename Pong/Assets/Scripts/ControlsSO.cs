using UnityEngine;

[CreateAssetMenu(fileName = "Control_", menuName = "ScriptableObjects/Controls", order = 1)]
public class ControlsSO : ScriptableObject
{
    #region SERIALIZED_FIELDS
    [SerializeField] private CONTROL control = default;
    #endregion

    #region PROPERTIES
    public CONTROL Control { get => control; }
    #endregion
}

public enum CONTROL
{
    KEYBOARD,
    MOUSE
}
