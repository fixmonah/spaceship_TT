using UnityEngine;
public class BulletGun : Gun
{
    [SerializeField] AmmoBullet defaultAmmo;

    public new void Init()
    {
        base.Init();
        SetAmmo(defaultAmmo);
    }

    public new void SetAmmo<T>(T newAmmo) where T : AmmoBullet
    {
        ammo = newAmmo;
    }
}
