using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickable : MonoBehaviour {
  public bool wasStacked = false;
  public int stackDepth = 0;
  public int StackDepth {
    get => stackDepth;
  }

  void Start() {
  }

  // Stacks keep springs pointing downward.
  void OnCollisionEnter(Collision collision) {
    if (wasStacked) {
      // We've already been stacked, no need to worry about collisions.
      return;
    }
    var stickable = collision.gameObject.GetComponent<Stickable>();
    if (!stickable) {
      // The thing we collided with wasn't a stickable, ignore.
      return;
    }
    if (transform.position.y <= collision.gameObject.transform.position.y) {
      // We are lower than the thing we collided with, so not stacking.
      return;
    }
    var spring = GetComponent<SpringJoint>();
    if (spring) {
      // We already have a spring, ignore.
      return;
    }
    // OK, we're reasonably confident we're stacked on top of another stackable and should stack with it.
    wasStacked = true;
    stackDepth = stickable.stackDepth + 1;
    spring = gameObject.AddComponent<SpringJoint>();
    spring.autoConfigureConnectedAnchor = true;
    spring.spring = 1000.0f;
    spring.enableCollision = true;
    spring.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
    spring.breakForce = Mathf.Clamp(200.0f - (stackDepth * 40.0f), 20.0f, 200.0f);
  }
}
