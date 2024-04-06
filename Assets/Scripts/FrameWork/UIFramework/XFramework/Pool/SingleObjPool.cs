using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework.Objects
{
    /// <summary>
    /// 单个对象池
    /// </summary>
    public class SingleObjPool<T> where T:Object
    {
        /// <summary>
        /// 已经使用过的对象
        /// </summary>
        private Dictionary<T, ObjectContainer<T>> useDict;
        /// <summary>
        /// 没有使用过的对象列表
        /// </summary>
        private List<ObjectContainer<T>> unUseList;
        /// <summary>
        /// 该对象池存储的原始对象
        /// 之后的对象都是其克隆体
        /// </summary>
        private T original;

        /// <summary>
        /// 单个对象池
        /// </summary>
        /// <param name="__original">原始对象</param>
        public SingleObjPool(T __original)
        {
            original = __original;
            useDict = new Dictionary<T, ObjectContainer<T>>();
            unUseList = new List<ObjectContainer<T>>();
        }

        /// <summary>
        /// 创建对象容器
        /// </summary>
        /// <returns></returns>
        private ObjectContainer<T> CreateObjContainer()
        {
            ObjectContainer<T> container = new ObjectContainer<T>();

            container.Item = Object.Instantiate<T>(original);
            unUseList.Add(container);

            return container;
        }

        /// <summary>
        /// 从对象池里获取一个对象
        /// </summary>
        /// <returns></returns>
        public T Get()
        {
            ObjectContainer<T> container;
            if (unUseList.Count == 0)
                container = CreateObjContainer();
            else
                container = unUseList[0];

            container.Consume();
            unUseList.Remove(container);
            useDict.Add(container.Item, container);

            return container.Item;
        }

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="item">要释放的对象</param>
        /// <returns>释放成功返回true</returns>
        public bool Release(T item)
        {
            if (useDict.ContainsKey(item))
            {
                ObjectContainer<T> container = useDict[item];
                unUseList.Add(container);
                useDict.Remove(item);
                container.Release();
                return true;
            }
            else
                Debug.Log($"名为{item.name}的对象可能已经被回收");
            return false;
        }
    }
}
