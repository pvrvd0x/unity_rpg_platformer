using System;
using System.Collections;
using UnityEngine;

public class MovementController : MonoBehaviour {
  [Header("Components")]
  
  [SerializeField] private BoxCollider2D boxCollider;
  [SerializeField] private Rigidbody2D rigidbody2D;
  [SerializeField] private AnimationController _playerAnimationController;
  [SerializeField] private LayerMask groundLayers;
  [SerializeField] private StateController stateController;

  [Header("Parameters")] 
  
  [SerializeField] private float movementSpeed = 100f;
  [SerializeField] private float jumpForce = 100f;
  [SerializeField] private float rollSpeed = 100f;

  private float _currentRollSpeed;

  private bool _isFacingRight = true;
  private bool _isRolling = false;
  private bool _isGrounded = true;
  private bool _wasGrounded;

  public bool IsFacingRight => _isFacingRight;

    private void Start() {
    _currentRollSpeed = rollSpeed;
  }

    private void Update() {
      if (stateController.CheckState(State.Stunned)
          || stateController.CheckState(State.Dead)
          || stateController.CheckState(State.Moveless))
        return;
      
      Jump();
      Roll();
      GetDown();
    }

    private void FixedUpdate() {
    if (stateController.CheckState(State.Stunned)
        || stateController.CheckState(State.Dead)
        || stateController.CheckState(State.Moveless))
      return;
    
    Move();
    }

  private void Move() {
    float movementDirection = Input.GetAxisRaw("Horizontal");
    float movement = movementDirection * movementSpeed * 5f * Time.deltaTime;
    
    rigidbody2D.velocity = new Vector2(movement, rigidbody2D.velocity.y);
    _playerAnimationController.SetRunSpeedParameter(Mathf.Abs(movementDirection * movementSpeed));

    if (movementDirection > 0 && !_isFacingRight || movementDirection < 0 && _isFacingRight)
      Flip();
  }

  private void Roll() {
    if (Input.GetKeyDown(KeyCode.LeftControl) && !_isRolling) {
      _isRolling = true;
      stateController.State = State.Invulnerable;
      _playerAnimationController.EnableRollAnimation();
    }
    
    if (!_isRolling) return;
    
    int direction = _isFacingRight ? 1 : -1;
    transform.position += new Vector3(direction * _currentRollSpeed / 10f * Time.deltaTime, 0f, 0f);
    _currentRollSpeed -= _currentRollSpeed * 10f * Time.deltaTime;
    
    if (_currentRollSpeed <= 20f) {
      _isRolling = false;
      _currentRollSpeed = rollSpeed;
      stateController.State = State.Active;
    }
  }

  private void Jump() {
    _wasGrounded = _isGrounded;
    _isGrounded = false;
    CheckIfGrounded();

    if (Input.GetButtonDown("Jump") && _isGrounded) {
      rigidbody2D.velocity = Vector2.up * jumpForce;
    }
    
    _playerAnimationController.TriggerJumpAnimation(!_wasGrounded);
  }
  
  private void GetDown() {
    if (Input.GetKeyDown(KeyCode.S) && !_isRolling) {
      gameObject.layer = LayerMask.NameToLayer("player through platform");
    }
    // gameObject.layer = LayerMask.NameToLayer("player");
  }

  private void CheckIfGrounded() {
    Bounds bounds = boxCollider.bounds;
    RaycastHit2D raycastHit2D = 
        Physics2D.BoxCast(
            bounds.center,
            bounds.size,
            0f,
            Vector2.down,
            .3f,
            groundLayers);

    _isGrounded = raycastHit2D.collider != null;
  }

  private void Flip() {
    _isFacingRight = !_isFacingRight;
    
    transform.Rotate(0f, 180f, 0f);
  }
}
