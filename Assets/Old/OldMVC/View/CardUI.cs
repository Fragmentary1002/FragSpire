using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TJ
{
    public class CardUI : MonoBehaviour
    {
        // 引用卡牌数据
        public CardTj card;

        // 显示卡牌标题的文本
        public TMP_Text cardTitleText;

        // 显示卡牌描述的文本
        public TMP_Text cardDescriptionText;

        // 显示卡牌费用的文本
        public TMP_Text cardCostText;

        // 显示卡牌图像的图像组件
        public Image cardImage;

        // 丢弃卡牌时的特效
        public GameObject discardEffect;

        // 战斗场景管理器的引用
        BattleSceneManager battleSceneManager;

        // 控制卡牌UI动画的Animator组件
        Animator animator;

        private void Awake()
        {
            // 查找并存储战斗场景管理器和Animator组件
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            // 当卡牌UI启用时，播放悬停关闭动画
            animator.Play("HoverOffCard");
        }

        // 加载卡牌数据到UI中的方法
        public void LoadCard(CardTj _card)
        {
            // 设置卡牌数据并更新UI元素
            card = _card;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            cardTitleText.text = card.cardTitle;
            cardDescriptionText.text = card.GetCardDescriptionAmount();
            cardCostText.text = card.GetCardCostAmount().ToString();
            cardImage.sprite = card.cardIcon;
        }

        // 选择该卡牌的方法
        public void SelectCard()
        {
            // 告知战斗场景管理器该卡牌已被选择
            battleSceneManager.selectedCard = this;
        }

        // 取消选择该卡牌的方法
        public void DeselectCard()
        {
            // 告知战斗场景管理器没有卡牌被选择，并播放悬停关闭动画
            battleSceneManager.selectedCard = null;
            animator.Play("HoverOffCard");
        }

        // 当鼠标悬停在卡牌上时触发的方法
        public void HoverCard()
        {
            // 如果没有卡牌被选择，则播放悬停打开动画
            if (battleSceneManager.selectedCard == null)
                animator.Play("HoverOnCard");
        }

        // 当鼠标拖拽卡牌时触发的方法
        public void HandleDrag()
        {
            // 暂未实现
        }

        // 当鼠标结束拖拽卡牌时触发的方法
        public void HandleEndDrag()
        {
            // 如果能量不足以支付卡牌费用，则直接返回
            if (battleSceneManager.energy < card.GetCardCostAmount())
                return;

            // 根据卡牌类型执行相应操作
            if (card.cardType == CardTj.CardType.Attack)
            {
                // 播放卡牌并执行关闭悬停动画
                battleSceneManager.PlayCard(this);
                animator.Play("HoverOffCard");
            }
            else // 如果卡牌类型不是攻击，则直接播放卡牌
            {
                animator.Play("HoverOffCard");
                battleSceneManager.PlayCard(this);
            }
        }

        // 当放下卡牌时触发的方法
        public void DropCard()
        {
            // 如果没有卡牌被选择，则播放悬停关闭动画
            if (battleSceneManager.selectedCard == null)
                animator.Play("HoverOffCard");
        }
    }
}
