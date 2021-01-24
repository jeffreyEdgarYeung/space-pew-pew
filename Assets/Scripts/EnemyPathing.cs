using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    [SerializeField] float moveSpeed = 2f;

    List<Transform> waypoints;

    int waypointIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIdx].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (waypointIdx < waypoints.Count)
        {
            var targetPos = waypoints[waypointIdx].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards
            (
                transform.position,
                targetPos,
                movementThisFrame
            );

            if (targetPos == transform.position)
                waypointIdx++;

        }
        else
        {
            Destroy(gameObject);
        }
    }
}
