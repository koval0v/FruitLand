using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    private Animator _animator;
    private int _currentWaypointIndex = 0;
    private Rigidbody2D _rigidBody;

    [SerializeField] private Rigidbody2D player;
    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();
    [SerializeField] private float speed = 2f;

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        float countedSpeed = speed;
        Vector3 localScale = transform.localScale;
        if (Vector2.Distance(waypoints[_currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            _rigidBody.transform.localScale = new Vector3(-1f, 1f, 1f);
            _currentWaypointIndex++;
            if (_currentWaypointIndex >= waypoints.Count())
            {
                _currentWaypointIndex = 0;
                _rigidBody.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }

        if (((player.transform.position.x >= waypoints[0].transform.position.x && player.transform.position.x <= waypoints[1].transform.position.x) ||
            (player.transform.position.x >= waypoints[1].transform.position.x && player.transform.position.x <= waypoints[0].transform.position.x)) &&
            player.transform.position.y - transform.position.y < 0.5f)
        {
            _animator.SetBool("isRunning", true);
            countedSpeed = speed * 3;
            // flip chicken
            _rigidBody.transform.localScale = new Vector3(1f, 1f, 1f);
            // direct chicken to player
            if (player.transform.position.x > transform.position.x)
            {
                _currentWaypointIndex = waypoints.IndexOf(waypoints.OrderBy(x => x.transform.position.x).LastOrDefault());
                _rigidBody.transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else
            {
                _currentWaypointIndex = waypoints.IndexOf(waypoints.OrderBy(x => x.transform.position.x).FirstOrDefault());
                _rigidBody.transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            _animator.SetBool("isRunning", false);
            countedSpeed = speed;
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[_currentWaypointIndex].transform.position, Time.deltaTime * countedSpeed);
    }
}
