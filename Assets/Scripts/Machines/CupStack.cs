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
        gO.transform.parent = gameObject.transform.parent;
        Vector2 vec = (Vector2)Camera.main.ScreenToWorldPoint(DragController.mousePos);
        gO.transform.position = new Vector3(vec.x, vec.y, -10);
        CoroutineUtils.ExecuteAfterEndOfFrame(() => DragController._instance.HoldObject(gO), this);
    }
}
