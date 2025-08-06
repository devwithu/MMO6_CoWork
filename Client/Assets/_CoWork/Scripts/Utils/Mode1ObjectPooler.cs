using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//오브젝트풀링
[System.Serializable]
public class ObjectPoolEnemy
{
    public GameObject enemyObjectToPool; //풀링할 오브젝트
    public int amountToPool; //풀링을 할 양
    public bool shouldExpand; //많으면 늘어나는거..
}

public class Mode1ObjectPooler : MonoBehaviour
{
    public Transform enemyPool; //풀링할 위치
    public static Mode1ObjectPooler sharedInstance; //인스턴스
    public List<ObjectPoolEnemy> enemyToPool; //몬스터 풀링
    public List<GameObject> pooledObjects; //풀링된 오브젝트

    public void Awake()
    {
        sharedInstance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        foreach (ObjectPoolEnemy item in enemyToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.enemyObjectToPool); //enemy 오브젝트 생성

                obj.transform.parent = enemyPool; //생성위치
                obj.SetActive(false);
                pooledObjects.Add(obj); 
            }
        }
    }
    
   public GameObject GetPooledEnemyObject(string tag)
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
            {
                return pooledObjects[i];
            }

        }
        foreach (ObjectPoolEnemy item in enemyToPool)
        {
            if(item.enemyObjectToPool.tag == tag)
            {
                if (item.shouldExpand)
                {
                    GameObject obj = (GameObject)Instantiate(item.enemyObjectToPool);
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                }
            }
        }
        return null;
    }
}
