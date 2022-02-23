using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
  [SerializeField] float verticalBound = 9f;
  [SerializeField] float horizontalSpace = 6f;
  [SerializeField] int rowNum = 4;
  [SerializeField] int enemyPerWave = 20;
  [SerializeField] float delayFirstWave = 2f;
  [SerializeField] float waveInterval = 12f;

  private ObjectPoolManager _objectPoolManager;

  void Start()
  {
    GameObject enemyPools = GameObject.Find("Enemy_Pools");
    _objectPoolManager = enemyPools.GetComponent<ObjectPoolManager>();
    _objectPoolManager.Initialize();

    Invoke(nameof(Spawn), delayFirstWave);
  }

  void Update()
  {
  }

  void Spawn()
  {
    int colNum = enemyPerWave / rowNum;
    for (int i = 0; i < enemyPerWave; i++)
    {
      GameObject enemy = _objectPoolManager.ExtractPool();
      if (enemy != null)
      {
        int row = i / colNum;
        int col = i % colNum;
        enemy.GetComponent<EnemyMovement>().row = row;
        enemy.GetComponent<EnemyMovement>().ResetExiting();
        enemy.GetComponent<EnemyMovement>().InitExitingSequence();
        enemy.transform.position = new Vector3(col * (horizontalSpace / 2) - horizontalSpace, 0.25f, verticalBound);
        enemy.transform.Rotate(Vector3.up, 180f);
      }
    }
    Invoke(nameof(Spawn), waveInterval);
  }
}
