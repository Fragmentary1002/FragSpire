using System.Collections.Generic;
using UnityEngine;



namespace Frag
{
    public class EnemyActionSystem : MonoSingleton<EnemyActionSystem>
    {
     

        public List<Enemy> enemies = new List<Enemy>();

        public List<EnemyAction> enemyActionList = new List<EnemyAction>();

        //private int trunCnt = 0;

        //        // 表示EnemyAction的意图类型。  
        //        public EnemyActionType enemyActionType;

        //        public EnemyIntentCell cell;

        //        private void Start()
        //        {
        //            cell = FindObjectOfType<EnemyIntentCell>();
        //        }

        //        public void ChangeType(EnemyActionType type)
        //        {
        //            EnemyAction neweNemyAction = enemyActionList[trunCnt];
        //            DisplayIntent(neweNemyAction);
        //            Debug.Log($"EnemyActionType ChangeType");
        //            switch (type)
        //            {
        //                case EnemyActionType.Attack:
        //                    // 攻击玩家的协程  
        //                    AttackPlayer(ToolManager.Instance.TestAmount);
        //                    // StartCoroutine(AttackPlayer());
        //                    break;
        //                case EnemyActionType.Block:
        //                    // 执行阻挡动作  
        //                    PerformBlock(ToolManager.Instance.TestAmount);
        //                    // StartCoroutine(ApplyBuff());
        //                    break;
        //                case EnemyActionType.StrategicBuff:
        //                    // 对自己应用增益效果  
        //                    ApplyBuffToSelf(neweNemyAction.buffType);
        //                    // 应用增益效果的协程  
        //                    //StartCoroutine(ApplyBuff());
        //                    break;
        //                case EnemyActionType.StrategicDebuff:
        //                    // 对玩家应用减益效果  
        //                    ApplyDebuffToPlayer(neweNemyAction.buffType);
        //                    // 应用增益效果的协程
        //                    // StartCoroutine(ApplyBuff());
        //                    break;
        //                case EnemyActionType.AttackDebuff:
        //                    // 对玩家应用减益效果  
        //                    ApplyDebuffToPlayer(neweNemyAction.buffType);

        //                    //StartCoroutine(AttackPlayer());
        //                    // StartCoroutine(ApplyBuff());
        //                    break;
        //                default:
        //                    Debug.Log("EnemyActionType 没有找到状态");
        //                    break;
        //            }
        //            WrapUpTurn();

        //        }

        //        /// <summary>
        //        /// 显示敌人的意图  
        //        /// </summary>
        //        private void DisplayIntent(EnemyAction newEnemyAction)
        //        {
        //            if (newEnemyAction != null) { return; }

        //            newEnemyAction.Damage
        //            ();

        //            cell.DisplayIntent(newEnemyAction);

        //        }


        //        /// <summary>
        //        /// 敌人回合的函数  
        //        /// </summary>
        //        private void GenerateTurns()
        //        {

        //            // DisplayIntent();

        //            print("敌人回合的函数");
        //        }

        //        /// <summary>
        //        /// 攻击玩家的协程函数  
        //        /// </summary>
        //        /// <returns></returns>
        //        //private IEnumerator AttackPlayer()
        //        //{
        //        //    print("攻击玩家的协程函数");

        //        //    yield return new WaitForSeconds(0.5f);
        //        //}
        //        private void AttackPlayer(int amount)
        //        {
        //            RoleManager.Instance.DoTakeDamage(amount,RoleType.Play);
        //            print("攻击玩家的协程函数");

        //        }

        //        /// <summary>
        //        /// 定义一个协程方法，用于在一段时间后应用增益效果  
        //        /// </summary>
        //        /// <returns></returns>
        //        //private IEnumerator ApplyBuff()
        //        //{
        //        //    print("定义一个协程方法，用于在一段时间后应用增益效果 ");
        //        //    yield return new WaitForSeconds(0.5f);
        //        //}


        //        // 给自身应用增益效果  
        //        private void ApplyBuffToSelf(BuffType t)
        //        {
        //           RoleManager.Instance.DoAddBuff(t, RoleType.Enemy);

        //            print(" 给自身应用增益效果  ");
        //        }
        //        // 给玩家施加减益效果  
        //        private void ApplyDebuffToPlayer(BuffType t)
        //        {
        //            RoleManager.Instance.DoAddBuff(t, RoleType.Play);
        //            print(" 给玩家施加减益效果  ");
        //        }

        //        // 执行阻挡动作  
        //        private void PerformBlock(int amount)
        //        {
        //            RoleManager.Instance.DoAddBlock(amount, RoleType.Enemy);
        //            print(" 执行阻挡动作  ");
        //        }


        //        /// 结束回合的处理方法  
        //        private void WrapUpTurn()
        //        {
        //            trunCnt++;
        //            print(" 结束回合的处理方法");
        //        }

    }

}