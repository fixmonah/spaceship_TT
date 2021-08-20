public class MachineGun : Gun
{
    public new void SetAmmo<T>(T newAmmo) where T : AmmoBullet
    {
        ammo = newAmmo;
    }
}
