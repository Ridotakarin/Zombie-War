using System;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] private float spinSpeed= 5f;

    void Update()
    {
        transform.Rotate(0, spinSpeed*Time.deltaTime, 0);    
    }
}
