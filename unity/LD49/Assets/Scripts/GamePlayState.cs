using System.Collections;
using UnityEngine;

public struct LevelInfo {
  public float Seconds;
  public int Deliveries;
  public int MinPackages;
  public int MaxPackages;
}

[RequireComponent(typeof(PlayerController))]
public class GamePlayState : GameStateMonoBehavior {
  public GamePlayHUD hud;
  public Goal goal;
  public GameLevelCompletedState gameLevelCompletedState;
  public GameCompletedState gameCompletedState;
  public GameEndState gameEndState;

  private PlayerController playerController;
  private float timeRemaining;
  private float totalMoney;

  private int currentDeliveries;
  private int currentDeliveriesTarget;
  private int currentPackages;
  private int currentPackagesTarget;

  private bool hasWon;

  private int currentLevel = 0;
  private LevelInfo[] levels = {
    new LevelInfo(){ Seconds = 60, Deliveries = 3, MinPackages = 1, MaxPackages = 3 },
    new LevelInfo(){ Seconds = 60, Deliveries = 4, MinPackages = 3, MaxPackages = 5 },
    new LevelInfo(){ Seconds = 60, Deliveries = 5, MinPackages = 3, MaxPackages = 7 },
    new LevelInfo(){ Seconds = 90, Deliveries = 6, MinPackages = 4, MaxPackages = 8 },
    new LevelInfo(){ Seconds = 90, Deliveries = 7, MinPackages = 5, MaxPackages = 9 },
  };
  private LevelInfo level { get => levels[currentLevel]; }
  private GameStateManager states;

  public MusicManager musicManager;

  private void Start() {
    playerController = GetComponent<PlayerController>();
    musicManager = GetComponent<MusicManager>();
    if (hud) {
      hud.gameObject.SetActive(true);
    }
    totalMoney = 0.0f;

    StartLevel();
  }

  private void StartLevel() {
    Game.instance.ResetLevel();
    timeRemaining = level.Seconds;
    currentDeliveries = 0;
    currentDeliveriesTarget = level.Deliveries;
    hasWon = false;
    StartBuilding();
  }

  private void StartBuilding() {
    currentPackages = 0;
    currentPackagesTarget = Random.Range(level.MinPackages, level.MaxPackages);
    if (goal) {
      goal.PickBuilding();
      UpdateGoal();
      hud.SetDispatchText();
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

  private void CheckPackageConditions() {
    if (currentPackages >= currentPackagesTarget) {
      currentDeliveries += 1;
      StartBuilding();
    }
  }

  private void CheckWinConditions() {
    if (hasWon) {
      return;
    }
    if (currentDeliveries >= currentDeliveriesTarget) {
      hasWon = true;
      StartCoroutine(HandleLevelCompleted());
    }
  }

  private IEnumerator HandleLevelCompleted() {
    Debug.Log("Won!");
    playerController.StopEngineSound();
    musicManager.PlayVictoryMusic();
    yield return new WaitForSeconds(1.0f);
    currentLevel += 1;
    if (currentLevel >= levels.Length) {
      SetGameState(gameCompletedState);
      Debug.Log("Won game!");
    } else {
      SetGameState(gameLevelCompletedState);
      StartLevel();
    }
  }

  private void CheckLoseConditions() {
    if (hasWon) {
      return;
    }
    if (timeRemaining <= 0.0f) {
      Debug.Log("Lost!");
      playerController.StopEngineSound();
      musicManager.PlayFailureMusic();
      SetGameState(gameEndState);
    }
  }

  private void CheckUrgency() {
    if (timeRemaining > 0.0f && timeRemaining < 15.0f) {
      musicManager.PlayUrgentMusic();
    }
  }

  public override void Register(GameStateManager s) {
    states = s;
  }

  public override void Unregister(GameStateManager s) {
    states = null;
  }

  public override void OnCurrentEnter() {
    if (hud) {
      hud.gameObject.SetActive(true);
    }
    if (timeRemaining > 0.0f && currentLevel < levels.Length)
    {
      musicManager.ResetMusic();
    }
  }

  public override void OnCurrentExit() {
    if (hud) {
      hud.gameObject.SetActive(false);
    }
  }

  public override void StateUpdate(GameStateManager states) {
    if (!hasWon) {
      float horizontalInput = Input.GetAxisRaw("Horizontal");
      float verticalInput = Input.GetAxisRaw("Vertical");
      Vector3 adj = new Vector3(horizontalInput, 0.0f, verticalInput);
      playerController.InputDirection = adj;

      // Debug stuff - safe to leave in since this should be disabled in builds.
      if (Game.instance.DebugEnabled) {
        if (Input.GetKeyUp(KeyCode.F9)) {
          currentPackages += 1;
        }
        if (Input.GetKeyUp(KeyCode.F10)) {
          currentDeliveries += 1;
        }
        if (Input.GetKeyUp(KeyCode.F11)) {
          timeRemaining -= 5;
        }
      }
      timeRemaining -= Time.deltaTime;
    }
    UpdateHUD();
    UpdateGoal();
    CheckPackageConditions();
    CheckUrgency();
    CheckWinConditions();
    CheckLoseConditions();
  }

  public void ReportDelivery(Item item) {
    currentPackages += 1;
    totalMoney += item.GetValue();
  }

  public void ReportLoss(Item item) {
    totalMoney -= 5.0f;
  }

  private void SetGameState(GameStateMonoBehavior state) {
    state.gameObject.SetActive(true);
    if (states != null) {
      states.PushState(state);
    }
  }
}