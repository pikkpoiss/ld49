using UnityEngine;

public class GameCompletedState : GameStateMonoBehavior {
  public GameObject quitButton;
  public const string AdvanceButton = "Fire1";

  private GameStateManager stateManager;

  public override void Register(GameStateManager states) {
    stateManager = states;
  }

  public override void Unregister(GameStateManager states) {
    stateManager = null;
  }

  public void Awake() {
    gameObject.SetActive(false);
  }

  public override void OnCurrentEnter() {
    gameObject.SetActive(true);
    Time.timeScale = 0.0f;
  }

  public override void OnCurrentExit() {
    gameObject.SetActive(false);
    Time.timeScale = 1.0f;
  }

  public override void StateUpdate(GameStateManager states) {
    if (Input.GetButtonUp(AdvanceButton)) {
      states.PopState();
      Game.instance.Reload();
    }
  }
}