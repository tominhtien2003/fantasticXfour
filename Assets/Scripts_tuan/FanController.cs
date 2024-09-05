using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;

    void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        transform.Rotate(direction * speed);
    }
}
