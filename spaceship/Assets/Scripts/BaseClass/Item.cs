using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] protected string itemName;
    [SerializeField] protected int level = 1;
    [Header("Default")]
    [SerializeField] protected int maxDamageDefault;
    [Header("Update per level (auto)")]
    [SerializeField] protected int maxDamage;

    protected int damage;
    protected bool isBroken;

    public void Init()
    {
        maxDamage = maxDamageDefault * level;
    }
    public void AddDamage(int newDamage) 
    {
        damage = damage + newDamage;
        if (damage >= maxDamage)
        {
            damage = maxDamage;
            isBroken = true;
        }
    }
    public string GetName() { return itemName; }
    public int GetLevel() { return level; }
    public void SetLevel(int newLevel)
    {
        level = newLevel;
        if (level < 1)
        {
            level = 1;
        }
        if (level > 3)
        {
            level = 3;
        }
        Init();
    }
    public bool IsBroken() { return isBroken; }
}
