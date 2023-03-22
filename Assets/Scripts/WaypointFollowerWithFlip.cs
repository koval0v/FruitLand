using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollowerWithFlip : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private int _currentWaypointIndex = 0;

    [SerializeField] private GameObject[] _waypoints;
    [SerializeField] private float _speed = 2f;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(_waypoints[_currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            _rigidBody.transform.localScale = new Vector3(-1f, 1f, 1f);
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= _waypoints.Length)
            {
                _currentWaypointIndex = 0;
                _rigidBody.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, _waypoints[_currentWaypointIndex].transform.position, Time.deltaTime * _speed);
    }
}
