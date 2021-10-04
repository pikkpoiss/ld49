using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
  public FMODUnity.StudioEventEmitter fmodEventEmitter;

  void Start()
  {
    fmodEventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
  }
    
  public void PlayUrgentMusic()
  {
    fmodEventEmitter.SetParameter("Urgency", 0.5f);
  }

  public void PlayVictoryMusic()
  {
    fmodEventEmitter.SetParameter("Urgency", 0.0f);
    fmodEventEmitter.SetParameter("Victory", 0.75f);
  }

  public void PlayFailureMusic()
  {
    fmodEventEmitter.SetParameter("Urgency", 0.0f);
    fmodEventEmitter.SetParameter("Victory", 0.25f);
  }

  public void ResetMusic()
  {
    Debug.Log("ResetMusic");
    fmodEventEmitter.SetParameter("Urgency", 0.0f);
    fmodEventEmitter.SetParameter("Victory", 0.0f);
  }
}
