using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework;

namespace Frag
{
    public class CharacterSelectScene : SceneState
    {
        /// <summary>
        /// 选择人物场景
        /// </summary>
        public CharacterSelectScene()
        {
            sceneName = "CharacterSelectScene";
        }

        public override void OnEnter()
        {
            panelManager.Push(new CharacterSelectPanel());
        }
    }

}