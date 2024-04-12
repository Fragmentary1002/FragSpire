using UnityEditor;
using System;
using System.Collections.Generic;
using QFramework;

namespace Frag
{
    /// <summary>
    /// 实现了战利品接口如果要使用随机战利品方式需要这个接口辅助
    /// 并且需要chance来实现GatChance接口方法
    /// </summary>
    public interface ILoot
    {
        public abstract int GetChance();
    }


    public class LootBag : IUtility
    {

        //private List<ILoot> lootList = new List<ILoot>();

       // public List<ILoot> LootList { set { value=lootList; } }

        public ILoot GetDroppedItem(List<ILoot> lootList)
        {

            if (lootList.Count == 0)
            {
                Tool.Log("ILootList 不存在");

                return null;
            }

            Random random = new Random();

            int randomNum = random.Next(1, 101); //1-100

            List<ILoot> possibleItems = new List<ILoot>();

            foreach (ILoot item in lootList)
            {

                if (randomNum < item.GetChance())
                {
                    possibleItems.Add(item);
                }

            }

            if (possibleItems.Count > 0)
            {

                ILoot droppable = possibleItems[random.Next(0, possibleItems.Count)];

                return droppable;

            }
            Tool.Log("没有物品掉落");
            return null;
        }
    }


}