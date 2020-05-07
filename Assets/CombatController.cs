using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class CombatController : MonoBehaviour {
  [Header("Components")]
  [SerializeField] private AnimationController animationController;
  [SerializeField] private Collider2D deathDisableCollider;
  [SerializeField] private LayerMask enemyLayers;
  [SerializeField] private Rigidbody2D rigidbody;
  [SerializeField] private BarStatsController healthBar;
  [SerializeField] private BarStatsController manaBar;
  [SerializeField] private StateController stateController;

  [Header("Parameters")]
  [SerializeField] private float maxHealth;
  [SerializeField] private float baseAttackDamage;
  [SerializeField] private float attackRange;
  [SerializeField] private float attackRate = 2f;
  [SerializeField] [Range(1f, 100f)] private float critChance = 20f;
  [SerializeField] [Range(1f, 100f)] private float healthRegen = 5f;
  [SerializeField] private float healthRegenTimeout = 0f;

  [Header("Events")]
  [SerializeField] private UnityEvent DeathEvent;

  private float healthRegenTimeoutCoef = 2f;
  private float _nextAttackTime = 0f;
  private float _currentHealth;
  private float _comboCounter = 1;
  private float _attackDamage;
  
  public float AttackRange => attackRange;
  public float AttackDamage => _attackDamage;

  public LayerMask EnemyLayers => enemyLayers;
  public float NextAttackTime {
    get => _nextAttackTime;
    set => _nextAttackTime = value;
  }
  public float AttackRate => attackRate;

  private void Start() {
    _currentHealth = maxHealth;
    _attackDamage = baseAttackDamage;
  }

  private void Update() {
    if (Time.time > healthRegenTimeout && _currentHealth <= maxHealth) {
      float healthChange = healthRegen / 100;
      _currentHealth += healthChange;
      healthBar.Change(healthChange, true);
    }
  }

  public void Attack() {
    var random = Random.value;

    if (random < critChance / 100) {
      _attackDamage *= 2;
    } else {
      _attackDamage = baseAttackDamage;
    }
    
    animationController.EnableAttackAnimation();
  }

  public void TakeDamage(float damage) {
    if (stateController.CheckState(State.Invulnerable) || stateController.CheckState(State.Dead)) return;

    _currentHealth -= damage;
    DamagePopup.Create(transform.position, damage);
    healthRegenTimeout = Time.time + healthRegenTimeoutCoef;
  
    if (_currentHealth <= 0) {
      Die();
      return;
    }
    
    animationController.EnableHurtAnimation();
    healthBar.Change(damage, false);
  }

  private void Die() {
    stateController.State = State.Dead;
    animationController.EnableDeathAnimation();
    DeathEvent.Invoke();
    healthBar.Change(maxHealth, false);
  }

  private void OnDrawGizmosSelected() {
    Gizmos.DrawWireSphere(transform.position, attackRange);
  }
}
