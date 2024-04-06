using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    // ս������

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

        // ��ǰ�赲ֵ��Ĭ��Ϊ0
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
        #region ������
        public void ResetCurrentBlock()
        {

            this.currentBlock = 0;
        }

        /// <summary>
        /// /�ܵ��˺��ķ��� ,�����Ƿ�����
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
                    // ȫ���赲
                    this.currentBlock -= amount;
                    amount = 0;
                }
                else
                {
                    // �޷�ȫ���赲
                    amount -= this.currentBlock;
                    this.currentBlock = 0;
                }
            }

            // ��ӡ��ɵ��˺�ֵ
            Tool.Log("$\"��� {amount} ���˺�\"");


            // ʵ�����˺�ָʾ��������һ��ʱ�������


            // ���ٵ�ǰ����ֵ������������ֵUI
            this.hp.cur -= amount;

            if (this.hp.cur < 0) { return false; }

            return true;
        }

        /// <summary>
        /// �����赲ֵ�ķ���
        /// </summary>
        /// <param name="amount"></param>
        public void DoAddBlock(int amount)
        {
            blockFication.GetAmount(ref amount);

            this.currentBlock += amount;

            Tool.Log($"���� {amount} ����");
        }
        public void DoAddBuff(BaseBuff newBuff, int buffAmount)
        {
            Tool.Log($"AddBuff {newBuff.GetType()}");

            newBuff.Owner = this;

            newBuff.AfterBeAdded();

            buffs.Add(newBuff);
        }

        /// <summary>
        /// �غϽ���ʱ��������Ч���ķ���
        /// </summary>
        public void EvaluateBuffsAtTurnEnd()
        { }

        /// <summary>
        /// ��������Ч���ķ���
        /// </summary>
        public void DoResetBuffs()
        {
            this.buffs.Clear();
        }
        #endregion 
    }



}

