using UnityEngine;

public class Parallax : MonoBehaviour {
  [SerializeField] private Camera camera;
  [SerializeField] private float parallaxEffect;

  private float _length, _startPosition;

  private void Start() {
    _startPosition = transform.position.x;
    _length = GetComponent<SpriteRenderer>().bounds.size.x;
  }

  private void FixedUpdate() {
    float distance = (camera.transform.position.x * parallaxEffect);
    Transform objectTransform = transform;
    objectTransform.position = new Vector2(_startPosition * distance, objectTransform.position.y);
  }
}
