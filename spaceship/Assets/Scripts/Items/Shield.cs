using UnityEngine;

public class Shield : Item
{
    [Header("Default")]
    [SerializeField] int bulletShieldPercentDefault = 0;
    [SerializeField] int energyShieldPercentDefault = 0;
    [Header("Update per level (auto)")]
    [SerializeField] int bulletShield = 0;
    [SerializeField] int energyShield = 0;

    public new void Init()
    {
        base.Init();
        bulletShield = bulletShieldPercentDefault * level;
        energyShield = energyShieldPercentDefault * level;
    }

    public int GetBulletShieldPercent() 
    {
        if (isBroken)
        {
            return 0;
        }
        else
        {
            return bulletShield;
        }
    }
    public int GetEnergyShieldPercent() 
    {
        if (isBroken)
        {
            return 0;
        }
        else
        {
            return energyShield;
        }
    }

    public new string ToString()
    {
        return $"Name: {itemName}, Damage: {damage}/{maxDamage}, BulletShield: {bulletShield / 100f}%, EnergyShield: {energyShield / 100f}%";
    }
}
