using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeContainerBtn : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private TextMeshProUGUI valueText;
    [field:SerializeField] public Button Button{get; private set;}


    public void ConfigUpgradeBtn(Image icon, PlayerAttr attr, string valueString)
    {
        this.icon = icon;
        statsText.text = attr.FormatEnumName();
        valueText.text = valueString;
        Button.onClick.RemoveAllListeners();
    }
}
