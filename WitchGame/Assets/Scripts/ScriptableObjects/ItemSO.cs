using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public string Name;

    [Header("Item Icon")]
    public Sprite ItemIcon;
}
