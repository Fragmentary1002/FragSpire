using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag {

    [CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObject/Monster")]
    public class Monster : ScriptableObject
    {
        public string monsterId;

        public MonsterClass monsterClass;   
        public enum MonsterClass { enemy, eliteEnemy }   
        
        public GameObject MonsterClassPrefab;      // ��ɫԤ�������
        
        public int startHealth;                 //��ʼ�������ֵ
    }
}
