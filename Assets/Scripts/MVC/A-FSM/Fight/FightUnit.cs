using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    public class FightUnit : IController

    {

        public virtual void Init() { }

        public virtual void OnUpdate() { }

        public virtual void OnDestroy() { }

        //ָ���ܹ�
        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }
    }
}