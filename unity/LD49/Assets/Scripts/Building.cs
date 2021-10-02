using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour {
  public Vector3 GetDimensions() {
    return transform.localScale;
  }


  public Vector3 GetPosition() {
    return transform.position;
  }
}
