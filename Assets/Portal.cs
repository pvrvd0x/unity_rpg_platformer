using System;
using UnityEngine;

public class Portal : MonoBehaviour {
  [Header("Components")]
  [SerializeField] private Portal destination;

  private Vector2 _destinationTransformPosition;

  private void Start() {
    _destinationTransformPosition = destination.transform.position;
  }

  private void OnCollisionEnter2D(Collision2D other) {
    Teleport(other.transform);
  }

  private void Teleport(Transform objectTransform) {
    int positionAfterTeleportationChangeCoef = 1;
    int rotation = objectTransform.rotation.y >= 0 ? 1 : -1;
    objectTransform.position = new Vector2(
      _destinationTransformPosition.x + rotation * positionAfterTeleportationChangeCoef,
      _destinationTransformPosition.y);
  }
}
