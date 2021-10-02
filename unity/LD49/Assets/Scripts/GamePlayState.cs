using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class GamePlayState : MonoBehaviour, IGameState {
  public GamePlayHUD hud;

  private PlayerController playerController;

  private void Start() {
    playerController = GetComponent<PlayerController>();
    if (hud) {
      hud.gameObject.SetActive(false);
      hud.SetMoneyText("$0.00");
    }
  }

  public void Register(GameStateManager states) {}

  public void Unregister(GameStateManager states) {}

  public void OnCurrentEnter() {
    if (hud) {
      hud.gameObject.SetActive(true);
    }
  }

  public void OnCurrentExit() {
    if (hud) {
      hud.gameObject.SetActive(false);
    }
  }

  public void StateUpdate(GameStateManager states) {
    float horizontalInput = Input.GetAxisRaw("Horizontal");
    float verticalInput = Input.GetAxisRaw("Vertical");
    Vector3 adj = new Vector3(horizontalInput, 0.0f, verticalInput);
    playerController.InputDirection = adj;
  }
}