using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    bool alive = true;

    public float speed = 5;
    [SerializeField] Rigidbody rb;

    int horizontalInput;
    [SerializeField] float horizontalMultiplier = 2;

    public float speedIncreasePerPoint = 0.1f;

    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;


    private void FixedUpdate()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        

        //if (horizontalInput == 0)
        //    horizontalMove += Vector3.left * horizontalMultiplier;
        //else if (horizontalInput == 2)
        //    horizontalMove += Vector3.right * horizontalMultiplier;

        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Update()
    {
       // horizontalInput = Input.GetAxis("Horizontal");

        if (SwipeController.swipeRight)
        {
            if (horizontalInput < 2)
            {
                horizontalInput++;
            }
            else horizontalInput = 0;
        }

        if (SwipeController.swipeLeft)
        {
           
            if (horizontalInput > -1)
            {
                horizontalInput--;
            }
            else horizontalInput = 0;
        }

        
        if (SwipeController.swipeUp)
        {
            Jump();
        }

      
        if (transform.position.y < -5)
        {
            Die();
        }
    }

    public void Die()
    {
        alive = false;
        // Restart the game
        Invoke("Restart", 2);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Jump()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        rb.AddForce(Vector3.up * jumpForce);
    }

}