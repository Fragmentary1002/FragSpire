using QFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



namespace Frag
{


    public class EnemyIntentSystem : AbstractSystem
    {
        //public List<Enemy> enemies = new List<Enemy>();

        //public Enemy enemy;

        public LootBag lootBag;

        public BaseIntent newEnemyIntent;

     

        public EnemyIntentCell cell;

        protected override void OnInit()
        {
            //  throw new System.NotImplementedException(); 
            lootBag = this.GetUtility<LootBag>();

        }

        public void StartTurn(Enemy enemy)
        {

            List<ILoot> loots = new List<ILoot>();
            //List<ILoot> loots = intents;

            foreach (ILoot elem in enemy.intents)
            {
                loots.Add(elem);
                // Tool.Log(elem.ToString());
            }

            //强制类型转换 LINQ
            //list = enemy?.intents.Cast<ILoot>().ToList();

            newEnemyIntent  = lootBag.GetDroppedItem(loots) as BaseIntent;

            Tool.Log(newEnemyIntent.name);
            //try
            //{
            //    List<ILoot> list = new List<ILoot>();

            //    //强制类型转换 LINQ
            //    list = enemy.intents.Cast<ILoot>().ToList();

            //    ILoot loot = lootBag.GetDroppedItem(list);

            //    newEnemyIntent = loot as BaseIntent;

            //}
            //catch
            //{
            //    Tool.Log("CreateIntent 生成失效", LogLevel.Error);
            //}

        }

        public void EndTrun()
        {
            ChangeType(newEnemyIntent);
        }

        private void ChangeType(BaseIntent enemyIntent)
        {

            //EnemyAction neweNemyAction = enemyActionList[trunCnt];
            //DisplayIntent(neweNemyAction);
            Debug.Log($"EnemyActionType ChangeType");
            newEnemyIntent = enemyIntent;

  

        }

       








    }

}


