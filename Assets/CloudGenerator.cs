using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CloudGenerator : MonoBehaviour {
  [Header("Components")]
  [SerializeField] private Transform[] clouds;
  
  [Header("Params")]
  [SerializeField] private float height;
  [SerializeField] private int timeoutBetweenCloudsSpawn = 5;

  private Vector2 _position;
  private int nextCloudSpawnTime;
    
  private void Start() {
    nextCloudSpawnTime = timeoutBetweenCloudsSpawn;
  }

  private void Update() {
    _position = transform.position;
  }
  
  private void FixedUpdate() {
    if (Time.time >= nextCloudSpawnTime) {
      SpawnCloud();
      nextCloudSpawnTime += timeoutBetweenCloudsSpawn;
    }
  }

  private void SpawnCloud() {
    int random = Random.Range(0, clouds.Length);

    Instantiate(
      clouds[random],
      new Vector2(
        _position.x, 
        _position.y + Random.Range(-height / 2f, height / 2f)
      ),
      Quaternion.identity);
  }

  private void OnDrawGizmos() { 
    Gizmos.DrawWireCube(transform.position, new Vector3(1f, height, 0f));
  }
}
