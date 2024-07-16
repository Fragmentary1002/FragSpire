using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Frag
{
    public class Fight_BattleInit : FightUnit,ICanSendCommand
    {
        //指定架构
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }

    
        public override void Init()
        {
            // 实例化一个随机的敌人预制体  可以使用对象池？
            // EnemyManager.Instance.CreateEnemy();

            // 如果存在结束屏幕，就将其设为非活动状态（这里也被注释掉了）
            //if (endScreen != null)
            //    endScreen.gameObject.SetActive(false);


            // 将玩家的牌堆加入弃牌堆，并洗牌，然后抽一定数量的牌加入手中的卡牌列表中  
            //FightCardManager.Instance.Init();
            

            this.SendCommand(new ApplyTimeCommand(ApplyTime.BattleBegin));

            FightFSM.Instance.ChangeType(FightType.Player);

        }
    }
}