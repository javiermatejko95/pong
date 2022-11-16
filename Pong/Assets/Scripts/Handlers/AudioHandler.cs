using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandlerActions
{
    public Action<bool> onGameOver = null;
    public Action onPongHit = null;
    public Action onUIClick = null;
}

public class AudioHandler : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private AudioSource audioSource = null;

    [Header("Clips"), Space]
    [SerializeField] private AudioClipSO[] clips = null;
    #endregion

    #region PRIVATE_FIELDS
    private AudioHandlerActions audioHandlerActions = new();
    #endregion

    #region PROPERTIES
    public AudioHandlerActions AudioHandlerActions { get => audioHandlerActions; }
    #endregion

    #region INIT
    public void Init(GameControllerActions gameControllerActions)
    {
        audioHandlerActions.onGameOver += (status) => PlayAudioClip(status ? Constants.gameOverWinId : Constants.gameOverLoseId);
        audioHandlerActions.onPongHit += () => PlayAudioClip(Constants.hitId);
        audioHandlerActions.onUIClick += () => PlayAudioClip(Constants.clickId);
    }
    #endregion

    #region PRIVATE_METHODS
    private void PlayAudioClip(string id)
    {
        for(int i = 0; i < clips.Length; i++)
        {
            if(clips[i].Id == id)
            {
                audioSource.PlayOneShot(clips[i].Clip.Length > 1 ? clips[i].Clip[UnityEngine.Random.Range(0, clips[i].Clip.Length)] : clips[i].Clip[0]);
            }
        }
    }
    #endregion
}
