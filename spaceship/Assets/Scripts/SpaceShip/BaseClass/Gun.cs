using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Default")]
    [SerializeField] private string gunName;
    [SerializeField] private int maxDamageDefault = 0;              
    [SerializeField] private int rateOfFirePerSecondsDefault = 0;   
    [SerializeField] private int ammoSizeDefault = 0;
    [SerializeField] private float reloadTimeInSecondsDefault = 0;
    [Header("Update per level (auto)")]
    [SerializeField] private int maxDamage = 0;                     
    [SerializeField] private int rateOfFirePerSeconds = 0;          
    [SerializeField] private int ammoSize = 0;
    [SerializeField] private float reloadTimeInSeconds = 0;
    [Space]
    [SerializeField] private int level = 1;
    [SerializeField] private GunState state = GunState.Wait;

    public Action<Ammo> fireAction;
    public Action updateSettings;

    protected Ammo ammo;
    private bool isBroken;
    private int remnantAmmoInGun;                                     
    private int damage;

    bool fireOrder;
    DateTime nextFireTime;
    bool reloadOrder;
    DateTime endReloadTime;

    public bool GetBroken() { return isBroken; }                     
    public void AddDamage(int hitDamage)
    {
        damage += hitDamage;
        if (damage >= maxDamage)
        {
            damage = maxDamage;
            SwitchState(GunState.Broken);
        }
    }
    public string GetName() { return gunName; }                     
    public int GetLevel() { return level; }                         
    public void Fire() 
    {
        if (state == GunState.Wait)
        {
            SwitchState(GunState.Fire);
        }
    }
    public void Wait() 
    {
        if (state == GunState.Fire)
        {
            SwitchState(GunState.Wait);
        }
    }
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
    public void SetAmmo<T>(T newAmmo) where T : Ammo 
    {
        ammo = newAmmo;
    }

    public void Init()
    {
        // update for level
        maxDamage = maxDamageDefault * level;
        rateOfFirePerSeconds = rateOfFirePerSecondsDefault * level;
        ammoSize = ammoSizeDefault * level;
        reloadTimeInSeconds = reloadTimeInSecondsDefault / level;

        remnantAmmoInGun = ammoSize;
        nextFireTime = DateTime.Now;

        updateSettings?.Invoke();
    }

    private void StateReload()
    {
        endReloadTime = DateTime.Now.AddSeconds(reloadTimeInSeconds);
        reloadOrder = true;
    }
    private void ReloadTimer() 
    {
        if (reloadOrder && DateTime.Now > endReloadTime)
        {
            remnantAmmoInGun = ammoSize;
            reloadOrder = false;
            SwitchState(GunState.Wait);
        }
    }

    private void StateWait()
    {
        fireOrder = false;
    }

    private void StateFire()
    {
        fireOrder = true;
    }
    private void FireTimer()
    {
        if (DateTime.Now >= nextFireTime 
            && isBroken == false 
            && remnantAmmoInGun > 0 
            && fireOrder)
        {
            // Fire
            fireAction?.Invoke(ammo);
            remnantAmmoInGun--;
            
            int mSeconds = 1000 / rateOfFirePerSeconds;
            nextFireTime = DateTime.Now.AddMilliseconds(mSeconds);
        }
        if (state != GunState.Reload && remnantAmmoInGun <= 0 && ammoSize != 0)
        {
            SwitchState(GunState.Reload);
        }
    }

    private void StateBroken()
    {
        isBroken = true;
        updateSettings?.Invoke();
    }

    private void SwitchState(GunState newstate) 
    {
        if (newstate == state) return;

        state = newstate;
        switch (state)
        {
            case GunState.Broken: StateBroken(); break;
            case GunState.Fire: StateFire(); break;
            case GunState.Reload: StateReload(); break;
            case GunState.Wait: StateWait(); break;
        }
    }

    void Update()
    {
        FireTimer();
        ReloadTimer();
    }

    public new string ToString() 
    {
        return $"Name: {gunName}, Level: {level}, Damage: {damage}/{maxDamage}, ammo:{remnantAmmoInGun}/{ammoSize}, bullet damage: {ammo.bulletDamage}, plasma damage:{ammo.plasmaDamage}";
    }
}
