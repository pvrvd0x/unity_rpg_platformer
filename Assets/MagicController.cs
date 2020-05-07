using UnityEngine;

public class MagicController : MonoBehaviour {
  [Header("Components")] 
  [SerializeField] private SpellTome spellTome;
  [SerializeField] private BarStatsController manabar;

  [Header("Parameters")] 
  [SerializeField] private float maxMana;
  [SerializeField] [Range(0, 100)] private float manaRegen = 20f;
  [SerializeField] private float manaRegenTimeout = 0f;

  private float manaRegenTimeoutCoef = 2f;
  private float currentMana;
  public float CurrentMana {
    get => currentMana;
    set => currentMana = value;
  }

  private void Start() {
    currentMana = maxMana;
  }

  private void Update() {
    if (Time.time > manaRegenTimeout && currentMana <= maxMana) {
      float manaChange = manaRegen / 100;
      currentMana += manaChange;
      manabar.Change(manaChange, true);
    }
    
    for (var i = 0; i < spellTome.SpellPrefabs.Length; i++) {
      if (Input.GetKeyDown(spellTome.KeyBindings[i])) {
        var position = transform.position;
        GetComponentInParent<AnimationController>().EnableCastAnimation();

        float manaReduction = spellTome.SpellPrefabs[i].GetComponent<Spell>().ManaCost;
        
        if (currentMana - manaReduction < 0) {
          return;
        }
        
        var spell = Instantiate(
          spellTome.SpellPrefabs[i],
          new Vector3(position.x, position.y, 0f),
          gameObject.transform.rotation
        );

        currentMana -= manaReduction;
        manabar.Change(manaReduction, false);
        spell.GetComponent<Projectile>().SpawnOrigin = gameObject;
        manaRegenTimeout = Time.time + manaRegenTimeoutCoef;
      }
    }
  }
}
