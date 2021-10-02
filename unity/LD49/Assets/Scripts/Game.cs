using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
  public static Game instance = null;
  public GameStateManager states;
  public GamePlayState playState;
  public GameEndState endState;
  public GameSplashState splashState;

  public bool unityEditorShowStartup = false;
  public bool ShowStartup {
    get {
#if UNITY_EDITOR
      return unityEditorShowStartup;
#else
      return true;
#endif
    }
  }

  public void End() {
    endState.gameObject.SetActive(true);
    states.PushState(endState);
  }

  public void Quit() {
    Application.Quit();
  }

  public void Reload() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  private void Awake() {
    if (instance == null) {
      instance = this;
    } else if (instance != this) {
      Destroy(gameObject);
      return;
    }

    Application.targetFrameRate = 60;

    states = new GameStateManager();
    states.PushState(playState);
    splashState.gameObject.SetActive(true);
  }

  private void Update() {
    states.Current.StateUpdate(states);
  }
}
