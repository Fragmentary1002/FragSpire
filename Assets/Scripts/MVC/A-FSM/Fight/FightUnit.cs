using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    public class FightUnit : IController
    {
        protected EnemyManager enemyManager;
        public virtual void Init()
        {
            enemyManager = this.GetSystem<EnemyManager>();
        }

        public virtual void OnUpdate() { }

        public virtual void OnDestroy() { }

        //Ö¸¶¨¼Ü¹¹
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}