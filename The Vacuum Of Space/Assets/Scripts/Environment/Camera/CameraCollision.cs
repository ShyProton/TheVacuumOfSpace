using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float minDist = 1.0f;
    public float maxDist = 4.0f;
    public float smooth = 10.0f;

    private Vector3 dollyDir;
    private float distance;

    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    void Update()
    {
        Vector3 desiredPos = transform.parent.TransformPoint(dollyDir * maxDist);
        RaycastHit hit;

        if (Physics.Linecast(transform.parent.position, desiredPos, out hit))
        {
            distance = Mathf.Clamp((hit.distance * 0.9f), minDist, maxDist);
        } 
        else
        {
            distance = maxDist;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);
    }
}
