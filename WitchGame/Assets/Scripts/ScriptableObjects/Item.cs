using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public string Name;

    [Header("3D Item Meshes")]
    public Mesh ItemMesh;

    [Header("UI Item Icons")]
    public Image ItemIcon;
}
