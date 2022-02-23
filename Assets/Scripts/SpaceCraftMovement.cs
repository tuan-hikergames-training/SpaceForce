using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraftMovement : MonoBehaviour
{
  [SerializeField] float verticalBound = 4.5f;
  [SerializeField] float horizontalBound = 6.5f;

  void Start()
  { }

  void Update()
  {
    Plane plane = new Plane(Vector3.up, transform.position);
    Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (plane.Raycast(mouseRay, out float point))
    {
      Vector3 pointPos = mouseRay.GetPoint(point);
      float xPos = Mathf.Clamp(pointPos.x, -horizontalBound, horizontalBound);
      float zPos = Mathf.Clamp(pointPos.z, -verticalBound, verticalBound);
      transform.position = new Vector3(xPos, transform.position.y, zPos);
    }
  }
}
