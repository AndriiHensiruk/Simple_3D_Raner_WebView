using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    bool alive = true;
    
    public float speed = 5;
  
    [SerializeField] Rigidbody rb;

    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 200;

    public float speedIncreasePerPoint = 0.1f;

    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        if (!alive) return;
        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
       //Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
      
        rb.MovePosition(rb.position + forwardMove);
    }

    private void Update()
    {
         
        if (SwipeController.swipeRight)
        {
            rb.AddForce(horizontalMultiplier * Time.deltaTime, 0f, 0f, ForceMode.VelocityChange);
        }

        if (SwipeController.swipeLeft)
        {
            rb.AddForce(-horizontalMultiplier * Time.deltaTime, 0f, 0f, ForceMode.VelocityChange);
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