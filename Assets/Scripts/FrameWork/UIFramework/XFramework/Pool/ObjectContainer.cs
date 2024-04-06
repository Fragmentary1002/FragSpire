using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFramework.Objects
{
    /// <summary>
    /// 存储单个对象的容器
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public class ObjectContainer<T> where T : Object
    {
        /// <summary>
        /// 物品
        /// </summary>
        private T item;
        /// <summary>
        /// 是否已经使用
        /// </summary>
        public bool Used { get; private set; }
        /// <summary>
        /// 物品
        /// </summary>
        public T Item { get => item; set => item = value; }

        public ObjectContainer()
        {
            Used = false;
        }

        /// <summary>
        /// 设置为使用状态
        /// </summary>
        public void Consume()
        {
            Used = true;
        }

        /// <summary>
        /// 设置为闲置状态
        /// </summary>
        public void Release()
        {
            Used = false;
        }
    }
}
