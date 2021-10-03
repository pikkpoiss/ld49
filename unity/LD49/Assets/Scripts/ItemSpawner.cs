using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
  public GameObject itemPrefab;
  public float startHeight = 8.0f;
  public const string SpawnZone = "SpawnZone";
  public const string SpawnButton = "Fire1";
  public GamePlayState state;

  private SpawnZone spawnZone;

  void Start() {
    var count = Random.Range(3,6);
    for (var i = 0; i < count; i++) {
      Spawn(startHeight + (2.0f * i));
    }
  }

  void Update() {
    if (itemPrefab && spawnZone && spawnZone.IsDropEnabled && Input.GetButtonDown(SpawnButton)) {
      Spawn(startHeight);
    }
  }

  private void Spawn(float height) {
    var spawnPosition = transform.position + Vector3.up * height;
    var instance = Instantiate(itemPrefab, spawnPosition, Quaternion.identity, transform.parent);
    var instanceBody = instance.GetComponent<Rigidbody>();
    if (instanceBody) {
      instanceBody.WakeUp();
      instanceBody.AddForce(Vector3.down * 4.0f, ForceMode.VelocityChange);
    }
    var item = instance.GetComponent<Item>();
    if (item) {
      item.state = state;
    }
  }

  void OnTriggerEnter(Collider collider) {
    if (collider.CompareTag(SpawnZone)) {
      spawnZone = collider.GetComponent<SpawnZone>();
    }
  }

  void OnTriggerExit(Collider collider) {
    if (collider.CompareTag(SpawnZone)) {
      spawnZone = null;
    }
  }
}
