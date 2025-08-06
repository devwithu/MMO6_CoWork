using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������ƮǮ��
[System.Serializable]
public class ObjectPoolEnemy
{
    public GameObject enemyObjectToPool; //Ǯ���� ������Ʈ
    public int amountToPool; //Ǯ���� �� ��
    public bool shouldExpand; //������ �þ�°�..
}

public class Mode1ObjectPooler : MonoBehaviour
{
    public Transform enemyPool; //Ǯ���� ��ġ
    public static Mode1ObjectPooler sharedInstance; //�ν��Ͻ�
    public List<ObjectPoolEnemy> enemyToPool; //���� Ǯ��
    public List<GameObject> pooledObjects; //Ǯ���� ������Ʈ

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
                GameObject obj = (GameObject)Instantiate(item.enemyObjectToPool); //enemy ������Ʈ ����

                obj.transform.parent = enemyPool; //������ġ
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
