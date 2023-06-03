using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    [SerializeField] float speed = 1.0f;
    private int waypointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        Debug.Log(waypointIndex);
        Debug.Log(transform.position);
        Debug.Log(waypoints[waypointIndex].transform.position);
        if (Vector2.Distance(transform.position, waypoints[waypointIndex].transform.position) < 0.01)
        {
            
            waypointIndex++;
            if (waypointIndex >= waypoints.Length) waypointIndex = 0;
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, Time.deltaTime * speed);
    }
}
