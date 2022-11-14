using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class CustomButton : MonoBehaviour
{
    #region SERIALIZED_FIELDS
    [SerializeField] private Button button = null;
    [SerializeField] private TextMeshProUGUI text = null;
    [SerializeField] private Image image = null;
    #endregion

    #region PRIVATE_FIELDS
    private Color selectedColor = new Color();
    private Color unselectedColor = new Color();
    #endregion

    #region PUBLIC_METHODS
    public void SetOnClick(Action onClick)
    {
        button.onClick.AddListener(() => onClick?.Invoke()) ;
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void SetColor(Color selectedColor, Color unselectedColor)
    {
        this.selectedColor = selectedColor;
        this.unselectedColor = unselectedColor;
    }

    public void ToggleSelected(bool status)
    {
        image.color = status ? selectedColor : unselectedColor;
    }
    #endregion
}
