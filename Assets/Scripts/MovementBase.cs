using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectToPool))]
public class MovementBase : MonoBehaviour
{
  [SerializeField] float verticalBound = 4.5f;
  [SerializeField] float horizontalBound = 6.5f;
  [SerializeField] protected float speed = 5f;

  protected ObjectToPool _objectToPool;

  protected void MoveInBounds()
  {
    float xPos = transform.position.x;
    float zPos = transform.position.z;
    if (!IsBetween(xPos, -horizontalBound, horizontalBound) || !IsBetween(zPos, -verticalBound, verticalBound))
    {
      _objectToPool.PoolManager.PushPool(gameObject);
    }
  }

  private bool IsBetween(float value, float min, float max)
  {
    return value >= min && value <= max;
  }
}
