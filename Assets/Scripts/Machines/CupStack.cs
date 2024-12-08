using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CupStack : MonoBehaviour
{
    [SerializeField] private GameObject _cup;
    // Start is called before the first frame update
    void OnEnable()
    {
        DragController.StartClickAction += Click;
    }

    void OnDisable()
    {
        DragController.StartClickAction -= Click;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click(GameObject gO)
    {
        if (!gO)
        {
            return;
        }

        if (gameObject.Equals(gO))
        {
            GetCup();
        }
    }

    public void GetCup()
    {
        GameObject gO = Instantiate(_cup);
        gO.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(DragController.mousePos);
        CoroutineUtils.ExecuteAfterEndOfFrame(() => DragController._instance.HoldObject(gO), this);
    }
}
