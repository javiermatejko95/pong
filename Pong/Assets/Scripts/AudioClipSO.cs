using UnityEngine;

[CreateAssetMenu(fileName = "AudioClip_", menuName = "ScriptableObjects/Audios", order = 1)]
public class AudioClipSO : ScriptableObject
{
    #region SERIALIZED_FIELDS
    [SerializeField] private string id = string.Empty;
    [SerializeField] private AudioClip[] clips = null;
    #endregion

    #region PROPERTIES
    public string Id { get => id; }
    public AudioClip[] Clip { get => clips; }
    #endregion
}
