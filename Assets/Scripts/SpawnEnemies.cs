using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
  [SerializeField] float horizontalSpace = 6f;
  [SerializeField] float verticalSpace = 1.5f;
  [SerializeField] float verticalOffset = 8f;
  [SerializeField] int enemyPerWave = 20;

  private ObjectPoolManager _objectPoolManager;

  void Start()
  {
    _objectPoolManager = GetComponent<ObjectPoolManager>();
    _objectPoolManager.Initialize();

    Spawn();
  }

  void Update()
  {
  }

  void Spawn()
  {

    for (int i = 0; i < enemyPerWave; i++)
    {
      GameObject enemy = _objectPoolManager.ExtractPool();
      if (enemy != null)
      {
        int row = i % 4;
        int col = i % 5;
        enemy.GetComponent<EnemyMovement>().row = row;
        enemy.transform.Rotate(Vector3.up, 180f);
        enemy.transform.position = new Vector3(col * (horizontalSpace / 2) - horizontalSpace, 0, row * verticalSpace + verticalOffset);
      }
    }
  }
}
