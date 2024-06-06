using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    // ս������

  
    [Obsolete]
    public class FighterOb : MonoBehaviour
    {
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


        public HP hp = new HP();

        // ��ǰ�赲ֵ��Ĭ��Ϊ0
        [Range(0, 999)]
        public int currentBlock = 0;

        public BuffHandler buffHandler = new BuffHandler();
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
        public void DoBeDamage(int amount)
        {


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
            Tool.Log($"��� {amount} ���˺� , hp cur:{hp.cur}");


            // ʵ�����˺�ָʾ��������һ��ʱ�������


            // ���ٵ�ǰ����ֵ������������ֵUI
            this.hp.cur -= amount;

        }

        /// <summary>
        /// �����赲ֵ�ķ���
        /// </summary>
        /// <param name="amount"></param>
        public void DoAddBlock(int amount)
        {

            this.currentBlock += amount;

            Tool.Log($"���� {amount} ����");
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

        #endregion 
    }



}

