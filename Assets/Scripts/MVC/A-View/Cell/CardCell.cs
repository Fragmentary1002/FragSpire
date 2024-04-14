using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEditor.Rendering.Universal.ShaderGUI;
using static UnityEditor.PlayerSettings;
using Unity.Burst.CompilerServices;
using static UnityEngine.UI.Image;
using Unity.VisualScripting;


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






        #region ����ui�Ĳ���

        public void PushCardPool()
        {
            //Instantiate(this.discardEffect, this.transform.position, Quaternion.identity);
            PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/CardCell", this.gameObject);
        }


        #endregion

        #region ����¼�

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
        BazierArrows bazierArrows = null;
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
               transform.GetComponent<RectTransform>(),
               eventData.position,
               eventData.pressEventCamera,
               out pos))
                {
                    SetBazierArrows();

                    //�������߼���Ƿ���������
                    //CheckRayToEnemy();
                    testFun(eventData);

                }


                yield return null;


            }
            Cursor.visible = true;


            // closeUI -line
            CloseBazierArrows();
        }
        public LayerMask layerMask;
      
        private void testFun(PointerEventData eventData)
        {

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            foreach (RaycastResult result in results)
            {
                // �����Ҫ��������������� UI Ԫ�ص���Ϣִ����Ӧ�Ĳ���
                Debug.Log("Clicked on UI element: " + result.gameObject.name);

            }

            results.Clear();
        }

        Enemy hitEnemy;
        private void GetEnemy(Transform tran)
        {
            this.hitEnemy = tran.transform.GetComponent<EnemyOwner>().owner;

            Tool.Log("CheckRayToEnemy");


            if (Input.GetMouseButton(0))
            {
                StopAllCoroutines();

                Cursor.visible = true;

                //closeui -line
                CloseBazierArrows();

                if (FightCardManager.Instance.TryPlayCard(this.card, this.hitEnemy))
                {
                    animator?.Play("HoverOffCard");
                    PushCardPool();
                }
                this.hitEnemy = null;
            }

            else
            {
                if (this.hitEnemy != null)
                {
                    this.hitEnemy = null;
                }

            }
        }

        [ObsoleteAttribute]
        private void CheckRayToEnemy()
        {

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //RaycastHit hit;


            //Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition); // �������Ļλ��ת��Ϊ�������� 


            Vector2 mousePos = Input.mousePosition;

            Vector2 origin = new Vector2(Camera.main.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y); // ���ߵ���㣬ͨ���ǵ�ǰ�����λ��  

            //Vector2 direction = mousePos - origin; // ���ߵķ��򣬴����ָ�����λ��  

            Vector2 direction = mousePos - origin; // ���ߵķ��򣬴����ָ�����λ��  

            Tool.Log($"mousePos:x={mousePos.x},y={mousePos.y}");

            Tool.Log($"origin:x={origin.x},y={origin.y}");


            // ʹ��Physics2D.Raycast�������߼��  

            float distance = 10000f; // ���ߵ�������  

            RaycastHit2D hit;

            // ����2D�߶�ģ������  
            Debug.DrawLine(origin, origin + direction * distance, Color.red);

            if (Physics2D.Raycast(origin, direction, distance, LayerMask.GetMask("Enemy")))
            {
                hit = Physics2D.Raycast(origin, direction, distance, LayerMask.GetMask("Enemy"));
                GetEnemy(hit.transform);
            }

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
            //bazierArrows?.CloseUI();
        }

        #endregion
    }
}
