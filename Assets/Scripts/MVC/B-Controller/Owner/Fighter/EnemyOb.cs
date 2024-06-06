using QFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    // [Serializable]
    [Obsolete]
    public class EnemyOb : Fighter
    {


        public BaseMonster monster;
        public List<BaseIntent> intents;
        protected  void OnInit()
        {
            //this.monster = GameApp.Instance.monster ?? (new BaseMonster());
            //hp.max = monster.startHealth;
            //hp.cur = monster.startHealth;
            //intents = monster.IntentList;

        }

        //public Enemy(BaseMonster monster)
        //{
        //    this.monster = monster;
        //    this.intents = monster.IntentList;
        //}    
    }

}