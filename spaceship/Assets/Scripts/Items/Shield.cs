using UnityEngine;

public class Shield : Item
{
    [Header("Default")]
    [SerializeField] int bulletShieldDefault;
    [SerializeField] int energyShieldDefault;
    [Header("Update per level (auto)")]
    [SerializeField] int bulletShield;
    [SerializeField] int energyShield;

    public new void Init()
    {
        base.Init();
        bulletShield = bulletShieldDefault * level;
        energyShield = energyShieldDefault * level;
    }


    public int GetBulletShield() { return bulletShield; }
    public int GetEnergyShield() { return energyShield; }
}
