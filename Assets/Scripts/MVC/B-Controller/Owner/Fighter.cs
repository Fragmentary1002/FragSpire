using QFramework;
using System.Collections;
using System.Collections.Generic;
using TJ;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

namespace Frag
{
    // 战斗者类

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

        // 当前阻挡值，默认为0
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
            Tool.Log($"造成 {amount} 点伤害 , hp cur:{hp.cur}");


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
            //更新防御显示
            healthBar.DisplayBlock(this.currentBlock);

            //更新血条显示
            healthBar.DisplayHealth(this.hp.cur, this.hp.max);

        }



        public void DisPlayBuffPool()
        {
            //不够要新建一个对象
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
            //更新buff页面
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
        //指定架构
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }



}