using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRandomizer : MonoBehaviour {
  [MinMaxSlider(1.0f, 10.0f, FlexibleFields = true)] public Vector2 heightRange = new Vector2(2.0f, 5.0f);
  [MinMaxSlider(-1.0f, 1.0f)] public Vector2 positionRange = new Vector2(-0.1f, 0.1f);

  void Awake() {
    // Randomize scale;
    var scale = transform.localScale;
    scale.y = Random.Range(heightRange.x, heightRange.y);
    transform.localScale = scale;

    // Randomize position;
    var newPosition = new Vector3(
      transform.position.x + Random.Range(positionRange.x, positionRange.y),
      scale.y / 2.0f,
      transform.position.z + Random.Range(positionRange.x, positionRange.y)
    );
    transform.position = newPosition;

    // Randomize color;
    var renderer = gameObject.GetComponent<MeshRenderer>();
    renderer.material.color = Random.ColorHSV(0.0f, 0.1f);
  }
}
