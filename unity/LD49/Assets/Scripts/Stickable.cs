using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stickable : MonoBehaviour {
  public bool createSpring = true;
  public float maxBreakForce = 400.0f;
  public float minBreakForce = 100.0f;
  public float springForce = 1500.0f;
  public float maxStackDepth = 10;
  public AnimationCurve breakForceCurve;

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
    spring.spring = springForce;

    spring.enableCollision = true;
    spring.connectedBody = collision.gameObject.GetComponent<Rigidbody>();
    var pct = stackDepth / maxStackDepth;
    spring.breakForce = Mathf.Lerp(maxBreakForce, minBreakForce, breakForceCurve.Evaluate(pct));
  }

  public void DestroySpring() {
    var spring = GetComponent<SpringJoint>();
    if (spring) {
      spring.connectedBody = null;
      Destroy(spring);
    }
  }
}
