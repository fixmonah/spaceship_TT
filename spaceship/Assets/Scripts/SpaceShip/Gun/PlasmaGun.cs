using UnityEngine;
public class PlasmaGun : Gun
{
    [SerializeField] AmmoPlasma defaultAmmo;

    public new void Init() 
    {
        base.Init();
        SetAmmo(defaultAmmo);
    }

    public new void SetAmmo<T>(T newAmmo) where T : AmmoPlasma
    {
        ammo = newAmmo;
    }
}
