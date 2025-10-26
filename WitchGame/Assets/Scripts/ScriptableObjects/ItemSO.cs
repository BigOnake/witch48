using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public string Name;

    [Header("3D Item Meshes")]
    public Mesh ItemMesh;

    [Header("UI Item Icons")]
    public Sprite ItemIcon;
}
