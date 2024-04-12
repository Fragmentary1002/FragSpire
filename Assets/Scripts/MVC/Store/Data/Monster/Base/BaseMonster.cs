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
        
        public GameObject MonsterClassPrefab;      // 角色预制体对象
        
        public int startHealth;                 //初始最大生命值

        public List<BaseIntent> IntentList;
        
         
    }
}
