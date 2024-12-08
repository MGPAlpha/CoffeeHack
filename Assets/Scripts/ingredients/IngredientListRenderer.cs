using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientListRenderer : MonoBehaviour
{
    public IngredientSpritesDict spritsDict;
    public Vector3 topOfList;
    public Vector3 offset;
    public List<Image> IngredientImages;

    public List<Ingredient> ingredientList = new List<Ingredient>();
    private int ingredientsCount;


    public void AddIngredient(Ingredient ingredient, bool addToList = true)
    {
        print(ingredient.ToString());
        if (ingredientsCount >= IngredientImages.Count)
        {
            Debug.LogError("Too many ingredients");
            return;
        }
        if (addToList)
        {
            ingredientList.Add(ingredient);
        }
        Image image = IngredientImages[ingredientsCount];
        image.sprite = spritsDict.spriteDict[ingredient];
        image.enabled = true;
        ingredientsCount++;
    }

    public void AddIngredients(List<Ingredient> ingredients, bool addToList = true)
    {
        foreach (Ingredient ingredient in ingredients)
        {
            AddIngredient(ingredient, addToList);
        }
    }
}
