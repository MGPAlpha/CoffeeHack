using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeJug : DragAndDrop, IInteractable
{
    [SerializeField] private CoffeeMachine _coffeeMachine;
    [SerializeField] private Sprite[] _sprites;

    public int MaxUses = 3;
    private int _numUses;
    public int NumUses
    {
        get
        {
            return _numUses;
        }
        set
        {
            _numUses = value;
            if (_numUses > MaxUses)
            {
                _numUses = MaxUses;
            }

            if (_numUses < 0)
            {
                _numUses = 0;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        NumUses = 0;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        _spriteRenderer.sprite = _sprites[NumUses];
    }

    public void Interact(DragAndDrop drag)
    {
        _coffeeMachine.Interact(drag);
    }

    public void UseCoffee()
    {
        NumUses--;
    }
}
