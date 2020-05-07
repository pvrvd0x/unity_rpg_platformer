using UnityEngine;

public class BunchOfCoins : MonoBehaviour {
  [Header("Components")] 
  [SerializeField] private GameObject coinPrefab;
  [SerializeField] private int maxAmountOfCoins;
  [SerializeField] private int minAmountOfCoins;

  public int MaxAmountOfCoins {
    set => maxAmountOfCoins = value;
  }

  public int MinAmountOfCoins {
    set => minAmountOfCoins = value;
  }

  private void Start() {
    int amountOfCoins = Random.Range(minAmountOfCoins, maxAmountOfCoins);
    
    for (int i = 0; i < amountOfCoins; i++) {
      Vector2 currentPosition = transform.position;
      GameObject coin = Instantiate(coinPrefab, currentPosition, Quaternion.identity);

      float xPosition = Random.Range(-1, 2) * (currentPosition.x + Random.Range(0, 50));

      coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(
        xPosition,
        currentPosition.y
        ));
    }
    
    Destroy(gameObject);
  }
}
