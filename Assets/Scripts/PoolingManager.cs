using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string key;
        public GameObject prefab;
        public int initAmount;
        public int additionalAmount;
        private Transform holder;
        private Queue<GameObject> queue;

        public Pool(string key, GameObject prefab, int initAmount, int additionalAmount)
        {
            this.key = key;
            this.prefab = prefab;
            this.initAmount = initAmount;
            this.additionalAmount = additionalAmount;
        }

        public void Init()
        {
            holder = new GameObject(key + "Holder").transform;
            holder.transform.SetParent(Instance.transform);

            queue = new Queue<GameObject>();

            for (int i = 0; i < initAmount; i++)
            {
                GameObject go = Object.Instantiate(prefab, holder);
                go.SetActive(false);
                go.name = key;
                queue.Enqueue(go);
            }
        }

        public void Destroy()
        {
            while (queue.Count > 0)
                Object.Destroy(queue.Dequeue());

            Object.Destroy(holder.gameObject);
        }

        public GameObject Spawn()
        {
            if (queue.Count == 0)
            {
                for (int i = 0; i < additionalAmount; i++)
                {
                    GameObject go = Object.Instantiate(prefab, holder);
                    go.SetActive(false);
                    go.name = key;
                    queue.Enqueue(go);
                }
            }

            return queue.Dequeue();
        }

        public void DeSpawn(GameObject go)
        {
            go.SetActive(false);
            go.transform.SetParent(holder);
            queue.Enqueue(go);
        }
    }

    public static PoolingManager Instance;

    [SerializeField] private bool dontDestroy;

    [Space(10)]
    [SerializeField] private Pool[] pools;
    private Dictionary<string, Pool> poolDic = new Dictionary<string, Pool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (dontDestroy) DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Initialize pools
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].Init();
            poolDic.Add(pools[i].key, pools[i]);
        }
    }

    public void CreatePool(Pool pool)
    {
        pool.Init();
        poolDic.Add(pool.key, pool);
    }

    public void CreatePool(string key, GameObject prefab, int initAmount, int additionalAmount)
    {
        Pool pool = new Pool(key, prefab, initAmount, additionalAmount);
        pool.Init();
        poolDic.Add(key, pool);
    }

    public void RemovePool(string key)
    {
        if (poolDic.ContainsKey(key)) poolDic.Remove(key);
    }

    public void DestroyPool(string key)
    {
        if (poolDic.ContainsKey(key))
        {
            poolDic[key].Destroy();
            poolDic.Remove(key);
        }
    }

    public GameObject Spawn(string key)
    {
        if (!poolDic.ContainsKey(key)) return null;

        GameObject go = poolDic[key].Spawn();
        go.SetActive(true);

        return go;
    }

    public GameObject Spawn(string key, Transform parent)
    {
        if (!poolDic.ContainsKey(key)) return null;

        GameObject go = poolDic[key].Spawn();
        go.transform.SetParent(parent);
        go.SetActive(true);

        return go;
    }

    public GameObject Spawn(string key, Vector3 pos, bool world = true)
    {
        if (!poolDic.ContainsKey(key)) return null;

        GameObject go = poolDic[key].Spawn();
        if (world)
        {
            go.transform.position = pos;
        }
        else
        {
            go.transform.localPosition = pos;
        }
        go.SetActive(true);

        return go;
    }

    public GameObject Spawn(string key, Quaternion rot, bool world = true)
    {
        if (!poolDic.ContainsKey(key)) return null;

        GameObject go = poolDic[key].Spawn();
        if (world)
        {
            go.transform.rotation = rot;
        }
        else
        {
            go.transform.localRotation = rot;
        }
        go.SetActive(true);

        return go;
    }

    public GameObject Spawn(string key, Vector3 pos, Quaternion rot, bool world = true)
    {
        if (!poolDic.ContainsKey(key)) return null;

        GameObject go = poolDic[key].Spawn();
        if (world)
        {
            go.transform.position = pos;
            go.transform.rotation = rot;
        }
        else
        {
            go.transform.localPosition = pos;
            go.transform.localRotation = rot;
        }
        go.SetActive(true);

        return go;
    }

    public GameObject Spawn(string key, Transform parent, Vector3 pos, bool world = true)
    {
        if (!poolDic.ContainsKey(key)) return null;

        GameObject go = poolDic[key].Spawn();
        go.transform.SetParent(parent);
        if (world)
        {
            go.transform.position = pos;
        }
        else
        {
            go.transform.localPosition = pos;
        }
        go.SetActive(true);

        return go;
    }

    public GameObject Spawn(string key, Transform parent, Vector3 pos, Quaternion rot, bool world = true)
    {
        if (!poolDic.ContainsKey(key)) return null;

        GameObject go = poolDic[key].Spawn();
        go.transform.SetParent(parent);
        if (world)
        {
            go.transform.position = pos;
            go.transform.rotation = rot;
        }
        else
        {
            go.transform.localPosition = pos;
            go.transform.localRotation = rot;
        }
        go.SetActive(true);

        return go;
    }

    public void Despawn(GameObject go)
    {
        if (!poolDic.ContainsKey(go.name)) return;
        poolDic[go.name].DeSpawn(go);
    }

    public void Despawn(GameObject go, float delay)
    {
        if (!poolDic.ContainsKey(go.name)) return;
        StartCoroutine(DespawnCoroutine(go, delay));
    }

    private IEnumerator DespawnCoroutine(GameObject go, float delay)
    {
        yield return new WaitForSeconds(delay);
        poolDic[go.name].DeSpawn(go);
    }
}
