using UnityEngine;

public class StunOnDuringHurtAnimation : StateMachineBehaviour {
  public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    animator.gameObject.GetComponent<StateController>().State = State.Stunned;
  }

  public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    animator.gameObject.GetComponent<StateController>().State = State.Active;
  }
}
