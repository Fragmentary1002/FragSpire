using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    /// <summary>
    /// 表示遗物的类，可作为ScriptableObject存在
    /// </summary>
    [CreateAssetMenu]
    public class RelicTj : ScriptableObject
    {
        public string relicName;           // 遗物名称
        public string relicDescription;    // 遗物描述
        public Sprite relicIcon;           // 遗物图标
    }
}
