using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MovementBase
{
  public int row;
  public float enemyInterval;

  [SerializeField] float exitSpeed = 15f;
  [SerializeField] float enemyBaseOffset = 2f;

  private float _speed;
  private bool _isExiting = false;

  void Start()
  {
    _objectToPool = GetComponent<ObjectToPool>();
    _speed = speed;
  }

  void FixedUpdate()
  {
    if (_isExiting || transform.position.z >= 1.5f * row + enemyBaseOffset)
    {
      transform.Translate(_speed * Time.deltaTime * Vector3.forward);
      MoveInBounds();
    }
  }

  public void InitExitingSequence()
  {
    Invoke(nameof(MarkExiting), enemyInterval);
  }

  public void ResetExiting()
  {
    _speed = speed;
    _isExiting = false;
  }

  void MarkExiting()
  {
    _speed = exitSpeed;
    _isExiting = true;
  }
}
