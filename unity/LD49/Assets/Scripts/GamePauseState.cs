using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePauseState : GameStateMonoBehavior {
  public const string MenuButton = "Menu";
  public const string AdvanceButton = "Fire1";
  public TextMeshProUGUI textMesh;

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
    if (Input.GetButtonUp(AdvanceButton) || Input.GetButtonUp(MenuButton)) {
      Continue();
    }
  }

  public void SetText(string text) {
    if (textMesh) {
      textMesh.text = text;
    }
  }

  public void Continue() {
    stateManager.PopState();
  }

  public void Exit() {
    Game.instance.Quit();
  }
}
