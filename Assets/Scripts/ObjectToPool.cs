using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToPool : MonoBehaviour
{
  private ObjectPoolManager _poolManager;
  public ObjectPoolManager PoolManager
  {
    get
    {
      return _poolManager;
    }
    set
    {
      _poolManager = value;
    }
  }

  private GameObject _parent;
  public GameObject Parent
  {
    get
    {
      return _parent;
    }
    set
    {
      _parent = value;
    }
  }

  private string _poolType;
  public string PoolType
  {
    get
    {
      return _poolType;
    }
    set
    {
      _poolType = value;
    }
  }
}
