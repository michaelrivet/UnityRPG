using System.Collections;
using UnityEngine;

public class PlayerStats {
    // Stats that can depete
    public int Health;
    public int Shield;
    public int Energy;
    public int Ammo;

    public int Armor;
    public int WeaponAttack;

    public int Defense;
    public int BaseAttack;
    public int Speed;
    public int Accuracy;
    public int Luck;

    public float PlayerX;
    public float PlayerY;

    public int MaxHealth;
    public int MaxShield;
    public int MaxEnergy;

    public float LastHitTime;
    public float LastSkillTime;
    
    private bool _isInitialized = false;

    public void SetPlayerStats(Class PlayerClass)
	{

		if (!_isInitialized) {
			LoadPlayerStats(PlayerClass);
			//_isInitialized = true;
		}
	}

	private void LoadPlayerStats(Class PlayerClass)
    {

        // Defaulting in values now, will be loaded from file.
        MaxHealth = 100 + GlobalControl.Instance.UpgradeStats.GetLevel(PlayerClass) * 25;
		MaxShield = 50 + GlobalControl.Instance.UpgradeStats.GetLevel(PlayerClass) * 10;
		MaxEnergy = 50 + GlobalControl.Instance.UpgradeStats.GetLevel(PlayerClass) * 10;

        Health = MaxHealth;
        Shield = MaxShield;
        Energy = MaxEnergy;

        Armor = 5;
        Defense = 5 + GlobalControl.Instance.UpgradeStats.GetLevel(PlayerClass) * 5;

        BaseAttack = 5 + GlobalControl.Instance.UpgradeStats.GetLevel(PlayerClass) * 5;
        WeaponAttack = 2;

		Ammo = -1;
    }

    
}
