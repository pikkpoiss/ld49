using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour {
  private float rotationSpeed = 100.0f;

  void Update() {
    var rotationAngle = rotationSpeed * Time.deltaTime;
    transform.Rotate(Vector3.up, rotationAngle, Space.World);
    //transform.localRotation = Quaternion.AngleAxis(rotationAngle, Vector3.up);
  }
}
