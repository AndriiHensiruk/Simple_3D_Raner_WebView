using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 move;
    public float forwardSpeed;
    public float maxSpeed;

    private int desiredLane = 1;//0:left, 1:middle, 2:right
    public float laneDistance = 2.5f;//The distance between tow lanes

    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public float gravity = -12f;
    public float jumpHeight = 2;
    private Vector3 velocity;

    public Animator animator;
    private bool isSliding = false;

    public float slideDuration = 1.5f;

    bool toggle = false;

    public Transform ball;
    public Transform arms; 
    public Transform posOverHead;
    public Transform posDribble;
    public Transform target;
    private bool isBallInHands = true;
    private bool isBallFlying = false;
    private float T = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1.2f;
    }

    private void FixedUpdate()
    {
       
        //Increase Speed
        if (toggle)
        {
            toggle = false;
            if (forwardSpeed < maxSpeed)
                forwardSpeed += 0.1f * Time.fixedDeltaTime;
        }
        else
        {
            toggle = true;
            if (Time.timeScale < 2f)
                Time.timeScale += 0.005f * Time.fixedDeltaTime;
        }
    }

    private void Update()
    {
        move.z = forwardSpeed;

        isGrounded = Physics.CheckSphere(groundCheck.position, 0.17f, groundLayer);
        if (isGrounded && velocity.y < 0)
            velocity.y = -1f;


        if (isGrounded)
        {
            if (SwipeController.swipeUp)
                Jump();
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        //Gather the inputs on which lane we should be
        if (SwipeController.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
                desiredLane = 2;
        }
        if (SwipeController.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
                desiredLane = 0;
        }

        //Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;

        //transform.position = targetPosition;
        if (transform.position != targetPosition)
        {
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 30 * Time.deltaTime;
            if (moveDir.sqrMagnitude < diff.magnitude)
                controller.Move(moveDir);
            else
                controller.Move(diff);
        }

        controller.Move(move * Time.deltaTime);
    



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
            controller.center = Vector3.zero;
            controller.height = 2;
            isSliding = false;

            velocity.y = Mathf.Sqrt(jumpHeight * 2 * -gravity);
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