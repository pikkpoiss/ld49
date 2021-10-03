using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
  private FMOD.Studio.EventInstance fmodInstance;

  public FMODUnity.EventReference fmodEvent;

  void Start()
  {
    fmodInstance = FMODUnity.RuntimeManager.CreateInstance(fmodEvent);
    fmodInstance.start();
  }
    
  public void PlayUrgentMusic()
  {
    fmodInstance.setParameterByName("Urgency", 0.5f);
  }

  public void PlayVictoryMusic()
  {
    fmodInstance.setParameterByName("Urgency", 0.0f);
    fmodInstance.setParameterByName("Victory", 0.75f);
  }

  public void PlayFailureMusic()
  {
    fmodInstance.setParameterByName("Urgency", 0.0f);
    fmodInstance.setParameterByName("Victory", 0.25f);
  }

  public void ResetMusic()
  {
    fmodInstance.setParameterByName("Urgency", 0.0f);
    fmodInstance.setParameterByName("Victory", 0.0f);
    fmodInstance.start();
  }
}
