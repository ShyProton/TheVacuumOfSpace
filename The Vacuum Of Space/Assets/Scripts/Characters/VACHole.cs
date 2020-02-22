using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VACHole : MonoBehaviour
{
    public Animator vacimation;
    public Transform blackHole;
    public Transform capsuleBase;
    public Transform mainCam;
    public float holeAffectAngle;
    public float capsuleRotation = 20f;

    private bool mouseClicked;
    private GameObject[] affectedObjs;
    private float holeAffectVal;

    void Start()
    {
        affectedObjs = GameObject.FindGameObjectsWithTag("VACHole");
        holeAffectVal = Mathf.Cos(holeAffectAngle * Mathf.Deg2Rad);
    }

    void Update()
    {
        mouseClicked = Input.GetMouseButton(0);
        if (mouseClicked) vacimation.SetTrigger("MouseClicked"); else vacimation.ResetTrigger("MouseClicked");
    }

    void FixedUpdate()
    {
        if(mouseClicked)
        {
            foreach (GameObject g in affectedObjs)
            {
                Vector3 heading = g.GetComponent<Transform>().position - blackHole.position;
                float dot = Vector3.Dot(g.GetComponent<Transform>().forward, heading);

                if(dot > 0)
                {
                    Vector3 direction = blackHole.transform.position - g.transform.position;
                    g.GetComponent<Rigidbody>().AddForce(direction.normalized * Time.deltaTime * 1000);
                }
            }
        } 
    }

    void LateUpdate()
    {
        if(mouseClicked)
        {
            float angle = Quaternion.Slerp(capsuleBase.rotation, mainCam.rotation, capsuleRotation * Time.deltaTime).eulerAngles.y;
            capsuleBase.rotation = Quaternion.Euler(0, angle, 0);
        }
    }
}
