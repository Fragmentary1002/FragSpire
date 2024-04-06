using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frag
{

    [CreateAssetMenu(fileName = "BaseBuff", menuName = "ScriptableObject/BaseBuff/BaseBuff ")]
    public class BaseBuff : ScriptableObject
    {
        protected Fighter owner;
        
        public Fighter Owner
        {
            set { owner = value; }
        }


        #region 字段实现

        [Header("用于实现")]
        [InspectorName("最大等级")]
        [Tooltip("buff的最大等级（堆叠层数）")]
        public int maxLevel = 1;

        [InspectorName("是永久性")]
        [Tooltip("buff是不是永久性的，如果是则忽略持续时间")]
        public bool isPermanent = true;


        [InspectorName("到时降级量")]
        [Tooltip("当buff持续时间到0时要降低的等级，如果设置为0则代表降至0级")]
        public int demotion = 1;


        [InspectorName("自动刷新")]
        [Tooltip("当buff持续等级到最大等级后又提升等级是否自动刷新持续时间")]
        public bool autoRefresh = true;

        [InspectorName("冲突处理")]
        [Tooltip("当两个不同来源者向同一个单位施加同一个buff时的冲突处理\r\n分为四种")]
        public ConflictResolution conflictResolution;
        /// <summary>
        /// 当两个不同来源者向同一个单位施加同一个buff时的冲突处理
        /// </summary>
        public enum ConflictResolution
        {
            combine,//合并成一个
            append,//添加等级
            cover,//独立
            independent//覆盖

        }

        [InspectorName("buff应用时间")]
        [Tooltip("对于buff应用时间的处理用来分类")]
        public ApplyTime applyTime;


        [Header("用于分类处理")]
        [InspectorName("buff种类")]
        [Tooltip("此属性用于对buff进行分类处理")]
        public BuffType buffType;
        public enum BuffType           // Buff类型枚举
        {
            strength,       // 力量
            Agility,        // 敏捷
            vulnerable,     // 弱点
            weak,           // 虚弱
            ritual,         // 仪式
            enrage          // 激怒
        }

        [InspectorName("强度")]
        [Tooltip("此属性用于对buff进行分类处理")]
        public int intensity = 0;

        [InspectorName("tag")]
        [Tooltip("此属性用于对buff进行分类处理")]
        public string[] tags = null;

        [Header("UI")]
        public string buffName = null;

        public Sprite buffIcon;     // Buff图标

        [Range(0, 999)]
        public int buffValue;       // Buff值

        // public BuffCell buffGO;       // Buff UI对象
        #endregion

        #region 函数
        /// <summary>
        /// 被添加后,一般是添加事件
        /// </summary>
        public virtual void AfterBeAdded()
        {
            // 实现添加后的逻辑
            Tool.Log("实现添加后的逻辑 BaseBuff");
            EventCenter.GetInstance().AddEventListener(this.applyTime.ToString(), Apply);
            return;
        }
        /// <summary>
        /// 更新
        /// </summary>
        public virtual void OnUpdate()
        {
            // 实现更新的逻辑
        }
        /// <summary>
        /// 被移除后,一般是移除事件
        /// </summary>
        public virtual void AfterBeRemoved()
        {
            // 实现移除后的逻辑
            EventCenter.GetInstance().RemoveEventListener(this.applyTime.ToString(), Apply);
        }

        /// <summary>
        /// 实现
        /// </summary>
        public virtual void Apply()
        {
            // 实现应用的逻辑
        }

        #endregion

    }
}