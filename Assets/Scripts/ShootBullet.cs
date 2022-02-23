using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : MonoBehaviour
{
  [SerializeField] float trippleBulletAngle = 30f;

  private ObjectPoolManager _objectPoolManager;

  void Start()
  {
    GameObject bulletPools = GameObject.Find("Player_Bullet_Pools");
    _objectPoolManager = bulletPools.GetComponent<ObjectPoolManager>();
    _objectPoolManager.Initialize();
  }

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      StartShootBullet();
    }
    if (Input.GetKeyDown(KeyCode.E))
    {
      RotateBulletTypeNext();
    }
    else if (Input.GetKeyDown(KeyCode.Q))
    {
      RotateBulletTypePrev();
    }
  }

  void StartShootBullet()
  {
    switch (_objectPoolManager.CurrentPoolType)
    {
      case "Normal_Bullet":
        {
          GameObject bullet = _objectPoolManager.ExtractPool();
          if (bullet != null)
          {
            bullet.transform.position = transform.position;
          }
          break;
        }
      case "Tripple_Bullet":
        {
          GameObject leftBullet = _objectPoolManager.ExtractPool();
          if (leftBullet != null)
          {
            leftBullet.transform.position = transform.position;
            leftBullet.transform.Rotate(Vector3.up, -trippleBulletAngle);
          }
          GameObject middleBullet = _objectPoolManager.ExtractPool();
          if (middleBullet != null)
          {
            middleBullet.transform.position = transform.position;
          }
          GameObject rightBullet = _objectPoolManager.ExtractPool();
          if (rightBullet != null)
          {
            rightBullet.transform.position = transform.position;
            rightBullet.transform.Rotate(Vector3.up, trippleBulletAngle);
          }
          break;
        }
    }
  }

  void RotateBulletTypePrev()
  {
    _objectPoolManager.RotatePoolOption(-1);
  }

  void RotateBulletTypeNext()
  {
    _objectPoolManager.RotatePoolOption(1);
  }
}
