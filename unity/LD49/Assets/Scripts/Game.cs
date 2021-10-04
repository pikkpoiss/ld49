using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
  private bool audioResumed = false;

  public static Game instance = null;
  public GameStateManager states;
  public GamePlayState playState;
  public GameEndState endState;
  public GameSplashState splashState;
  public Canvas canvas;

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

  public bool DebugEnabled {
    get {
#if UNITY_EDITOR
      return true;
#else
      return false;
#endif
    }
  }

  // See https://alessandrofama.com/tutorials/fmod-unity/fix-blocked-audio-browsers/
  public void ResumeAudio() {
    if (!audioResumed) {
      var result = FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
      Debug.Log(result);
      result = FMODUnity.RuntimeManager.CoreSystem.mixerResume();
      Debug.Log(result);
      audioResumed = true;
    }
  }

  public void End() {
    endState.gameObject.SetActive(true);
    states.PushState(endState);
  }

  public void Quit() {
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
  }

  public void Reload() {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void ResetLevel() {
    var players = FindObjectsOfType<PlayerController>();
    for (var i = 0; i < players.Length; i++) {
      players[i].Reset();
    }
    var items = FindObjectsOfType<Item>();
    for (var i = 0; i < items.Length; i++) {
      Destroy(items[i].gameObject);
    }
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

    canvas.gameObject.SetActive(true);
  }

  private void Update() {
    states.Current.StateUpdate(states);
  }
}
