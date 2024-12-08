using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerControls;

    public static DragController _instance;

    public InputActionMap playerActionMap;
    public InputAction playerDragAction;
    public InputAction playerPointAction;

    public static event Action<GameObject> ClickAction;
    public static event Action<GameObject> StartClickAction;

    public static Vector2 mousePos;
    public static Vector2 prevMousePos;

    public float clickEdge = 0.25f;

    public DragAndDrop heldObject;
    public GameObject topObject;

    private float _mouseDown;


    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;

        playerActionMap = _playerControls.FindActionMap("Drag and Drop");
        playerDragAction = playerActionMap.FindAction("Click");
        playerPointAction = playerActionMap.FindAction("Pointer");

    }

    void OnEnable()
    {
        playerDragAction.Enable();
        playerDragAction.started += MouseDown;
        playerDragAction.performed += MousePressed;
        playerDragAction.canceled += MouseUp;

        playerPointAction.Enable();
        mousePos = playerPointAction.ReadValue<Vector2>();
        prevMousePos = mousePos;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = playerPointAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        prevMousePos = mousePos;
    }

    void OnDisable()
    {
        playerDragAction.Disable();
        playerDragAction.started -= MouseDown;
        playerDragAction.performed -= MousePressed;
        playerDragAction.canceled -= MouseUp;

        playerPointAction.Disable();
    }

    private void MouseDown(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse Down");
        _mouseDown = Time.time;
        Ray mouseRay = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
        if (hit)
        {
            topObject = hit.collider.transform.gameObject;
            HoldObject(topObject);
            
        }
        StartClickAction?.Invoke(topObject);
    }

    public void HoldObject(GameObject gO)
    {
        DragAndDrop drag = gO.GetComponent<DragAndDrop>();
        if (drag)
        {
            heldObject = drag;
            heldObject.PickUp();
        }
    }

    private void MousePressed(InputAction.CallbackContext context)
    {
        Debug.Log("Performed!");
    }

    private void MouseUp(InputAction.CallbackContext context)
    {
        Debug.Log("Canceled!");
        if ((Time.time - _mouseDown) <= clickEdge)
        {
            Debug.Log("Just a click");
            if (topObject)
            {
                ClickAction?.Invoke(topObject);
            }
        }
        else
        {
            Debug.Log("Drag!");
            if (heldObject)
            {
                heldObject.Drop();
                heldObject = null;
            }
        }

        topObject = null;
    }
}
