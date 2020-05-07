using UnityEngine;

public class Spell : MonoBehaviour {
  [Header("Parameters")] 
  [SerializeField] private float manaCost;

  public float ManaCost {
    get => manaCost;
    set => manaCost = value;
  }
}
