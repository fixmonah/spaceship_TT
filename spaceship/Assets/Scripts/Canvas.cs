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
    public SpaceShip shipPrefab;
    [SerializeField] private List<GunSlotLight> gunSlotsLight = new List<GunSlotLight>();
    [SerializeField] private List<GunSlotMiddle> gunSlotsMiddle = new List<GunSlotMiddle>();
    [SerializeField] private List<GunSlotHeavy> gunSlotHeavy = new List<GunSlotHeavy>();

    // Start is called before the first frame update
    void Start()
    {
        //ship.Init();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = ship?.ToString();
    }

    public void CreateRamdomShip() 
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

    private void DestriyShip() 
    {
        
    }

    public void AddShipDamageBullet() 
    {
        ship.AddDamage(bulletAmmo);
    }
    public void AddShipDamagePlasma()
    {
        ship.AddDamage(plasmaAmmo);
    }
   
    int shipLevel = 1;
    public void ShipLevelUp() 
    {
        shipLevel = Mathf.Clamp(shipLevel+1, 1, 3);
        ship.SetLevel(shipLevel);
    }
    public void ShipLevelDown()
    {
        shipLevel = Mathf.Clamp(shipLevel-1, 1, 3);
        ship.SetLevel(shipLevel);
    }
    
    int bulletGunLevel = 1;
    public void BulletGunLevelUp() 
    {
        bulletGunLevel = Mathf.Clamp(bulletGunLevel+1, 1, 3);
        ship.SetLevelBulletGuns(bulletGunLevel);
    }
    public void BulletGunLevelDown()
    {
        bulletGunLevel = Mathf.Clamp(bulletGunLevel-1, 1, 3);
        ship.SetLevelBulletGuns(bulletGunLevel);
    }

    int plasmaGunLevel = 1;
    public void PlasmaGunLevelUp()
    {
        plasmaGunLevel = Mathf.Clamp(plasmaGunLevel+1, 1, 3);
        ship.SetLevelPlasmaGuns(plasmaGunLevel);
    }
    public void PlasmaGunLevelDown()
    {
        plasmaGunLevel = Mathf.Clamp(plasmaGunLevel-1, 1, 3);
        ship.SetLevelPlasmaGuns(plasmaGunLevel);
    }

    int engineLevel = 1;
    public void EngineLevelUp()
    {
        engineLevel = Mathf.Clamp(engineLevel+1, 1, 3);
        ship.SetLevelEngines(engineLevel);
    }
    public void EngineLevelDown()
    {
        engineLevel = Mathf.Clamp(engineLevel-1, 1, 3);
        ship.SetLevelEngines(engineLevel);
    }

    int hpGeneratorLevel = 1;
    public void HpGeneratorsLevelUp()
    {
        hpGeneratorLevel = Mathf.Clamp(hpGeneratorLevel+1, 1, 3);
        ship.SetLevelHpGenerators(hpGeneratorLevel);
    }
    public void HpGeneratorsLevelDown()
    {
        hpGeneratorLevel = Mathf.Clamp(hpGeneratorLevel-1, 1, 3);
        ship.SetLevelHpGenerators(hpGeneratorLevel);
    }

    int shieldLevel = 1;
    public void ShieldLevelUp()
    {
        shieldLevel = Mathf.Clamp(shieldLevel+1, 1, 3);
        ship.SetLevelShields(shieldLevel);
    }
    public void ShieldLevelDown()
    {
        shieldLevel = Mathf.Clamp(shieldLevel-1, 1, 3);
        ship.SetLevelShields(shieldLevel);
    }

    public void AddGunsAndItemDamage() 
    {
        ship.AddGunsAndItemDamage(10);
    }
}
