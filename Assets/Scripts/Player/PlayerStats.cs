using System.Collections;
using UnityEngine;

public class PlayerStats {
    // Stats that can depete
    public int Health;
    public int Shield;
    public int Energy;
    public int Ammo;

    public int Armor;
    public int Speed;
    public int Accuracy;
    public int Defense;
    public int Luck;

    public float PlayerX;
    public float PlayerY;

    public int MaxHealth;
    public int MaxShield;
    public int MaxEnergy;

    public float LastHitTime;

	private bool _isInitialized = false;

	public void SetPlayerStats()
	{

		if (!_isInitialized) {
			LoadPlayerStats();
			_isInitialized = true;
		}
	}

	private void LoadPlayerStats() {
		// Defaulting in values now, will be loaded from file.
		Health = 100;
		Shield = 100;
		Energy = 100;

		MaxHealth = 100;
		MaxShield = 100;
		MaxEnergy = 100;

		Ammo = -1;
	}
}
