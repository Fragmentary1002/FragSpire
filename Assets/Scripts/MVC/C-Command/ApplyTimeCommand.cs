using QFramework;
using System;
using UnityEngine;


namespace Frag
{
    public enum ApplyTime
    {
        None,
        BattleBegin, //开始战斗
        BattleEnd,   //战斗结束
        DoDamage,    //攻击
        BeDamaged,  //被攻击
        TurnStart, //回合开始
        TurnEnd,   //回合结束
        DrawCard,    //抽牌
        PlayHand     //出牌
    }

    public class ApplyTimeCommand : AbstractCommand
    {
        ApplyTime applyTime;

        public ApplyTimeCommand(ApplyTime applyTime)
        {
            this.applyTime = applyTime;
        }   

        protected override void OnExecute()
        {
            PerformAction();

            //return this;
        }

        private void PerformAction()
        {
            switch (applyTime)
            {
                case ApplyTime.BattleBegin:
                    // 在战斗开始时执行操作
                    Tool.Log("战斗已开始！");
                    break;
                case ApplyTime.BattleEnd:
                    // 在战斗结束时执行操作
                    Tool.Log("战斗已结束！");
                    break;
                case ApplyTime.DoDamage:
                    // 在攻击时执行操作
                    Tool.Log("执行攻击！");
                    break;
                case ApplyTime.BeDamaged:
                    // 在被攻击时执行操作
                    Tool.Log("受到攻击！");
                    break;
                case ApplyTime.TurnStart:
                    // 在回合开始时执行操作
                    Tool.Log("回合开始！");
                    break;
                case ApplyTime.TurnEnd:
                    // 在回合结束时执行操作
                    Tool.Log("回合结束！");
                    break;
                case ApplyTime.DrawCard:
                    // 在抽牌时执行操作
                    Tool.Log("抽牌！");
                    break;
                case ApplyTime.PlayHand:
                    Tool.Log("出牌！");
                    break;
                default:
                    // 默认情况
                    Tool.Log("未知操作！");
                    break;
            }
            ApplyEvent(applyTime);

        }


        private void ApplyEvent(ApplyTime applyTime)
        {
            EventCenter.GetInstance().EventTrigger(applyTime.ToString());
        }

    }
}
