using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

  public const string GroundTag = "Ground";
  public Material fadeMaterial;

  public float fadeDuration = 2.0f;
  public float timeBeforeCleanup = 10.0f;

  void OnCollisionEnter(Collision collision) {
    if (collision.collider.CompareTag(GroundTag)) {
      StartCoroutine(AnimateDeath());
    }
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
     renderer.material = fadeMaterial;
     renderer.material.color = color;
     float elapsed = 0.0f;
     while (elapsed < fadeDuration) {
      elapsed += Time.deltaTime;
      float pct = elapsed / fadeDuration;
      color.a = Mathf.Lerp(1.0f, 0.0f, pct);
      renderer.material.color = color;
      yield return new WaitForEndOfFrame();
    }
    Destroy(gameObject);
  }
}
