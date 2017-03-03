
using System.Collections.Generic;

public enum Class
{
    Soldier,
    Technician,
    Doctor
}

public class Upgrade
{
    public int UpgradeId;
    public int SkillId;
    public int UpgradeCost;
    public bool IsActive;
    public Class Class;
    public string Name;
    public int RequiredId;
}

public class UpgradeStats
{
    public int SoldierLevel;
    public int DoctorLevel;
    public int TechnicianLevel;

    public bool SoldierHunkerDown;
    public bool TechnicianOverload;
    public bool DoctorHeal;

    public bool SoldierSuppressFire = true;
    public bool TechnicianTurret;
    public bool DoctorPoison;

    public List<Upgrade> Upgrades;

    private bool _isInitialized = false;

    public void SetPlayeUpgrades()
    {

        if (!_isInitialized)
        {
            BuildUpgradeList();
            LoadPlayerUpgrades();
            _isInitialized = true;
        }
    }

    private void BuildUpgradeList()
    {

    }

    private void LoadPlayerUpgrades()
    {
        // Defaulting in values now, will be loaded from file.
        SoldierLevel = 3;
        DoctorLevel = 1;
        TechnicianLevel = 1;
    }

    public void UpgradeClass(Class PlayerClass)
    {
        switch (PlayerClass)
        {
            case Class.Soldier:
                SoldierLevel++;
                break;
            case Class.Doctor:
                DoctorLevel++;
                break;
            case Class.Technician:
                TechnicianLevel++;
                break;
        }
    }

    public int GetLevel(Class PlayerClass)
    {
        switch (PlayerClass)
        {
            case Class.Soldier:
                return SoldierLevel;
            case Class.Doctor:
                return DoctorLevel;
            case Class.Technician:
                return TechnicianLevel;
        }
        return -1;
    }

    public bool GetIsSkillEnabled(int skillId, Class PlayerClass)
    {
        switch(PlayerClass)
        {
            case Class.Soldier:
                switch(skillId)
                {
                    case 1:
                        return SoldierHunkerDown;
                    case 2:
                        return SoldierSuppressFire;
                }
                break;
        }
        return false;
    }
}
