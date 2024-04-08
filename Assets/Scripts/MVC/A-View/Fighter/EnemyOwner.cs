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
            //更新防御显示
            healthBar.DisplayBlock(owner.currentBlock);

            //更新血条显示
            healthBar.DisplayHealth(owner.hp.cur, owner.hp.max);
        }

    }



}


