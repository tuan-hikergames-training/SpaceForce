using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectToPool))]
public class EnemyGetHit : MonoBehaviour
{
  private ObjectToPool _objectToPool;

  void Start()
  {
    _objectToPool = GetComponent<ObjectToPool>();
  }

  void Update()
  {

  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("PlayerBullet"))
    {
      _objectToPool.PoolManager.PushPool(gameObject);
      other.gameObject.GetComponent<ObjectToPool>().PoolManager.PushPool(other.gameObject);
    }
  }
}
