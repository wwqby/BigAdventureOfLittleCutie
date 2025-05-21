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
    [Header("Settings")]
    [field:SerializeField] public  PlayerAttr Attr{get; private set;}
    [field:SerializeField] public  int AttrValue{get; private set;}


    public void ConfigUpgradeBtn(Image icon, PlayerAttr attr, int value)
    {
        this.icon = icon;
        this.Attr = attr;
        this.AttrValue = value;
        statsText.text = attr.FormatEnumName();
        valueText.text = value.ToString();
        Button.onClick.RemoveAllListeners();
    }
}
