using UnityEngine;

public class cloud : MonoBehaviour {
  [Header("Params")] 
  [SerializeField] private float flightSpeed = 10f;
  
  private void FixedUpdate() {
    transform.Translate(Vector2.left * (Time.deltaTime * flightSpeed));
  }
}
