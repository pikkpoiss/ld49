using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRandomizer : MonoBehaviour {
  [MinMaxSlider(0.1f, 5f, FlexibleFields = true)] public Vector2 scaleRange = new Vector2(0.7f, 1.3f);
  [MinMaxSlider(0.1f, 10.0f)] public Vector2 massRange = new Vector2(0.5f, 1.3f);
  [MinMaxSlider(-5.0f, 5.0f)] public Vector2 rotationRange = new Vector2(-1.0f, 1.0f);
  [MinMaxSlider(-1.0f, 1.0f)] public Vector2 positionRange = new Vector2(-0.1f, 0.1f);

  void Start() {
    // Randomize scale;
    var scale = transform.localScale;
    scale.x = Random.Range(scaleRange.x, scaleRange.y);
    scale.z = Random.Range(scaleRange.x, scaleRange.y);
    transform.localScale = scale;

    // Randomize mass;
    var body = GetComponent<Rigidbody>();
    if (body) {
      body.mass = Random.Range(massRange.x, massRange.y);
    }

    // Randomize rotation;
    transform.Rotate(Vector3.up, Random.Range(rotationRange.x, rotationRange.y));

    // Randomize position;
    var position = new Vector3();
    position.x = Random.Range(positionRange.x, positionRange.y);
    position.z = Random.Range(positionRange.x, positionRange.y);
    transform.position += position;

    // Randomize color;
    var renderer = gameObject.GetComponent<MeshRenderer>();
    renderer.material.color = Random.ColorHSV(0.0f, 0.1f);
  }
}
