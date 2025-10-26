using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class NewCauldron : AbstractItemHolder
{
    [SerializeField] private RecipeListSO _recipeListSO; //Change to have a global recipe list, or recieve level recipe list
    private List<Item> heldIngredients = new();

    protected override void Start()
    {
        base.Start();
        MixCauldron();
    }

    public override void HandlePlayerInteraction(GameObject playerHeldItem, GameObject playerItemHolder, GameObject CorrectInteractable)
    {
        if (CorrectInteractable != gameObject)
        {
            return;
        }

        ItemObject itemObj = playerHeldItem.GetComponent<ItemObject>();

        if (itemObj == null || itemObj.Item)
        {
            return;
        }

        heldIngredients.Add(itemObj.Item);
        Destroy(playerHeldItem);
    }

    public void MixCauldron()
    {
        if (heldIngredients.Count < 1) { return; }

        RecipeSO matchingRecipe = getMatchingRecipe();
        if (matchingRecipe != null)
        {
            Debug.Log("Recipe Mixed: " + matchingRecipe.name);
        }
        else
        {
            Debug.Log("Recipe Mixed: Invalid!!");
        }

        heldIngredients.Clear();
    }

    private RecipeSO getMatchingRecipe()
    {
        Dictionary<Item, int> cauldronCounts = new Dictionary<Item, int>();

        foreach (Item ingredient in heldIngredients)
        {
            if (ingredient == null)
            {
                continue;
            }

            if (cauldronCounts.ContainsKey(ingredient))
            {
                cauldronCounts[ingredient]++;
            }
            else
            {
                cauldronCounts.Add(ingredient, 1);
            }
        }

        foreach (RecipeSO recipe in _recipeListSO.recipeList)
        {
            if (IsMatchingRecipe(cauldronCounts, recipe))
            {
                return recipe;
            }
        }

        return null;
    }

    private bool IsMatchingRecipe(Dictionary<Item, int> cauldronCounts, RecipeSO recipe)
    {
        if (cauldronCounts.Count != recipe.IngredientCounts.Count)
        {
            return false;
        }

        foreach (KeyValuePair<Item, int> recipePair in recipe.IngredientCounts)
        {
            Item recipeIngredient = recipePair.Key;
            int recipeCount = recipePair.Value;
            if (!cauldronCounts.ContainsKey(recipeIngredient) || cauldronCounts[recipeIngredient] != recipeCount)
            {
                return false;
            }
        }

        return true;
    }
}
