using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework.Extend;

namespace XFramework
{
    /// <summary>
    /// 所有UI的父类
    /// 用来控制UI的状态
    /// </summary>
    public abstract class BasePanel :MonoBehaviour
    {
        private Transform activePanel;
        private PanelManager panelManager;
        private UIType ui;
        protected SceneManager Game { get => SceneManager.Instance; }
        /// <summary>
        /// 当前活动面板
        /// </summary>
        public Transform ActivePanel { get => activePanel; set => activePanel = value; }
        public UIType UI { get => ui;}

        public BasePanel(UIType ui)
        {
            this.ui = ui;
        }

        /// <summary>
        /// 初始化面板管理器
        /// </summary>
        /// <param name="manager"></param>
        public void Initializa(PanelManager manager)
        {
            panelManager = manager;
        }

        /// <summary>
        /// 推入一个面板
        /// </summary>
        /// <param name="newPanel"></param>
        public void Push(BasePanel newPanel)
        {
            panelManager?.Push(newPanel);
        }

        /// <summary>
        /// 弹出一个面板
        /// </summary>
        public void Pop()
        {
            panelManager?.Pop();
        }

        /// <summary>
        /// 用于初始化
        /// 在UI销毁之前只会执行一次
        /// </summary>
        protected virtual void InitEvent()
        {

        }

        /// <summary>
        /// 每次推出一个面板执行的操作
        /// </summary>
        public virtual void OnStart()
        {
            activePanel.PanelAppearance(true);
            activePanel.SetSiblingIndex(activePanel.parent.childCount - 1);
            if (!ui.Init)
            {
                InitEvent();
                ui.Init = true;
            }
        }

        /// <summary>
        /// 每次激活时执行的操作
        /// </summary>
        public virtual void OnEnable()
        {
            activePanel.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
        }

        /// <summary>
        /// 每次失去激活时执行的操作
        /// 比如新推出了一个面板，当前面板就会执行此方法
        /// </summary>
        public virtual void OnDisable()
        {
            activePanel.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
        }

        /// <summary>
        /// 退出时执行的操作
        /// isDestroy默认值为false，表示不销毁对象
        /// 频繁销毁和生成是比较消耗性能的，如果非必要情况建议使isDestroy为false
        /// </summary>
        /// <param name="isDestroy">是否销毁</param>
        public virtual void OnDestroyOrSetActive(bool isDestroy = false)
        {
            panelManager?.DestroyUI(ui, isDestroy);
        }

        /// <summary>
        /// 改变该面板的变量
        /// 如果在执行OnDestroy时isDestroy值为false
        /// 并且有些Panel类需要在new的时候传递参数
        /// 那么就需要使用此方法将新new的类的参数传递给以前的类
        /// 如果不知道如何使用请参考WarningPanel类(如果该类没有被删除的话)
        /// </summary>
        /// <param name="newPanel">新的面板</param>
        public virtual void OnChange(BasePanel newPanel)
        {

        }
    }
}
