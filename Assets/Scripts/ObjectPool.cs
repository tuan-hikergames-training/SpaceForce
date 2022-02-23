using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ObjectPool
{
  public string type;
  public GameObject prefab;
  public int size;
}
