using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public List<Ingredient> order;
    public IngredientListRenderer ingredientListRenderer;
    private float timer = 60f;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            StrikeManger._instance.AddStrike();
            GoAway();
        }
    }

    public void GoAway()
    {
        CustomerManager._instance.RemoveCustomer(this);
    }

    public void SetOrder(List<Ingredient> order)
    {
        this.order = order;
        ingredientListRenderer.AddIngredients(order);
    }

    public void SubmitOrder(List<Ingredient> submittedOrder)
    {
        bool correctOrder = order.SequenceEqual(submittedOrder);
        if (!correctOrder)
        {
            StrikeManger._instance.AddStrike();
        }
        GoAway();
    }



}
