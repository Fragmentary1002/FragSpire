using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Frag
{
    public class CardTarget : MonoBehaviour, ICanGetModel { 
        // 战斗场景管理器的引用
        // 敌方战斗者的引用
        Enemy enemyFighter;
        BattleInfo battleInfo;

        private void Awake()
        {
            // 查找并存储战斗场景管理器和敌方战斗者
            enemyFighter = GetComponent<EnemyOwner>().owner;
            battleInfo = this.GetModel<BattleInfo>();

        }

        // 当鼠标指针进入卡牌目标时触发的方法
        public void OnPointerEnter()
        {
            // 如果敌方战斗者为空，重新查找
            if (enemyFighter == null)
            {
                Debug.Log("fighter is null");

                enemyFighter = GetComponent<EnemyOwner>().owner;
            }

            // 将目标设置为敌方战斗者
            battleInfo.target = this.enemyFighter;
            Debug.Log("set target");

        }

        // 当鼠标指针退出卡牌目标时触发的方法
        public void OnPointerExit()
        {
            // 将卡牌目标设置为空
            battleInfo.target = null;
            Debug.Log("drop target");
        }

        //指定架构
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}
