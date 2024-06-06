using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frag
{
    public class MapNode
    {
        public Image clickedIcon;       // 节点被点击时显示的图标
        public Image availableIcon;     // 节点可点击时显示的图标
        //public Floor floor;             // 节点所在的地板

        /// <summary>
        /// 当节点被点击时调用的方法，通知所在地板节点被点击
        /// </summary>
        public void ClickMe()
        {
            //floor.ClickedOnMe(this);
        }
    }
}