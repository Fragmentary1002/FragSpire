using UnityEditor;
using UnityEngine;

namespace Frag
{

    public class EnemyOwner : MonoBehaviour
    {
        public Enemy owner;

        public FighterHealthBarCell healthBar;



        public void OnUpdate()
        {
            //���·�����ʾ
            healthBar.DisplayBlock(owner.currentBlock);

            //����Ѫ����ʾ
            healthBar.DisplayHealth(owner.hp.cur, owner.hp.max);
        }

    }



}


