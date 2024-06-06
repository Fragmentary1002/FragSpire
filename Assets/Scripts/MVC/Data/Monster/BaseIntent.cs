using QFramework;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Frag
{
    public enum EnemyActionType
    {
        None,
        // ������ͼ  
        Attack,
        // �赲��ͼ  
        Defense,
        AttackDefense,
        Buff,
        Buff_Attack,
        Buff_Defense,
        Debuff,
        Debuff_Attack,
        Debuff_Defense
    }

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

        [Range(0, 100)]
        public int chance;  // ��ʾĳ����ͼ���������ļ��ʡ�  

        public int GetChance()
        {
            return chance; 
        }
    }

}