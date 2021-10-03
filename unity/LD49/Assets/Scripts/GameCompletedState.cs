using UnityEngine;

public class GameCompletedState : MonoBehaviour, IGameState {
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
    gameObject.SetActive(false);
  }

  public void OnCurrentEnter() {
    gameObject.SetActive(true);
    Time.timeScale = 0.0f;
  }

  public void OnCurrentExit() {
    gameObject.SetActive(false);
    Time.timeScale = 1.0f;
  }

  public void Advance() {
    stateManager.PopState();
    gameObject.SetActive(false);
    Time.timeScale = 1.0f;
  }

  public void StateUpdate(GameStateManager states) {
    if (Input.GetButtonUp(AdvanceButton)) {
      stateManager.PopState();
    }
  }
}