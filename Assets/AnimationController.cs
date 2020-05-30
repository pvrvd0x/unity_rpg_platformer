using UnityEngine;

public class AnimationController : MonoBehaviour {
  [SerializeField] private Animator animator;

  public void TriggerJumpAnimation(bool state) {
    animator.SetBool(AnimatorProperties.IsJumping, state);
  }
  
  public void EnableAttackAnimation() {
    animator.SetTrigger(AnimatorProperties.Attack);
  }

  public void EnableHurtAnimation() {
    animator.SetTrigger(AnimatorProperties.Hurt);
  }

  public void EnableDeathAnimation() {
    animator.SetTrigger(AnimatorProperties.Dead);
  }

  public void ToggleAgroedAnimation(bool state) {
    animator.SetBool(AnimatorProperties.IsAgroed, state);
  }

  public void SetRunSpeedParameter(float moveSpeed) {
    animator.SetFloat(AnimatorProperties.Speed, moveSpeed);
  }
  
  public void EnableRollAnimation() {
    animator.SetTrigger(AnimatorProperties.Roll);
  }

  public void EnableCastAnimation() {
    animator.SetTrigger(AnimatorProperties.Cast);
  }

  public void ToggleContinuousCastAnimation(bool state) {
    animator.SetBool(AnimatorProperties.IsCasting, state);
  }
}