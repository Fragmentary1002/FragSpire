using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    /// <summary>
    /// 表示玩家角色的类
    /// </summary>
    public class Player
    {
        public string playerName;           // 玩家名称
        public CharacterTj selectedClass;     // 已选择的角色类别
        public int currentHealth;           // 当前生命值
        public int maxHealth;               // 最大生命值
        public int gold;                    // 金币数量
        public List<Potion> potions;       // 玩家持有的药水列表
        public int currentFloor;            // 当前所在楼层
        public List<CardTj> cards;           // 玩家持有的卡牌列表
        public List<RelicTj> relics;         // 玩家持有的遗物列表
    }
}
