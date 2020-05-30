using UnityEngine;

public enum State {
  Active,
  Stunned,
  Dead,
  Invulnerable,
  Agroed,
  Moveless,
}

public class StateController : MonoBehaviour {
  [Header("Parameters")] 
  [SerializeField] private bool isInvulnerable = false;
  
  private State state = State.Active;
  
  public State State {
    get => state;
    set => state = value;
  }

  private void Start() {
    if (isInvulnerable)
      state = State.Invulnerable;
  }

  public bool CheckState(State requiredState) {
    return state == requiredState;
  }
}
