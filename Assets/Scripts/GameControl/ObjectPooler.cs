using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcade.Spawn { 
    [System.Serializable]
    public class ObjectPoolItem
    {
        public int amountToPool;
        public GameObject objectToPool;
        public bool shouldExpand;
    }

    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField]
        private List<ObjectPoolItem> itemsToPool;
        private List<GameObject> pooledObjects;
        public static ObjectPooler SharedInstance;



        // Start is called before the first frame update
        void Awake()
        {
            SharedInstance = this;
        }

        void Start()
        {
            pooledObjects = new List<GameObject>();
            foreach(ObjectPoolItem item in itemsToPool)
            {
                for(int i = 0; i < item.amountToPool; i ++)
                { 
                GameObject obj = (GameObject)Instantiate(item.objectToPool, this.gameObject.transform);
                obj.GetComponent<BaseCircle>().spawnListener = this.gameObject.GetComponent<Spawner>();
                obj.SetActive(false);
                pooledObjects.Add(obj);
                }
             
            }
        }

        public GameObject GetPooledObject(string tag)
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (!pooledObjects[i].activeInHierarchy && pooledObjects[i].tag == tag)
                {
                    return pooledObjects[i];
                }
            }
            foreach(ObjectPoolItem item in itemsToPool)
            {
                if (item.objectToPool.tag == tag)
                {
                    if(item.shouldExpand)
                    { 
                    GameObject obj = (GameObject)Instantiate(item.objectToPool, this.gameObject.transform);
                    obj.GetComponent<BaseCircle>().spawnListener = this.gameObject.GetComponent<Spawner>();
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                    return obj;
                    }
                }
            }
            return null;
        }


    }
}