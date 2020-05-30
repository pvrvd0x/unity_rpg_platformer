using UnityEngine;

public enum SpellType {
  SingleCast,
  ContinuousCast,
}

public class SpellTome : MonoBehaviour {
  [SerializeField] private Transform[] spellPrefabs;
  [SerializeField] private string[] keyBindings;

  public Transform[] SpellPrefabs => spellPrefabs;
  public string[] KeyBindings => keyBindings;
}
