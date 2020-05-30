using System;
using UnityEngine;

public class MagicController : MonoBehaviour {
  [Header("Components")] 
  [SerializeField] private SpellTome spellTome;
  [SerializeField] private BarStatsController manabar;
  [SerializeField] private AnimationController animationController;
  [SerializeField] private StateController stateController;

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
      var spell = spellTome.SpellPrefabs[i];
      var spellComponent = spell.GetComponent<Spell>();
      
      float manaReduction = spellComponent.ManaCost;

      switch (spellComponent.SpellType) {
        case SpellType.SingleCast:
          if (Input.GetKeyDown(spellTome.KeyBindings[i])) {
            if (currentMana - manaReduction <= 0) {
              return;
            }
            
            SingleCast(spell);
            ReduceMana(manaReduction);
          }
          break;
        
        case SpellType.ContinuousCast:
          if (Input.GetKeyDown(spellTome.KeyBindings[i])) {
            if (currentMana - manaReduction <= 0) {
              CancelContinuousCast(spell);
              return;
            }
            
            var position = transform.position;
            var parentTransform = gameObject.transform;
            Instantiate(
              spell,
              new Vector3(position.x, position.y, 0f),
              parentTransform.rotation,
              parentTransform
            );
          }
          
          if (Input.GetKey(spellTome.KeyBindings[i])) {
            if (currentMana - manaReduction <= 0) {
              CancelContinuousCast(spell);
              return;
            }
            
            ContinuousCast(spell);
            ReduceMana(manaReduction);
          } else {
            CancelContinuousCast(spell);
          }

          break;
      }
    }
  }

  private void ReduceMana(float reduction) {
    currentMana -= reduction;
    manabar.Change(reduction, false);
    manaRegenTimeout = Time.time + manaRegenTimeoutCoef;
  }

  private void SingleCast(Transform spellPrefab) {
    var position = transform.position;
    animationController.EnableCastAnimation();

    var spell = Instantiate(
      spellPrefab,
      new Vector3(position.x, position.y, 0f),
      gameObject.transform.rotation
    );
    try {
      spell.GetComponent<Projectile>().SpawnOrigin = gameObject;
    } catch(NullReferenceException e) {}
  }

  private void ContinuousCast(Transform spellPrefab) {
    animationController.ToggleContinuousCastAnimation(true);
    stateController.State = State.Moveless;
  }

  private void CancelContinuousCast(Transform spellInstance) {
    animationController.ToggleContinuousCastAnimation(false);
    stateController.State = State.Active;
    Destroy(GameObject.Find($"{spellInstance.name}(Clone)"));
  }
}
