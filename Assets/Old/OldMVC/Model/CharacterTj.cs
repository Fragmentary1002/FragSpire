using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    /// <summary>
    /// 角色类，继承自ScriptableObject，用于创建角色资源
    /// </summary>
    //[CreateAssetMenu]
    public class CharacterTj : ScriptableObject
    {
        public CharacterClass characterClass;   // 角色职业
        public enum CharacterClass { ironChad, silent }   // 角色职业枚举
        public GameObject characterPrefab;      // 角色预制体对象
        public RelicTj startingRelic;             // 角色初始遗物
        public Sprite splashArt;                // 角色头像
        public List<CardTj> startingDeck;         // 角色初始卡组
    }
}
