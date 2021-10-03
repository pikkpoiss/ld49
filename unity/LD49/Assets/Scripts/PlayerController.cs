using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
  private Vector3 movement;
  private Rigidbody body;
  private Vector3 targetDirection = Vector3.zero;
  private Vector3 inputDirection = Vector3.zero;
  private Vector3 startLocation;
  private Quaternion startRotation;
  private AudioSource audioSource;
  private float speed = 0.0f;
  private bool hitCollider = false;
  private float autobrakeElapsed = 0.0f;
  private float autobrakeStartSpeed = 0.0f;

  public const string ObstacleTag = "Obstacle";
  public const string GoalTag = "Goal";

  public float turnSpeed = 20.0f;
  public float moveSpeed = 0.01f;
  public float slowdownRate = 0.1f;
  public float autoBrakeTime = 1.0f;
  public AnimationCurve autobrakeCurve;

  public Vector3 InputDirection {
    set => this.inputDirection = value;
  }

  void Start() {
    body = GetComponent<Rigidbody>();
    if (body) {
      startLocation = body.transform.position;
      startRotation = body.transform.rotation;
    }

    audioSource = GetComponent<AudioSource>();
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
      autobrakeElapsed = 0.0f;
      autobrakeStartSpeed = speed;
    } else {
      autobrakeElapsed += Time.fixedDeltaTime;
      var pct = autobrakeElapsed / autoBrakeTime;
      speed = Mathf.Lerp(autobrakeStartSpeed, 0.0f,  autobrakeCurve.Evaluate(pct));
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

    if (Input.GetKeyDown(KeyCode.H))
    {
      audioSource.Play();
    }
  }

  void OnTriggerEnter(Collider collider) {
    if (collider.CompareTag(ObstacleTag)) {
      hitCollider = true;
    } else if (collider.CompareTag(GoalTag)) {
    }
  }

  public void Reset() {
    Debug.Log("Player reset");
    body.MovePosition(startLocation);
    body.MoveRotation(startRotation);
    gameObject.transform.position = startLocation;
    gameObject.transform.rotation = startRotation;
    inputDirection = Vector3.zero;
    speed = 0.0f;
  }
}
