using UnityEngine;

public class Fireblast : MonoBehaviour {
  [Header("Parameters")] 
  [SerializeField] private float knockbackForce;

  private AttackDamageController attackDamageController;
  private CombatController combatController;

  private void Start() {
    attackDamageController = GetComponent<AttackDamageController>();
    combatController = attackDamageController.CombatController;
  }

  public void KnockBack() {
    Collider2D[] hitTargets = Physics2D.OverlapCircleAll(transform.position, attackDamageController.RangeOfImpact, combatController.EnemyLayers);

    foreach (Collider2D hitTarget in hitTargets) {
      var direction = hitTarget.transform.position - transform.position;
      direction.Normalize();
      hitTarget.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Round(direction.x) * knockbackForce, knockbackForce));
    }
  }
}
