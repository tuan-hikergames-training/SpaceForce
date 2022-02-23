using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingMovement : MonoBehaviour
{
  [SerializeField] float guideSpeed = 0.5f;
  [SerializeField] float finalGuideRadius = 12f;
  [SerializeField] float steerSpeed = 20f;

  private SphereCollider _sphereCollider;
  private GameObject _detectedObject;

  void Start()
  {
    _sphereCollider = GetComponent<SphereCollider>();
  }

  void Update()
  {
    if (_detectedObject == null)
    {
      float newRadius = Mathf.Lerp(_sphereCollider.radius, finalGuideRadius, Time.deltaTime * guideSpeed);
      _sphereCollider.radius = newRadius;
    }
    else if (!_detectedObject.activeSelf)
    {
      _detectedObject = null;
    }
  }

  void FixedUpdate()
  {
    if (_detectedObject != null && _detectedObject.activeSelf)
    {
      Transform parentTransform = transform.parent;
      Vector3 direction = _detectedObject.transform.position - parentTransform.position;
      Quaternion toRotation = Quaternion.FromToRotation(parentTransform.forward, direction);
      parentTransform.rotation = Quaternion.Lerp(parentTransform.rotation, toRotation, steerSpeed * Time.deltaTime);
    }
  }

  // TODO: Trigger not re-called for new target when old target is disabled
  void OnTriggerEnter(Collider other)
  {
    if ((_detectedObject == null || !_detectedObject.activeSelf) && other.gameObject.CompareTag("Enemy"))
    {
      _detectedObject = other.gameObject;
    }
  }
}
