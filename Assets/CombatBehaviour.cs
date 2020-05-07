using UnityEngine;

public class CombatBehaviour : MonoBehaviour {
  [Header("Components")] 
  [SerializeField] private LayerMask enemyLayer;
  [SerializeField] private AnimationController animationController;
  [SerializeField] private MobsMovementController mobsMovementController;
  [SerializeField] private CombatController combatController;
  [SerializeField] private StateController stateController;

  [Header("Parameters")]
  [SerializeField] private float rangeOfAgro = 20f;
  [SerializeField] private float attackRate = 1.8f;

  private float _attackRange;
  private float _nextAttackTime = 0f;
  private Collider2D[] _spottedEnemies;
  private Vector3 _position;

  private void Start() {
    _attackRange = combatController.AttackRange;
  }
  
  private void Update() {
    if (stateController.CheckState(State.Stunned)) {
      return;
    }
    
    if (stateController.CheckState(State.Dead)) {
      combatController.enabled = false;
      mobsMovementController.enabled = false;
      return;
    }
    
    _position = transform.position;
    SpotEnemies();
  }

  private void SpotEnemies() {
    _spottedEnemies = Physics2D.OverlapCircleAll(_position, rangeOfAgro, enemyLayer);
    Vector2 currentPosition = _position;
    float minDist = Mathf.Infinity;
    Transform nearestEnemy = null;
    
    stateController.State = _spottedEnemies.Length != 0 ? State.Agroed : State.Active;
    animationController.ToggleAgroedAnimation(stateController.CheckState(State.Dead));

    foreach (Collider2D enemy in _spottedEnemies) {
      float dist = Vector2.Distance(enemy.transform.position, currentPosition); 
      if (dist < minDist) { 
        nearestEnemy = enemy.transform; 
        minDist = dist;
      }
    }

    if (nearestEnemy != null 
        && !nearestEnemy.CompareTag("GameController") 
        && Vector2.Distance(nearestEnemy.position, transform.position) <= _attackRange) {
      if (Time.time >= _nextAttackTime) { 
        mobsMovementController.Stop(); 
        combatController.Attack(); 
        _nextAttackTime = Time.time + attackRate;
      }
    } else { 
      mobsMovementController.Move(nearestEnemy);
    }

    if (nearestEnemy != null 
        && (nearestEnemy.position.x > transform.position.x && !mobsMovementController.IsFacingRight 
            || nearestEnemy.position.x < transform.position.x && mobsMovementController.IsFacingRight)) { 
      mobsMovementController.Flip();
    }
  }

  private void OnDrawGizmosSelected() {
    Gizmos.DrawWireSphere(transform.position, rangeOfAgro);
  }
}
