using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{


    #region 枚举类

    public enum CardClass
    {
        IronChad,       //战
        Silent,         //猎
        Colorless,      //无色
        Curse,          //诅咒
        Status          //状态
    }

    public enum CardType
    {
        Attack,     //攻击
        Skill,      //技能
        Power       //能力
    }

    public enum CardTargetType
    {
        Self,
        Enemy
    };

    #endregion

    [CreateAssetMenu(fileName = "Card", menuName = "ScriptableObject/Card")]
    public class BaseCard : ScriptableObject
    {
        #region 基础

        [Header("基础")]
        //卡牌id
        public int cardId;

        // 是否已升级
        public bool isUpgraded;

        #endregion



        #region 升级效果结构体
        // 卡牌费用和效果的结构体
        [System.Serializable]
        public struct ScardAmount
        {
            public int baseAmount; // 基础值
            public int upgradedAmount; // 升级后的值
        }

        // 卡牌描述的结构体
        [System.Serializable]
        public struct ScardDescription
        {
            public string baseAmount; // 基础值
            public string upgradedAmount; // 升级后的值
        }
        #endregion



        #region 关于UI
        [Header("关于UI")]


        // 卡牌标题
        [SerializeField]
        private string cardTitle;
        public string CardTitle
        {
            get { return cardTitle; }
        }




        // 卡牌描述
        [SerializeField]
        private ScardDescription cardDescription;

        public string CardDescription
        {
            get
            {
                if (!isUpgraded)
                    return cardDescription.baseAmount;
                else
                    return cardDescription.upgradedAmount;
            }
        }


        // 卡牌类别
        [SerializeField]
        private CardClass cardColor;
        public CardClass CardColor
        {
            get { return cardColor; }
        }



        [SerializeField]
        // 卡牌图标
        private Sprite cardIcon;
        public Sprite CardIcon
        {
            get { return cardIcon; }
        }
        #endregion



        #region 关于基础数值

        [Header("关于基础数值")]

        // 卡牌费用
        [SerializeField]
        private ScardAmount cardCost;
        public int CardCost
        {
            get
            {
                if (!isUpgraded)
                    return cardCost.baseAmount;
                else
                    return cardCost.upgradedAmount;
            }
        }





        // 卡牌数值
        [SerializeField]
        private ScardAmount cardEffect;
        public int CardEffect
        {
            get
            {
                if (!isUpgraded)
                    return cardEffect.baseAmount;
                else
                    return cardEffect.upgradedAmount;
            }
        }





        // 卡牌类型
        [SerializeField]
        private CardType type;
        public CardType Type
        {
            get { return type; }
        }



        // 卡牌目标类型
        [SerializeField]
        private CardTargetType target;
        public CardTargetType Target
        {
            get { return target; }
        }

        #endregion



        #region 关于Buff
        [Header("关于Buff")]
        //是否施加buff


        [SerializeField]
        private bool isBuffs;
        public bool IsBuffs
        {
            get { return isBuffs; }
        }

        //施加的buff
        [SerializeField]
        private BuffInfo cardBuff;
        public BuffInfo CardBuff
        {
            get { return cardBuff; }
        }




        // 卡牌buff增益数值
        [SerializeField]
        private ScardAmount buffAmount;
        public int BuffAmount
        {
            get
            {
                if (!isUpgraded)
                    return buffAmount.baseAmount;
                else
                    return buffAmount.upgradedAmount;
            }
        }





        // 卡牌buff目标类型
        [SerializeField]
        private CardTargetType targetBuff;
        public CardTargetType TargetBuff
        {
            get { return targetBuff; }
        }

        #endregion



        public virtual void Apply() {}
    }




}


