using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Canvas : MonoBehaviour
{
    public SpaceShip ship;
    
    [Space]
    public Text text;
    public AmmoBullet bulletAmmo;
    public AmmoPlasma plasmaAmmo;
    public AmmoBullet bulletAmmo2;
    public AmmoPlasma plasmaAmmo2;
    public SpaceShip shipPrefab;
    [SerializeField] private List<GunSlotLight> gunSlotsLight = new List<GunSlotLight>();
    [SerializeField] private List<GunSlotMiddle> gunSlotsMiddle = new List<GunSlotMiddle>();
    [SerializeField] private List<GunSlotHeavy> gunSlotHeavy = new List<GunSlotHeavy>();

    private int shipLevel = 1;
    private int bulletGunLevel = 1;
    private int plasmaGunLevel = 1;
    private int engineLevel = 1;
    private int hpGeneratorLevel = 1;
    private int shieldLevel = 1;

    void Update()
    {
        text.text = ship?.ToString();
    }

    public void ButtonCreateRamdomShip() 
    {
        Destroy(ship?.gameObject);
        ship = Instantiate(shipPrefab);

        List<GunSlotLight> slotsLight = new List<GunSlotLight>();
        List<GunSlotMiddle> slotsMiddle = new List<GunSlotMiddle>();
        List<GunSlotHeavy> slotHeavy = new List<GunSlotHeavy>();

        int randomNumberOfLightSlots = Random.Range(1, 4);
        for (int i = 0; i < randomNumberOfLightSlots; i++)
        {
            slotsLight.Add(gunSlotsLight[Random.Range(0, gunSlotsLight.Count)]);
        }
        int randomNumberOfMiddleSlots = Random.Range(1, 4);
        for (int i = 0; i < randomNumberOfMiddleSlots; i++)
        {
            slotsMiddle.Add(gunSlotsMiddle[Random.Range(0, gunSlotsMiddle.Count)]);
        }
        int randomNumberOfHeavySlots = Random.Range(1, 4);
        for (int i = 0; i < randomNumberOfHeavySlots; i++)
        {
            slotHeavy.Add(gunSlotHeavy[Random.Range(0, gunSlotHeavy.Count)]);
        }

        ship.SetGunSlots(slotsLight, slotsMiddle, slotHeavy);
    }

    public void ButtonSetNormalAmmo() 
    {
        ship.SetBulletGunAmmo(bulletAmmo);
        ship.SetPlasmaGunAmmo(plasmaAmmo);
    }
    public void ButtonSetPowerlAmmo()
    {
        ship.SetBulletGunAmmo(bulletAmmo2);
        ship.SetPlasmaGunAmmo(plasmaAmmo2);
    }

    public void ButtonAddShipDamageBullet() 
    {
        ship.AddDamage(bulletAmmo);
    }
    public void ButtonAddShipDamagePlasma()
    {
        ship.AddDamage(plasmaAmmo);
    }
   
    public void ButtonShipLevelUp() 
    {
        shipLevel = Mathf.Clamp(shipLevel+1, 1, 3);
        ship.SetLevel(shipLevel);
    }
    public void ButtonShipLevelDown()
    {
        shipLevel = Mathf.Clamp(shipLevel-1, 1, 3);
        ship.SetLevel(shipLevel);
    }

    public void ButtonBulletGunLevelUp() 
    {
        bulletGunLevel = Mathf.Clamp(bulletGunLevel+1, 1, 3);
        ship.SetLevelBulletGuns(bulletGunLevel);
    }
    public void ButtonBulletGunLevelDown()
    {
        bulletGunLevel = Mathf.Clamp(bulletGunLevel-1, 1, 3);
        ship.SetLevelBulletGuns(bulletGunLevel);
    }

    public void ButtonPlasmaGunLevelUp()
    {
        plasmaGunLevel = Mathf.Clamp(plasmaGunLevel+1, 1, 3);
        ship.SetLevelPlasmaGuns(plasmaGunLevel);
    }
    public void ButtonPlasmaGunLevelDown()
    {
        plasmaGunLevel = Mathf.Clamp(plasmaGunLevel-1, 1, 3);
        ship.SetLevelPlasmaGuns(plasmaGunLevel);
    }

    public void ButtonEngineLevelUp()
    {
        engineLevel = Mathf.Clamp(engineLevel+1, 1, 3);
        ship.SetLevelEngines(engineLevel);
    }
    public void ButtonEngineLevelDown()
    {
        engineLevel = Mathf.Clamp(engineLevel-1, 1, 3);
        ship.SetLevelEngines(engineLevel);
    }

    public void ButtonHpGeneratorsLevelUp()
    {
        hpGeneratorLevel = Mathf.Clamp(hpGeneratorLevel+1, 1, 3);
        ship.SetLevelHpGenerators(hpGeneratorLevel);
    }
    public void ButtonHpGeneratorsLevelDown()
    {
        hpGeneratorLevel = Mathf.Clamp(hpGeneratorLevel-1, 1, 3);
        ship.SetLevelHpGenerators(hpGeneratorLevel);
    }

    public void ButtonShieldLevelUp()
    {
        shieldLevel = Mathf.Clamp(shieldLevel+1, 1, 3);
        ship.SetLevelShields(shieldLevel);
    }
    public void ButtonShieldLevelDown()
    {
        shieldLevel = Mathf.Clamp(shieldLevel-1, 1, 3);
        ship.SetLevelShields(shieldLevel);
    }

    public void ButtonAddGunsAndItemDamage() 
    {
        ship.AddGunsAndItemDamage(10);
    }
}
