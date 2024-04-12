using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Frag
{


    public enum EnemyActionType
    {
        // 攻击意图  
        Attack,
        // 阻挡意图  
        Defense,
        AttackDefense,
        Buff,
        Buff_Attack,
        Buff_Defense,
        Debuff,
        Debuff_Attack,
        Debuff_Defense
    }

    public class EnemyIntentSystem : AbstractSystem, ICanSendCommand
    {


        //public List<Enemy> enemies = new List<Enemy>();

        public Enemy enemy;

        public LootBag lootBag;


        BaseIntent newEnemyIntent;

        private int trunCnt = 0;

        public EnemyIntentCell cell;

        protected override void OnInit()
        {
            //  throw new System.NotImplementedException(); 
            lootBag = this.GetUtility<LootBag>();

        }

        public void StartTurn()
        {

            try
            {
                List<ILoot> list = new List<ILoot>();

                //强制类型转换 LINQ
                list = enemy.monster.IntentList.Cast<ILoot>().ToList();

                ILoot loot = lootBag.GetDroppedItem(list);

                newEnemyIntent = loot as BaseIntent;

                ChangeType(newEnemyIntent);

            }
            catch
            {
                Tool.Log("CreateIntent 生成失效", LogLevel.Error);
            }

        }


        private void ChangeType(BaseIntent enemyIntent)
        {

            //EnemyAction neweNemyAction = enemyActionList[trunCnt];
            //DisplayIntent(neweNemyAction);
            Debug.Log($"EnemyActionType ChangeType");
            newEnemyIntent = enemyIntent;
            switch (newEnemyIntent.type)
            {
                case EnemyActionType.Attack:
                    MonoMgr.GetInstance().StartCoroutine(ApplyAttack());
                    break;
                case EnemyActionType.Defense:
                    MonoMgr.GetInstance().StartCoroutine(ApplyDefense());
                    break;
                case EnemyActionType.AttackDefense:
                    MonoMgr.GetInstance().StartCoroutine(ApplyAttack());
                    MonoMgr.GetInstance().StartCoroutine(ApplyDefense());
                    break;
                case EnemyActionType.Buff:
                    MonoMgr.GetInstance().StartCoroutine(ApplyBuff(newEnemyIntent.creator));
                    break;
                case EnemyActionType.Buff_Attack:
                    MonoMgr.GetInstance().StartCoroutine(ApplyBuff(newEnemyIntent.creator));
                    MonoMgr.GetInstance().StartCoroutine(ApplyAttack());
                    break;
                case EnemyActionType.Buff_Defense:
                    MonoMgr.GetInstance().StartCoroutine(ApplyBuff(newEnemyIntent.creator));
                    MonoMgr.GetInstance().StartCoroutine(ApplyDefense());
                    break;
                case EnemyActionType.Debuff:
                    MonoMgr.GetInstance().StartCoroutine(ApplyBuff(newEnemyIntent.target));
                    break;
                case EnemyActionType.Debuff_Attack:
                    MonoMgr.GetInstance().StartCoroutine(ApplyBuff(newEnemyIntent.target));
                    MonoMgr.GetInstance().StartCoroutine(ApplyAttack());
                    break;
                case EnemyActionType.Debuff_Defense:
                    MonoMgr.GetInstance().StartCoroutine(ApplyBuff(newEnemyIntent.target));
                    MonoMgr.GetInstance().StartCoroutine(ApplyDefense());
                    break;
                default:
                    Debug.Log("EnemyActionType 没有找到状态");
                    break;
            }
            WrapUpTurn();

        }



        /// <summary>
        /// 攻击
        /// </summary>
        /// <returns></returns>
        private IEnumerator ApplyAttack()
        {
            yield return new WaitForSeconds(0.5f);
            this.SendCommand<DataAttackCommand>(new DataAttackCommand(new DamageInfo(newEnemyIntent.creator, newEnemyIntent.target, newEnemyIntent.intentAttack)));

        }


        /// <summary>
        /// 防御
        /// </summary>
        /// <returns></returns>
        private IEnumerator ApplyDefense()
        {
            yield return new WaitForSeconds(0.5f);
            this.SendCommand<DataDefenseCommand>(new DataDefenseCommand(newEnemyIntent.target, newEnemyIntent.intentDefense));
        }

        /// <summary>
        /// 用于在一段时间后应用buff效果  
        /// </summary>
        /// <returns></returns>
        private IEnumerator ApplyBuff(Fighter fighter)
        {
            yield return new WaitForSeconds(0.5f);
            this.SendCommand<DataBuffCommand>(new DataBuffCommand(fighter, newEnemyIntent.buffInfo));
        }



        ///// <summary>
        ///// 显示敌人的意图  
        ///// </summary>
        //private void DisplayIntent(EnemyAction newEnemyAction)
        //{
        //    if (newEnemyAction != null) { return; }

        //    newEnemyAction.Damage
        //    ();

        //    cell.DisplayIntent(newEnemyAction);

        //}


        /// 结束回合的处理方法  
        private void WrapUpTurn()
        {
            trunCnt++;
            Tool.Log(" 结束回合的处理方法");
        }

    }

}