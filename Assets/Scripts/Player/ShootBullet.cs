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
    public float fireRate = 1f;
  }

  [SerializeField] float trippleBulletAngle = 30f;
  [SerializeField] Image currentBulletImage;
  [SerializeField] List<BulletResource> bulletResourceOptions;

  private ObjectPoolManager _objectPoolManager;
  private Dictionary<string, BulletResource> _bulletImageCollection;
  private float _currentFireRateDelay;
  private float _fireRateTimer;
  private float _fireRateTimerError;

  void Awake()
  {
    _bulletImageCollection = new Dictionary<string, BulletResource>();
    foreach (BulletResource bulletResource in bulletResourceOptions)
    {
      _bulletImageCollection.Add(bulletResource.type, bulletResource);
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
    if (_fireRateTimer > 0)
    {
      _fireRateTimer -= Time.deltaTime;
    }
    else if (_fireRateTimer < 0)
    {
      _fireRateTimerError = _fireRateTimer;
    }

    //if (Input.GetMouseButtonDown(0))
    //{
    StartShootBullet();
    //}
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
    if (_fireRateTimer > 0)
    {
      return;
    }

    // Add fireRateTimerError when the last frame until next available shoot action took longer that the fireRateTimer value
    _fireRateTimer = _currentFireRateDelay + _fireRateTimerError;
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
      _currentFireRateDelay = 1 / _bulletImageCollection[_objectPoolManager.CurrentPoolType].fireRate;
      currentBulletImage.sprite = _bulletImageCollection[_objectPoolManager.CurrentPoolType].image;
    }
  }
}
