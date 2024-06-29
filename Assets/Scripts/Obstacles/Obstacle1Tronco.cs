using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle1 : MonoBehaviour
{

    [SerializeField] float speedRot;

    private void Update()
    {
        Vector3 rote = new Vector3(0, 1, 0);
        this.transform.Rotate(rote * speedRot * Time.deltaTime);
    }
}
