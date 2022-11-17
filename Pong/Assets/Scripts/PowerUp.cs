using UnityEngine;

public class PowerUp : MonoBehaviour
{
    #region EXPOSED_FIELDS
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    #endregion

    #region PUBLIC_METHODS
    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
    #endregion
}
