using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEditor.Rendering.Universal.ShaderGUI;
using static UnityEditor.PlayerSettings;


namespace Frag
{
    public class CardCell : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
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






        //// ѡ��ÿ��Ƶķ��� point down
        //public void SelectCard()
        //{
        //    // ��֪ս�������������ÿ����ѱ�ѡ��
        //    //battleSceneManager.selectedCard = this;
        //    // Debug.Log("ѡ��ÿ��Ƶķ���");
        //}

        //// ȡ��ѡ��ÿ��Ƶķ��� pointer up
        //public void DeselectCard()
        //{
        //    // ��֪ս������������û�п��Ʊ�ѡ�񣬲�������ͣ�رն���
        //    //battleSceneManager.selectedCard = null;
        //    animator?.Play("HoverOffCard");
        //    // Debug.Log("ȡ��ѡ��ÿ��Ƶķ���");
        //}

        //// �������ͣ�ڿ�����ʱ�����ķ��� pointer enter
        //public void HoverCard()
        //{
        //    // ���û�п��Ʊ�ѡ���򲥷���ͣ�򿪶���
        //    // if (battleSceneManager.selectedCard == null)
        //    animator?.Play("HoverOnCard");
        //    //Debug.Log("�������ͣ�ڿ�����ʱ�����ķ���");
        //}

        //// �������ק����ʱ�����ķ��� begin drag
        //public void HandleDrag()
        //{
        //    // ��δʵ��
        //    // Debug.Log("�������ק����ʱ�����ķ���");
        //}

        //// ����������ק����ʱ�����ķ��� end drag
        //public void HandleEndDrag()
        //{

        //    Tool.Log("����������ק����ʱ�����ķ���");


        //    animator?.Play("HoverOffCard");
        //    if (FightCardManager.Instance.PlayCardAndIsSuccess(this.card))
        //    {
        //        PushCardPool();
        //    }



        //    //Vector3 mousePosition = Input.mousePosition;

        //    //// ����Ļ����ת��Ϊ��������
        //    ////Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        //    //Ray ray = Camera.main.ViewportPointToRay(mousePosition);
        //    //RaycastHit hitInfo;

        //    //// �������ߣ�������Ƿ��������Ϸ����
        //    //if (Physics.Raycast(ray, out hitInfo))
        //    //{
        //    //    GameObject clickedObject = hitInfo.collider.gameObject;

        //    //    Tool.Log("�������ߣ�������Ƿ��������Ϸ����");
        //    //    if (FightCardManager.Instance.PlayCardAndIsSuccess(this.card, clickedObject))
        //    //    {
        //    //        animator?.Play("HoverOffCard");
        //    //        PushCardPool();
        //    //    }


        //    //}

        //    //return;


        //    //if (card.Type == CardType.Attack)
        //    //{
        //    //    // ���ſ��Ʋ�ִ�йر���ͣ����
        //    //    FightCardManager.Instance.PlayCard(this.card);
        //    //    animator?.Play("HoverOffCard");
        //    //}
        //    //else
        //    //{
        //    //    // ����������Ͳ��ǹ�������ֱ�Ӳ��ſ���
        //    //    animator?.Play("HoverOffCard");
        //    //    FightCardManager.Instance.PlayCard(this.card);
        //    //}
        //    ////animator?.Play("HoverOnCard");


        //}

        //// �����¿���ʱ�����ķ��� point exit
        //public void DropCard()
        //{

        //    // ���û�п��Ʊ�ѡ���򲥷���ͣ�رն���
        //    //if (battleSceneManager.selectedCard == null)
        //    animator?.Play("HoverOffCard");
        //    // Debug.Log("�����¿���ʱ�����ķ���");
        //}
        #endregion



        #region ����ui�Ĳ���

        public void PushCardPool()
        {
            //Instantiate(this.discardEffect, this.transform.position, Quaternion.identity);
            PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/CardCell", this.gameObject);
        }


        #endregion


        private int index;

        // �����ָ���뿪��UIԪ��ʱ�����˺���    
        public void OnPointerEnter(PointerEventData eventData)
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
        public void OnPointerExit(PointerEventData eventData)
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
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            // ��ȡ��ǰUIԪ�ص�RectTransform���������ȡ��anchoredPosition���ԣ����浽initPos������  
            // ������Ա�ʾUIԪ��������丸RectTransform��ê��λ��  

            initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        }


        // ����ק��UIԪ��ʱ�����˺���  
        public virtual void OnDrag(PointerEventData eventData)
        {
            // ����һ��Vector2���͵ı���pos�����ڴ洢����Ļ����ת����RectTransform�ֲ�����Ľ��  


            // ʹ��RectTransformUtility��ScreenPointToLocalPointInRectangle��������Ļ����ת��ΪRectTransform�ľֲ�����  
            // transform.parent.GetComponent<RectTransform>() ��ȡ��ǰUIԪ�ظ�Ԫ�ص�RectTransform���  
            // eventData.position�ǵ�ǰ����Ļ����  
            // eventData.pressEventCamera�Ǵ�����ѹ�¼������  
            // out pos ��������������ڽ���ת����ľֲ�����  
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out initPos))
            {
                // ���ת���ɹ�����ת����ľֲ���������Ϊ��ǰUIԪ�ص�anchoredPosition  
                // ������UIԪ�ؾͻ��������ƶ�  
                transform.GetComponent<RectTransform>().anchoredPosition = initPos;
            }
        }


        // ����ק��������ʱ�����˺���  
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            // ��UIԪ�ص�anchoredPosition���û���ק��ʼʱ��λ�ã�initPos��  
            transform.GetComponent<RectTransform>().anchoredPosition = initPos;


            // ����UIԪ�����丸Ԫ���е��ֵ�����Ϊindex  
            // ���ı�UIԪ�����丸Ԫ���ڵĻ���˳��Ͳ�νṹ�е�λ��  
            // ����index��һ���Ѿ������������ж���ı��������������������ֵ�����ֵ  
            //  transform.SetSiblingIndex(index);
        }
        BazierArrows bazierArrows=null;
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            // Cursor.visible = false;

            Tool.Log("OnPointerDown");

            //showUI -line

            ShowBazierArrows();

          
            //�ر�����Эͬ����
            StopAllCoroutines();
            //����������Эͬ
            StartCoroutine(OnMouseDownRight(eventData));
        }

        IEnumerator OnMouseDownRight(PointerEventData eventData)
        {
            while (true)
            {
                //  Tool.Log("OnMouseDownRight");
                if (Input.GetMouseButtonDown(1))
                {

                    break;
                }

                Vector2 pos;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
               transform.parent.GetComponent<RectTransform>(),
               eventData.position,
               eventData.pressEventCamera,
               out pos))
                {
                    SetBazierArrows(pos);

                    //�������߼���Ƿ���������
                    CheckRayToEnemy();

                }


                yield return null;


            }
            Cursor.visible = true;


            // closeUI -line
            CloseLineUI();
        }

        Enemy hitEnemy;
        private void CheckRayToEnemy()
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * 10000, Color.red);

            if (Physics.Raycast(ray, out hit, 10000, LayerMask.GetMask("Enemy")))
            {
                this.hitEnemy = hit.transform.GetComponent<Enemy>();

                Tool.Log("CheckRayToEnemy");


                if (Input.GetMouseButton(0))
                {
                    StopAllCoroutines();

                    Cursor.visible = true;

                    //closeui -line
                    CloseLineUI();

                    if (FightCardManager.Instance.TryPlayCard(this.card, this.hitEnemy))
                    {
                        animator?.Play("HoverOffCard");
                        PushCardPool();
                    }
                    this.hitEnemy = null;
                }
            }
            else
            {
                if (this.hitEnemy != null)
                {
                    this.hitEnemy = null;
                }
            }

        }

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

        private void SetBazierArrows(Vector2 pos)
        {
            this.bazierArrows?.SetEndPos(pos);
        }


        private void CloseLineUI()
        {
            bazierArrows.CloseUI();
        }


    }
}
