using UnityEngine;
using UnityEngine.UI;

public class MobileController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded = false;
    private bool isJumping = false;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    public Button jumpButton;
    public Button leftButton;
    public Button rightButton;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Butonlara tıklama eventlerini bağla
        jumpButton.onClick.AddListener(Jump);
        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);

        // Butonun bırakılması durumunda hareketi durdur
        leftButton.onClick.AddListener(StopMoving);
        rightButton.onClick.AddListener(StopMoving);
    }

    void Update()
    {
        // Yatay hareket kontrolü
        float moveDirection = 0f;
        if (isMovingLeft)
        {
            moveDirection = -1f;
        }
        else if (isMovingRight)
        {
            moveDirection = 1f;
        }
        Vector2 movement = new Vector2(moveDirection, 0f).normalized;
        rb.velocity = new Vector2(movement.x * moveSpeed, rb.velocity.y);

        // Yatay hareket animasyonu
        animator.SetFloat("Speed", Mathf.Abs(moveDirection));

        // Karakterin dönüşü
        if (moveDirection > 0)
        {
            transform.localScale = new Vector3(7.1f, 7.1f, 7.1f);
        }
        else if (moveDirection < 0)
        {
            transform.localScale = new Vector3(-7.1f, 7.1f, 7.1f);
        }

        // Zıplama kontrolü
        if (isGrounded && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false;
            isJumping = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Zemin teması kontrolü
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void Jump()
    {
        // Zıplama butonuna tıklandığında
        if (isGrounded)
        {
            isJumping = true;
        }
    }

    public void MoveLeft()
    {
        // Sol butona tıklandığında
        isMovingLeft = true;
        isMovingRight = false;
    }

    public void MoveRight()
    {
        // Sağ butona tıklandığında
        isMovingRight = true;
        isMovingLeft = false;
    }

    public void StopMoving()
    {
        // Butonun bırakılması durumunda
        isMovingLeft = false;
        isMovingRight = false;
    }
}
