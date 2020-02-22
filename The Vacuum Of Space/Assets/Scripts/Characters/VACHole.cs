using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VACHole : MonoBehaviour
{
    public Animator vacimation;

    private bool mouseClicked;

    void Start()
    {
        
    }

    void Update()
    {
        mouseClicked = Input.GetMouseButtonDown(0);

        if(Input.GetMouseButton(0))
        {
            vacimation.SetTrigger("MouseClicked");
        }

        if(!Input.GetMouseButton(0))
        {
            vacimation.ResetTrigger("MouseClicked");
        }
    }
}
