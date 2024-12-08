using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cup : DragAndDrop, IInteractable
{
    [SerializeField] private Canvas _ingredientCanvas;
    [SerializeField] private IngredientListRenderer _ingredientListRenderer;
    public List<Ingredient> ingredients;

    public int Priority { get => 10;}

    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        _ingredientCanvas.enabled = false;
        ingredients = new List<Ingredient>();
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
    }

    public void Interact(DragAndDrop drag)
    {
        if (drag.GetType() == typeof(CoffeeJug))
        {
            ((CoffeeJug)drag).Interact(this);
        }
    }

    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);
        _ingredientListRenderer.ingredientList.Add(ingredient);
    }

    public void TrashCup()
    {
        Destroy(gameObject);
    }
}
