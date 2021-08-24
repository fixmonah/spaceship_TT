using UnityEngine;

public class GunSlot : MonoBehaviour
{
    [SerializeField] private Gun[] guns;
    [SerializeField] private Item[] items;

    public Gun[] GetGuns() { return guns; }
    public Item[] GetItems() { return items; }
}
