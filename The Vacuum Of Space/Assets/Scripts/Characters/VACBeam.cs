using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VACBeam : MonoBehaviour
{
    public GameObject laserPrefab;
    public GameObject firePoint;

    private GameObject spawnedLaser;

    void Start()
    {
        spawnedLaser = Instantiate(laserPrefab, firePoint.transform) as GameObject;
        SetLaser(false);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SetLaser(true);
        }

        if(Input.GetMouseButton(0))
        {
            UpdateLaser();
        } 
        else
        {
            SetLaser(false);
        }
    }

    void SetLaser(bool setting)
    {
        spawnedLaser.SetActive(setting);
    }

    void UpdateLaser()
    {
        if(firePoint != null)
        {
            spawnedLaser.transform.position = firePoint.transform.position;
        }
    }
}
