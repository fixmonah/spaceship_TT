using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Default")]
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
    [Header("Debug")]
    [SerializeField] protected Ammo ammo;
    [SerializeField] private bool isBroken;
    [SerializeField] private int remnantOfAmmo;
    [SerializeField] private int damage;

    bool fireOrder;
    DateTime nextFireTime;
    bool reloadOrder;
    DateTime endReloadTime;

    public Action<Ammo> fireAction;

    public void SetDamage(int hitDamage)
    {
        damage = damage + hitDamage;
        if (damage >= maxDamage)
        {
            damage = maxDamage;
            SwitchState(GunState.Broken);
        }
    }
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
    //public void SetAmmo(Ammo newAmmo)
    public void SetAmmo<T>(T newAmmo) where T : Ammo
    {
        ammo = newAmmo;
    }

    void Start()
    {
        Init();
    }
    private void Init()
    {
        // update for level
        maxDamage = maxDamageDefault * level;
        rateOfFirePerSeconds = rateOfFirePerSecondsDefault * level;
        ammoSize = ammoSizeDefault * level;
        reloadTimeInSeconds = reloadTimeInSecondsDefault / level;

        remnantOfAmmo = ammoSize;
        nextFireTime = DateTime.Now;
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
            remnantOfAmmo = ammoSize;
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
            && remnantOfAmmo > 0 
            && fireOrder)
        {
            // Fire
            fireAction?.Invoke(ammo);
            remnantOfAmmo--;
            
            int mSeconds = 1000 / rateOfFirePerSeconds;
            nextFireTime = DateTime.Now.AddMilliseconds(mSeconds);
        }
        if (remnantOfAmmo <= 0 && ammoSize != 0)
        {
            SwitchState(GunState.Reload);
        }
    }

    private void StateBroken()
    {
        isBroken = true;
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
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
        if (Input.GetMouseButtonUp(0))
        {
            Wait();
        }
        if (Input.GetKey(KeyCode.Q))
        {
            SetDamage(1);
        }

        FireTimer();
        ReloadTimer();
    }
}