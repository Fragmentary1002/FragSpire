using UnityEngine;
using static UnityEditor.PlayerSettings;

namespace Frag
{
    public class BezierTwo : MonoBehaviour
    {

        public void SetStartPos(Vector2 pos)
        {
            transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition = pos;
        }

        public void SetEndPos(Vector2 pos)
        {
            transform.GetChild(transform.childCount - 1).GetComponent<RectTransform>().anchoredPosition = pos;

            Bezier2(pos);

        }


        private void Bezier2(Vector2 pos)
        {

            Vector3 startPos = transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;

            Vector3 endPos = pos;

            Vector3 midPos = Vector3.zero;

            midPos.y = (endPos.y - startPos.y) * 0.5f + startPos.y;

            midPos.x = (endPos.x - startPos.x) * 0.5f + startPos.x;


            //计算开始点和终点的方向
            Vector3 dir = (endPos - startPos).normalized;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;//弧度转角度

            //设置 终点角度
            transform.GetChild(transform.childCount - 1).eulerAngles = new Vector3(0, 0, angle);

            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition = GetBeZier(startPos, midPos, endPos, i / (float)transform.childCount);

                if (i != transform.childCount - 1)
                {
                    dir = (transform.GetChild(i + 1).GetComponent<RectTransform>().anchoredPosition - transform.GetChild(i).GetComponent<RectTransform>().anchoredPosition).normalized;

                    angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    transform.GetChild(i).eulerAngles = new Vector3(0, 0, angle);
                }
            }

        }


        /// <summary>
        /// 贝塞尔曲线
        /// </summary>
        public Vector3 GetBeZier(Vector3 startPos, Vector3 midPos, Vector3 endPos, float t)
        {

            //B(t)=(1-t)^2*P0+2*t(1-t)P1+t^2*P2
            return (1.0f - t) * (1.0f * t) * startPos + 2.0f * (1.0f - t) * midPos + t * t * endPos;
        }

        public void CloseUI()
        {
            PoolMgr.GetInstance().PushObj("Prefabs/UI/Cell/LineUI", this.gameObject);
        }
    }
}