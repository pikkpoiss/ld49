using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour {
  private Vector3 movement;
  private UnityEngine.AI.NavMeshAgent agent;
  private Rigidbody body;

  // Start is called before the first frame update
  void Start() {
    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    body = GetComponent<Rigidbody>();
  }

  void FixedUpdate() {
    float horizontalInput = Input.GetAxisRaw("Horizontal");
    float verticalInput = Input.GetAxisRaw("Vertical");

    Vector3 adj = new Vector3(horizontalInput, 0.0f, verticalInput);

    if (adj.sqrMagnitude > 0.01f) {
      movement += adj * 0.005f;
    } else {
      movement = Vector3.Lerp(movement, Vector3.zero, 0.2f);
    }

    // agent.Move(movement * Time.deltaTime * agent.speed);
    // agent.SetDestination(transform.position + movement);

    body.MovePosition(transform.position + movement);
  }
}
