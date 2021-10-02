using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
  private Vector3 movement;
  private Rigidbody body;
  private Vector3 targetDirection = Vector3.zero;
  private Vector3 inputDirection = Vector3.zero;
  private float speed = 0.0f;

  public float turnSpeed = 20.0f;
  public float moveSpeed = 0.01f;

  public Vector3 InputDirection {
    set => this.inputDirection = value;
  }
  void Start() {
    body = GetComponent<Rigidbody>();
  }

  void FixedUpdate() {
    float horizontalInput = Input.GetAxisRaw("Horizontal");
    float verticalInput = Input.GetAxisRaw("Vertical");
    Vector3 adj = new Vector3(horizontalInput, 0.0f, verticalInput);

    if (inputDirection.sqrMagnitude >= 0.01f) {
      Vector3 camDirection = Camera.main.transform.rotation * inputDirection;
      targetDirection.Set(camDirection.x, 0, camDirection.z);
      body.MoveRotation(Quaternion.Slerp(
        body.rotation,
        Quaternion.LookRotation(targetDirection),
        Time.fixedDeltaTime * turnSpeed
      ));
      speed += moveSpeed;
    } else {
      speed = Mathf.Lerp(speed, 0.0f, 0.2f);
    }
    body.MovePosition(body.position + body.rotation * (Vector3.forward * speed));
  }
}
