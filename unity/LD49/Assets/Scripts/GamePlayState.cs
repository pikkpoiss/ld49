using UnityEngine;

public struct LevelInfo {
  public float Seconds;
  public float Deliveries;
}

[RequireComponent(typeof(PlayerController))]
public class GamePlayState : MonoBehaviour, IGameState {
  public GamePlayHUD hud;

  private PlayerController playerController;
  private float timeRemaining;
  private float totalMoney;
  private float deliveries;
  private int currentLevel = 0;
  private LevelInfo[] levels = {
    new LevelInfo(){ Seconds = 30, Deliveries = 5 },
    new LevelInfo(){ Seconds = 40, Deliveries = 8 },
  };
  private LevelInfo level { get => levels[currentLevel]; }

  private void Start() {
    playerController = GetComponent<PlayerController>();
    if (hud) {
      hud.gameObject.SetActive(false);
      hud.SetMoneyText("$0.00");
    }
    totalMoney = 0.0f;
    StartLevel();
  }

  private void StartLevel() {
    timeRemaining = level.Seconds;
  }

  private void UpdateHUD() {
    if (!hud) {
      return;
    }
    hud.SetTimeText(string.Format("{0:F1}s", timeRemaining));
    hud.SetMoneyText(string.Format("${0:F2}", totalMoney));
    hud.SetDeliveriesText(string.Format("{0}/{1} deliveries", deliveries, level.Deliveries));
  }

  private void CheckWinConditions() {
    if (deliveries >= level.Deliveries) {
      Debug.Log("Won!");
      currentLevel += 1;
      if (currentLevel >= levels.Length) {
        Debug.Log("Won game!");
      } else {
        StartLevel();
      }
    }
  }

  private void CheckLoseConditions() {
    if (timeRemaining <= 0.0f) {
      Debug.Log("Lost!");
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

    timeRemaining -= Time.deltaTime;
    UpdateHUD();
    CheckWinConditions();
    CheckLoseConditions();
  }
}