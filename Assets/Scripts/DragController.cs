using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _playerControls;

    public InputActionMap playerActionMap;
    public InputAction playerDragAction;
    public InputAction playerPointAction;

    public static Vector2 mousePos;
    public static Vector2 prevMousePos;

    public DragAndDrop heldObject;


    // Start is called before the first frame update
    void Awake()
    {
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
        Ray mouseRay = Camera.main.ScreenPointToRay(Mouse.current.position.value);
        RaycastHit2D hit = Physics2D.Raycast(mouseRay.origin, mouseRay.direction);
        if (hit)
        {
            DragAndDrop drag = hit.transform.gameObject.GetComponent<DragAndDrop>();
            if (!drag)
            {
                return;
            }

            heldObject = drag;
            Debug.Log(heldObject);
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
        if (heldObject)
        {
            heldObject.Drop();
            heldObject = null;
        }
    }
}
