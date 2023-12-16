using System.Collections;
using UnityEngine;

namespace RoninLabs.Maze3D
{
    /// <summary>
    /// Third person movment attached on player
    /// it yakes cinamachine to move dyanamically in maze world
    /// </summary>
    public class ThirdPersonMovement : MonoBehaviour, IDamagable
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
        [SerializeField] private AnimationCurve dashCurve; // Animation curve for easing
        [SerializeField] private float dashDistance = 5f; // Distance to dash
        [SerializeField] private float dashDuration = 0.5f; // Duration of the dash
        [SerializeField] private float dashSpeed = 20f; // Speed of the dash

        private bool isDashing = false;
        private float dashTimer = 0f;
        private Vector3 dashStartPosition;
        private Vector3 dashTargetPosition;
        [SerializeField] float easeAmount = 1.5f;
        private void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            if (GameManager.Instance.IsGameOver)
                return;

            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
            if (direction.magnitude >= 0.1f)//&& !isDash)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, SmooTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                controller.Move(moveDir.normalized * speed * Time.deltaTime);

            }

            if (Input.GetKeyDown(KeyCode.Space))// && !isDashing)
            {
                StartCoroutine(Dashw());
                //StartDash();
            }

            //if (isDashing)
            //{
            //    Dash();
            //}
        }


        void StartDash()
        {
            isDashing = true;
            dashTimer = 0f;
            dashStartPosition = transform.position;
            dashTargetPosition = transform.position + transform.forward * dashDistance;
        }

        void Dash()
        {
            float dashProgress = dashTimer / dashDuration;
            float easedProgress = dashCurve.Evaluate(dashProgress); // Using the curve for easing

            transform.position = Vector3.Lerp(dashStartPosition, dashTargetPosition, easedProgress);
            dashTimer += Time.deltaTime;

            if (dashTimer >= dashDuration)
            {
                isDashing = false;
            }
        }

        // Easing function (EaseInOutQuad)
        float CustomEase(float t, float easeAmount)
        {
            float a = Mathf.Pow(t, easeAmount) / (Mathf.Pow(t, easeAmount) + Mathf.Pow(1 - t, easeAmount));
            return a;
        }

        IEnumerator Dashw()
        {
            float startTime = Time.time;
            float elapsedTime = 0f;

            while (elapsedTime < dashDuration)
            {
                float t = elapsedTime / dashDuration;
                t = CustomEase(t, easeAmount); // Applying custom easing function

                controller.Move(transform.forward * dashSpeed * t * Time.deltaTime);

                elapsedTime = Time.time - startTime;
                yield return null;
            }
        }
            //isDashing = true;

            //Vector3 originalPosition = transform.position;
            //float startTime = Time.time;

            //while (Time.time < startTime + dashDuration)
            //{
            //    controller.Move(transform.forward * speed*1.5f * dashDistance * Time.deltaTime);
            //    yield return null; // Wait for the next frame
            //}

            //isDashing = false;
            //float CustomEase(float t, float easeAmount)
            //{
            //    float a = Mathf.Pow(t, easeAmount) / (Mathf.Pow(t, easeAmount) + Mathf.Pow(1 - t, easeAmount));
            //    return a;
            //}

            //IEnumerator Dashw()
            //{
            //    float startTime = Time.time;
            //    float elapsedTime = 0f;

            //    while (elapsedTime < dashDuration)
            //    {
            //        float t = elapsedTime / dashDuration;
            //        t = CustomEase(t, easeAmount); // Applying custom easing function

            //        controller.Move(transform.forward * dashSpeed * t * Time.deltaTime);

            //        elapsedTime = Time.time - startTime;
            //        yield return null;
            //    }
            //}
        //}
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("CheckPoint"))
            {
                GameManager.Instance.RemoveCheckPoint(other.gameObject);
                Vector3 playerPosition = transform.position;
                float elapsedTime = ScoreManager.Instance.GetLastSavedTime();
                CheckpointData checkpointData = new CheckpointData
                {
                    elapsedTime = elapsedTime,
                    playerPosition = playerPosition,
                };
                EventManager.Instance.TriggerCheckpointReachedEvent(checkpointData); // Trigger the checkpoint reached event
            }
        }

        //interface method
        public void TakeDamage()
        {
            GameManager.Instance.GameOver();
        }
    }
}