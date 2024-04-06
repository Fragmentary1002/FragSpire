using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XFramework.Extend;

namespace XFramework
{
    /// <summary>
    /// 场景状态
    /// </summary>
    public abstract class SceneState
    {
        protected PanelManager panelManager;
        /// <summary>
        /// 场景名称
        /// </summary>
        protected string sceneName = "";
        /// <summary>
        /// 加载界面
        /// </summary>
        protected SceneManager Game { get => SceneManager.Instance; }
        public string SceneName { get => sceneName; }

        public SceneState()
        {
            panelManager = new PanelManager();
        }

        /// <summary>
        /// 场景进入时
        /// </summary>
        public virtual void OnEnter()
        {
            
        }

        /// <summary>
        /// 场景持续时
        /// </summary>
        public virtual void OnUpdate()
        {

        }

        /// <summary>
        /// 场景退出
        /// </summary>
        public virtual void OnExit()
        {
            panelManager.PopAll();
        }
    }
}
