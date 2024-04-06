
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frag;
using XFramework.Extend;



namespace XFramework
{


    /// <summary>
    /// 场景管理类 对于场景以及页面的生命周期的处理，承载与UI框架
    /// </summary>
    //[RequireComponent(typeof(AudioManager))]
    public class SceneManager : MonoSingleton<SceneManager>
    {
        /// <summary>
        /// 场景管理器
        /// </summary>
        private SceneController sceneSystem;
        /// <summary>
        /// 面板管理器
        /// </summary>
        private PanelManager panelManager;
        /// <summary>
        /// 加载场景时显示的进度条面板名称
        /// </summary>
        [Header("加载场景时显示的进度条面板名称")]
        public string loadPanelName= "AsyncLoadPanel";
        /// <summary>
        /// 面板管理器
        /// </summary>
        public PanelManager PanelManager { get => panelManager; }

        /// <summary>
        /// 输入加载场景时显示的进度条面板名称 默认为AsyncLoadPanel
        /// </summary>
        public void Init(SceneState sceneState)
        {
            SceneManager.Instance.LoadScene(sceneState, false);
            return;

        }

        /// <summary>
        /// 写的有点丑，待优化
        /// </summary>
        override protected void Awake()
        {
            base.Awake();
            sceneSystem = new SceneController();

        }


        // Start is called before the first frame update
        protected virtual void Start()
        {
            GameObject asyncLoadPanel = GameObject.Find(loadPanelName);
            asyncLoadPanel?.transform.PanelAppearance(false);

            //ScenceManager.Instance.LoadScene(new StartScene(), false);
           
        }

        private void Update()
        {
            sceneSystem?.OnSceneUpdate();
        }

        /// <summary>
        /// 加载一个场景
        /// </summary>
        /// <param name="sceneState"></param>
        /// <param name="reload"></param>
        public void LoadScene(SceneState sceneState, bool reload = true)
        {
            sceneSystem?.SetScene(sceneState, reload);
        }

        /// <summary>
        /// 异步加载一个场景
        /// </summary>
        /// <param name="sceneState"></param>
        /// <param name="loadPanel"></param>
        /// <param name="reload"></param>
        public void LoadScene(SceneState sceneState, bool loadPanel, bool reload = true)
        {
            sceneSystem?.SetScene(sceneState, loadPanel, reload);
        }

        /// <summary>
        /// 初始化面板管理器
        /// </summary>
        /// <param name="manager"></param>
        public void Initialize(PanelManager manager)
        {
            panelManager = manager;
        }
    }
}
