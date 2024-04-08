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
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }


        #region ����¼�



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
            cardTitleText.text = card.CardTitle;
            cardDescriptionText.text = card.CardDescription;
            cardCostText.text = card.CardCost.ToString();
            cardImage.sprite = card.CardIcon;
        }

        // ѡ��ÿ��Ƶķ���
        public void SelectCard()
        {
            // ��֪ս�������������ÿ����ѱ�ѡ��
            //battleSceneManager.selectedCard = this;
            // Debug.Log("ѡ��ÿ��Ƶķ���");
        }

        // ȡ��ѡ��ÿ��Ƶķ���
        public void DeselectCard()
        {
            // ��֪ս������������û�п��Ʊ�ѡ�񣬲�������ͣ�رն���
            //battleSceneManager.selectedCard = null;
            animator?.Play("HoverOffCard");
            // Debug.Log("ȡ��ѡ��ÿ��Ƶķ���");
        }

        // �������ͣ�ڿ�����ʱ�����ķ���
        public void HoverCard()
        {
            // ���û�п��Ʊ�ѡ���򲥷���ͣ�򿪶���
            // if (battleSceneManager.selectedCard == null)
            animator?.Play("HoverOnCard");
            //Debug.Log("�������ͣ�ڿ�����ʱ�����ķ���");
        }

        // �������ק����ʱ�����ķ���
        public void HandleDrag()
        {
            // ��δʵ��
            // Debug.Log("�������ק����ʱ�����ķ���");
        }

        // ����������ק����ʱ�����ķ���
        public void HandleEndDrag(PointerEventData eventData)
        {
            Vector3 mousePosition = Input.mousePosition;

            // ����Ļ����ת��Ϊ��������
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hitInfo;

            // �������ߣ�������Ƿ��������Ϸ����
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
            //    // ���ſ��Ʋ�ִ�йر���ͣ����
            //    FightCardManager.Instance.PlayCard(this.card);
            //    animator?.Play("HoverOffCard");
            //}
            //else
            //{
            //    // ����������Ͳ��ǹ�������ֱ�Ӳ��ſ���
            //    animator?.Play("HoverOffCard");
            //    FightCardManager.Instance.PlayCard(this.card);
            //}
            ////animator?.Play("HoverOnCard");


        }

        // �����¿���ʱ�����ķ���
        public void DropCard()
        {

            // ���û�п��Ʊ�ѡ���򲥷���ͣ�رն���
            //if (battleSceneManager.selectedCard == null)
            animator?.Play("HoverOffCard");
            // Debug.Log("�����¿���ʱ�����ķ���");
        }
        #endregion


        #region ����ui�Ĳ���

        public void PushCardPool()
        {
            //Instantiate(this.discardEffect, this.transform.position, Quaternion.identity);
            PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/CardCell", this.gameObject);
        }

        #endregion
    }
}
