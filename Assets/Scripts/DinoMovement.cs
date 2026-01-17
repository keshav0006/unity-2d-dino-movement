using UnityEngine;

public class DinoMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;
    public Animator animator;
    private bool isGrounded = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        if (move > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (move < 0) transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
        }

        animator.SetFloat("Speed", Mathf.Abs(move));
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = false;
    }
}
