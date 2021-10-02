using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class GamePlayState : MonoBehaviour, IGameState {
  private PlayerController playerController;

  private void Start() {
    playerController = GetComponent<PlayerController>();
  }

  public void Register(GameStateManager states) { }
  public void Unregister(GameStateManager states) { }

  public void StateUpdate(GameStateManager states) {
    float horizontalInput = Input.GetAxisRaw("Horizontal");
    float verticalInput = Input.GetAxisRaw("Vertical");
    Vector3 adj = new Vector3(horizontalInput, 0.0f, verticalInput);
    playerController.InputDirection = adj;
  }
}