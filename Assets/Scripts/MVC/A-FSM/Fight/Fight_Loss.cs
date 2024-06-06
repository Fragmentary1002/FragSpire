using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    //战斗失败
    public class Fight_Loss : FightUnit

    {
        public override void Init()
        {
            base.Init();

            //// 如果战斗没有胜利（win为false），则激活游戏结束的UI组件  
            //if (!win)
            //    gameover.SetActive(true);

        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

      
    }
}