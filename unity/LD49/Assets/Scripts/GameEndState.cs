using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameEndState : GameStateMonoBehavior {
  private float elapsed = 0.0f;
  public float minimumDelay = 1.0f;
  public const string AdvanceButton = "Fire1";
  public TextMeshProUGUI textMesh;

  void Awake() {
    gameObject.SetActive(false);
  }

  private void OnEnable() {
    elapsed = 0.0f;
    Game.instance.states.PushState(this);
  }

  public override void Register(GameStateManager states) { }
  public override void Unregister(GameStateManager states) { }
  public override void OnCurrentEnter() { }
  public override void OnCurrentExit() { }

  public override void StateUpdate(GameStateManager states) {
    if (!gameObject.activeSelf) {
      states.PopState();
    }
    if (elapsed < minimumDelay) {
      elapsed += Time.deltaTime;
    } else {
      if (Input.GetButtonUp(AdvanceButton)) {
        Game.instance.Reload();
      }
    }
  }

  public void SetText(string text) {
    if (textMesh) {
      textMesh.text = text;
    }
  }
}