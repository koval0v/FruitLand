using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 360 * _speed * Time.deltaTime);
    }
}
