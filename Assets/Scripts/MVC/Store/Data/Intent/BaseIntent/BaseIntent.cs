using QFramework;
using UnityEditor;
using UnityEngine;

namespace Frag
{

    public class BaseIntent : ICanSendCommand { 


        public BaseIntent()
		{ 
		}
        public virtual void Apply() { }

        public IArchitecture GetArchitecture()
        {
            return CounterApp.Interface;
        }

    }



}