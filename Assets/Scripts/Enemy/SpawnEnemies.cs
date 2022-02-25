using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
  [SerializeField] float spawnPosY = 9f;
  [SerializeField] float horizontalSpace = 2f;
  [SerializeField] int minEnemyColNum = 2;
  [SerializeField] int maxEnemyColNum = 6;
  [SerializeField] int minEnemyRowNum = 3;
  [SerializeField] int maxEnemyRowNum = 5;
  [SerializeField] float delayFirstWave = 2f;
  [SerializeField] float waveInterval = 12f;
  [SerializeField] float enemyInterval = 10f;

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
    int rowNum = Random.Range(minEnemyRowNum, maxEnemyRowNum + 1);
    int colNum = Random.Range(minEnemyColNum, maxEnemyColNum + 1);
    int enemyPerWave = rowNum * colNum;

    float halfRange = ((colNum - 1) * horizontalSpace) / 2;

    for (int i = 0; i < enemyPerWave; i++)
    {
      GameObject enemy = _objectPoolManager.ExtractPool();
      if (enemy != null)
      {
        EnemyMovement enemyMovementComp = enemy.GetComponent<EnemyMovement>();
        int row = i / colNum;
        int col = i % colNum;
        enemyMovementComp.row = row;
        enemyMovementComp.enemyInterval = enemyInterval;
        enemyMovementComp.ResetExiting();
        enemyMovementComp.InitExitingSequence();
        enemy.transform.position = new Vector3(col * horizontalSpace - halfRange, 0.25f, spawnPosY);
        enemy.transform.Rotate(Vector3.up, 180f);
      }
    }
    Invoke(nameof(Spawn), waveInterval);
  }
}
