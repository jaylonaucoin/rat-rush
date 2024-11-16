using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    public Transform[] waypoints; // Array of waypoints
    public float moveSpeed = 3f;  // Speed of the boat
    public float rotationSpeed = 2.5f; // Speed of rotation
    private int currentWaypointIndex = 0;

    void Update()
    {
        // Check if there are waypoints
        if (waypoints.Length == 0) return;

        // Get the current target waypoint
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Calculate direction to the waypoint
        Vector3 directionToWaypoint = targetWaypoint.position - transform.position;

        // Rotate the boat to face the waypoint
        Quaternion targetRotation = Quaternion.LookRotation(directionToWaypoint);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move the boat forward
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        // Check if the boat has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform); // Parent the player to the boat
        }
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null); // Unparent the player when they leave the boat
        }
    }
}