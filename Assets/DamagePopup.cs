using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour {
  [Header("Components")] 
  [SerializeField] private TextMeshPro textMesh;
  [SerializeField] private float fadeTimeout;
  [SerializeField] private Color textMeshColor;

  public static DamagePopup Create(Vector2 position, float damage) {
    Transform damagePopupTransform = Instantiate(AssetsController.Instance.DamagePopup, position, Quaternion.identity);
    DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
    damagePopup.Setup(damage);

    return damagePopup;
  }
  
  private void Setup(float damage) {
    textMesh.SetText(damage.ToString());
  }

  private void Update() {
    const float moveYSpeed = 10f;
    transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

    fadeTimeout -= Time.deltaTime;

    if (fadeTimeout <= 0) {
      const float fadeSpeed = 3f;
      textMeshColor.a -= fadeSpeed * Time.deltaTime;
      textMesh.color = textMeshColor;

      if (textMeshColor.a < 0) {
        Destroy(gameObject);
      }
    }
  }
}
