using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Frag
{
    public class EnemyIntent
    {

        public EnemyActionType type;  //意图类型


        public int intentAttack;

        public int intentDefense;

        public Fighter creator;

        public Fighter target;

        public BuffInfo buffInfo;//施加或给自身buff类型

        public int chance;  // 表示某个意图或动作触发的几率。  

        public bool isTarget = true;

        //   public Sprite icon;     // 显示该EnemyAction的图标。 

    }

}