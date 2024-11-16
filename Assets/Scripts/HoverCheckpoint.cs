using UnityEngine;

public class HoveringIcon : MonoBehaviour
{
    public float rotationSpeed = 50f; // Speed of rotation
    public float bounceHeight = 0.5f; // Height of bounce
    public float bounceSpeed = 2f;    // Speed of bounce

    private Vector3 originalPosition;

    void Start()
    {
        // Store the original position of the icon
        originalPosition = transform.position;
    }

    void Update()
    {
        // Rotate the icon
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);

        // Bounce the icon up and down
        float newY = Mathf.Sin(Time.time * bounceSpeed) * bounceHeight + originalPosition.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}