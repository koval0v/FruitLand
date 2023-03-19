using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollowerInOneWay : MonoBehaviour
{
    [SerializeField] private GameObject _startPoint;
    [SerializeField] private GameObject _endPoint;
    [SerializeField] private float _speed = 2f;

    // Update is called once per frame
    private void Update()
    {
        if (transform.position.x == _endPoint.transform.position.x)
        {
            transform.position = new Vector2(_startPoint.transform.position.x, _startPoint.transform.position.y);
        }

        transform.position = Vector2.MoveTowards(transform.position, _endPoint.transform.position, Time.deltaTime * _speed);
    }
}
