using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
  private Vector3 movement;
  private Rigidbody body;
  private Vector3 targetDirection = Vector3.zero;
  private Vector3 inputDirection = Vector3.zero;
  private float speed = 0.0f;
  private bool hitCollider = false;

  public const string ObstacleTag = "Obstacle";
  public const string GoalTag = "Goal";

  public float turnSpeed = 20.0f;
  public float moveSpeed = 0.01f;

  public Vector3 InputDirection {
    set => this.inputDirection = value;
  }

  void Start() {
    body = GetComponent<Rigidbody>();
  }

  void FixedUpdate() {
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

    if (hitCollider) {
      // Bounce back.
      speed = -speed * 0.5f;
      body.MovePosition(body.position + body.rotation * (Vector3.forward * speed));
      hitCollider = false;
    } else {
      // Normal movement.
      body.MovePosition(body.position + body.rotation * (Vector3.forward * speed));
    }
  }

  void OnTriggerEnter(Collider collider) {
    if (collider.CompareTag(ObstacleTag)) {
      Debug.Log("Player hit an obstacle!");
      hitCollider = true;
    } else if (collider.CompareTag(GoalTag)) {
      Debug.Log("Player hit the goal!");
    }
  }
}
