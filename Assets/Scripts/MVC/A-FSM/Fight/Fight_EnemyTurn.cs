using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

namespace Frag
{
    //敌人回合
    public class Fight_EnemyTurn : FightUnit
    {
        public override void Init()
        {
            base.Init();

            //// 开始一个新的协程来处理敌人的回合。
            //StartCoroutine(HandleEnemyTurn());
          //  MonoMgr.GetInstance().StartCoroutine(this.enemyManager.DoAllEnemyAction());

        }

        public override void OnUpdate()
        {
            base.OnUpdate();
           
        }

        public override void OnDestroy() {

            //重置玩家防御
            //PlayerActionManager.Instance.ResetCurrentBlock();
          //  MonoMgr.GetInstance().StartCoroutine(this.enemyManager.DoAllUpdateIntent());
            FightFSM.Instance.ChangeType(FightType.Player);

        }
    }
}