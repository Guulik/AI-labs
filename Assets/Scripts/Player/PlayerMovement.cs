using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed;
        private Rigidbody2D _rb2D; 
        private Vector2 _moveDirection;
        private PlayerInput _playerInput;
        private void Awake()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
            _rb2D = gameObject.GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            ProcessInput();
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void ProcessInput()
        {
            var movement = _playerInput.Player.Move.ReadValue<Vector2>();
            _moveDirection = new Vector2(movement.x, movement.y).normalized;
        }

        private void Move()
        {
            _rb2D.velocity = new Vector2(_moveDirection.x * movementSpeed, _moveDirection.y * movementSpeed);
        }
    }
}
