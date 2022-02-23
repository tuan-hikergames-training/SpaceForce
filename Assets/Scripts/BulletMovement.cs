using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MovementBase
{
  void Start()
  {
    _objectToPool = GetComponent<ObjectToPool>();
  }

  void FixedUpdate()
  {
    transform.Translate(speed * Time.deltaTime * Vector3.forward);
    MoveInBounds();
  }
}
