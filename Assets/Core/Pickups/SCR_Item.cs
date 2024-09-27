using UnityEngine;

public class SCR_Item : MonoBehaviour
{
    [SerializeField] private SO_Item _item;
    public SO_Item item { get { return _item; } }
}