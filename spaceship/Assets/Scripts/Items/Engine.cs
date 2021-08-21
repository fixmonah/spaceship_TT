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
        return $"Name: {itemName}, Damage: {damage}/{maxDamage}, Power: {power}";
    }
}
