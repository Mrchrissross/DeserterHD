using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {

    public float pickUpDistance = 5.0f;
    private Transform target;
    private int targetMask;
    private bool targetVisible;

    void Start ()
    {
        targetVisible = false;
        targetMask = (1 << LayerMask.NameToLayer("PickUps"));
    }

    void Update ()
    {
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, pickUpDistance, targetMask))
        {
            target = hit.transform;
            targetVisible = true;
        }
        else
        {
            target = null;
            targetVisible = false;
        }  

        if (targetVisible == true)
            ItemManager.visibleItem = target.name;
        else
            ItemManager.visibleItem = "";
    }
}
