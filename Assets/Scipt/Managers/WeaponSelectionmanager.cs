
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class WeaponSelectionManager : MonoBehaviour, IGameStateListener
{

    [Header("Components")]
    [SerializeField] private Transform weaponSelectionContainer;
    [Header("elements")]
    [SerializeField] private WeaponSelectionBtn prefab;
    [SerializeField] private WeaponDataSO[] weaponDataSOs;
    [SerializeField] private PlayerWeapon playerWeapon;
    [SerializeField] private WeaponDataSO selectedWeapon;
    [SerializeField] private int selectedWeaponLevel;


    public void OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.WEAPON_SELECTION)
        {
            ConfigWeaponSelection();
            return;
        }
        if (gameState == GameState.GAME)
        {
            if (selectedWeapon == null)
                return;
            playerWeapon.AddWeapon(selectedWeapon, selectedWeaponLevel);
            selectedWeapon = null;
            selectedWeaponLevel = -1;
        }
    }


    [NaughtyAttributes.Button]
    private void ConfigWeaponSelection()
    {
        weaponSelectionContainer.Clear();
        for (int i = 0; i < 3; i++)
        {
            WeaponSelectionBtn btn = Instantiate(prefab, weaponSelectionContainer);
            WeaponDataSO weapon = weaponDataSOs[Random.Range(0, weaponDataSOs.Length)];
            int level = Random.Range(0, 4);
            btn.ConfigureUI(weapon, level);
            btn.Button.onClick.AddListener(() => ClickCallback(btn, weapon, level));
        }
    }

    private void ClickCallback(WeaponSelectionBtn btn, WeaponDataSO weapon, int level)
    {
        foreach (WeaponSelectionBtn button in weaponSelectionContainer.GetComponentsInChildren<WeaponSelectionBtn>())
        {
            if (button == btn)
            {
                button.Selected();
                selectedWeapon = weapon;
                selectedWeaponLevel = level;
            }
            else
            {
                button.Deselected();
            }
        }
    }


    public void StartGame()
    {
        if (selectedWeapon == null)
        {
            return;
        }
        GameManager.instance.StateStartGame();
    }
}
