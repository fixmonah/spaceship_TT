using System;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [Header("Ship settings")]
    [SerializeField] private string shipName = "";
    [SerializeField] private int level = 1;
    [SerializeField] private int maxDamageDefault;
    [SerializeField] private int enginePowerDefault;
    [SerializeField] private int shieldBulletPercentDefault;
    [SerializeField] private int shieldPlasmaPercentDefault;

    [Header("Update per level (auto)")]
    [SerializeField] private int maxDamage;
    [SerializeField] private int enginePower;
    [SerializeField] private int shieldBulletPercent;
    [SerializeField] private int shieldEnergyPercent;

    [Header("GunSlots")]
    [SerializeField] private List<GunSlotLight> gunSlotsLight = new List<GunSlotLight>();
    [SerializeField] private List<GunSlotMiddle> gunSlotsMiddle = new List<GunSlotMiddle>();
    [SerializeField] private List<GunSlotHeavy> gunSlotHeavy = new List<GunSlotHeavy>();


    [Header("Guns and Items from slots (auto)")]
    [SerializeField] private List<BulletGun> bulletGuns = new List<BulletGun>();
    [SerializeField] private List<PlasmaGun> plasmaGuns = new List<PlasmaGun>();
    [Space]
    [SerializeField] private List<Engine> engines = new List<Engine>();
    [SerializeField] private List<HpGenerator> hpGenerators = new List<HpGenerator>();
    [SerializeField] private List<Shield> shields = new List<Shield>();

    private int damage;
    private bool isBroken;

    #region Customize Ship
    public void SetGunSlots(List<GunSlotLight> _gunSlotsLight, List<GunSlotMiddle> _gunSlotsMiddle, List<GunSlotHeavy> _gunSlotsHeavie) 
    {
        gunSlotsLight.Clear();
        gunSlotsLight = _gunSlotsLight;
        gunSlotsMiddle.Clear();
        gunSlotsMiddle = _gunSlotsMiddle;
        gunSlotHeavy.Clear();
        gunSlotHeavy = _gunSlotsHeavie;

        ClearGunAndItems();
        Init();
    }
    private void ClearGunAndItems() 
    {
        bulletGuns.Clear();
        plasmaGuns.Clear();
        engines.Clear();
        hpGenerators.Clear();
        shields.Clear();
    }
    public List<BulletGun> GetBulletsGun() { return bulletGuns; }
    public List<PlasmaGun> GetPlasmaGuns() { return plasmaGuns; }
    public List<Engine> GetEngines() { return engines; }
    public List<HpGenerator> GetHpGenerators() { return hpGenerators; }
    public List<Shield> GetShields() { return shields; }
    public void SetLevel(int newLevel) { level = newLevel; }
    public int GetLevel() { return level; }
    #endregion


    //private void Start()
    //{
    //    Init();
    //}

    public void Init()
    {
        // update ship per level
        maxDamage = maxDamageDefault * level;
        enginePower = enginePowerDefault * level;
        shieldBulletPercent = shieldBulletPercentDefault * level;
        shieldEnergyPercent = shieldPlasmaPercentDefault * level;

        // guns and items
        GetGunsAndItemsFromSlots();
        InstantiateGunsAndItems();
        InitGunsAndItemsParametrs();
        ApplyItemsParametersToShip();
    }

    private void InstantiateGunsAndItems()
    {
        List<BulletGun> instantiateBulletGunObjects = new List<BulletGun>();
        foreach (var item in bulletGuns)
        {
            var itemGO = Instantiate(item, transform);
            instantiateBulletGunObjects.Add(itemGO);
        }
        bulletGuns.Clear();
        bulletGuns = instantiateBulletGunObjects;

        List<PlasmaGun> instantiatePlasmaGun = new List<PlasmaGun>();
        foreach (var item in plasmaGuns)
        {
            var itemGO = Instantiate(item, transform);
            instantiatePlasmaGun.Add(itemGO);
        }
        plasmaGuns.Clear();
        plasmaGuns = instantiatePlasmaGun;

        List<Engine> instantiateEngine = new List<Engine>();
        foreach (var item in engines)
        {
            var itemGO = Instantiate(item, transform);
            instantiateEngine.Add(itemGO);
        }
        engines.Clear();
        engines = instantiateEngine;

        List<HpGenerator> instantiateHpGenerators = new List<HpGenerator>();
        foreach (var item in hpGenerators)
        {
            var itemGO = Instantiate(item, transform);
            instantiateHpGenerators.Add(itemGO);
        }
        hpGenerators.Clear();
        hpGenerators = instantiateHpGenerators;

        List<Shield> instantiateShields = new List<Shield>();
        foreach (var item in shields)
        {
            var itemGO = Instantiate(item, transform);
            instantiateShields.Add(itemGO);
        }
        shields.Clear();
        shields = instantiateShields;
    }

    private void GetGunsAndItemsFromSlots()
    {
        foreach (var gunSlot in gunSlotsLight)
        {
            var guns = gunSlot.GetGuns();
            GetGuns(guns);
            var items = gunSlot.GetItems();
            GetItems(items);
        }
        foreach (var gunSlot in gunSlotsMiddle)
        {
            var guns = gunSlot.GetGuns();
            GetGuns(guns);
            var items = gunSlot.GetItems();
            GetItems(items);
        }
        foreach (var gunSlot in gunSlotHeavy)
        {
            var guns = gunSlot.GetGuns();
            GetGuns(guns);
            var items = gunSlot.GetItems();
            GetItems(items);
        }
    }
    private void GetGuns(Gun[] guns)
    {
        foreach (var gun in guns)
        {
            if (gun is BulletGun)
            {
                bulletGuns.Add((BulletGun)gun);
            }
            if (gun is PlasmaGun)
            {
                plasmaGuns.Add((PlasmaGun)gun);
            }
        }
    }
    private void GetItems(Item[] items) 
    {
        foreach (var item in items)
        {
            if (item is Engine)
            {
                engines.Add((Engine)item);
            }
            if (item is HpGenerator)
            {
                hpGenerators.Add((HpGenerator)item);
            }
            if (item is Shield)
            {
                shields.Add((Shield)item);
            }
        }
    }

    private void InitGunsAndItemsParametrs()
    {
        foreach (var gun in bulletGuns)
        {
            gun.Init();
        }
        foreach (var gun in plasmaGuns)
        {
            gun.Init();
        }
        foreach (var item in engines)
        {
            item.Init();
        }
        foreach (var item in hpGenerators)
        {
            item.Init();
        }
        foreach (var item in shields)
        {
            item.Init();
        }
    }
    private void ApplyItemsParametersToShip()
    {
        foreach (var item in engines)
        {
            enginePower += item.GetPower();
        }
        foreach (var item in shields)
        {
            shieldBulletPercent = item.GetBulletShieldPercent();
            shieldEnergyPercent = item.GetEnergyShieldPercent();
        }
    }

    public void AddDamage(Ammo ammo) 
    {
        int plasmaShield = 0;
        int bulletShield = 0;
        foreach (var item in shields)
        {
            plasmaShield += item.GetEnergyShieldPercent();
            bulletShield += item.GetBulletShieldPercent();
        }
        if (plasmaShield > 100)
        {
            plasmaShield = 100;
        }
        if (bulletShield > 100)
        {
            bulletShield = 100;
        }

        int energyDamage = ammo.plasmaDamage * (1 - (plasmaShield / 100));
        int bulletDamage = ammo.bulletDamage * (1 - (bulletShield / 100));


        damage += energyDamage;
        damage += bulletDamage;
        if (damage < 0)
        {
            damage = 0;
        }
        if (damage > maxDamage)
        {
            damage = maxDamage;
        }

        // generation HP
        if (energyDamage > 0)
        {
            GenerationHP();
        }

        if (damage == 0)
        {
            isBroken = true;
        }
    }

    private void GenerationHP()
    {
        foreach (var hpGen in hpGenerators)
        {
            damage -= hpGen.GetRegenerationHP();
        }
        if (damage < 0)
        {
            damage = 0;
        }
        if (damage > maxDamage)
        {
            damage = maxDamage;
        }
    }

    public new string ToString() 
    {
        string answer = "";
        answer += shipName + "\n";

        answer += $"level: {level}, Damage: {damage}/{maxDamage}, enginePower: {enginePower}, shieldBulletPercent: {shieldBulletPercent}, shieldEnergyPercent: {shieldEnergyPercent}\n";
        
        answer += $"GunSlotLight: ";
        foreach (var item in gunSlotsLight)
        {
            answer += $"{item.name}, ";
        }
        answer += "\n";
        answer += $"GunSlotsMiddle: ";
        foreach (var item in gunSlotsMiddle)
        {
            answer += $"{item.name}, ";
        }
        answer += "\n";
        answer += $"GunSlotHeavy: ";
        foreach (var item in gunSlotHeavy)
        {
            answer += $"{item.name}, ";
        }
        answer += "\n";

        answer += "BulletGun\n";
        for (int i = 0; i < bulletGuns.Count; i++)
        {
            answer += $"+ {bulletGuns[i].ToString()}\n";
        }
        answer += "PlasmaGuns\n";
        for (int i = 0; i < plasmaGuns.Count; i++)
        {
            answer += $"+ {plasmaGuns[i].ToString()}\n";
        }

        answer += "Engine\n";
        for (int i = 0; i < engines.Count; i++)
        {
            answer += $"+ {engines[i].ToString()}\n";
        }
        answer += "Shields\n";
        for (int i = 0; i < shields.Count; i++)
        {
            answer += $"+ {shields[i].ToString()}\n";
        }
        answer += "HPGenerators\n";
        for (int i = 0; i < hpGenerators.Count; i++)
        {
            answer += $"+ {hpGenerators[i].ToString()}\n";
        }
        return answer;
    }
}
