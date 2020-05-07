using UnityEngine;

public class MobsMovementController : MonoBehaviour {
  [Header("Components")]
  [SerializeField] private float moveSpeed;
  [SerializeField] private AnimationController animationController;
  [SerializeField] private Rigidbody2D rigidbody2D;
  [SerializeField] private StateController stateController;
  
  private bool _isFacingRight = false;

  public bool IsFacingRight => _isFacingRight;

  public void Stop() {
    rigidbody2D.velocity = Vector2.zero;
    animationController.SetRunSpeedParameter(0f);
  }
  
  public void Move(Transform nearestEnemy = null) {
    if (nearestEnemy 
        && stateController.CheckState(State.Agroed) 
        && !nearestEnemy.GetComponent<StateController>().CheckState(State.Dead)) {
      ChaseTarget(nearestEnemy);
    } else {
      Patrol();
    }
  }

  private void ChaseTarget(Transform target) {
    animationController.SetRunSpeedParameter(moveSpeed);
    float movementDirection = _isFacingRight ? 1 : -1;
    float movement = movementDirection * moveSpeed * 5f * Time.deltaTime;
    
    rigidbody2D.velocity = new Vector2(movement, rigidbody2D.velocity.y);
  }

  private void Patrol() {
     animationController.SetRunSpeedParameter(0f);
  }

  public void Flip() {
    _isFacingRight = !_isFacingRight;
    
    transform.Rotate(0f, 180f, 0f);
  }
}
