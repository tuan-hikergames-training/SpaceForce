using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootBullet : MonoBehaviour
{
  [System.Serializable]
  public class BulletResource
  {
    public string type;
    public Sprite image;
  }

  [SerializeField] float trippleBulletAngle = 30f;
  [SerializeField] Image currentBulletImage;
  [SerializeField] List<BulletResource> bulletResourceOptions;

  private ObjectPoolManager _objectPoolManager;
  private Dictionary<string, Sprite> _bulletImageCollection;

  void Awake()
  {
    _bulletImageCollection = new Dictionary<string, Sprite>();
    foreach (BulletResource bulletResource in bulletResourceOptions)
    {
      _bulletImageCollection.Add(bulletResource.type, bulletResource.image);
    }
  }

  void Start()
  {
    GameObject bulletPools = GameObject.Find("Player_Bullet_Pools");
    _objectPoolManager = bulletPools.GetComponent<ObjectPoolManager>();
    _objectPoolManager.Initialize();
    SetCurrentBulletImage();
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
      case "Guided_Missle":
        {
          GameObject bullet = _objectPoolManager.ExtractPool();
          if (bullet != null)
          {
            bullet.transform.position = transform.position;
          }
          break;
        }
    }
  }

  void RotateBulletTypePrev()
  {
    RotateBulletType(-1);
  }

  void RotateBulletTypeNext()
  {
    RotateBulletType(-1);
  }

  void RotateBulletType(int step)
  {
    _objectPoolManager.RotatePoolOption(step);
    SetCurrentBulletImage();
  }

  void SetCurrentBulletImage()
  {
    if (_bulletImageCollection.ContainsKey(_objectPoolManager.CurrentPoolType))
    {
      currentBulletImage.sprite = _bulletImageCollection[_objectPoolManager.CurrentPoolType];
    }
  }
}
