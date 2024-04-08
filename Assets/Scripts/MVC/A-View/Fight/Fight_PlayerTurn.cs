using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace Frag
{
    //玩家回合
    public class Fight_PlayerTurn : FightUnit

    {
        public override void Init()
        {
            //  显示敌人的意图或动作
            // EnemyManager.Instance.DisplayIntent();
            Debug.Log("显示敌人的意图或动作");
          
            base.Init();

            FightCardManager.Instance.InitTrun();
            Debug.Log("抽牌开始");
            this.SendCommand(new ApplyTimeCommand(ApplyTime.TurnStart));



            FightCardManager.Instance.DrawCards(5);



        }

        public override void OnUpdate()
        {
       
        }
    
        public override void OnDestroy()
        {

            //  EnemyManager.Instance.DisplayReset();
         

            //回合结束丢弃手牌
            FightCardManager.Instance.DisCardHandAll();

            this.SendCommand(new ApplyTimeCommand(ApplyTime.TurnEnd));

            //切换到敌人回合
            FightTurnController.Instance.ChangeType(FightType.Enemy);

        }

    }
}