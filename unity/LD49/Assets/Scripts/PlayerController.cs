using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
  private Vector3 movement;
  private UnityEngine.AI.NavMeshAgent agent;
  private Rigidbody body;
  private Vector3 targetDirection = Vector3.zero;
  private float speed = 0.0f;

  public float turnSpeed = 20.0f;
  public float moveSpeed = 0.01f;

  // Start is called before the first frame update
  void Start() {
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    body = GetComponent<Rigidbody>();
  }

  void Update() {
    /*
    if (targetDirection != Vector3.zero) {
      body.MoveRotation(Quaternion.Slerp(
        body.rotation,
        Quaternion.LookRotation(targetDirection),
        Time.deltaTime * turnSpeed
      ));
    }
    */
  }

  void FixedUpdate() {
    float horizontalInput = Input.GetAxisRaw("Horizontal");
    float verticalInput = Input.GetAxisRaw("Vertical");
    Vector3 adj = new Vector3(horizontalInput, 0.0f, verticalInput);

    if (adj.sqrMagnitude >= 0.01f) {
      Vector3 camDirection = Camera.main.transform.rotation * adj; 
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

    /*
    if (adj.sqrMagnitude > 0.01f) {
      movement += adj * 0.005f;
    } else {
      movement = Vector3.Lerp(movement, Vector3.zero, 0.2f);
    }
    */

    // agent.Move(movement * Time.deltaTime * agent.speed);
    // agent.SetDestination(transform.position + movement);
    //body.AddRelativeForce(Vector3.forward * Vector3.forward * speed);
    body.MovePosition(body.position + body.rotation * (Vector3.forward * speed));
    //body.MovePosition(transform.position +  movement);
  }
}
