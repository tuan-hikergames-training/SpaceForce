using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingRadar : MonoBehaviour
{
  [SerializeField] float steerSpeed = 20f;
  [SerializeField] float radarExpandSpeed = 0.5f;
  [SerializeField] float baseGuideRadius = 0.25f;
  [SerializeField] float finalGuideRadius = 10f;

  private SphereCollider _sphereCollider;
  private GameObject _parentObject;
  private GameObject _detectedObject;

  void Start()
  {
    _sphereCollider = GetComponent<SphereCollider>();
    _parentObject = transform.parent.gameObject;
  }

  void Update()
  {
    if (_detectedObject == null)
    {
      float newRadius = Mathf.Lerp(_sphereCollider.radius, finalGuideRadius, Time.deltaTime * radarExpandSpeed);
      _sphereCollider.radius = newRadius;
    }
    else if (!_detectedObject.activeSelf)
    {
      _detectedObject = null;
      _sphereCollider.radius = baseGuideRadius;
    }
    if (_detectedObject != null && _detectedObject.activeSelf)
    {
      Transform parentTransform = _parentObject.transform;
      Vector3 direction = _detectedObject.transform.position - parentTransform.position;
      Quaternion toRotation = Quaternion.FromToRotation(parentTransform.forward, direction);
      parentTransform.rotation = Quaternion.Lerp(parentTransform.rotation, toRotation, steerSpeed * Time.deltaTime);
    }
  }

  void FixedUpdate()
  {
  }

  void OnTriggerEnter(Collider other)
  {
    if ((_detectedObject == null || !_detectedObject.activeSelf) && other.gameObject.CompareTag("Enemy"))
    {
      _detectedObject = other.gameObject;
    }
  }
}
