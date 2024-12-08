using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour, IInteractable
{
    public Ingredient ingredient;

    public void Interact(DragAndDrop drag)
    {
        if (drag.GetType() != typeof(Cup))
        {
            return;
        }
        Cup cup = (Cup)drag;
        if (cup.ingredients.Contains(ingredient))
        {
            return;
        }
        cup.AddIngredient(ingredient);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
