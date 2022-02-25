using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotation : MonoBehaviour
{
  public GameObject target;

  void Start()
  {
  }

  void Update()
  {
    transform.Translate(5f * Time.deltaTime * Vector3.forward);

    Vector3 direction = target.transform.position - transform.position;
    Quaternion toRotation = Quaternion.LookRotation(direction);
    transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 50 * Time.deltaTime);
  }
}
