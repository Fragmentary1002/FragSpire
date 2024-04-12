using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{

    public class Enemy : Fighter
    {


        public BaseMonster monster;
        protected override void OnInit()
        {
            this.monster = GameApp.Instance.monster ?? (new BaseMonster());
            hp.max = monster.startHealth;
            hp.cur = monster.startHealth;

        }

    }

}