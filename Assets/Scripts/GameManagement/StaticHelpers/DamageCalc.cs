using System;
using UnityEngine;

static class DamageCalc
{
    public static int CalcDamage(int Attack, int Defense, DamageType damageType)
    {
        double damage;

        //10 * LOG(Power + 2, 1.2) + Power * 2
        double baseDamage = 10 * Math.Log((double)Attack + 2, 1.2) + Attack * 2.0;
        //=7 * LOG(Defense + 2, 1.3) + Defense * 0.8
        double damageMit = 7 * Math.Log((double)Defense + 2, 1.3) + Defense * 0.8;

        damage = baseDamage - damageMit > 0 ? baseDamage - damageMit : 1;

        if (damageType == DamageType.Kinetic)
            damage *= 1.25;
        Debug.Log("Health: Power: " + Attack + " Defense: " + Defense + " = Damage : " + damage);
        return (int)damage;
    }

    public static int CalcShield(int Attack, int Defense, DamageType damageType)
    {
        double damage;

        //10 * LOG(Power + 2, 1.2) + Power * 2
        double baseDamage = 10 * Math.Log((double)Attack + 2, 1.2) + Attack * 2.0;
        //=7 * LOG(Defense + 2, 1.3) + Defense * 0.8
        double damageMit = 7 * Math.Log((double)Defense + 2, 1.3) + Defense * 0.8;

        damage = baseDamage - damageMit > 0 ? baseDamage - damageMit : 1;

        if (damageType == DamageType.Energy)
            damage *= 1.25;
        Debug.Log("Shield: Power: " + Attack + " Defense: " + Defense + " = Damage : " + damage);
        return (int)damage;
    }
}

enum DamageType
{
    Kinetic,
    Energy
}