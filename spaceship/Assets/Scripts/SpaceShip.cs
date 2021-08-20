using System;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    [Header("GunSlots")]
    [SerializeField] private List<GunSlotLight> gunSlotsLight = new List<GunSlotLight>();
    [SerializeField] private List<GunSlotMiddle> gunSlotsMiddle = new List<GunSlotMiddle>();
    [SerializeField] private List<GunSlotHeavy> gunSlotsHeavie = new List<GunSlotHeavy>();

    [Header("Guns and Items from slots (auto)")]
    [SerializeField] private List<BulletGun> bulletGuns = new List<BulletGun>();
    [SerializeField] private List<PlasmaGun> plasmaGuns = new List<PlasmaGun>();
    [Space]
    [SerializeField] private List<Engine> engines = new List<Engine>();
    [SerializeField] private List<HpGenerator> hpGenerators = new List<HpGenerator>();
    [SerializeField] private List<Shield> shields = new List<Shield>();

    #region Customize Ship
    public void SetGunSlots(List<GunSlotLight> _gunSlotsLight, List<GunSlotMiddle> _gunSlotsMiddle, List<GunSlotHeavy> _gunSlotsHeavie) 
    {
        gunSlotsLight.Clear();
        gunSlotsLight = _gunSlotsLight;
        gunSlotsMiddle.Clear();
        gunSlotsMiddle = _gunSlotsMiddle;
        gunSlotsHeavie.Clear();
        gunSlotsHeavie = _gunSlotsHeavie;

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
    #endregion


    private void Start()
    {
        Init();
    }

    private void Init()
    {
        // get
        GetGunsAndItemsFromSlots();
        // customize
        SetLevelGunsAndItems(); // replace to random
        // update
        UpdateGunsAndItemsParametrs();
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
        foreach (var gunSlot in gunSlotsHeavie)
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

    private void SetLevelGunsAndItems()
    {
        foreach (var gun in bulletGuns)
        {
            gun.SetLevel(2);
        }
        foreach (var gun in plasmaGuns)
        {
            gun.SetLevel(2);
        }
        foreach (var item in engines)
        {
            item.SetLevel(2);
        }
        foreach (var item in hpGenerators)
        {
            item.SetLevel(2);
        }
        foreach (var item in shields)
        {
            item.SetLevel(2);
        }
    }
    private void UpdateGunsAndItemsParametrs()
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
}
