using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{
    //ս��ʧ��
    public class Fight_Loss : FightUnit

    {
        public override void Init()
        {
            base.Init();

            //// ���ս��û��ʤ����winΪfalse�����򼤻���Ϸ������UI���  
            //if (!win)
            //    gameover.SetActive(true);

        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

      
    }
}