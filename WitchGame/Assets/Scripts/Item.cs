using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    public string Name;

    [Header("3D Item Meshes")]
    public Mesh RAWItemMesh;
    public Mesh CHOPPEDItemMesh;
    public Mesh GRINDEDItemMesh;

    [Header("UI Item Icons")]
    public Image RAWItemIcon;
    public Image CHOPPEDItemIcon;
    public Image GRINDEDItemIcon;
}
