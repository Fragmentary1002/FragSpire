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
        // ���ÿ�������
        public BaseCard card;

        // ��ʾ���Ʊ�����ı�
        public TMP_Text cardTitleText;

        // ��ʾ�����������ı�
        public TMP_Text cardDescriptionText;

        // ��ʾ���Ʒ��õ��ı�
        public TMP_Text cardCostText;

        // ��ʾ����ͼ���ͼ�����
        public Image cardImage;

        // ��������ʱ����Ч
        //public GameObject discardEffect;

        // ���ƿ���UI������Animator���
        public Animator animator;



        BazierArrows bazierArrows = null;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            // ������UI����ʱ��������ͣ�رն���
            animator.Play("HoverOffCard");


        }

        // ���ؿ������ݵ�UI�еķ���
        public void LoadCard(BaseCard card)
        {
            // ���ÿ������ݲ�����UIԪ��
            this.card = card;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            cardTitleText.text = this.card.CardTitle;
            cardDescriptionText.text = this.card.CardDescription;
            cardCostText.text = this.card.CardCost.ToString();
            cardImage.sprite = this.card.CardIcon;
        }

        #region ����ui�Ĳ���

        public void PushCardPool()
        {
            //Instantiate(this.discardEffect, this.transform.position, Quaternion.identity);
            PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/CardCell", this.gameObject);
        }


        #endregion

        #region ����¼�

        private int index;
        // ѡ��ÿ��Ƶķ���
        public void OnSelectCard()
        {
            // ��֪ս�������������ÿ����ѱ�ѡ��
            index = transform.GetSiblingIndex();

        }

        // ȡ��ѡ��ÿ��Ƶķ���
        public void OnDeselectCard()
        {
            // ��֪ս������������û�п��Ʊ�ѡ�񣬲�������ͣ�رն���
            animator.Play("HoverOffCard");
        }

        // �������ͣ�ڿ�����ʱ�����ķ���
        public void OnHoverCard()
        {
            // ���û�п��Ʊ�ѡ���򲥷���ͣ�򿪶���

            animator.Play("HoverOnCard");
        }

        // �������ק����ʱ�����ķ���
        public void OnHandleDrag()
        {
            // ��δʵ��
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


        // ����������ק����ʱ�����ķ���
        public void OnHandleEndDrag()
        {
            Tool.Log("OnHandleEndDrag");
            //ApplyCard();
        }

        // �����¿���ʱ�����ķ���
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
