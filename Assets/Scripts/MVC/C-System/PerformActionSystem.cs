
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    public class PerformActionSystem : AbstractSystem
    {
        private Player player;
        private Enemy enemy;

        protected override void OnInit()
        {
            player = this.GetModel<Player>();
            enemy = this.GetModel<Enemy>();
  
        }

        public void PerformAction(BaseCard card)
        {
            if (card == null) return;

            Tool.Log($"执行卡片的动作");

            switch (card.Type)
            {
                case CardType.Attack:
                    enemy.DoTakeDamageAndIsEndFight(card.CardEffect);
                    //FighterManager.Instance.DoTakeDamageAndIsEndFight(player,card.CardEffect);
                    break;
                case CardType.Skill:
                    player.DoAddBlock(card.CardEffect);
                    break;
                case CardType.Power:
                    player.DoAddBuff(card.CardBuff, card.BuffAmount);
                    break;
            }

        }

        public void PerformAction(EnemyAction enemyAction)
        {

            switch (enemyAction.type)
            {
                case EnemyActionType.Attack:
                    //FighterManager.Instance.DoTakeDamageAndIsEndFight(enemy, enemyAction.amount);
                    player.DoTakeDamageAndIsEndFight(enemyAction.amount);
                    break;
                case EnemyActionType.Block:
                    enemy.DoAddBlock(enemyAction.amount);
                    break;
                case EnemyActionType.Buff:
                    enemy.DoAddBuff(enemyAction.Buff, enemyAction.buffAmount);
                    break;

            }
        }



     



    }
}