using UnityEditor;
using UnityEngine;

namespace Frag
{

    public class PlayerOwner : MonoBehaviour
    {
       public Player owner;

        public FighterHealthBarCell healthBar;

        public Transform buffParent;


        private void Start()
        {
            
        }

        public void OnUpdate()
        {
            //���·�����ʾ
            healthBar.DisplayBlock(owner.currentBlock);

            //����Ѫ����ʾ
            healthBar.DisplayHealth(owner.hp.cur, owner.hp.max);
        }

    }



}


