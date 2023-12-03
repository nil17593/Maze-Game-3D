using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private bool blockMouse = true;
    [SerializeField] private Camera Cam;

    [Header("Rotation speed For Button Movement")]
    [SerializeField] private float rotationSpeed;

    #region private fields
    private float mouseX;
    private float horizontal;
    private float vertical;
    private Animator animator;
    private Rigidbody rb;
    Vector3 Movement;
    #endregion


    void Start()
    {
        BlockMouse();
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        //WalkAnimation();
        MoveCharacter();
    }

    private void FixedUpdate()
    {
        rb.velocity = Movement * speed * Time.fixedDeltaTime;
    }

    //Moves character by using camera rotation
    void MoveCharacter()
    {
        Movement = Cam.transform.right * horizontal + Cam.transform.forward * vertical;
        Movement.y = rb.velocity.y;

        if (Movement.magnitude != 0f)
        {
            //transform.Rotate(Vector3.up * mouseX * Cam.GetComponent<ThirdPersonCamera>().sensivity * Time.deltaTime);

            Quaternion CamRotation = Cam.transform.rotation;
            CamRotation.x = 0f;
            CamRotation.z = 0f;

            rb.rotation = Quaternion.Lerp(rb.rotation, CamRotation, 0.1f);

        }
    }

    //enebling and disabling walk animation
    void WalkAnimation()
    {
        if (horizontal != 0 || vertical != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }


    //function to block Mouse Cursor
    void BlockMouse()
    {
        if (!blockMouse)
        {
            return;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
