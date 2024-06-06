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


        // ѡ��ÿ��Ƶķ���
        public void OnSelectCard()
        {
            // ��֪ս�������������ÿ����ѱ�ѡ��


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
            Tool.Log("OnPointerDown");

            ShowBazierArrows();

            StartCoroutine(DisPlayArrow());
        }

        IEnumerator DisPlayArrow()
        {
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



        private int index;

        // �����ָ������UIԪ��ʱ�����˺���    
        public void OnPointerEnter()
        {

            animator.Play("HoverOnCard");

            // Tool.Log("�����ָ������UIԪ��ʱ�����˺���  ");
            // ��0.25���ڽ���ǰUIԪ�ص����ű�������Ϊ1.5��  
            // transform.DOScale(1.5f, 0.25f);

            // ��ȡ��ǰUIԪ�����丸Ԫ���е�ͬ���ֵ�Ԫ�ص�����  
            // index = transform.GetSiblingIndex();

            // ����ǰUIԪ������Ϊ�丸Ԫ�ص����һ����Ԫ��  
            // ����ζ��������ʾ���丸Ԫ�ص����  
            // transform.SetAsLastSibling();

            // ������Ϊ"bg"����Ԫ�أ�����ȡ��Image���  
            // Ȼ�����ø�Image���ʹ�õĲ��ʵ�_lineColor����Ϊ��ɫ  
            // transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);

            // ������Ϊ"bg"����Ԫ�أ�����ȡ��Image���  
            // Ȼ�����ø�Image���ʹ�õĲ��ʵ�_lineWidth����Ϊ10 
            //transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
        }
        //����뿪
        // �����ָ���뿪��UIԪ��ʱ�����˺���  
        public void OnPointerExit()
        {
            animator.Play("HoverOffCard");
            // ��0.25���ڽ���ǰUIԪ�ص����ű�����ԭΪ1����ԭʼ��С��  
            //  Tool.Log("�����ָ���뿪��UIԪ��ʱ�����˺���  ");
            //  transform.DOScale(1, 0.25f);

            // ����ǰUIԪ������Ϊ�丸Ԫ�ص�ָ������λ��  
            // index����Ӧ�ڴ˺����ⲿ���壬��������֮ǰ��ȡ�����ֵ�Ԫ������  
            // transform.SetSiblingIndex(index);

            // ������Ϊ"bg"����Ԫ�أ�����ȡ��Image���  
            // Ȼ�����ø�Image���ʹ�õĲ��ʵ�_lineColor����Ϊ��ɫ  
            // transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);

            // ������Ϊ"bg"����Ԫ�أ�����ȡ��Image���  
            // Ȼ�����ø�Image���ʹ�õĲ��ʵ�_lineWidth����Ϊ1  
            // ע�⣺����Ĵ����ƺ��и�С����GetComponent<Image>������˸�O��Ӧ����GetComponent<Image>()  
            //transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
        }
        Vector2 initPos;

        // ����ק������ʼʱ�����˺���  
        public virtual void OnBeginDrag()
        {
            // ��ȡ��ǰUIԪ�ص�RectTransform���������ȡ��anchoredPosition���ԣ����浽initPos������  
            // ������Ա�ʾUIԪ��������丸RectTransform��ê��λ��  

            initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        }


        // ����ק��UIԪ��ʱ�����˺���  
        public virtual void OnDrag()
        {
            // ����һ��Vector2���͵ı���pos�����ڴ洢����Ļ����ת����RectTransform�ֲ�����Ľ��  


            // ���ת���ɹ�����ת����ľֲ���������Ϊ��ǰUIԪ�ص�anchoredPosition  
            // ������UIԪ�ؾͻ��������ƶ�  
            transform.GetComponent<RectTransform>().anchoredPosition = initPos;

        }


        // ����ק��������ʱ�����˺���  
        public virtual void OnEndDrag()
        {
            // ��UIԪ�ص�anchoredPosition���û���ק��ʼʱ��λ�ã�initPos��  
            transform.GetComponent<RectTransform>().anchoredPosition = initPos;


            // ����UIԪ�����丸Ԫ���е��ֵ�����Ϊindex  
            // ���ı�UIԪ�����丸Ԫ���ڵĻ���˳��Ͳ�νṹ�е�λ��  
            // ����index��һ���Ѿ������������ж���ı��������������������ֵ�����ֵ  
            //  transform.SetSiblingIndex(index);
        }

        public virtual void OnPointerDown()
        {
            // Cursor.visible = false;

            Tool.Log("OnPointerDown");

            ShowBazierArrows();


        }

        //public LayerMask layerMask;
        //List<RaycastResult> results = new List<RaycastResult>();
        //[Obsolete]
        //private void testFun(PointerEventData eventData)
        //{


        //    EventSystem.current.RaycastAll(eventData, results);

        //    foreach (RaycastResult result in results)
        //    {
        //        // �����Ҫ��������������� UI Ԫ�ص���Ϣִ����Ӧ�Ĳ���
        //        //Debug.Log("Clicked on UI element: " + result.gameObject.name);
        //        //Debug.Log($"name={result.gameObject.name},layer={result.gameObject.layer}");
        //        //Debug.Log(LayerMask.GetMask("Enemy"));
        //        if (result.gameObject.layer == 7)
        //        {
        //            // ApplyCard(result.gameObject);
        //        }

        //    }

        //    results.Clear();
        //}




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
