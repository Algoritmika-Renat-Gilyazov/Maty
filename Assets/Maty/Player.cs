using UnityEngine;

namespace Maty
{
    public class Player : MonoBehaviour
    {
        [Header("Movement Speed")]
        [Tooltip("How fast the player moves")]
        public float movementSpeed;
        
        [Header("Transform Component")]
        [Tooltip("The transform of the player")]
        [SerializeField]
        Transform playerTransform;
        
        [Header("Rigidbody Component")]
        [Tooltip("The rigidbody of the player")]
        [SerializeField]
        Rigidbody rb;
        
        [Header("Mouse Sensitivity")]
        [Tooltip("How large the player mouse sensitivity")]
        [SerializeField]
        float mouseSensitivity;
        
        private float targetYaw;
        private float currentYaw;
        private float yawVelocity;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            targetYaw = transform.eulerAngles.y;
            currentYaw = targetYaw;
        }

        void FixedUpdate()
        {
            float moveX = Input.GetAxis("Horizontal"); 
            float moveY = Input.GetAxis("Vertical");   
    
            Vector3 direction = new(moveX, 0, moveY);
            direction.Normalize();

            // Поворачиваем направление движения в соответствии с текущим поворотом игрока
            direction = transform.rotation * direction;

            rb.MovePosition(rb.position + direction * (movementSpeed * Time.fixedDeltaTime));
    
            HandleRotation();
        }
        
        private void HandleRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            
            targetYaw += mouseX;

            currentYaw = Mathf.SmoothDampAngle(currentYaw, targetYaw, ref yawVelocity, 0f);

            
            transform.localRotation = Quaternion.Euler(0f, currentYaw, 0f);
        }
        
        public void SetYaw(float angle)
        {
            targetYaw = angle;
            currentYaw = angle;
            yawVelocity = 0f;
            transform.localRotation = Quaternion.Euler(0f, currentYaw, 0f);
        }
        
        public float GetYaw() => currentYaw;
    }
}