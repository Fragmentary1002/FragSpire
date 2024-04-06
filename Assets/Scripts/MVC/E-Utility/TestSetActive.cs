using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    public class TestSetActive : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Awake()
        {
            this.gameObject.SetActive(false);
        }
    }

}