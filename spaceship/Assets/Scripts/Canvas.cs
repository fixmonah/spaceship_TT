using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public SpaceShip ship;
    
    [Space]
    public Text text;
    public Ammo damageAmmo;

    // Start is called before the first frame update
    void Start()
    {
        ship.Init();
        StartCoroutine(WaitForEndOfFrame());
    }
    IEnumerator WaitForEndOfFrame() 
    {
        yield return new WaitForEndOfFrame();
        text.text = ship.ToString();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void AddShipDamage() 
    {
        ship.AddDamage(damageAmmo);
        text.text = ship.ToString();
    }

    public void AddGunsAndItemDamage() 
    {
        int damageValue = 10;

        var bulletGuns = ship.GetBulletsGun();
        foreach (var gun in bulletGuns)
        {
            gun.AddDamage(damageValue);
        }
        var plasmaGuns = ship.GetPlasmaGuns();
        foreach (var gun in plasmaGuns)
        {
            gun.AddDamage(damageValue);
        }
        var engines = ship.GetEngines();
        foreach (var item in engines)
        {
            item.AddDamage(damageValue);
        }
        var hpGenerators = ship.GetHpGenerators();
        foreach (var item in hpGenerators)
        {
            item.AddDamage(damageValue);
        }
        var shields = ship.GetShields();
        foreach (var item in shields)
        {
            item.AddDamage(damageValue);
        }

        text.text = ship.ToString();
    }
}
