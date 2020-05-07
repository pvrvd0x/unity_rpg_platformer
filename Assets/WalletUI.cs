using TMPro;
using UnityEngine;

public class WalletUI : MonoBehaviour {
  [Header("Components")] 
  [SerializeField] private Wallet wallet;
  [SerializeField] private TextMeshPro textMeshPro;

  private void Update() {
    textMeshPro.text = wallet.Money.ToString();
  }
}
