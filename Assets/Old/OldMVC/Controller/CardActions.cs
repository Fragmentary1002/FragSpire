using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    /// <summary>
    /// 执行卡牌操作的类
    /// </summary>
    public class CardActions : MonoBehaviour
    {
        CardTj card; // 当前使用的卡牌
        public Fighter target; // 目标战斗者
        public Fighter player; // 玩家
        BattleSceneManager battleSceneManager; // 战斗场景管理器

        private void Awake()
        {
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
        }

        /// <summary>
        /// 执行卡牌的操作
        /// </summary>
        /// <param name="_card">要执行的卡牌</param>
        /// <param name="_fighter">目标战斗者</param>
        public void PerformAction(CardTj _card, Fighter _fighter)
        {
            card = _card;
            target = _fighter;

            switch (card.cardTitle)
            {
                case "Strike":
                    AttackEnemy();
                    break;
                case "Defend":
                    PerformBlock();
                    break;
                case "Bash":
                    AttackEnemy();
                    ApplyBuff(Buff.Type.vulnerable);
                    break;
                case "Inflame":
                    ApplyBuffToSelf(Buff.Type.strength);
                    break;
                case "Clothesline":
                    AttackEnemy();
                    ApplyBuff(Buff.Type.weak);
                    break;
                case "ShrugItOff":
                    PerformBlock();
                    battleSceneManager.DrawCards(1);
                    break;
                case "IronWave":
                    AttackEnemy();
                    PerformBlock();
                    break;
                case "Bloodletting":
                    AttackSelf();
                    battleSceneManager.energy += 2;
                    break;
                case "Bodyslam":
                    BodySlam();
                    break;
                case "Entrench":
                    Entrench();
                    break;
                default:
                    Debug.Log("There's an issue");
                    break;
            }
        }

        /// <summary>
        /// 攻击敌人
        /// </summary>
        private void AttackEnemy()
        {
            int totalDamage = card.GetCardEffectAmount() + player.strength.buffValue;
            if (target.vulnerable.buffValue > 0)
            {
                float a = totalDamage * 1.5f;
                Debug.Log("Increased damage from " + totalDamage + " to " + (int)a);
                totalDamage = (int)a;
            }
            target.TakeDamage(totalDamage);
        }

        /// <summary>
        /// 拥有额外攻击力的攻击
        /// </summary>
        private void AttackStrength()
        {
            int totalDamage = card.GetCardEffectAmount() + (player.strength.buffValue * 3);
            if (target.vulnerable.buffValue > 0)
            {
                float a = totalDamage * 1.5f;
                Debug.Log("Increased damage from " + totalDamage + " to " + (int)a);
                totalDamage = (int)a;
            }
            target.TakeDamage(totalDamage);
        }

        /// <summary>
        /// 使用Bodyslam技能
        /// </summary>
        private void BodySlam()
        {
            int totalDamage = player.currentBlock;
            if (target.vulnerable.buffValue > 0)
            {
                float a = totalDamage * 1.5f;
                Debug.Log("Increased damage from " + totalDamage + " to " + (int)a);
                totalDamage = (int)a;
            }
            target.TakeDamage(totalDamage);
        }

        /// <summary>
        /// 使用Entrench技能
        /// </summary>
        private void Entrench()
        {
            player.AddBlock(player.currentBlock);
        }

        /// <summary>
        /// 对目标施加指定类型的Buff
        /// </summary>
        /// <param name="t">要施加的Buff类型</param>
        private void ApplyBuff(Buff.Type t)
        {
            target.AddBuff(t, card.GetBuffAmount());
        }

        /// <summary>
        /// 对自己施加指定类型的Buff
        /// </summary>
        /// <param name="t">要施加的Buff类型</param>
        private void ApplyBuffToSelf(Buff.Type t)
        {
            player.AddBuff(t, card.GetBuffAmount());
        }

        /// <summary>
        /// 对自己造成伤害
        /// </summary>
        private void AttackSelf()
        {
            player.TakeDamage(2);
        }

        /// <summary>
        /// 施加阻挡
        /// </summary>
        private void PerformBlock()
        {
            player.AddBlock(card.GetCardEffectAmount());
        }
    }
}
