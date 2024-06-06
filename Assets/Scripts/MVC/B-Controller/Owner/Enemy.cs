using QFramework;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

namespace Frag
{


    public class Enemy : Fighter
    {
        #region model Field

        public BaseMonster baseMonster;

        // public Enemy owner;

        public List<BaseIntent> intents;

        private BaseIntent newEnemyIntent;

        private int trunCnt = 0; //回合数
        #endregion


        #region view Field
        public GameObject selectImage;

        public EnemyIntentCell enemyIntentCell;

        #endregion

        private LootBag lootBag;



        private void Start()
        {
            this.hp.max = baseMonster.startHealth;
            this.hp.cur = baseMonster.startHealth;
            this.intents = baseMonster.IntentList;

            // this.newEnemyIntent = intents[0];

            lootBag = this.GetUtility<LootBag>();

            UpdateIntent();

            OnUnSelect();

        }
        #region UI method

        public void OnSelect()
        {
            selectImage.SetActive(true) ;
        }

        public void OnUnSelect()
        {
            selectImage.SetActive(false);
        }
        #endregion

        #region Intent method

        public void UpdateIntent()
        {
            List<ILoot> loots = new List<ILoot>();

            for (int i = 0; i < intents.Count; i++)
            {
                loots.Add(intents[i]);
            }

            foreach (ILoot l in loots)
            {
                Tool.Log(l.ToSafeString());
            }

            ILoot loot = this.lootBag.GetDroppedItem(loots);
            newEnemyIntent = loot as BaseIntent;
            enemyIntentCell.DisplayIntent(this.newEnemyIntent);

            trunCnt++;
        }


        public void DoAction()
        {
            Chance(newEnemyIntent.type);
        
        }
        private void Chance(EnemyActionType enemyActionType)
        {

            switch (enemyActionType)
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
          
        }

        /// <summary>
        /// 攻击
        /// </summary>
        /// <returns></returns>
        private IEnumerator ApplyAttack()
        {
            yield return new WaitForSeconds(0.5f);
            this.SendCommand<DataDamageCommand>(new DataDamageCommand(new DamageInfo(newEnemyIntent.creator, newEnemyIntent.target, newEnemyIntent.intentAttack)));

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


        /// 结束回合的处理方法  

        #endregion


    }



}


