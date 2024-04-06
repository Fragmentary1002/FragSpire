using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    [CreateAssetMenu]
    public class CardTj : ScriptableObject
    {
        public int cardId;

        // 卡牌标题
        public string cardTitle;

        // 是否已升级
        public bool isUpgraded;

        // 卡牌描述
        public CardDescription cardDescription;

        // 卡牌费用
        public CardAmount cardCost;

        // 卡牌效果
        public CardAmount cardEffect;

        // 卡牌增益效果
        public CardAmount buffAmount;

        // 卡牌图标
        public Sprite cardIcon;

        // 卡牌类型
        public CardType cardType;
        public enum CardType { Attack, Skill, Power }

        // 卡牌类别
        public CardClass cardClass;
        public enum CardClass { IronChad, Silent, Colorless, Curse, Status }

        // 卡牌目标类型
        public CardTargetType cardTargetType;
        public enum CardTargetType { Self, Enemy };

        // 获取卡牌费用的方法
        public int GetCardCostAmount()
        {
            if (!isUpgraded)
                return cardCost.baseAmount;
            else
                return cardCost.upgradedAmount;
        }

        // 获取卡牌效果的方法
        public int GetCardEffectAmount()
        {
            if (!isUpgraded)
                return cardEffect.baseAmount;
            else
                return cardEffect.upgradedAmount;
        }

        // 获取卡牌描述的方法
        public string GetCardDescriptionAmount()
        {
            if (!isUpgraded)
                return cardDescription.baseAmount;
            else
                return cardDescription.upgradedAmount;
        }

        // 获取卡牌增益效果的方法
        public int GetBuffAmount()
        {
            if (!isUpgraded)
                return buffAmount.baseAmount;
            else
                return buffAmount.upgradedAmount;
        }
    }

    // 卡牌费用和效果的结构体
    [System.Serializable]
    public struct CardAmount
    {
        public int baseAmount; // 基础值
        public int upgradedAmount; // 升级后的值
    }

    // 卡牌描述的结构体
    [System.Serializable]
    public struct CardDescription
    {
        public string baseAmount; // 基础值
        public string upgradedAmount; // 升级后的值
    }

    // 卡牌增益效果的结构体
    [System.Serializable]
    public struct CardBuffs
    {
        public Buff.Type buffType; // 增益类型
        public CardAmount buffAmount; // 增益数值
    }
}
