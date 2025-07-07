//using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
//public class Moovement_NEW_Input : MonoBehaviour
//{
//    public float moveSpeed = 5f;
//    public float jumpHeight = 1.5f;
//    public float gravity = -9.81f;

//    private CharacterController controller;
//    private Vector3 velocity;
//    private bool isGrounded;

//    public Transform groundCheck;
//    public float groundDistance = 0.4f;
//    public LayerMask groundMask;
//    private Rigidbody2D rb2d;

//    void Start()
//    {
//        rb2d = GetComponent<Rigidbody2D>();
//        controller = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

//        if (isGrounded && velocity.y < 0)
//            velocity.y = -2f;

//        float x = Input.GetAxis("Horizontal");
//        float z = Input.GetAxis("Vertical");

//        Vector3 move = transform.right * x + transform.forward * z;
//        controller.Move(move * moveSpeed * Time.deltaTime);

//        if (Input.GetButtonDown("Jump") && isGrounded)
//            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

//        velocity.y += gravity * Time.deltaTime;
//        controller.Move(velocity * Time.deltaTime);
//    }
//}
