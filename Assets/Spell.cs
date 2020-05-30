using UnityEngine;

public class Spell : MonoBehaviour {
  [Header("Parameters")] 
  [SerializeField] private float manaCost;
  [SerializeField] private SpellType spellType;

  public float ManaCost {
    get => manaCost;
    set => manaCost = value;
  }

  public SpellType SpellType {
    get => spellType;
  }
}
