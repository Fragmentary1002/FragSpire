﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 基于Unity某些类的扩展
/// </summary>
namespace XFramework.Extend
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class UnityExtentionMethod
    {
        /// <summary>
        /// 通过名称查找子对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="childName">名称</param>
        /// <returns></returns>
        public static GameObject FindChildGameObject(this GameObject obj, string childName)
        {
            //第一版
            //Transform[] children = obj.GetComponentsInChildren<Transform>();

            //foreach (Transform child in children)
            //{
            //    if (child.name == childName)
            //        return child.gameObject;
            //}

            //Debug.LogWarning($"{obj.name}里找不到名为{childName}的子对象");
            //return null;

            //第二版
            //这边用LINQ表达式重写方法

            //Transform child = obj.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == childName);

            //if (child != null)
            //{
            //    return child.gameObject;
            //}
            //else
            //{
            //    Debug.LogWarning($"{obj.name}里找不到名为{childName}的子对象");
            //    return null;
            //}

            //第三版
            //利用Find可以寻找子对象的子对象

            Transform child = obj.transform.Find(childName);
            if (child != null)
            {
                //Debug.Log($"{obj.name}里找到名为{childName}的子对象");
                return child.gameObject;
            }
            Debug.LogWarning($"{obj.name}里找不到名为{childName}的子对象");
            return null;

        }

        /// <summary>
        /// 获取当前对象的某组件
        /// 如获取失败则添加该组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T GetOrAddComponent<T>(this Transform t) where T : Component
        {
            T component = t.GetComponent<T>();
            if (component == null)
                component = t.gameObject.AddComponent<T>();

            return component;

            //这边用c#语法糖

            //return t.GetComponent<T>()?? t.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 获取一个子对象的某组件
        /// 如获取失败则添加
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="t"></param>
        /// <param name="childName">子对象名称</param>
        /// <returns></returns>
        public static T GetOrAddComponentInChildren<T>(this Transform t, string childName) where T : Component
        {
            GameObject childObj = t.gameObject.FindChildGameObject(childName);

            if (childObj == null)
                return null;
            return childObj.transform.GetOrAddComponent<T>();

        }

        /// <summary>
        /// 控制一个面板的外观
        /// </summary>
        /// <param name="t"></param>
        /// <param name="on_off">显示则为true</param>
        /// <param name="active">是否为活动对象</param>
        public static void PanelAppearance(this Transform t, bool on_off, bool active = false)
        {
            CanvasGroup group = t.GetOrAddComponent<CanvasGroup>();
            int value = on_off == true ? 1 : 0;

            //射线检测
            group.blocksRaycasts = on_off;
            //交互
            group.interactable = on_off;
            //透明度
            group.alpha = value;

            t.gameObject.SetActive(on_off || active);
        }


    }
}
