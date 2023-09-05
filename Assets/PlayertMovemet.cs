using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;
    public float gravity = 9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Text deathCountText;

    private Vector3 velocity;
    private bool isGrounded;
    private int deathCount = 0;
    private Vector3 spawnPosition = new Vector3(9.358128f, 3.96f, 1.979087f);

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DangerousCube"))
        {
            Die();
        }
    }

    public void Die()
    {
        deathCount++;
        Debug.Log("Player has died! Death Count: " + deathCount);
        deathCountText.text = "Deaths: " + deathCount.ToString();

        controller.enabled = false;
        transform.position = spawnPosition;
        controller.enabled = true;
    }

    public void SetSpawnPosition(Vector3 position)
    {
        spawnPosition = position;
    }
}