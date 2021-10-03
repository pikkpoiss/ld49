using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

  public const string GroundTag = "Ground";
  public const string GoalTag = "Goal";

  public Material fadeMaterial;

  public float fadeDuration = 2.0f;
  public float timeBeforeCleanup = 10.0f;
  public float goalDuration = 0.5f;
  public AnimationCurve goalMoveCurve;

  private bool touchedGoal = false;
  private bool touchedGround = false;
  private Stickable stickable;

  void Start() {
    stickable = GetComponent<Stickable>();
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.collider.CompareTag(GroundTag)) {
      touchedGround = true;
      StartCoroutine(AnimateDeath());
    } else if (collision.collider.CompareTag(GoalTag)) {
      touchedGoal = true;
      StartCoroutine(AnimateGoal(collision.collider.transform.position));
    }
  }

  private IEnumerator AnimateGoal(Vector3 destination) {
    var body = GetComponent<Rigidbody>();
    body.isKinematic = true;
    float elapsed = 0.0f;
    Vector3 start = transform.position;
    while (elapsed < goalDuration) {
      elapsed += Time.deltaTime;
      float pct = elapsed / goalDuration;
      body.MovePosition(Vector3.Lerp(start, destination, goalMoveCurve.Evaluate(pct)));
      yield return new WaitForEndOfFrame();
    }
    Destroy(gameObject);
  }

  private IEnumerator AnimateDeath() {
    var renderer = GetComponent<MeshRenderer>();
    if (!renderer) {
      yield break;
    }
    if (!fadeMaterial) {
      yield break;
    }
    yield return new WaitForSeconds(timeBeforeCleanup);
    var color = renderer.material.color;
    var originalMaterial = renderer.material;
    renderer.material = fadeMaterial;
    renderer.material.color = color;
    float elapsed = 0.0f;
    while (elapsed < fadeDuration) {
      if (touchedGoal) {
        renderer.material = originalMaterial;
        yield break;
      }
      elapsed += Time.deltaTime;
      float pct = elapsed / fadeDuration;
      color.a = Mathf.Lerp(1.0f, 0.0f, pct);
      renderer.material.color = color;
      yield return new WaitForEndOfFrame();
    }
    Destroy(gameObject);
  }

  public float GetValue() {
    var baseRate = 20.0f;
    var depthBonus = 10.0f * (stickable ? stickable.StackDepth : 0);
    var damageAdjustment = touchedGround ? 0.5f : 1.0f;
    return (baseRate + depthBonus) * damageAdjustment;
  }
}
