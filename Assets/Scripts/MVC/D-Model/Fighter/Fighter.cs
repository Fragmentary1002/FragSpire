using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    // 战斗者类

    public struct HP
    {
        public int max;
        public int cur;
    }

    public struct Enegry
    {
        public int max;
        public int cur;
    }


    public class Fighter : AbstractModel
    {
        public HP hp = new HP();

        // 当前阻挡值，默认为0
        [Range(0, 999)]
        public int currentBlock = 0;

        public BuffHandler buffHandler = new BuffHandler();

        protected override void OnInit()
        {

        }
        #region 改数据
        public void ResetCurrentBlock()
        {

            this.currentBlock = 0;
        }

        /// <summary>
        /// /受到伤害的方法 ,返回是否死亡
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public void DoBeDamage(int amount)
        {


            if (this.currentBlock > 0)
            {
                if (this.currentBlock >= amount)
                {
                    // 全部阻挡
                    this.currentBlock -= amount;
                    amount = 0;
                }
                else
                {
                    // 无法全部阻挡
                    amount -= this.currentBlock;
                    this.currentBlock = 0;
                }
            }

            // 打印造成的伤害值
            Tool.Log("$\"造成 {amount} 点伤害\"");


            // 实例化伤害指示器，并在一段时间后销毁


            // 减少当前生命值，并更新生命值UI
            this.hp.cur -= amount;

        }

        /// <summary>
        /// 增加阻挡值的方法
        /// </summary>
        /// <param name="amount"></param>
        public void DoAddBlock(int amount)
        {

            this.currentBlock += amount;

            Tool.Log($"增加 {amount} 防御");
        }

        public void DoAddBuff(BuffInfo newBuff, int buffAmount)
        {
            Tool.Log($"AddBuff {newBuff.GetType()}");
            this.buffHandler.AddBuff(newBuff);
        }


        public bool IsCanBeKill()
        {
            return this.hp.cur <= 0;
        }

        /// <summary>
        /// 回合结束时评估增益效果的方法
        /// </summary>
        public void EvaluateBuffsAtTurnEnd()
        { }

        /// <summary>
        /// 重置增益效果的方法
        /// </summary>
        public void DoResetBuffs()
        {
            this.buffHandler.Clear();
        }
        #endregion 
    }



}

