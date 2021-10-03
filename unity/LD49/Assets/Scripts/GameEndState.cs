using UnityEngine;
using UnityEngine.UI;

public class GameEndState : GameStateMonoBehavior {
  public Text pointsText;
  private float elapsed = 0.0f;
  public float minimumDelay = 1.0f;

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
      if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {
        states.PopState();
        Game.instance.Reload();
      }
    }
  }

  public void SetPoints(int points) {
    pointsText.text = string.Format("You scored {0} points!", points);
  }
}