using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {

        private static T _instance;

        public static T Instance
        {
            get
            {
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if ( _instance == null )
            {
              //  Debug.Log($"Éú³Éµ¥Àý: {typeof(T)}");
                _instance = this as T;
            }
            else if( _instance != this)
            {
                Destroy( gameObject );
            }
            DontDestroyOnLoad(gameObject);

        }

        public virtual void Init() { }
    }
}
