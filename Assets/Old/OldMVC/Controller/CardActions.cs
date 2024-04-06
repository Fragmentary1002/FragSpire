using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    /// <summary>
    /// ִ�п��Ʋ�������
    /// </summary>
    public class CardActions : MonoBehaviour
    {
        CardTj card; // ��ǰʹ�õĿ���
        public Fighter target; // Ŀ��ս����
        public Fighter player; // ���
        BattleSceneManager battleSceneManager; // ս������������

        private void Awake()
        {
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
        }

        /// <summary>
        /// ִ�п��ƵĲ���
        /// </summary>
        /// <param name="_card">Ҫִ�еĿ���</param>
        /// <param name="_fighter">Ŀ��ս����</param>
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
        /// ��������
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
        /// ӵ�ж��⹥�����Ĺ���
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
        /// ʹ��Bodyslam����
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
        /// ʹ��Entrench����
        /// </summary>
        private void Entrench()
        {
            player.AddBlock(player.currentBlock);
        }

        /// <summary>
        /// ��Ŀ��ʩ��ָ�����͵�Buff
        /// </summary>
        /// <param name="t">Ҫʩ�ӵ�Buff����</param>
        private void ApplyBuff(Buff.Type t)
        {
            target.AddBuff(t, card.GetBuffAmount());
        }

        /// <summary>
        /// ���Լ�ʩ��ָ�����͵�Buff
        /// </summary>
        /// <param name="t">Ҫʩ�ӵ�Buff����</param>
        private void ApplyBuffToSelf(Buff.Type t)
        {
            player.AddBuff(t, card.GetBuffAmount());
        }

        /// <summary>
        /// ���Լ�����˺�
        /// </summary>
        private void AttackSelf()
        {
            player.TakeDamage(2);
        }

        /// <summary>
        /// ʩ���赲
        /// </summary>
        private void PerformBlock()
        {
            player.AddBlock(card.GetCardEffectAmount());
        }
    }
}
