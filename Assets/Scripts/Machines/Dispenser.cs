using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour, IInteractable
{
    public Ingredient ingredient;
    public List<Collider2D> triggers;

    protected LayerMask colMask;

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
        colMask = LayerMask.GetMask("Coffee", "CoffeeCup", "CoffeeIngredients");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (((1 << col.gameObject.layer) & colMask) != 0)
        {
            triggers.Add(col);
        }
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        triggers.Remove(col);
    }
}
