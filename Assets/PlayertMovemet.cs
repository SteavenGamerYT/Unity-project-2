using UnityEngine;

public class PlayertMovemet : MonoBehaviour {

    #region Variables
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = 9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 Velocity;
    bool isGrounded;
#endregion

    #region Unity Methods
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && Velocity.y < 0)
		{
            Velocity.y = -2f;
		}
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed *Time.deltaTime);

        if(Input.GetButtonDown("Jump") && isGrounded)
		{
            Velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}

        Velocity.y += gravity * Time.deltaTime;

        controller.Move(Velocity * Time.deltaTime);
    }
      #endregion
}
