using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Goal : MonoBehaviour {
  public const string ItemTag = "Item";
  private BoxCollider boxCollider;
  private Icon icon;
  public GamePlayState state;
  public TextMeshPro buildingText;

  void Start() {
    boxCollider = GetComponent<BoxCollider>();
    icon = GetComponentInChildren<Icon>();

    SetBuildingText("");
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
    if (icon) {
      var iconPosition = icon.transform.position;
      iconPosition.y = boxCollider.size.y + 1.0f;
      icon.transform.position = iconPosition;
    }

    // Move the building text.
    if (buildingText) {
      var textPosition = buildingText.transform.position;
      textPosition.y = boxCollider.size.y + 4.0f;
      buildingText.transform.position = textPosition;
    }
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.collider.CompareTag(ItemTag)) {
      var item = collision.collider.gameObject.GetComponent<Item>();
      if (state && item) {
        state.ReportDelivery(item);
      }
    }
  }

  public void SetBuildingText(string text) {
    if (text != "") {
      buildingText.gameObject.SetActive(true);
      buildingText.text = text;
      buildingText.gameObject.transform.rotation = Camera.main.transform.rotation;
    } else {
      buildingText.gameObject.SetActive(false);
    }
  }
}
