using UnityEngine;

public class AssetsController : MonoBehaviour {
  [SerializeField] private Transform damagePopup;
  public Transform DamagePopup => damagePopup;

  [SerializeField] private Transform fireball;
  public Transform Fireball => fireball;

  [SerializeField] private Transform bunchOfCoins;
  public Transform BunchOfCoins => bunchOfCoins;
  
  private static AssetsController _instance;
  public static AssetsController Instance {
    get {
      if (_instance == null) {
        _instance = Instantiate(Resources.Load<AssetsController>("AssetsController"));
      }
      return _instance;
    }
  }
}
