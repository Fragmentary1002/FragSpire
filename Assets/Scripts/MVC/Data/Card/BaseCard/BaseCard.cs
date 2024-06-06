using QFramework;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Frag
{


    #region ö����

    public enum CardClass
    {
        IronChad,       //ս
        Silent,         //��
        Colorless,      //��ɫ
        Curse,          //����
        Status          //״̬
    }

    public enum CardType
    {
        Attack,     //����
        Skill,      //����
        Power       //����
    }

    public enum CardTargetType
    {
        Self,
        Enemy
    };

    #endregion

    [CreateAssetMenu(fileName = "Card", menuName = "ScriptableObject/Card/BaseCard")]
    public class BaseCard : ScriptableObject, ICanSendCommand
    {
        #region ����

        [Header("����")]
        //����id
        public int cardId;

        // �Ƿ�������
        public bool isUpgraded;

        #endregion



        #region ����Ч���ṹ��
        // ���Ʒ��ú�Ч���Ľṹ��
        [System.Serializable]
        public struct ScardAmount
        {
            public int baseAmount; // ����ֵ
            public int upgradedAmount; // �������ֵ
        }

        // ���������Ľṹ��
        [System.Serializable]
        public struct ScardDescription
        {
            public string baseAmount; // ����ֵ
            public string upgradedAmount; // �������ֵ
        }
        #endregion



        #region ����UI
        [Header("����UI")]


        // ���Ʊ���
        [SerializeField]
        private string cardTitle;
        public string CardTitle
        {
            get { return cardTitle; }
        }




        // ��������
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


        // �������
        [SerializeField]
        private CardClass cardColor;
        public CardClass CardColor
        {
            get { return cardColor; }
        }



        [SerializeField]
        // ����ͼ��
        private Sprite cardIcon;
        public Sprite CardIcon
        {
            get { return cardIcon; }
        }
        #endregion



        #region ���ڻ�����ֵ

        [Header("���ڻ�����ֵ")]
        // ���Ʒ���
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





        // ������ֵ
        [SerializeField]
        private ScardAmount cardAttack;
        public int CardAttack
        {
            get
            {
                if (!isUpgraded)
                    return cardAttack.baseAmount;
                else
                    return cardAttack.upgradedAmount;
            }
        }



        // ������ֵ
        [SerializeField]
        private ScardAmount cardDefense;
        public int CardDefense
        {
            get
            {
                if (!isUpgraded)
                    return cardDefense.baseAmount;
                else
                    return cardDefense.upgradedAmount;
            }
        }



        // ��������
        [SerializeField]
        private CardType type;
        public CardType Type
        {
            get { return type; }
        }



        // ����Ŀ������
        [SerializeField]
        private CardTargetType target;
        public CardTargetType Target
        {
            get { return target; }
        }

        #endregion



        #region ����Buff
        [Header("����Buff")]


        ////�Ƿ�ʩ��buff
        //[SerializeField]
        //private bool isBuffs;
        //public bool IsBuffs
        //{
        //    get { return isBuffs; }
        //}

        //ʩ�ӵ�buff
        [SerializeField]
        private BuffInfo cardBuff;
        public BuffInfo CardBuff
        {
            get { return cardBuff; }
        }




        // ����buff������ֵ
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





        // ����buffĿ������
        [SerializeField]
        private CardTargetType targetBuff;
        public CardTargetType TargetBuff
        {
            get { return targetBuff; }
        }

        #endregion



        public virtual void Apply(Fighter creator, Fighter target) { }


        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }




}


