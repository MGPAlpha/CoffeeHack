using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private bool _physicsEnabled;

    private Rigidbody2D _rb;
    public Vector3 offset;
    public Vector3 returnPosition;
    
    public bool isHeld;
    // Start is called before the first frame update
    public void Start()
    {
        if (_physicsEnabled)
        {
            _rb = gameObject.AddComponent<Rigidbody2D>();
        }
        else
        {
            returnPosition = gameObject.transform.position;
        }

        isHeld = false;
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void FixedUpdate()
    {
        FollowMouse();
    }

    public void PickUp()
    {
        isHeld = true;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(DragController.mousePos);
    }

    public void Drop()
    {
        isHeld = false;
        if (_physicsEnabled)
        {
            _rb.velocity = DragController.mousePos - DragController.prevMousePos;
        }
        else
        {
            gameObject.transform.position = returnPosition;
        }

        offset = Vector3.zero;
    }

    public void FollowMouse()
    {
        if (!isHeld)
        {
            return;
        }

        if (_physicsEnabled)
        {
            _rb.velocity = Vector2.zero;
        }
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(DragController.mousePos) + offset;
    }

}
