using UnityEngine;

public class Projectile : MonoBehaviour {
  [Header("Components")] 
  [SerializeField] private Transform impactEffectPrefab;
  [SerializeField] private Collider2D collider;
  
  [Header("Parameters")] 
  [SerializeField] private float flightSpeed;
  [SerializeField] private float damage;

  private GameObject spawnOrigin;
  private CombatController combatController;
  private float direction;

  public GameObject SpawnOrigin {
    get => spawnOrigin;
    set => spawnOrigin = value;
  }

  private void Start() {
    direction = spawnOrigin.GetComponent<MovementController>().IsFacingRight ? 1 : -1;
    combatController = spawnOrigin.GetComponent<CombatController>();
    Physics2D.IgnoreCollision(spawnOrigin.GetComponent<Collider2D>(), collider);
  }

  private void Update() {
    transform.position += new Vector3( direction * flightSpeed / 100, 0, 0);
  }

  private void OnCollisionEnter2D(Collision2D other) {
    Destroy(gameObject);
    Transform impactEffect = Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
    impactEffect.GetComponent<AttackDamageController>().CombatController = combatController;
  }
}
