using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TJ
{
    /// <summary>
    /// ��ʾ��ͼ�ڵ���࣬�̳���MonoBehaviour
    /// </summary>
    public class Node:MonoBehaviour
    {
        public Image clickedIcon;       // �ڵ㱻���ʱ��ʾ��ͼ��
        public Image availableIcon;     // �ڵ�ɵ��ʱ��ʾ��ͼ��
        public Floor floor;             // �ڵ����ڵĵذ�

        /// <summary>
        /// ���ڵ㱻���ʱ���õķ�����֪ͨ���ڵذ�ڵ㱻���
        /// </summary>
        public void ClickMe()
        {
            floor.ClickedOnMe(this);
        }
    }
}
