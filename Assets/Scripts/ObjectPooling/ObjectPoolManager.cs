using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
  [SerializeField] List<ObjectPool> poolOptions;
  [SerializeField] string currentPoolType;
  public string CurrentPoolType
  {
    get { return currentPoolType; }
  }

  private Dictionary<string, Queue<GameObject>> _poolCollection;
  private int _currentPoolOptionIndex = 0;

  void Start()
  {
    if (poolOptions.Count > 0)
    {
      SetPoolOption(0);
    }
  }

  void Update()
  { }

  public void Initialize(int currentPoolTypeIndex = 0)
  {
    SetPoolOption(currentPoolTypeIndex);
    _poolCollection = new Dictionary<string, Queue<GameObject>>();

    foreach (ObjectPool poolOption in poolOptions)
    {
      GameObject poolGameObj = new GameObject(poolOption.type + "_Pool");
      Queue<GameObject> poolData = new Queue<GameObject>();
      for (int i = 0; i < poolOption.size; i++)
      {
        GameObject objectToPool = Instantiate(poolOption.prefab, Vector3.zero, Quaternion.identity);
        ObjectToPool objectToPoolComp = objectToPool.GetComponent<ObjectToPool>();
        objectToPoolComp.PoolManager = this;
        objectToPoolComp.Parent = poolGameObj;
        objectToPoolComp.PoolType = poolOption.type;
        objectToPool.SetActive(false);
        objectToPool.transform.parent = poolGameObj.transform;
        poolData.Enqueue(objectToPool);
      }
      poolGameObj.transform.SetParent(gameObject.transform);
      poolGameObj.transform.localPosition = Vector3.zero;

      _poolCollection.Add(poolOption.type, poolData);
    }
  }

  public GameObject ExtractPool()
  {
    Queue<GameObject> poolData = _poolCollection[currentPoolType];
    if (poolData.Count <= 0)
    {
      return null;
    }
    GameObject objectToPool = poolData.Dequeue();
    objectToPool.SetActive(true);
    return objectToPool;
  }

  public void PushPool(GameObject objectToPool)
  {
    ObjectToPool objectToPoolComp = objectToPool.GetComponent<ObjectToPool>();
    objectToPool.SetActive(false);
    objectToPool.transform.localPosition = Vector3.zero;
    objectToPool.transform.rotation = Quaternion.identity;

    Queue<GameObject> poolData = _poolCollection[objectToPoolComp.PoolType];
    poolData.Enqueue(objectToPool);
  }

  public void SetPoolOption(int index)
  {
    _currentPoolOptionIndex = (index + poolOptions.Count) % poolOptions.Count;
    currentPoolType = poolOptions[_currentPoolOptionIndex].type;
  }

  public void RotatePoolOption(int step)
  {
    SetPoolOption(_currentPoolOptionIndex + step);
  }
}
