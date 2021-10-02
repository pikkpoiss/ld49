using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
  private BoxCollider boxCollider;
  private Icon icon;

  void Start() {
    boxCollider = GetComponent<BoxCollider>();
    icon = GetComponentInChildren<Icon>();

    PickBuilding();
  }

  public void Update() {
    // TODO: Remove before shipping!
    if (Input.GetKeyUp(KeyCode.Alpha2)) {
      PickBuilding();
    }
  }

  public void PickBuilding() {
    var buildings = FindObjectsOfType<Building>();
    var randomBuilding = buildings[Random.Range(0, buildings.Length)];
    var dim = randomBuilding.GetDimensions();
    var pos = randomBuilding.GetPosition();

    // Scale the collider bigger than the building.
    boxCollider.size = new Vector3(
      dim.x + 2.0f,
      dim.y + 1.0f,
      dim.z + 2.0f
    );

    // Move the object to where the building is, and adjust collider y position.
    transform.position = new Vector3(
      pos.x,
      0.0f,
      pos.z
    );
    boxCollider.center = new Vector3(
      0.0f,
      boxCollider.size.y / 2.0f,
      0.0f
    );

    // Move the icon above the top of the collider.
    var iconPosition = icon.transform.position;
    iconPosition.y = boxCollider.size.y + 1.0f;
    icon.transform.position = iconPosition;
  }
}
