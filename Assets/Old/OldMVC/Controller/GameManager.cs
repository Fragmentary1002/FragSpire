using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;


namespace TJ
{
    /// <summary>
    /// 管理游戏状态和数据的类
    /// </summary>
    /// 
    public class GameManager : MonoBehaviour
    {
        public CharacterTj character;    // 游戏中的角色
        public List<CardTj> playerDeck = new List<CardTj>();   // 玩家的卡牌组
        public List<CardTj> cardLibrary = new List<CardTj>();  // 卡牌库
        public List<RelicTj> relics = new List<RelicTj>();     // 玩家拥有的遗物
        public List<RelicTj> relicLibrary = new List<RelicTj>();   // 遗物库
        public int floorNumber = 1; // 当前楼层
        public int goldAmount;  // 金币数量
        PlayerStatsUI playerStatsUI;    // 玩家状态UI对象

        private void Awake()
        {
            playerStatsUI = FindObjectOfType<PlayerStatsUI>();   // 查找并获取玩家状态UI对象
        }

        // 加载角色初始状态
        public void LoadCharacterStats()
        {
            relics.Add(character.startingRelic);    // 添加角色初始遗物
            playerStatsUI.playerStatsUIObject.SetActive(true);  // 激活玩家状态UI对象
            playerStatsUI.DisplayRelics();  // 显示拥有的遗物
        }

        // 检查玩家是否拥有指定名称的遗物
        public bool PlayerHasRelic(string rName)
        {
            foreach (RelicTj r in relics)
            {
                if (r.relicName == rName)
                    return true;
            }
            return false;
        }

        // 更新当前楼层
        public void UpdateFloorNumber()
        {
            floorNumber += 1;

            switch (floorNumber)
            {
                case 1:
                    playerStatsUI.floorText.text = floorNumber + "st Floor";
                    break;
                case 2:
                    playerStatsUI.floorText.text = floorNumber + "nd Floor";
                    break;
                case 3:
                    playerStatsUI.floorText.text = floorNumber + "rd Floor";
                    break;
                default:
                    playerStatsUI.floorText.text = floorNumber + "th Floor";
                    break;
            }
        }

        // 更新金币数量
        public void UpdateGoldNumber(int newGold)
        {
            goldAmount += newGold;
            playerStatsUI.moneyAmountText.text = goldAmount.ToString();
        }

        // 显示当前生命值
        public void DisplayHealth(int healthAmount, int maxHealth)
        {
            playerStatsUI.healthDisplayText.text = $"{healthAmount} / {maxHealth}";
        }
    }

}

