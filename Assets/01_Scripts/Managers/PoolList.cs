using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolData
{
    public GameObject obj;
    public int num;
}


public class PoolList : ScriptableObject
{
    public List<PoolData> poolList;
}
