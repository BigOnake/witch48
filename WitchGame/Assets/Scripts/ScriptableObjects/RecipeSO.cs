using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "Scriptable Objects/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public Sprite recipeIcon;
    public List<ItemSO> ingredientSOList;
    public ItemSO result;
    private Dictionary<ItemSO, int> _ingredientCounts = new Dictionary<ItemSO, int>();
    public IReadOnlyDictionary<ItemSO, int> IngredientCounts => _ingredientCounts as IReadOnlyDictionary<ItemSO, int>;

    private void OnValidate()
    {
        CountIngredients();
    }

    private void OnEnable()
    {
        if (_ingredientCounts == null || _ingredientCounts.Count == 0)
        {
            CountIngredients();
        }
    }

    private void CountIngredients()
    {
        foreach (ItemSO ingredient in ingredientSOList)
        {
            if (ingredient == null)
            {
                continue;
            }

            if (_ingredientCounts.ContainsKey(ingredient))
            {
                _ingredientCounts[ingredient]++;
            }
            else
            {
                _ingredientCounts.Add(ingredient, 1);
            }
        }
    }
}