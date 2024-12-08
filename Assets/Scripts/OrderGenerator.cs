using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator
{
    public static List<Ingredient> GenerateOrder(int complexity)
    {
        if (complexity > 5) complexity = 5;
        List<Ingredient> order = new List<Ingredient>
        {
            Ingredient.Coffee
        };
        bool isIced = false;
        bool isBlended = false;

        while (complexity > 0)
        {
            if (complexity > 2)
            {
                if (Random.Range(0, 1) > 0.7)
                {
                    if (!isIced)
                    {
                        isIced = true;
                    }
                    else if (!isBlended)
                    {
                        isBlended = true;
                    }
                    else
                    {
                        order.Add(pickRandomIngredient());
                    }
                }
                else
                {
                    order.Add(pickRandomIngredient());
                }
            }
            else
            {
                order.Add(pickRandomIngredient());
            }
            complexity--;
        }
        if (isIced) order.Add(Ingredient.Ice);
        if (isBlended) order.Add(Ingredient.Blender);
        return order;
    }

    private static Ingredient pickRandomIngredient()
    {
        float random = Random.Range(0, 8);
        if (random < 1) return Ingredient.Caramel;
        else if (random < 2) return Ingredient.Vanilla;
        else if (random < 3) return Ingredient.Honey;
        else if (random < 4) return Ingredient.Chocolate;
        else if (random < 6) return Ingredient.Oat_Milk;
        return Ingredient.Milk;
    }
}
