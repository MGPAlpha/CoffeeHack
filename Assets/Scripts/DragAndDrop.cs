using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] private Collider2D _overlapTrigger;
    [SerializeField] private bool _physicsEnabled;

    private Rigidbody2D _rb;
    public List<Collider2D> triggers;
    public Vector3 offset;
    public Vector3 returnPosition;
    public bool isHeld;
    // Start is called before the first frame update
    public void Start()
    {
        if (_physicsEnabled)
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
            _rb.bodyType = RigidbodyType2D.Dynamic;
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

        GetInteract();
    }

    public void GetInteract()
    {
        IInteractable interactable = null;
        int priority = -1;
        foreach (Collider2D col in triggers)
        {
            IInteractable temp = col.gameObject.GetComponent<IInteractable>();
            if (temp == null)
            {
                continue;
            }
            if (temp.Priority > priority)
            {
                interactable = temp;
                priority = temp.Priority;
            }
        }

        if (interactable != null)
        {
            interactable.Interact(this);
        }
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

    public void OnTriggerEnter2D(Collider2D col)
    {
        triggers.Add(col);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        triggers.Remove(col);
    }

}
