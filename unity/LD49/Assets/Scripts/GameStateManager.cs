using System.Collections.Generic;
using UnityEngine;

public interface IGameState {
  void StateUpdate(GameStateManager states);
  void Register(GameStateManager states);
  void Unregister(GameStateManager states);
  void OnCurrentEnter();
  void OnCurrentExit();
}

public class GameStateManager {
  private LinkedList<IGameState> states_;

  public GameStateManager() {
    states_ = new LinkedList<IGameState>();
  }

  public void PushState(IGameState state) {
    if (states_.Count > 0) {
      Current.OnCurrentExit();
    }
    states_.AddFirst(state);
    state.Register(this);
    state.OnCurrentEnter();
  }

  public bool PopState() {
    if (states_.Count > 1) {
      Current.OnCurrentExit();
      Current.Unregister(this);
      states_.RemoveFirst();
      if (states_.Count > 0) {
        Current.OnCurrentEnter();
      }
      return true;
    }
    Debug.LogWarningFormat("Attempted to pop state from stack of length {0}", states_.Count);
    return false;
  }

  public bool RemoveState(IGameState state) {
    if (states_.Count > 1) {
      if (Current == state) {
        Current.OnCurrentExit();
      }
      state.Unregister(this);
      var removed = states_.Remove(state);
      if (states_.Count > 0) {
        Current.OnCurrentEnter();
      }
      return removed;
    }
    return false;
  }

  public IGameState Current {
    get { return states_.First.Value; }
  }
}