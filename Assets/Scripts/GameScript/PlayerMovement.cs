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

    [SerializeField] float jumpForce = 600f;
    [SerializeField] LayerMask groundMask;

    public Transform ball;
    public Transform arms; 
    public Transform posOverHead;
    public Transform posDribble;
    public Transform target;
    private bool isBallInHands = true;
    private bool isBallFlying = false;
    private float T = 0;

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

        // ball in hands
        if (isBallInHands)
        {

            // hold over head
            if (SwipeController.swipeDown)
            {
                ball.position = posOverHead.position;
                arms.localEulerAngles = Vector3.right * 180;

                // look towards the target
                transform.LookAt(target.parent.position);
            }
            // dribbling
            else
            {
                ball.position = posDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
                arms.localEulerAngles = Vector3.right * 0;
            }

            // throw ball
            if (SwipeController.swipeUp)
            {
                isBallInHands = false;
                isBallFlying = true;
                T = 0;
            }
        }

        // ball in the air
        if (isBallFlying)
        {
            T += Time.deltaTime;
            float duration = 0.66f;
            float t01 = T / duration;

            // move to target
            Vector3 A = posOverHead.position;
            Vector3 B = target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            // move in arc
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

            ball.position = pos + arc;

            // moment when ball arrives at the target
            if (t01 >= 1)
            {
                isBallFlying = false;
                ball.GetComponent<Rigidbody>().isKinematic = false;
            }
        }

    }
        public void Die()
        {
            alive = false;
            // Restart the game
            GameManager.inst.YouLoose();
            //Invoke("Restart", 2);
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

    private void OnTriggerEnter(Collider other)
    {

        if (!isBallInHands && !isBallFlying)
        {
            
            isBallInHands = true;
            ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}