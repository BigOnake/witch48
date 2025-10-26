using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "Scriptable Objects/RecipeSO")]
public class RecipeSO : ScriptableObject
{
    public string recipeName;
    public List<Item> ingredientSOList;
    public Item result;
    private Dictionary<Item, int> _ingredientCounts = new Dictionary<Item, int>();
    public IReadOnlyDictionary<Item, int> IngredientCounts => _ingredientCounts as IReadOnlyDictionary<Item, int>;

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
        foreach (Item ingredient in ingredientSOList)
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