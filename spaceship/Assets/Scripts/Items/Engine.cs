using System;
using UnityEngine;

public class Engine : Item
{
    [Header("Default")]
    [SerializeField] private int powerDefault;
    [Header("Update per level (auto)")]
    [SerializeField] private int power;

    public new void Init()
    {
        base.Init();
        power = powerDefault * level;
        updateSettings?.Invoke();
    }

    public new void SetLevel(int newLevel) 
    {
        level = Mathf.Clamp(newLevel, 1, 3);
        Init();
    }

    public int GetPower() 
    {
        if (isBroken)
        {
            return 0;
        }
        else
        {
            return power;
        }
    }
    public new string ToString()
    {
        return $"Name: {itemName}, Level: {level}, Damage: {damage}/{maxDamage}, Power: {power}";
    }
}
