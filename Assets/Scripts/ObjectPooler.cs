using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;      
        public GameObject prefab; 
        public int size;         
    }
    private static ObjectPooler instance;
    public static ObjectPooler Instance { get { return instance; } }
    [SerializeField] private List<Pool> pools = new List<Pool>();
    private Dictionary<string, Queue<GameObject>> objectPoolDictionary = new Dictionary<string, Queue<GameObject>>();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> queue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(transform); 
                queue.Enqueue(obj);
            }

            objectPoolDictionary.Add(pool.tag, queue);
        }
    }

    public GameObject GetPoolObject(string tag, Vector3? position = null, Quaternion? rotation = null, Transform transformParent = null)
    {
        if (!objectPoolDictionary.ContainsKey(tag))
        {
            Debug.LogError("Pool with tag: " + tag + " does not exist.");
            return null;
        }

        if (objectPoolDictionary[tag].Count == 0 || objectPoolDictionary[tag].Peek().activeInHierarchy)
        {
            Pool pool = pools.Find(p => p.tag == tag);
            if (pool != null)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                obj.transform.SetParent(transform);
                objectPoolDictionary[tag].Enqueue(obj);
            }
            else
            {
                Debug.LogError("Prefab với tag: " + tag + " không được tìm thấy.");
                return null;
            }
        }

        GameObject objectToSpawn = objectPoolDictionary[tag].Dequeue();

        objectToSpawn.transform.SetParent(transformParent);

        if (transformParent != null)
        {
            objectToSpawn.transform.localPosition = position ?? Vector3.zero;
            objectToSpawn.transform.localRotation = rotation ?? Quaternion.identity;
        }
        else
        {
            objectToSpawn.transform.position = position ?? objectToSpawn.transform.position;
            objectToSpawn.transform.rotation = rotation ?? objectToSpawn.transform.rotation;
        }

        objectToSpawn.SetActive(true);

        objectPoolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

}
