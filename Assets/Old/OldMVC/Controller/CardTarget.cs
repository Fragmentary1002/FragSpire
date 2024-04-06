using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    public class CardTarget : MonoBehaviour
    {
        // 战斗场景管理器的引用
        BattleSceneManager battleSceneManager;

        // 敌方战斗者的引用
        Fighter enemyFighter;

        private void Awake()
        {
            // 查找并存储战斗场景管理器和敌方战斗者
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            enemyFighter = GetComponent<Fighter>();
        }

        // 当鼠标指针进入卡牌目标时触发的方法
        public void PointerEnter()
        {
            // 如果敌方战斗者为空，重新查找
            if (enemyFighter == null)
            {
                Debug.Log("fighter is null");
                battleSceneManager = FindObjectOfType<BattleSceneManager>();
                enemyFighter = GetComponent<Fighter>();
            }

            // 如果选定的卡牌不为空且为攻击类型，则设置目标为敌方战斗者
            if (battleSceneManager.selectedCard != null && battleSceneManager.selectedCard.card.cardType == CardTj.CardType.Attack)
            {
                // 将目标设置为敌方战斗者
                battleSceneManager.cardTarget = enemyFighter;
                //Debug.Log("set target");
            }
        }

        // 当鼠标指针退出卡牌目标时触发的方法
        public void PointerExit()
        {
            // 将卡牌目标设置为空
            battleSceneManager.cardTarget = null;
            //Debug.Log("drop target");
        }
    }
}
