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


    private void Start()
    {
        foreach (var image in IngredientImages)
        {
            image.enabled = false;
        }
        AddIngredients(ingredientList, false);
    }

    public void AddIngredient(Ingredient ingredient, bool addToList = true)
    {
        print(ingredient.ToString());
        if (ingredientsCount >= ingredientList.Count)
        {
            Debug.LogError("Too many ingredients");
            return;
        }
        Vector3 pos = topOfList + offset * ingredientsCount;
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
