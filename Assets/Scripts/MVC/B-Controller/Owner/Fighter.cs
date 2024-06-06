using QFramework;
using System.Collections;
using System.Collections.Generic;
using TJ;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace Frag
{
    // ս������

    [SerializeField]
    public struct HP
    {
        public int max;
        public int cur;
    }
    [SerializeField]
    public struct Enegry
    {
        public int max;
        public int cur;
    }
    public class Fighter : MonoBehaviour, IController
    {

        //model
        public HP hp = new HP();

        // ��ǰ�赲ֵ��Ĭ��Ϊ0
        [Range(0, 999)]
        public int currentBlock = 0;

        public BuffHandler buffHandler = new BuffHandler();


        //view 

        public FighterHealthBarCell healthBar;

        public Transform buffParent;

        public List<GameObject> buffCells = new List<GameObject>();

        #region model
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

        public void DoAddBuff(BuffInfo newBuff)
        {
            Tool.Log($"AddBuff {newBuff.GetType()}");
            this.buffHandler.AddBuff(newBuff);
        }


        public bool IsCanBeKill()
        {
            return this.hp.cur <= 0;
        }

        #endregion

        #region view

        public void OnUpdateHealthBar()
        {
            //���·�����ʾ
            healthBar.DisplayBlock(this.currentBlock);

            //����Ѫ����ʾ
            healthBar.DisplayHealth(this.hp.cur, this.hp.max);

        }



        public void DisPlayBuffPool()
        {
            //����Ҫ�½�һ������
            if (buffCells.Count < buffHandler.buffList.Count)
            {
                PoolMgr.GetInstance().GetObj("Prefabs/UI/Cell/BuffCell", (go) =>
                {
                    if (buffParent != null)
                    {
                        go.transform.SetParent(buffParent);
                        buffCells.Add(go);
                        OnUpdateBuffPool();
                    }
                    else
                    {
                        Tool.Log("buffParentPlayer is null");
                    }
                });
            }
            else
            {
                OnUpdateBuffPool();
            }
            

            return;
        }

        

        private void OnUpdateBuffPool()
        {
            //����buffҳ��
            int index = 0;
            foreach (BuffInfo buff in buffHandler.buffList)
            {
                if (index < buffCells.Count)
                {
                    buffCells[index++].GetComponent<BuffCell>().LoadBuff(buff);
                }

            }

            for (int i = buffHandler.buffList.Count + 1; i < buffCells.Count; i++)
            {
                buffCells[i].GetOrAddComponent<BuffCell>().PushBuffPool();
                buffCells.RemoveAt(i);

            }
        }

        #endregion

        #region unity

        private void OnEnable()
        {

            EventCenter.GetInstance().AddEventListener("Battle", this.OnUpdate);

            EventCenter.GetInstance().AddEventListener("BuffUpdate", this.DisPlayBuffPool);

        }

        public void OnUpdate()
        {
            OnUpdateHealthBar();
        }
        #endregion
        //ָ���ܹ�
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }



}