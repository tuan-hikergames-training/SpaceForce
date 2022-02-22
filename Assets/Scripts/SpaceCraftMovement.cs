using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraftMovement : MonoBehaviour
{
  [SerializeField] float moveSpeed = 12f;
  [SerializeField] float turnSpeed = 8f;
  [SerializeField] float verticalBound = 4.5f;
  [SerializeField] float horizontalBound = 6.5f;

  private float _horizontalInput;
  private float _verticalInput;

  void Start()
  {

  }

  void Update()
  {
    _horizontalInput = Input.GetAxis("Horizontal");
    _verticalInput = Input.GetAxis("Vertical");
  }

  void FixedUpdate()
  {
    transform.Translate(_horizontalInput * turnSpeed * Time.deltaTime * Vector3.right);
    transform.Translate(_verticalInput * moveSpeed * Time.deltaTime * Vector3.forward);

    float xPos = Mathf.Clamp(transform.position.x, -horizontalBound, horizontalBound);
    float zPos = Mathf.Clamp(transform.position.z, -verticalBound, verticalBound);
    transform.position = new Vector3(xPos, transform.position.y, zPos);
  }
}
