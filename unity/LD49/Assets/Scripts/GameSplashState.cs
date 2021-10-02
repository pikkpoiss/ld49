using UnityEngine;

public class GameSplashState : MonoBehaviour, IGameState {
  public GameObject quitButton;
  public const string AdvanceButton = "Fire1";

  private GameStateManager stateManager;

  public void Register(GameStateManager states) {
    stateManager = states;
  }

  public void Unregister(GameStateManager states) {
    stateManager = null;
  }

  public void Start() {
    if (Game.instance.ShowStartup) {
      Game.instance.states.PushState(this);
      Time.timeScale = 0.0f;
    } else {
      gameObject.SetActive(false);
    }
  }

  public void Advance() {
    stateManager.PopState();
    gameObject.SetActive(false);
    Time.timeScale = 1.0f;
  }

  public void StateUpdate(GameStateManager states) {
    if (Input.GetButtonUp(AdvanceButton)) {
      Advance();
    }
  }
}