using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using XFramework.Extend;

namespace XFramework
{
    /// <summary>
    /// 面板管理器
    /// </summary>
    public class PanelManager
    {
        /// <summary>
        /// 存储所有的UI以及UI对应的GameObject
        /// key为路径
        /// </summary>
        private Dictionary<string, GameObject> dictUI;
        /// <summary>
        /// 存储所有的UI以及UI对应的BasePanel
        /// key为路径
        /// </summary>
        private Dictionary<string, BasePanel> dictPanel;
        /// <summary>
        /// 管理UI的栈
        /// </summary>
        private Stack<BasePanel> panelStack;
        /// <summary>
        /// 画布的名称
        /// </summary>
        private string canvasName;
        /// <summary>
        /// 画布对象
        /// </summary>
        private GameObject canvasObj;

        /// <summary>
        /// 面板管理器
        /// </summary>
        public PanelManager()
        {
            dictUI = new Dictionary<string, GameObject>();
            dictPanel = new Dictionary<string, BasePanel>();
            panelStack = new Stack<BasePanel>();
            canvasName = "Canvas";
            canvasObj = GameObject.Find(canvasName);
        }

        /// <summary>
        /// 面板管理器
        /// </summary>
        /// <param name="name">画布名称</param>
        public PanelManager(string name)
        {
            dictUI = new Dictionary<string, GameObject>();
            dictPanel = new Dictionary<string, BasePanel>();
            panelStack = new Stack<BasePanel>();
            canvasName = name;
        }

        /// <summary>
        /// 获取一个UI对象
        /// </summary>
        /// <param name="ui">UI类型</param>
        /// <returns></returns>
        private GameObject GetSingleUI(UIType ui)
        {
            if (dictUI.ContainsKey(ui.Path))
            {
                ui.Init = true;
                return dictUI[ui.Path];
            }
            if (canvasObj == null)
            {
                canvasObj = GameObject.Find(canvasName);
                if (canvasObj == null)
                {
                    Debug.LogError($"名为{canvasName}的画布不存在");
                    return null;
                }
            }
            //GameObject obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>(ui.Path), canvasObj.transform);
#if UNITY_EDITOR
            GameObject obj = GameObject.Instantiate<GameObject>(AssetDatabase.LoadAssetAtPath<GameObject>($"Assets/Resources/{ui.Path}.prefab"), canvasObj.transform);
#else
            
#endif
            obj.name = ui.Name;
            dictUI.Add(ui.Path, obj);

            return obj;
        }

        /// <summary>
        /// 移除UI
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="isDestroy">是否销毁，不销毁即隐藏</param>
        public void DestroyUI(UIType ui, bool isDestroy = false)
        {
            if (dictUI.ContainsKey(ui.Path))
            {
                if (isDestroy)
                {
                    GameObject.Destroy(dictUI[ui.Path]);
                    dictUI.Remove(ui.Path);
                    dictPanel.Remove(ui.Path);
                }
                else
                {
                    dictUI[ui.Path].transform.PanelAppearance(false);
                }
            }
        }

        /// <summary>
        /// 推入一个面板
        /// </summary>
        /// <param name="newPanel"></param>
        public void Push(BasePanel newPanel)
        {
            if (panelStack.Count > 0)
                panelStack.Peek().OnDisable();

            if (!dictPanel.ContainsKey(newPanel.UI.Path))
            {
                dictPanel.Add(newPanel.UI.Path, newPanel);
                GameObject obj = GetSingleUI(newPanel.UI);
                newPanel.ActivePanel = obj.transform;
                newPanel.Initializa(this);
            }
            else
            {
                BasePanel p = dictPanel[newPanel.UI.Path];
                p.OnChange(newPanel);
                newPanel = p;
            }

            newPanel.OnStart();
            if (panelStack.Count > 0)
            {
                //防止连续推送重复的面板
                if (panelStack.Peek().UI.Path != newPanel.UI.Path)
                    panelStack.Push(newPanel);
            }
            else
                panelStack.Push(newPanel);
        }

        /// <summary>
        /// 弹出一个面板
        /// </summary>
        public void Pop()
        {
            if (panelStack.Count > 0)
                panelStack.Pop().OnDestroyOrSetActive();
            if (panelStack.Count > 0)
                panelStack.Peek().OnEnable();
        }

        /// <summary>
        /// 弹出所有面板
        /// </summary>
        public void PopAll()
        {
            var values = new List<BasePanel>(dictPanel.Values);
            while (values.Count > 0)
            {
                values[0].OnDestroyOrSetActive(true);
                values.RemoveAt(0);
            }
        }
    }
}
