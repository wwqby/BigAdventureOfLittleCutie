using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelectionBtn : MonoBehaviour
{
    [Header("components")]
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI weaponNameText;
    [SerializeField] private TextMeshProUGUI weaponPriceText;
    [field: SerializeField] public Button Button { get; private set; }
    
    public void ConfigureUI(WeaponDataSO weaponDataSO,int level)
    {
        icon.sprite = weaponDataSO.Sprite;
        weaponNameText.text = weaponDataSO.WeaponName;
        weaponPriceText.text = weaponDataSO.PurchasePrice.ToString();
        Button.onClick.RemoveAllListeners();
        background.color = ColorHolder.GetColorByLevel(level);
    }


    public void Selected()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.1f, 0.3f).setEase(LeanTweenType.easeInSine);
    }

    public void Deselected()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, 0.3f);
    }
}
