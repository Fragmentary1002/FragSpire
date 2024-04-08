using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TJ;
using UnityEngine.EventSystems;
using Unity.Burst.CompilerServices;


namespace Frag
{
    public class CardCell : MonoBehaviour
    {
        // 引用卡牌数据
        public BaseCard card;

        // 显示卡牌标题的文本
        public TMP_Text cardTitleText;

        // 显示卡牌描述的文本
        public TMP_Text cardDescriptionText;

        // 显示卡牌费用的文本
        public TMP_Text cardCostText;

        // 显示卡牌图像的图像组件
        public Image cardImage;

        // 丢弃卡牌时的特效
        //public GameObject discardEffect;

        // 控制卡牌UI动画的Animator组件
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }


        #region 点击事件



        private void OnEnable()
        {
            // 当卡牌UI启用时，播放悬停关闭动画
            animator.Play("HoverOffCard");
        }

        // 加载卡牌数据到UI中的方法
        public void LoadCard(BaseCard card)
        {
            // 设置卡牌数据并更新UI元素
            this.card = card;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            cardTitleText.text = card.CardTitle;
            cardDescriptionText.text = card.CardDescription;
            cardCostText.text = card.CardCost.ToString();
            cardImage.sprite = card.CardIcon;
        }

        // 选择该卡牌的方法
        public void SelectCard()
        {
            // 告知战斗场景管理器该卡牌已被选择
            //battleSceneManager.selectedCard = this;
            // Debug.Log("选择该卡牌的方法");
        }

        // 取消选择该卡牌的方法
        public void DeselectCard()
        {
            // 告知战斗场景管理器没有卡牌被选择，并播放悬停关闭动画
            //battleSceneManager.selectedCard = null;
            animator?.Play("HoverOffCard");
            // Debug.Log("取消选择该卡牌的方法");
        }

        // 当鼠标悬停在卡牌上时触发的方法
        public void HoverCard()
        {
            // 如果没有卡牌被选择，则播放悬停打开动画
            // if (battleSceneManager.selectedCard == null)
            animator?.Play("HoverOnCard");
            //Debug.Log("当鼠标悬停在卡牌上时触发的方法");
        }

        // 当鼠标拖拽卡牌时触发的方法
        public void HandleDrag()
        {
            // 暂未实现
            // Debug.Log("当鼠标拖拽卡牌时触发的方法");
        }

        // 当鼠标结束拖拽卡牌时触发的方法
        public void HandleEndDrag(PointerEventData eventData)
        {
            Vector3 mousePosition = Input.mousePosition;

            // 将屏幕坐标转换为世界坐标
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hitInfo;

            // 发射射线，并检查是否击中了游戏对象
            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject clickedObject = hitInfo.collider.gameObject;


                if (FightCardManager.Instance.PlayCardAndIsSuccess(this.card,clickedObject))
                {
                    animator?.Play("HoverOffCard");
                    PushCardPool();
                }


            }

            return;


            //if (card.Type == CardType.Attack)
            //{
            //    // 播放卡牌并执行关闭悬停动画
            //    FightCardManager.Instance.PlayCard(this.card);
            //    animator?.Play("HoverOffCard");
            //}
            //else
            //{
            //    // 如果卡牌类型不是攻击，则直接播放卡牌
            //    animator?.Play("HoverOffCard");
            //    FightCardManager.Instance.PlayCard(this.card);
            //}
            ////animator?.Play("HoverOnCard");


        }

        // 当放下卡牌时触发的方法
        public void DropCard()
        {

            // 如果没有卡牌被选择，则播放悬停关闭动画
            //if (battleSceneManager.selectedCard == null)
            animator?.Play("HoverOffCard");
            // Debug.Log("当放下卡牌时触发的方法");
        }
        #endregion


        #region 卡牌ui的操作

        public void PushCardPool()
        {
            //Instantiate(this.discardEffect, this.transform.position, Quaternion.identity);
            PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/CardCell", this.gameObject);
        }

        #endregion
    }
}
