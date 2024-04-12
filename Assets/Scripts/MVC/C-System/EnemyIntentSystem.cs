using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Frag
{


    public enum EnemyActionType
    {
        // ������ͼ  
        Attack,
        // �赲��ͼ  
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

                //ǿ������ת�� LINQ
                list = enemy.monster.IntentList.Cast<ILoot>().ToList();

                ILoot loot = lootBag.GetDroppedItem(list);

                newEnemyIntent = loot as BaseIntent;

                ChangeType(newEnemyIntent);

            }
            catch
            {
                Tool.Log("CreateIntent ����ʧЧ", LogLevel.Error);
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
                    Debug.Log("EnemyActionType û���ҵ�״̬");
                    break;
            }
            WrapUpTurn();

        }



        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private IEnumerator ApplyAttack()
        {
            yield return new WaitForSeconds(0.5f);
            this.SendCommand<DataAttackCommand>(new DataAttackCommand(new DamageInfo(newEnemyIntent.creator, newEnemyIntent.target, newEnemyIntent.intentAttack)));

        }


        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        private IEnumerator ApplyDefense()
        {
            yield return new WaitForSeconds(0.5f);
            this.SendCommand<DataDefenseCommand>(new DataDefenseCommand(newEnemyIntent.target, newEnemyIntent.intentDefense));
        }

        /// <summary>
        /// ������һ��ʱ���Ӧ��buffЧ��  
        /// </summary>
        /// <returns></returns>
        private IEnumerator ApplyBuff(Fighter fighter)
        {
            yield return new WaitForSeconds(0.5f);
            this.SendCommand<DataBuffCommand>(new DataBuffCommand(fighter, newEnemyIntent.buffInfo));
        }



        ///// <summary>
        ///// ��ʾ���˵���ͼ  
        ///// </summary>
        //private void DisplayIntent(EnemyAction newEnemyAction)
        //{
        //    if (newEnemyAction != null) { return; }

        //    newEnemyAction.Damage
        //    ();

        //    cell.DisplayIntent(newEnemyAction);

        //}


        /// �����غϵĴ�����  
        private void WrapUpTurn()
        {
            trunCnt++;
            Tool.Log(" �����غϵĴ�����");
        }

    }

}