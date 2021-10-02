using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour {
  public GameObject itemPrefab;
  public float startHeight = 8.0f;

  public const string SpawnButton = "Fire1";
  // Start is called before the first frame update
  void Start() {
    var count = Random.Range(3,6);
    for (var i = 0; i < count; i++) {
      Spawn(startHeight + (2.0f * i));
    }
  }

  // Update is called once per frame
  void Update() {
    if (itemPrefab && Input.GetButtonDown(SpawnButton)) {
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
  }
}
