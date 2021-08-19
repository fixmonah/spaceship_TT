public class PlasmaCannon : Gun
{
    public new void SetAmmo<T>(T newAmmo) where T : AmmoPlasma
    {
        ammo = newAmmo;
    }
}
