using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour, IInteractable
{
    public List<Ingredient> order;
    public IngredientListRenderer ingredientListRenderer;
    public float time = 60f;
    private float timer = 10f;

    public Slider slider;

    private void Start()
    {
        timer = time;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            StrikeManger._instance.AddStrike();
            GoAway();
        }
        slider.value = timer / time;
    }

    public void GoAway()
    {
        CustomerManager._instance.RemoveCustomer(this);
    }

    public void SetOrder(List<Ingredient> order)
    {
        this.order = order;
        ingredientListRenderer.AddIngredients(order);
        print(order);
    }

    public void SubmitOrder(List<Ingredient> submittedOrder)
    {
        print("submitting order");
        print(string.Join(",", submittedOrder.ToArray()));
        bool correctOrder = order.SequenceEqual(submittedOrder);
        if (!correctOrder)
        {
            StrikeManger._instance.AddStrike();
        }
        GoAway();
    }

    public void Interact(DragAndDrop drag)
    {
        Cup c = drag.GetComponent<Cup>();
        if (!c || c == null) return;
        SubmitOrder(c.ingredients);
        c.TrashCup();
    }
}
