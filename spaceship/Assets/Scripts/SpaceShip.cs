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

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void SetLevel(int newLevel) 
    {
        level = newLevel;
        UpdateParametrsFerLevel();
    }
    public void SetLevelBulletGuns(int newLevel)
    {
        foreach (var item in bulletGuns)
        {
            item.SetLevel(newLevel);
        }
    }
    public void SetLevelPlasmaGuns(int newLevel)
    {
        foreach (var item in plasmaGuns)
        {
            item.SetLevel(newLevel);
        }
    }
    public void SetLevelEngines(int newLevel)
    {
        foreach (var item in engines)
        {
            item.SetLevel(newLevel);
        }
    }
    public void SetLevelHpGenerators(int newLevel)
    {
        foreach (var item in hpGenerators)
        {
            item.SetLevel(newLevel);
        }
    }
    public void SetLevelShields(int newLevel)
    {
        foreach (var item in shields)
        {
            item.SetLevel(newLevel);
        }
    }
    public int GetLevel() { return level; }
    #endregion


    public void Init()
    {
        damage = 0;
        isBroken = false;
        UpdateParametrsFerLevel();

        // guns and items
        GetGunsAndItemsFromSlots();
        InstantiateGunsAndItems();
        InitGunsAndItemsParametrs();
        ApplyItemsParametersToShip();

        ConnectToItemsEvent();
    }

    private void ConnectToItemsEvent()
    {
        foreach (var item in bulletGuns)
        {
            item.updateSettings = UpdateParametrsFerLevel;
        }
        foreach (var item in plasmaGuns)
        {
            item.updateSettings = UpdateParametrsFerLevel;
        }
        foreach (var item in engines)
        {
            item.updateSettings = UpdateParametrsFerLevel;
        }
        foreach (var item in hpGenerators)
        {
            item.updateSettings = UpdateParametrsFerLevel;
        }
        foreach (var item in shields)
        {
            item.updateSettings = UpdateParametrsFerLevel;
        }
    }

    void OnDestroy()
    {
        foreach (var item in bulletGuns)
        {
            item.updateSettings -= UpdateParametrsFerLevel;
        }
        foreach (var item in plasmaGuns)
        {
            item.updateSettings -= UpdateParametrsFerLevel;
        }
        foreach (var item in engines)
        {
            item.updateSettings -= UpdateParametrsFerLevel;
        }
        foreach (var item in hpGenerators)
        {
            item.updateSettings -= UpdateParametrsFerLevel;
        }
        foreach (var item in shields)
        {
            item.updateSettings -= UpdateParametrsFerLevel;
        }
    }

    private void UpdateParametrsFerLevel()
    {
        // update ship per level
        maxDamage = maxDamageDefault * level;
        enginePower = enginePowerDefault * level;
        shieldBulletPercent = shieldBulletPercentDefault * level;
        shieldEnergyPercent = shieldPlasmaPercentDefault * level;
        ApplyItemsParametersToShip();
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
            shieldBulletPercent += Mathf.Clamp(item.GetBulletShieldPercent(), 0, 100);
            shieldEnergyPercent += Mathf.Clamp(item.GetEnergyShieldPercent(), 0, 100);
        }
    }

    public void AddDamage(Ammo ammo)
    {
        int plasmaShield = shieldEnergyPercent;
        int bulletShield = shieldBulletPercent;

        int energyDamage = (int)(ammo.plasmaDamage * (1 - (plasmaShield / 100f)));
        int bulletDamage = (int)(ammo.bulletDamage * (1 - (bulletShield / 100f)));

        //Debug.Log($"plasmaShield {ammo.plasmaDamage} * (1-({plasmaShield} / 100)) = {energyDamage}");
        //Debug.Log($"bulletDamage {ammo.bulletDamage} * (1-({bulletShield} / 100)) = {bulletDamage}");

        damage += energyDamage;
        damage += bulletDamage;

        damage = Mathf.Clamp(damage,0, maxDamage);

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

    public void AddGunsAndItemDamage(int damageValue)
    {
        foreach (var gun in bulletGuns)
        {
            gun.AddDamage(damageValue);
        }
        foreach (var gun in plasmaGuns)
        {
            gun.AddDamage(damageValue);
        }
        foreach (var item in engines)
        {
            item.AddDamage(damageValue);
        }
        foreach (var item in hpGenerators)
        {
            item.AddDamage(damageValue);
        }
        foreach (var item in shields)
        {
            item.AddDamage(damageValue);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GunFire();
        }
        if (Input.GetMouseButtonUp(0))
        {
            GunWait();
        }
    }
    private void GunFire() 
    {
        foreach (var item in bulletGuns)
        {
            item.Fire();
        }
        foreach (var item in plasmaGuns)
        {
            item.Fire();
        }
    }
    private void GunWait()
    {
        foreach (var item in bulletGuns)
        {
            item.Wait();
        }
        foreach (var item in plasmaGuns)
        {
            item.Wait();
        }
    }

    public new string ToString()
    {
        string answer = "";
        answer += shipName + "\n";

        answer += $"level: {level}, Damage: {damage}/{maxDamage}, enginePower: {enginePower}, shieldBulletPercent: {shieldBulletPercent / 100f}%, shieldEnergyPercent: {shieldEnergyPercent/100f}%\n";

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
