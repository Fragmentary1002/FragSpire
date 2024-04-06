using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace XFramework.Objects
{
    /// <summary>
    /// 对象池管理
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        private static ObjectPool instance;
        /// <summary>
        /// 存放所有对象池的字典
        /// 每一个原始对象都有一个id，每个id就对应一个对象池
        /// </summary>
        private Dictionary<int, SingleObjPool<GameObject>> originalDict;
        /// <summary>
        /// 存放所有已经被实例化对象
        /// 每一个被实例化后的对象都有相应的对象池
        /// </summary>
        private Dictionary<GameObject, SingleObjPool<GameObject>> objPoolDict;
        /// <summary>
        /// 场景里面的对象
        /// 用于将已经释放的对象收起来
        /// </summary>
        private GameObject poolManager;
        private SceneManager Game { get => SceneManager.Instance; }

        private void Awake()
        {
            instance = this;
            originalDict = new Dictionary<int, SingleObjPool<GameObject>>();
            objPoolDict = new Dictionary<GameObject, SingleObjPool<GameObject>>();
        }

        // Start is called before the first frame update
        void Start()
        {
            poolManager = GameObject.Find("PoolManager") ?? new GameObject("PoolManager");
        }

        /// <summary>
        /// 根据原始对象获取对应的对象池
        /// </summary>
        /// <param name="prefab">对象</param>
        /// <returns></returns>
        private SingleObjPool<GameObject> GetPool(GameObject prefab)
        {
            int id = prefab.GetInstanceID();

            if (!originalDict.ContainsKey(id))
            {
                SingleObjPool<GameObject> objPool = new SingleObjPool<GameObject>(prefab);
                originalDict.Add(id, objPool);
            }

            return originalDict[id];
        }

        /// <summary>
        /// 从对象池里获取一个对象
        /// </summary>
        /// <param name="prefab">对象</param>
        /// <returns></returns>
        private GameObject GetObject(GameObject prefab)
        {
            GameObject obj = GetPool(prefab).Get();

            if (!objPoolDict.ContainsKey(obj))
                objPoolDict.Add(obj, GetPool(prefab));
            obj.SetActive(true);

            return obj;
        }

        /// <summary>
        /// 从对象池取出一个对象
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static GameObject Instantiate(GameObject prefab)
        {
            GameObject obj = instance.GetObject(prefab);
            obj.transform.SetParent(null);

            return obj;
        }

        /// <summary>
        /// 从对象池取出一个对象
        /// </summary>
        /// <param name="prefab">对象</param>
        /// <param name="parent">父对象</param>
        /// <returns></returns>
        public static GameObject Instantiate(GameObject prefab, Transform parent)
        {
            GameObject obj = instance.GetObject(prefab);
            obj.transform.SetParent(parent);

            return obj;
        }

        /// <summary>
        /// 从对象池取出一个对象
        /// </summary>
        /// <param name="prefab">对象</param>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <returns></returns>
        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            GameObject obj = instance.GetObject(prefab);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.SetParent(null);

            return obj;
        }

        /// <summary>
        /// 从对象池取出一个对象
        /// </summary>
        /// <param name="prefab">对象</param>
        /// <param name="position">位置</param>
        /// <param name="rotation">旋转</param>
        /// <param name="parent">父对象</param>
        /// <returns></returns>
        public static GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject obj = instance.GetObject(prefab);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.SetParent(parent);

            return obj;
        }

        /// <summary>
        /// 立即回收对象
        /// </summary>
        /// <param name="prefab">要回收的对象</param>
        /// <param name="action">回收对象后执行的方法</param>
        public static void Destroy(GameObject prefab, UnityAction<GameObject> action = null)
        {
            if (!instance.objPoolDict.ContainsKey(prefab))
                return;

            SingleObjPool<GameObject> objPool = instance.objPoolDict[prefab];
            if (objPool.Release(prefab))
            {
                instance.objPoolDict.Remove(prefab);
                prefab.transform.SetParent(instance.poolManager.transform);
                prefab.SetActive(false);
                action?.Invoke(prefab);
            }
        }

        /// <summary>
        /// 延迟执行回收
        /// </summary>
        /// <param name="prefab">要回收的对象</param>
        /// <param name="time">延迟时间</param>
        /// <param name="action">回收对象后执行的方法</param>
        public static void Destroy(GameObject prefab, float time, UnityAction<GameObject> action = null)
        {
            instance.Game.StartCoroutine(instance.Delay(prefab, time, action));
        }

        /// <summary>
        /// 延迟
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="time"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        private IEnumerator Delay(GameObject prefab, float time, UnityAction<GameObject> action)
        {
            yield return new WaitForSeconds(time);
            Destroy(prefab, action);
        }
    }
}
