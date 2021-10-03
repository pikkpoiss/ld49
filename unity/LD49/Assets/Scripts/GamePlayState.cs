using UnityEngine;

public struct LevelInfo {
  public float Seconds;
  public int Deliveries;
  public int MinPackages;
  public int MaxPackages;
}

[RequireComponent(typeof(PlayerController))]
public class GamePlayState : MonoBehaviour, IGameState {
  public GamePlayHUD hud;
  public Goal goal;

  private PlayerController playerController;
  private float timeRemaining;
  private float totalMoney;

  private int currentDeliveries;
  private int currentDeliveriesTarget;
  private int currentPackages;
  private int currentPackagesTarget;

  private int currentLevel = 0;
  private LevelInfo[] levels = {
    new LevelInfo(){ Seconds = 30, Deliveries = 5, MinPackages = 1, MaxPackages = 3 },
    new LevelInfo(){ Seconds = 40, Deliveries = 8, MinPackages = 2, MaxPackages = 5 },
  };
  private LevelInfo level { get => levels[currentLevel]; }

  private void Start() {
    playerController = GetComponent<PlayerController>();
    if (hud) {
      hud.gameObject.SetActive(true);
    }
    totalMoney = 0.0f;
    StartLevel();
  }

  private void StartLevel() {
    timeRemaining = level.Seconds;
    currentDeliveries = 0;
    currentDeliveriesTarget = level.Deliveries;
    StartBuilding();
  }

  private void StartBuilding() {
    currentPackages = 0;
    currentPackagesTarget = Random.Range(level.MinPackages, level.MaxPackages);
    if (goal) {
      goal.PickBuilding();
      UpdateGoal();
    }
  }

  private void UpdateHUD() {
    if (!hud) {
      return;
    }
    hud.SetTimeText(string.Format("{0:F1}s", timeRemaining));
    hud.SetMoneyText(string.Format("${0:F2}", totalMoney));
    hud.SetDeliveriesText(string.Format("{0}/{1} deliveries", currentDeliveries, currentDeliveriesTarget));
  }

  private void UpdateGoal() {
    if (!goal) {
      return;
    }
    goal.SetBuildingText(string.Format("{0}/{1}", currentPackages, currentPackagesTarget));
  }

  private void CheckWinConditions() {
    if (currentDeliveries >= currentDeliveriesTarget) {
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

  public void ReportDelivery(Item item) {
    currentPackages += 1;
    totalMoney += item.GetValue();
    if (currentPackages >= currentPackagesTarget) {
      currentDeliveries += 1;
      StartBuilding();
    } else {
      UpdateGoal();
    }
  }
}