using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalEnemyBehaviour : MonoBehaviour
{
  [SerializeField] float sinRange = 1.5f;
  [SerializeField] float sinSpeed = 4f;

  void Start()
  {

  }

  void Update()
  {
    float range = Mathf.Sin(Time.time * sinSpeed) * sinRange;
    transform.Translate(range * Time.deltaTime * Vector3.right);
  }
}
