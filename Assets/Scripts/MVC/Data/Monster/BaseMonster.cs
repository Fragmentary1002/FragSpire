using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag {

    [CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObject/Monster")]
    public class BaseMonster : ScriptableObject
    {
        public int monsterId;

        public string monsterName;

        public MonsterClass monsterClass;   
        public enum MonsterClass { enemy, eliteEnemy }   
        
        public GameObject MonsterClassPrefab;      // ��ɫԤ�������
        
        public int startHealth;                 //��ʼ�������ֵ

        public List<BaseIntent> IntentList;
        
         
    }
}
