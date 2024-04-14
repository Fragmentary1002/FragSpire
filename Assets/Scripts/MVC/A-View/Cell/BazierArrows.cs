using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using System;

namespace Frag
{
    public class BazierArrows : MonoBehaviour
    {
        #region public Fields
        public GameObject ArrowHeadPrefabs;

        public GameObject ArrowNodePrefab;

        public int arrowNodeNum;

        public float scaleFactor = 1f;

        #endregion

        #region private Fields



        private List<GameObject> arrowNodes = new List<GameObject>();

        //P0-P3
        private List<Vector2> controlPoints = new List<Vector2>();

        //决定控制点P1、P2位置的向量因子
        private readonly List<Vector2> controlPointFactors = new List<Vector2> {
            new Vector2(-0.3f,0.8f),   //P1=P0+(P3-P0)*vector2(-0.3,0.8)
            new Vector2(0.1f, 1.4f)      //P2=P0+(P3-P0)*vector2(0.1,1.4)
        };
        #endregion

        #region Public Methods

        public void SetStartPos()
        {


            for (int i = 0; i < this.arrowNodeNum; i++)
            {
                PoolMgr.GetInstance().GetObj("Prefabs/UI/Cell/ArrowBody", (go) =>
                {
                    if (go != null)
                    {
                        go.transform.SetParent(this.gameObject.transform);


                        this.arrowNodes.Add(go);
                    }


                });
            }

            PoolMgr.GetInstance().GetObj("Prefabs/UI/Cell/ArrowHead", (go) =>
            {
                if (go != null)
                {

                    go.transform.SetParent(this.gameObject.transform);

                    this.arrowNodes.Add(go);
                }
            });

            //this.arrowNodes.ForEach(node => { node.GetComponent<RectTransform>().position = new Vector2(-1000, -1000); });

            for (int i = 0; i < 4; i++)
            {
                this.controlPoints.Add(Vector2.zero);
            }
        }

        public void SetEndPos()
        {
            // transform.GetChild(transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition = pos;


            OnUpdate();

        }

        public void CloseUI()
        {
            PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/Arrow", this.gameObject);

            for (int i = 0; i < this.arrowNodeNum; i++)
            {
                PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/ArrowBody", this.arrowNodes[i]);
            }

            PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/ArrowHead", this.arrowNodes[arrowNodes.Count - 1]);

            arrowNodes.Clear();
        }

        #endregion


        #region Private Methods

        public void OnUpdate()
        {
            this.controlPoints[0] = new Vector2(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            this.controlPoints[3] = new Vector2(Input.mousePosition.x, Input.mousePosition.y);



            //P1=P0+(P3-P0)*vector2(-0.3,0.8)
            this.controlPoints[1] = this.controlPoints[0] + (this.controlPoints[3] - this.controlPoints[0]) * this.controlPointFactors[0];
            //P2=P0+(P3-P0)*vector2(0.1,1.4)
            this.controlPoints[2] = this.controlPoints[0] + (this.controlPoints[3] - this.controlPoints[0]) * this.controlPointFactors[1];

            for (int i = 0; i < this.arrowNodes.Count; i++)
            {
                var t = Mathf.Log(1f * i / (this.arrowNodes.Count - 1) + 1f, 2f);


                // B(t) = (1 - t) ^ 3 * P0 + 3 * (1 - t) ^ 2 * t * P1 + 3 * (1 - t) * t ^ 2 * P2 + t ^ 3 * P3
                this.arrowNodes[i].transform.position =
                    Mathf.Pow(1 - t, 3) * this.controlPoints[0] +
                    3 * Mathf.Pow(1 - t, 2) * t * this.controlPoints[1] +
                    3 * (1 - t) * Mathf.Pow(t, 2) * this.controlPoints[2] +
                    Mathf.Pow(t, 3) * this.controlPoints[3];

                if (i > 0)
                {
                    //除了第一个节点以外,我们让每个节点的方向等于上一个节点到这个节点的向量方向。
                    var euler = new Vector3(0, 0, Vector2.SignedAngle(Vector2.up, this.arrowNodes[i].transform.position - this.arrowNodes[i - 1].transform.position));

                    this.arrowNodes[i].transform.rotation = Quaternion.Euler(euler);

                }
                //对于节点的尺寸,我们让每个节点从前向后逐渐增大。
                var scale = this.scaleFactor * (1f - 0.03f * (this.arrowNodes.Count - 1 - i));
                this.arrowNodes[i].transform.localScale = new Vector3(scale, scale, 1f);
            }

            //对于第一个节点,我们让它的方向和第二个节点的方向一致即可。
            this.arrowNodes[0].transform.rotation = this.arrowNodes[1].transform.rotation;

        }


        #endregion
    }

}