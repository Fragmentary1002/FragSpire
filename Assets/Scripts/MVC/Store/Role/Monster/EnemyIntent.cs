using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Frag
{
    public class EnemyIntent
    {

        public EnemyActionType type;  //��ͼ����


        public int intentAttack;

        public int intentDefense;

        public Fighter creator;

        public Fighter target;

        public BuffInfo buffInfo;//ʩ�ӻ������buff����

        public int chance;  // ��ʾĳ����ͼ���������ļ��ʡ�  

        public bool isTarget = true;

        //   public Sprite icon;     // ��ʾ��EnemyAction��ͼ�ꡣ 

    }

}