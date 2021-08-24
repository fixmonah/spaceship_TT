using System;
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

        updateSettings?.Invoke();
    }

    public new void SetLevel(int newLevel)
    {
        level = Mathf.Clamp(newLevel, 1, 3);
        Init();
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
        return $"Name: {itemName}, Level: {level}, Damage: {damage}/{maxDamage}, RegenerationHP: {regenerationHP}";
    }
}
