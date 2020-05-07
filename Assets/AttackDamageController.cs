using System;
using UnityEngine;
using UnityEngine.Events;

public class AttackDamageController : MonoBehaviour {
  [Header("Parameters")]
  [SerializeField] private float rangeOfImpact;
  [SerializeField] private float damage;

  [Header("Events")]
  [SerializeField] private UnityEvent ImpactEvent;

  private CombatController combatController;
  private LayerMask enemyLayers;

  public float RangeOfImpact => rangeOfImpact;

  public CombatController CombatController {
    get => combatController;
    set => combatController = value;
  }

  private void Start() {
    combatController = combatController ? combatController : GetComponentInParent<CombatController>();
    enemyLayers = combatController.EnemyLayers;

    damage = damage != 0 ? damage : combatController.AttackDamage;

    OnEnable();
  }

  private void OnEnable() {
    Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, rangeOfImpact, enemyLayers);

    foreach (Collider2D hitObject in hitObjects) {
      hitObject.GetComponent<CombatController>().TakeDamage(damage);
      ImpactEvent.Invoke();
    }
  }

  private void OnDrawGizmosSelected() {
    Gizmos.DrawWireSphere(transform.position, rangeOfImpact);
  }
}
