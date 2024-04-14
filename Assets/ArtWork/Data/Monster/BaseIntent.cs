using QFramework;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Frag
{

    [CreateAssetMenu(fileName = "Intent", menuName = "ScriptableObject/BaseIntent")]
    public class BaseIntent:ScriptableObject,ILoot
    {

        public EnemyActionType type;  //��ͼ����

        public int intentAttack;

        public int intentDefense;

        public Fighter creator;

        public Fighter target;

        public BuffInfo buffInfo;//ʩ�ӻ������buff����

        public bool isTarget = true;

        public Sprite icon;     // ��ʾ��EnemyAction��ͼ��

        public int chance;  // ��ʾĳ����ͼ���������ļ��ʡ�  

        public int GetChance()
        {
            return chance; 
        }
    }

}