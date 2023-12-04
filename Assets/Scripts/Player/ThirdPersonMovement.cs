using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour//,IDamagable
{
    #region Private Components
    private CharacterController controller;
    #endregion

    [Header("Player Settings")]
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float SmooTime;
    [SerializeField] private Transform cam;

    #region private veriables
    private float horizontal;
    private float vertical;
    private float turnSmoothVelocity;
    #endregion

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    //void Start()
    //{
    //    Debug.Log(Application.persistentDataPath);

    //    //CheckpointData loadedData = CheckPointManager.Instance.LoadCheckpoint();


    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, SmooTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log(hit.collider.name);
    }
}
