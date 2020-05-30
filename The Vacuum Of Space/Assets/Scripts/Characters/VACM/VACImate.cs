using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VACImate : MonoBehaviour
{
    //this class will be the main hub for controlling how VACM's parts moves due to external or user input, as well as how some objects will look based on interaction
    public Animator animControl;
    public Transform vacBase;
    public Transform vacHead;
    public Transform capsuleBase;
    public Transform mainCam;

    public static Vector3 inputDir = Vector3.zero;

    public float capsuleRotation = 15f;
    public float baseRotation = 20f;
    public float headRotation = 10f;

    private void Update()
    {
        bool mouseClick = Input.GetMouseButton(0) || Input.GetMouseButton(1);

        Movimation();
        Gravimation(mouseClick);
    }

    void Movimation()
    {
        Quaternion baseQuaternion = Quaternion.LookRotation(inputDir);
        vacBase.rotation = Quaternion.Slerp(vacBase.rotation, baseQuaternion, baseRotation * Time.deltaTime);
        vacHead.rotation = Input.GetMouseButton(0) ? vacHead.rotation : Quaternion.Slerp(vacHead.rotation, baseQuaternion, headRotation * Time.deltaTime);
    }

    void Gravimation(bool setting)
    {
        if (setting)
        {
            animControl.SetTrigger("MouseClicked");
            Quaternion capQuat = Quaternion.Slerp(capsuleBase.rotation, mainCam.rotation, capsuleRotation * Time.deltaTime);
            Quaternion headQuat = Quaternion.Slerp(vacHead.rotation, mainCam.rotation, headRotation * Time.deltaTime);
            capsuleBase.rotation = Quaternion.Euler(0, capQuat.eulerAngles.y, 0);
            vacHead.rotation = Quaternion.Euler(0, headQuat.eulerAngles.y, 0);
        }
        else
        {
            animControl.ResetTrigger("MouseClicked");
        }
    }
    /*
    void Start()
    {
        velocity = Vector3.zero;
    }

    void Update()
    {
        bool mouseClicked = Input.GetMouseButton(0);

        Movimation(velocity, velocity);
        Gravimation(mouseClicked);
    }

    public void Movimation(Vector3 moveVector, Vector3 velocity)
    {
        Quaternion baseQuaternion = Quaternion.LookRotation(velocity);
        vacBase.rotation = Quaternion.Slerp(vacBase.rotation, baseQuaternion, baseRotation * Time.deltaTime);
        vacHead.rotation = Input.GetMouseButton(0) ? vacHead.rotation : Quaternion.Slerp(vacHead.rotation, baseQuaternion, headRotation * Time.deltaTime);
    }

    public void Gravimation(bool setting)
    {
        if(setting)
        {
            animControl.SetTrigger("MouseClicked");
            Quaternion capQuat = Quaternion.Slerp(capsuleBase.rotation, mainCam.rotation, capsuleRotation * Time.deltaTime);
            Quaternion headQuat = Quaternion.Slerp(vacHead.rotation, mainCam.rotation, headRotation * Time.deltaTime);
            capsuleBase.rotation = Quaternion.Euler(0, capQuat.eulerAngles.y, 0);
            vacHead.rotation = Quaternion.Euler(0, headQuat.eulerAngles.y, 0);
        }
        else
        {
            animControl.ResetTrigger("MouseClicked");
        }
    }*/
}
