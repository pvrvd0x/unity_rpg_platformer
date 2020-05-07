using UnityEngine;
using UnityEngine.UI;

public class BarStatsController : MonoBehaviour {
  [Header("Components")] 
  [SerializeField] private Slider slider;
  [SerializeField] private float maxStatValue;

  public void Change(float value, bool isIncreasement) {
    int changeDirection = isIncreasement ? 1 : -1;
    float change = changeDirection * value * 100 / maxStatValue / 100;

    slider.value += change;
  }
}
