using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Frag
{

    public enum EnemyActionType
    {
        // 攻击意图  
        Attack,
        // 阻挡意图  
        Block,
        Buff,
        doubleHit

    }
    /// <summary>
    ///
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyAction", menuName = "ScriptableObject/EnemyAction")]
    public class EnemyAction : ScriptableObject
    {

        public EnemyActionType type;  //意图类型

        public int amount;      //攻击防御数值

        public bool isGetBuff;

        public int buffAmount;  //施加或给自身buff数值

        public BuffInfo Buff;//施加或给自身buff类型

        public int chance;  // 表示某个意图或动作触发的几率。  

        public Sprite icon;     // 显示该EnemyAction的图标。 


        /// <summary>
        /// 检查加伤 易伤和力量
        /// </summary>
        //public void Damage()
        //{
        //    if (type == EnemyActionType.Attack)
        //    {
        //        // 攻击值增加敌人的力量增益效果  
        //        amount += amount;
        //        if (true)
        //        {
        //            // 如果玩家有易伤效果，则攻击值增加50%  
        //            if (true)
        //            {
        //                amount = (int)(amount * 1.5f);
        //            }
        //        }
        //    }
        //}
    }

}