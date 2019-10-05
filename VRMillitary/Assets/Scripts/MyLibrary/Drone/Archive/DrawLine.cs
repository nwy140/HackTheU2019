using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    public GameObject origin;
    public GameObject target;

    private GameObject defaultTarget;

    public bool targetIsChild;

    // Use this for initialization

    void Awake()
    {
        defaultTarget = target;
    }

    void Update()
    {
        LineRenderer line = GetComponent<LineRenderer>();

        line.SetPosition(0, origin.transform.position);
        line.SetPosition(1, target.transform.position);

        if (targetIsChild)
        {
            if (transform.childCount > 0)
            {
                SetTarget(transform.GetChild(0).gameObject);
            }
            else
            {
                ResetTarget();
            }
        }
    }

    public void SetTarget(GameObject target)
    {

        this.target = target;
    }

    public void ResetTarget()
    {
        target = defaultTarget;
    }



}
