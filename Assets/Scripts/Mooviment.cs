using UnityEngine;

public class Mooviment : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public int speed = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            var newPosition = transform.position + Vector3.right * horizontal * Time.deltaTime * speed;
            rb2d.MovePosition(newPosition);
        }

        if (rb2d.linearVelocity.y == 0 && Input.GetButtonDown("Jump"))
        {
            rb2d.AddForce(Vector2.up * 0.3f, ForceMode2D.Impulse);
        }
    }
}
