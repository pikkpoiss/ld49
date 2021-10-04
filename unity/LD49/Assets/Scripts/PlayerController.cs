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
  private AudioSource carHornAudioSource;
  private AudioSource carEngineAudioSource;
  private float speed = 0.0f;
  private bool hitCollider = false;
  private float speedElapsed = 0.0f;
  private bool isAccelerating = false;
  private float startSpeed;

  public const string ObstacleTag = "Obstacle";
  public const string GoalTag = "Goal";

  public float turnSpeed = 20.0f;
  public float maxSpeed = 1.0f;
  public float accelerationTime = 1.0f;
  public float brakingTime = 1.0f;
  public AnimationCurve accelerationCurve;
  public AnimationCurve brakingCurve;

  public Vector3 InputDirection {
    set => this.inputDirection = value;
  }

  void Start() {
    body = GetComponent<Rigidbody>();
    if (body) {
      startLocation = body.transform.position;
      startRotation = body.transform.rotation;
    }

    carHornAudioSource = GetComponent<AudioSource>();

    GameObject van = this.transform.Find("Van").gameObject;
    carEngineAudioSource = van.GetComponent<AudioSource>();
  }

  void FixedUpdate() {
    var pct = 0.0f;
    if (inputDirection.sqrMagnitude >= 0.01f) {
      Vector3 camDirection = Camera.main.transform.rotation * inputDirection;
      targetDirection.Set(camDirection.x, 0, camDirection.z);
      body.MoveRotation(Quaternion.Slerp(
        body.rotation,
        Quaternion.LookRotation(targetDirection),
        Time.fixedDeltaTime * turnSpeed
      ));
      if (!isAccelerating) {
        isAccelerating = true;
        speedElapsed = 0.0f;
        startSpeed = speed;
        PlayEngineSound();
      }
      speedElapsed += Time.fixedDeltaTime;
      speedElapsed = Mathf.Clamp(speedElapsed, 0.0f, accelerationTime);
      pct = speedElapsed / accelerationTime;
      speed = Mathf.Lerp(startSpeed, maxSpeed, accelerationCurve.Evaluate(pct));
    } else {
      if (isAccelerating) {
        isAccelerating = false;
        speedElapsed = 0.0f;
        startSpeed = speed;
        StopEngineSound();
      }
      speedElapsed += Time.fixedDeltaTime;
      speedElapsed = Mathf.Clamp(speedElapsed, 0.0f, brakingTime);
      pct = speedElapsed / brakingTime;
      speed = Mathf.Lerp(startSpeed, 0.0f, brakingCurve.Evaluate(pct));
    }
    if (hitCollider) {
      hitCollider = false;
      if (body.isKinematic) {
        // Bounce back.
        speed = speed * -0.5f;
        startSpeed = speed;
        speedElapsed = 0.0f;
      }
    }
    body.velocity = body.rotation * Vector3.forward * speed;
    body.MovePosition(body.position + (body.velocity * Time.fixedDeltaTime));


    if (Input.GetKeyDown(KeyCode.H)) {
      PlayCarHornSound();
    }
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.collider.CompareTag(ObstacleTag)) {
      hitCollider = true;
    }
  }

  void OnTriggerEnter(Collider collider) {
    if (collider.CompareTag(ObstacleTag)) {
      hitCollider = true;
      var collisionPoint = collider.ClosestPointOnBounds(body.position);
      var reverseVector = (body.position - collisionPoint).normalized;
      body.position = collisionPoint + reverseVector * 1.6f; // Just a magic number.
    }
  }

  private void PlayEngineSound() {
    carEngineAudioSource.Play();
  }

  public void StopEngineSound() {
    carEngineAudioSource.Stop();
  }

  private void PlayCarHornSound() {
    carHornAudioSource.Play();
  }

  public void Reset() {
    Debug.Log("Player reset");
    body.MovePosition(startLocation);
    body.MoveRotation(startRotation);
    gameObject.transform.position = startLocation;
    gameObject.transform.rotation = startRotation;
    inputDirection = Vector3.zero;
    speed = 0.0f;
    StopEngineSound();
  }
}
