using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using XFramework.Extend;

namespace XFramework
{
    /// <summary>
    /// 场景状态管理系统
    /// </summary>
    public class SceneController
    {
        SceneState state;
        bool isReady;
        string sceneName;
        /// <summary>
        /// 加载界面名称
        /// </summary>
        private string LoadPanelName { get => SceneManager.Instance.loadPanelName; }

        public SceneController()
        {
            isReady = false;
        }

        /// <summary>
        /// 设置场景
        /// </summary>
        /// <param name="sceneState"></param>
        public void SetScene(SceneState sceneState, bool reload = true)
        {
            isReady = false;
            state?.OnExit();
            state = sceneState;
            sceneName = sceneState.SceneName;

            if (reload)
                LoadScene();
            else
                state?.OnEnter();
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        /// <param name="sceneState"></param>
        public void SetScene(SceneState sceneState, bool loadPanel, bool reload = true)
        {
            isReady = false;
            state?.OnExit();
            state = sceneState;
            sceneName = sceneState.SceneName;

            if (reload)
                LoadSceneAsync(loadPanel);
            else
                state?.OnEnter();
        }

        /// <summary>
        /// 场景持续方法
        /// </summary>
        public void OnSceneUpdate()
        {
            if (isReady)
            {
                state?.OnUpdate();
            }
        }

        /// <summary>
        /// 同步加载场景
        /// </summary>
        protected void LoadScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneLoaded;
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        protected void LoadSceneAsync(bool loadPanel)
        {
            SceneManager.Instance.StartCoroutine(AsyncLoad(loadPanel));
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneLoaded;
        }

        /// <summary>
        /// 加载场景完毕后执行的方法
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="mode"></param>
        protected void SceneLoaded(Scene scene, LoadSceneMode mode)
        {
            state?.OnEnter();
            isReady = true;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= SceneLoaded;
            Debug.Log($"{sceneName}场景加载完毕！");
        }

        /// <summary>
        /// 异步加载场景的实现
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator AsyncLoad(bool loadPanel)
        {
            AsyncOperation operation;

            operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

            if (loadPanel)
            {
                GameObject panel = GameObject.Find(LoadPanelName);
                if (panel == null)
                {
                    Debug.LogError($"{LoadPanelName}面板不存在");
                    yield break;
                }
                panel.transform.PanelAppearance(true);
                operation.allowSceneActivation = false;
                Slider slider = panel.GetComponentInChildren<Slider>();
                slider.value = 0;
                float progressValue;

                while (!operation.isDone)
                {
                    if (operation.progress < 0.9f)
                        progressValue = operation.progress;
                    else
                        progressValue = 1.0f;

                    slider.value = progressValue;
                    if (progressValue >= 0.9f)
                    {
                        slider.value = 1f;
                        operation.allowSceneActivation = true;
                    }
                    yield return null;
                }
                panel.transform.PanelAppearance(false);
            }
            else
                operation.allowSceneActivation = true;
        }
    }
}
