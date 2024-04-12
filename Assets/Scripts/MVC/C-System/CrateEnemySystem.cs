using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFramework.Extend;

namespace Frag
{
    /// <summary>
    /// 敌人管理类 控制应该生成的怪物列表 和怪物状态机
    /// </summary>

    public class CrateEnemySystem : MonoSingleton<CrateEnemySystem>
    {
        private List<BaseMonster> enemiesList = new List<BaseMonster>();    // 可能出现的敌人列表  
        private List<BaseMonster> eliteEnemyList = new List<BaseMonster>(); // 可能出现的精英敌人列表  
        public List<BaseMonster> enemies;
        public BaseMonster enemyTest;
        public Transform enemyParent;                              //生成敌人父对象

        int tier = 1;
        public override void Init()
        {

        }

        /// <summary>
        /// 实例化敌人
        /// </summary>
        /// <param name="isEliteFight"></param>
        public void CreateEnemy(bool isEliteFight = false, bool isWeakFight = true)
        {
            if (enemyTest != null && enemyParent != null)
            {
                //print("实例化敌人");

                Instantiate(enemyTest.MonsterClassPrefab, enemyParent);
            }
            else
            {
                // print("没有找到实例化敌人");
            }

        }

        /// <summary>
        /// 获取随机生成某一批敌人的列表
        /// </summary>
        /// <param name="isEliteFight"></param>
        private List<BaseMonster> GetCreateEnemyList(bool isEliteFight = false)
        {
            return isEliteFight ? eliteEnemyList : enemiesList;

        }


        /// <summary>
        /// 获取应该生成的敌人列表
        /// </summary>
        /// <param name="isEliteFight"></param>
        /// <returns></returns>
        private List<Enemy> GetEnemyOrEnemyList(bool isEliteFight = false, bool isWeakFight = true)
        {

            return null;
        }

        /// <summary>
        /// 设置大层层数以便生成对应新怪物组
        /// </summary>
        /// <param name="tier"></param>
        private void SetTier(int tier)
        {
            this.tier = tier;
        }

        /// <summary>
        /// 显示敌人的意图或动作
        /// </summary>
        public void DisplayIntent()
        {
            Debug.Log("显示敌人的意图或动作");

        }

        /// <summary>
        /// 重置他们的阻挡值和显示阻挡的UI
        /// </summary>
        public void DisplayReset()
        {
            Debug.Log("重置他们的阻挡值和显示阻挡的UI");

            //// 遍历所有的敌人，重置他们的阻挡值和显示阻挡的UI。  
            //foreach (Enemy e in enemies)
            //{
            //    if (e.thisEnemy == null)
            //        e.thisEnemy = e.GetComponent<Fighter>();
            //    // 重置敌人的阻挡值。  
            //    //reset block
            //    e.thisEnemy.currentBlock = 0;
            //    // 更新敌人阻挡值的UI显示。  
            //    e.thisEnemy.fighterHealthBar.DisplayBlock(0);
            //}
            //// 评估玩家在当前回合结束时的增益效果
            //player.EvaluateBuffsAtTurnEnd();
        }
    }

}