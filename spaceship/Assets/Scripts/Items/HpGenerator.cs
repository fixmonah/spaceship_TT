using UnityEngine;

public class HpGenerator : Item
{
    [Header("Default")]
    [SerializeField] private int regenerationHPDefault;
    [Header("Update per level (auto)")]
    [SerializeField] private int regenerationHP;
    public new void Init()
    {
        base.Init();
        regenerationHP = regenerationHPDefault * level;
    }
    public int GetRegenerationHP() 
    {
        if (isBroken)
        {
            return 0;
        }
        else
        {
            return regenerationHP;
        }
    }
    public new string ToString()
    {
        return $"Name: {itemName}, Damage: {damage}/{maxDamage}, RegenerationHP: {regenerationHP}";
    }
}
