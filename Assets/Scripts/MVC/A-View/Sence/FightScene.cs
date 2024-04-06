using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XFramework;

namespace Frag
{/// <summary>
 /// Õ½¶·³¡¾°
 /// </summary>
    public class FightScene : SceneState
    {

        public FightScene()
        {
            sceneName = "FightScene";
        }

        public override void OnEnter()
        {
            FightPanel fightPanel = new FightPanel();
            panelManager.Push(fightPanel);


        }
   

    }
}
