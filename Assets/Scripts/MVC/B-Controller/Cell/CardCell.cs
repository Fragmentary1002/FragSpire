using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

using System;


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
        public Animator animator;



        BazierArrows bazierArrows = null;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

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
            cardTitleText.text = this.card.CardTitle;
            cardDescriptionText.text = this.card.CardDescription;
            cardCostText.text = this.card.CardCost.ToString();
            cardImage.sprite = this.card.CardIcon;
        }

        #region 卡牌ui的操作

        public void PushCardPool()
        {
            //Instantiate(this.discardEffect, this.transform.position, Quaternion.identity);
            PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/CardCell", this.gameObject);
        }


        #endregion

        #region 点击事件

        private int index;
        // 选择该卡牌的方法
        public void OnSelectCard()
        {
            // 告知战斗场景管理器该卡牌已被选择
            index = transform.GetSiblingIndex();

        }

        // 取消选择该卡牌的方法
        public void OnDeselectCard()
        {
            // 告知战斗场景管理器没有卡牌被选择，并播放悬停关闭动画
            animator.Play("HoverOffCard");
        }

        // 当鼠标悬停在卡牌上时触发的方法
        public void OnHoverCard()
        {
            // 如果没有卡牌被选择，则播放悬停打开动画

            animator.Play("HoverOnCard");
        }

        // 当鼠标拖拽卡牌时触发的方法
        public void OnHandleDrag()
        {
            // 暂未实现
            Tool.Log("OnHandleDrag");

            //if (this.card.Type == CardType.Attack)
            //{
            //    StartCoroutine(DisPlayArrow());
            //}

            StartCoroutine(DisPlayArrow());
        }

        public IEnumerator DisPlayFollow()
        {
            Transform parentTran;

            parentTran = this.transform.parent;

            transform.SetParent(parentTran.parent);

            while (true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if (FightCardManager.Instance.TryPlayCard(this.card))
                    {
                        animator?.Play("HoverOffCard");
                        PushCardPool();
                    }
                    break;
                }
                transform.position = Input.mousePosition;

                yield return null;
            }

            transform.SetParent(parentTran);

            transform.SetSiblingIndex(index);

            yield return null;
        }

        public IEnumerator DisPlayArrow()
        {
            ShowBazierArrows();

            while (true)
            {
                // Tool.Log("OnMouseDownRight");
                if (Input.GetMouseButtonDown(1))
                {
                    if (FightCardManager.Instance.TryPlayCard(this.card))
                    {
                        animator?.Play("HoverOffCard");
                        PushCardPool();
                    }
                    break;
                }
                SetBazierArrows();

                yield return null;
            }
            // StopAllCoroutines();
            Cursor.visible = true;
            //closeui -line
            CloseBazierArrows();
            yield return null;
        }


        // 当鼠标结束拖拽卡牌时触发的方法
        public void OnHandleEndDrag()
        {
            Tool.Log("OnHandleEndDrag");
            //ApplyCard();
        }

        // 当放下卡牌时触发的方法
        public void OnDropCard()
        {
            animator.Play("HoverOffCard");
        }
        #endregion

        #region BazierArrows
        private void ShowBazierArrows()
        {
            PoolMgr.GetInstance().GetObj("Prefabs/UI/Cell/Arrow", (go) =>
            {
                if (go != null)
                {
                    go.transform.SetParent(this.gameObject.transform.parent.parent);

                    go.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, 0);

                    this.bazierArrows = go.GetComponent<BazierArrows>();

                    this.bazierArrows.SetStartPos();

                }
            });
        }

        private void SetBazierArrows()
        {
            this.bazierArrows?.SetEndPos();
        }


        private void CloseBazierArrows()
        {
            bazierArrows?.CloseUI();
        }

        #endregion
    }
}
