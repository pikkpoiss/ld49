using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour {
  public const string Player = "Player";
  public Material activeMaterial;
  public bool IsDropEnabled = false;

  private Material defaultMaterial;
  private MeshRenderer meshRenderer;

  void Start() {
    meshRenderer = GetComponent<MeshRenderer>();
    if (!meshRenderer) {
      return;
    }
    defaultMaterial = meshRenderer.material;
  }

  void OnTriggerEnter(Collider collider) {
    if (collider.CompareTag(Player)) {
      EnableDrop();
    }
  }

  void OnTriggerExit(Collider collider) {
    if (collider.CompareTag(Player)) {
      DisableDrop();
    }
  }

  private void EnableDrop() {
    IsDropEnabled = true;
    if (!meshRenderer) {
      return;
    }
    if (!activeMaterial) {
      return;
    }
    meshRenderer.material = activeMaterial;
  }

  private void DisableDrop() {
    IsDropEnabled = false;
    if (!meshRenderer) {
      return;
    }
    if (!defaultMaterial) {
      return;
    }
    meshRenderer.material = defaultMaterial;
  }
}
