using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XFramework;

namespace Frag
{/// <summary>
 /// 开始场景
 /// </summary>
    public class StartScene : SceneState
    {
        /// <summary>
        /// 开始场景
        /// </summary>
        public StartScene()
        {
            sceneName = "Start";
        }

        public override void OnEnter()
        {
            panelManager.Push(new StartPanel());
        }
    }
}
