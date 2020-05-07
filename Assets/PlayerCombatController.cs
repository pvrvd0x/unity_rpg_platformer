using UnityEditor;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour {
  [Header("Components")]
  [SerializeField] private CombatController combatController;

  private void Update() {
    if (Time.time >= combatController.NextAttackTime) {
      if (Input.GetButtonDown("Fire1")) {
        combatController.Attack();
        combatController.NextAttackTime = Time.time + combatController.AttackRate;
      }
    }
  }
}
