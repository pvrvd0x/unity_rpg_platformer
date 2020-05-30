using UnityEngine;

public class Healing : MonoBehaviour {
  [Header("Parameters")] 
  [SerializeField] private float healingForce = 5f;

  private CombatController casterCombatController;

  private void Start() {
    casterCombatController = GetComponentInParent<CombatController>();
  }

  private void Update() {
    casterCombatController.Heal(healingForce * Time.time);
  }
}
