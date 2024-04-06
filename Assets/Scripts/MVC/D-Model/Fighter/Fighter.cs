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


    public class Fication
    {
        public int value;
        public float percentage;

        public Fication(int value = 0, float percentage = 100f)
        {
            this.value = value;
            this.percentage = percentage;
        }

        public void GetAmount(ref int amount)
        {
            amount += this.value;
            amount *= (int)(this.percentage - 100f);

        }
    }
    public class Fighter : AbstractModel
    {
        public HP hp = new HP();

        // 当前阻挡值，默认为0
        [Range(0, 999)]
        public int currentBlock = 0;

        public List<BaseBuff> buffs = new List<BaseBuff>();

        private Fication damageFication = new Fication();
        public Fication DamageFication
        {
            set { damageFication = value; }

        }
        private Fication blockFication = new Fication();

        public Fication BlockFication
        {
            set { damageFication = value; }

        }
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
        public bool DoTakeDamageAndIsEndFight(int amount)
        {
            damageFication.GetAmount(ref amount);

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

            if (this.hp.cur < 0) { return false; }

            return true;
        }

        /// <summary>
        /// 增加阻挡值的方法
        /// </summary>
        /// <param name="amount"></param>
        public void DoAddBlock(int amount)
        {
            blockFication.GetAmount(ref amount);

            this.currentBlock += amount;

            Tool.Log($"增加 {amount} 防御");
        }
        public void DoAddBuff(BaseBuff newBuff, int buffAmount)
        {
            Tool.Log($"AddBuff {newBuff.GetType()}");

            newBuff.Owner = this;

            newBuff.AfterBeAdded();

            buffs.Add(newBuff);
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
            this.buffs.Clear();
        }
        #endregion 
    }



}

