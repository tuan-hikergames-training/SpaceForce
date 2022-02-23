using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  public int row;

  [SerializeField] float verticalBound = 10f;
  [SerializeField] float horizontalBound = 10f;

  private ObjectToPool _objectToPool;
  private float _speed = 3f;


  void Start()
  {
    _objectToPool = GetComponent<ObjectToPool>();
  }

  void FixedUpdate()
  {
    if (transform.position.z >= 1.5f * row)
    {
      transform.Translate(_speed * Time.deltaTime * Vector3.forward);
      float xPos = transform.position.x;
      float zPos = transform.position.z;
      if (!IsBetween(xPos, -horizontalBound, horizontalBound) || !IsBetween(zPos, -verticalBound, verticalBound))
      {
        _objectToPool.PoolManager.PushPool(gameObject);
      }
    }
  }

  bool IsBetween(float value, float min, float max)
  {
    return value >= min && value <= max;
  }
}
