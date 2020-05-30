using System;
using UnityEngine;

public class Wallet : MonoBehaviour {
  [Header("Components")] 
  [SerializeField] private LayerMask pickupLayers;
  
  [Header("Parameters")]
  [SerializeField] private int money;
  [SerializeField] private float rangeOfMoneyPickup;

  public int Money => money;

  private Transform bunchOfCoinsTransform;
  private BunchOfCoins bunchOfCoins;

  private void Start() {
    bunchOfCoinsTransform = AssetsController.Instance.BunchOfCoins;
    bunchOfCoins = bunchOfCoinsTransform.GetComponent<BunchOfCoins>();
  }

  private void Update() {
    PickupMoney();
  }

  public void DropMoney() {
    var coins = Instantiate(bunchOfCoinsTransform, transform.position, Quaternion.identity);
    coins.GetComponent<BunchOfCoins>().SetCoinsRange(money);
  }

  private void PickupMoney() {
    Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, rangeOfMoneyPickup, pickupLayers);

    foreach (var coin in coins) {
      Destroy(coin.gameObject);
      money++;
      bunchOfCoins.SetCoinsRange(money);
    }
  }

  private void OnDrawGizmosSelected() {
    Gizmos.DrawWireSphere(transform.position, rangeOfMoneyPickup);
  }
}
