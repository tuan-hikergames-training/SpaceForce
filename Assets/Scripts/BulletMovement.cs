using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
  [SerializeField] float speed = 5f;
  [SerializeField] float verticalBound = 4.5f;
  [SerializeField] float horizontalBound = 6.5f;

  private ObjectToPool _objectToPool;

  void Start()
  {
    _objectToPool = GetComponent<ObjectToPool>();
  }

  void FixedUpdate()
  {
    transform.Translate(speed * Time.deltaTime * Vector3.forward);
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
