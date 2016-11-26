using UnityEngine;
using System.Collections.Generic;

namespace UCFW
{
    /// <summary>
    /// A struct to serialize objectpools
    /// </summary>
    [System.Serializable]
    public struct ObjectPoolDesc
    {
        public GameObject prefab;
        public int capacity;

        /// <summary>
        /// Creates a new the pool
        /// </summary>
        /// <param name="parent">the parent of the poolobjects</param>
        /// <returns></returns>
        public ObjectPool Create(Transform parent = null)
        {
            return new ObjectPool(prefab, capacity, parent);
        }

        public ObjectPool CreateAndInit(Transform parent = null)
        {
            ObjectPool o = Create(parent);
            o.Init();
            return o;
        }
    }
    /// <summary>
    /// Handles objectpooling for one prefab
    /// </summary>
    public class ObjectPool
    {
        /// <summary>
        /// Create a new object pool
        /// </summary>
        /// <param name="prefab">the gameobject that will be pooled</param>
        /// <param name="count">the capacity of the pool</param>
        /// <param name="parent">the parent of the pooled objects</param>
        public ObjectPool(GameObject prefab, int count, Transform parent = null)
        {
            _prefab = prefab;
            _count = count;
            _parent = parent;
        }

        private Transform _parent = null;
        private int _count = 0;
        private GameObject _prefab = null;


        private PoolComponent[] _objects = null;
        private int _activeIndex = 0;
        private List<GameObject> _actives = new List<GameObject>();

        public GameObject[] activeObjects
        {
            get { return _actives.ToArray(); }
        }

        public T[] GetComponentsOfActiveObjects<T>() where T : Component
        {
            T[] res = new T[_actives.Count];

            for(int i=0; i<res.Length; ++i)
            {
                res[i] = _actives[i].GetComponent<T>();
            }

            return res;
        }



        /// <summary>
        /// Initialize the objectpool
        /// </summary>
        /// <returns></returns>
        public bool Init()
        {
            if (_count < 1 || _prefab == null || _objects != null) return false;

            _objects = new PoolComponent[_count];

            if (_prefab.GetComponent<PoolComponent>() == null)
            {
                _prefab.AddComponent<PoolComponent>();
                Debug.LogWarning("Added PoolComponent to Prefab: " + _prefab.name);
            }

            for (int i = 0; i < _objects.Length; ++i)
            {
                GameObject g = Object.Instantiate(_prefab);
                g.transform.parent = _parent;
                _objects[i] = g.GetComponent<PoolComponent>();
                if(!_objects[i].Init(this))
                {
                    Debug.LogError("Initialization of pooled object failed");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Deinitialize the objectpool
        /// </summary>
        public void DeInit()
        {
            if (_objects == null) return;

            for(int i=0; i<_objects.Length; ++i)
            {
                Object.Destroy(_objects[i]);
            }
        }

        /// <summary>
        /// Spawn an object
        /// </summary>
        /// <param name="position">the position where the object shall spawn</param>
        /// <param name="rotation">the new rotation of the object</param>
        /// <returns></returns>
        public GameObject Spawn(Vector3 position = new Vector3(), Quaternion rotation = new Quaternion())
        {
            if (_objects == null || _activeIndex >= _objects.Length) return null;

            if (!SpawnInstance(_activeIndex, position, rotation)) return null;

            GameObject go =  _objects[_activeIndex++].gameObject;
            _actives.Add(go);
            return go;
        }

        /// <summary>
        /// Spawn an object an get a specific component directly
        /// </summary>
        /// <typeparam name="T">the type of the component</typeparam>
        /// <param name="position">the position where the object shall spawn</param>
        /// <param name="rotation">the new rotation of the object</param>
        /// <returns></returns>
        public T Spawn<T>(Vector3 position = new Vector3(), Quaternion rotation = new Quaternion()) where T : Component
        {
            GameObject go = Spawn(position, rotation);

            return go == null ? null : go.GetComponent<T>();
        }

        /// <summary>
        /// Helping method to spawn an object
        /// </summary>
        /// <param name="index">the index in the array</param>
        /// <param name="position">the position</param>
        /// <param name="rotation">the rotation</param>
        /// <returns></returns>
        private bool SpawnInstance(int index, Vector3 position, Quaternion rotation)
        {
            GameObject go = _objects[index].gameObject;
            if (go.activeSelf) return false;

            Transform t = go.transform;
            t.rotation = rotation;
            t.position = position;

            go.SetActive(true);

            PoolComponent p = go.GetComponent<PoolComponent>();

            p.Spawn();

            return true;
        }

        /// <summary>
        /// Recycle an object of the pool
        /// </summary>
        /// <param name="poolBehaviour">the object that shall be recycled</param>
        /// <returns></returns>
        public bool Recycle(PoolBehaviour poolBehaviour)
        {
            return Recycle(poolBehaviour.poolComponent);
        }

        /// <summary>
        /// Recycle an object of the pool
        /// </summary>
        /// <param name="poolObject">the object that shall be recycled</param>
        /// <returns></returns>
        public bool Recycle(GameObject poolObject)
        {
            return Recycle(poolObject.GetComponent<PoolComponent>());
        }

        /// <summary>
        /// Recycle an object of the pool
        /// </summary>
        /// <param name="component">the object that shall be recycled</param>
        /// <returns></returns>
        public bool Recycle(PoolComponent component)
        {
            if (_activeIndex < 1 || _objects == null || component == null) return false;

            for (int i = 0; i < _activeIndex; ++i)
            {
                if (_objects[i] == component)
                {
                    if (RecycleComponent(i))
                    {
                        _objects[i] = _objects[--_activeIndex];
                        _objects[_activeIndex] = component;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Helping method to recycle an object
        /// </summary>
        /// <param name="index">the index in the array</param>
        /// <returns></returns>
        bool RecycleComponent(int index)
        {
            GameObject go = _objects[index].gameObject;
            if (!go.activeSelf) return false;
            go.SetActive(false);
            _actives.Remove(go);

            return true;
        }

        /// <summary>
        /// Recycle all object from the pool
        /// </summary>
        public void RecycleAll()
        {
            if (_objects == null) return;

            for (int i = 0; i < _activeIndex; ++i)
            {
                _objects[i].gameObject.SetActive(false);
                _objects[i].Recycle();
            }
            _actives.Clear();
            _activeIndex = 0;
        }
    }
}