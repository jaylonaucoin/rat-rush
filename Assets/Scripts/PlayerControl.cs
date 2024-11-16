using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody rb;
    public float moveSpeed = 13f;
    public float sprintSpeed = 20f;
    public float jumpForce = 5f;
    public float turnSpeed = 15f;
    public Transform cameraTransform;
    private Vector3 moveDirection;
    public Animation animation;
    private bool isGrounded;  // Add grounded check

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animation = GetComponent<Animation>();
        animation["jump"].speed = 0.5f;
        animation["run"].speed = 1f;
        animation["walk"].speed = 0.65f;
    }

    void Update()
    {
        // Get input for movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Calculate the forward direction relative to the camera
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Ensure the player stays grounded (ignore camera's Y-axis)
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Calculate the move direction based on input and camera direction
        moveDirection = (forward * moveZ + right * moveX).normalized;

        // Determine move speed (sprint if holding the sprint key)
        float currentSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
            animation.CrossFade("run");
        }
        else if (moveDirection != Vector3.zero)
        {
            currentSpeed = moveSpeed;
            animation.CrossFade("walk");
        }
        else
        {
            currentSpeed = 0f;
            animation.CrossFade("idle");
        }

        // Move the character
        if (moveDirection.magnitude >= 0.1f)
        {
            // Move the character
            rb.MovePosition(rb.position + moveDirection * currentSpeed * Time.deltaTime);

            // Rotate the character to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }

        // Jump logic, only jump when grounded
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            animation.CrossFade("jump");
            isGrounded = false;  // Disable further jumps until the player lands
        }
    }

    // Detect when the player is grounded
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    // Detect when the player leaves the ground
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
