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
            cardTitleText.text = card.CardTitle;
            cardDescriptionText.text = card.CardDescription;
            cardCostText.text = card.CardCost.ToString();
            cardImage.sprite = card.CardIcon;
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

        // 当鼠标指针离开该UI元素时触发此函数    
        public void OnPointerEnter(PointerEventData eventData)
        {

            animator.Play("HoverOnCard");

            // Tool.Log("当鼠标指针进入该UI元素时触发此函数  ");
            // 在0.25秒内将当前UI元素的缩放比例设置为1.5倍  
            // transform.DOScale(1.5f, 0.25f);

            // 获取当前UI元素在其父元素中的同级兄弟元素的索引  
            // index = transform.GetSiblingIndex();

            // 将当前UI元素设置为其父元素的最后一个子元素  
            // 这意味着它会显示在其父元素的最顶层  
            // transform.SetAsLastSibling();

            // 查找名为"bg"的子元素，并获取其Image组件  
            // 然后设置该Image组件使用的材质的_lineColor属性为黄色  
            // transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);

            // 查找名为"bg"的子元素，并获取其Image组件  
            // 然后设置该Image组件使用的材质的_lineWidth属性为10 
            //transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
        }
        //鼠标离开
        // 当鼠标指针离开该UI元素时触发此函数  
        public void OnPointerExit(PointerEventData eventData)
        {
            animator.Play("HoverOffCard");
            // 在0.25秒内将当前UI元素的缩放比例还原为1（即原始大小）  
            //  Tool.Log("当鼠标指针离开该UI元素时触发此函数  ");
            //  transform.DOScale(1, 0.25f);

            // 将当前UI元素设置为其父元素的指定索引位置  
            // index变量应在此函数外部定义，并保存了之前获取到的兄弟元素索引  
            // transform.SetSiblingIndex(index);

            // 查找名为"bg"的子元素，并获取其Image组件  
            // 然后设置该Image组件使用的材质的_lineColor属性为黑色  
            // transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);

            // 查找名为"bg"的子元素，并获取其Image组件  
            // 然后设置该Image组件使用的材质的_lineWidth属性为1  
            // 注意：这里的代码似乎有个小错误，GetComponent<Image>后面多了个O，应该是GetComponent<Image>()  
            //transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
        }
        Vector2 initPos;

        // 当拖拽操作开始时触发此函数  
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
            // 获取当前UI元素的RectTransform组件，并获取其anchoredPosition属性，保存到initPos变量中  
            // 这个属性表示UI元素相对于其父RectTransform的锚点位置  

            initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        }


        // 当拖拽该UI元素时触发此函数  
        public virtual void OnDrag(PointerEventData eventData)
        {
            // 定义一个Vector2类型的变量pos，用于存储从屏幕坐标转换到RectTransform局部坐标的结果  


            // 使用RectTransformUtility的ScreenPointToLocalPointInRectangle方法将屏幕坐标转换为RectTransform的局部坐标  
            // transform.parent.GetComponent<RectTransform>() 获取当前UI元素父元素的RectTransform组件  
            // eventData.position是当前的屏幕坐标  
            // eventData.pressEventCamera是触发按压事件的相机  
            // out pos 是输出参数，用于接收转换后的局部坐标  
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.parent.GetComponent<RectTransform>(),
                eventData.position,
                eventData.pressEventCamera,
                out initPos))
            {
                // 如果转换成功，将转换后的局部坐标设置为当前UI元素的anchoredPosition  
                // 这样，UI元素就会跟随鼠标移动  
                transform.GetComponent<RectTransform>().anchoredPosition = initPos;
            }
        }


        // 当拖拽操作结束时触发此函数  
        public virtual void OnEndDrag(PointerEventData eventData)
        {
            // 将UI元素的anchoredPosition设置回拖拽开始时的位置（initPos）  
            transform.GetComponent<RectTransform>().anchoredPosition = initPos;


            // 设置UI元素在其父元素中的兄弟索引为index  
            // 这会改变UI元素在其父元素内的绘制顺序和层次结构中的位置  
            // 假设index是一个已经在类作用域中定义的变量，它包含了期望的兄弟索引值  
            //  transform.SetSiblingIndex(index);
        }
        BazierArrows bazierArrows = null;
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            // Cursor.visible = false;

            Tool.Log("OnPointerDown");

            //showUI -line

            ShowBazierArrows();


            //关闭所有协同程序
            StopAllCoroutines();
            //启动鼠标操作协同
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

                    //进行射线检测是否碰到怪物
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
                // 如果需要，可以在这里根据 UI 元素的信息执行相应的操作
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


            //Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition); // 将鼠标屏幕位置转换为世界坐标 


            Vector2 mousePos = Input.mousePosition;

            Vector2 origin = new Vector2(Camera.main.gameObject.transform.position.x, Camera.main.gameObject.transform.position.y); // 射线的起点，通常是当前物体的位置  

            //Vector2 direction = mousePos - origin; // 射线的方向，从起点指向鼠标位置  

            Vector2 direction = mousePos - origin; // 射线的方向，从起点指向鼠标位置  

            Tool.Log($"mousePos:x={mousePos.x},y={mousePos.y}");

            Tool.Log($"origin:x={origin.x},y={origin.y}");


            // 使用Physics2D.Raycast进行射线检测  

            float distance = 10000f; // 射线的最大距离  

            RaycastHit2D hit;

            // 绘制2D线段模拟射线  
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
