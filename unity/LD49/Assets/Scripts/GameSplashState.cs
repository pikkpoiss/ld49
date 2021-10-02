using UnityEngine;

public class GameSplashState : MonoBehaviour, IGameState {
  public GameObject quitButton;
  private GameStateManager stateManager;

  private bool keyWasDown = false;

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
    if (Input.anyKey) {
      keyWasDown = true;
    }
    if (!Input.anyKey && keyWasDown) {
      if (!Input.GetMouseButtonDown(0) && Input.touchCount == 0) {
        Advance();
      }
    }
  }
}