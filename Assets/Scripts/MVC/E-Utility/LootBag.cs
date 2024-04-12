using UnityEditor;
using System;
using System.Collections.Generic;
using QFramework;

namespace Frag
{
    /// <summary>
    /// ʵ����ս��Ʒ�ӿ����Ҫʹ�����ս��Ʒ��ʽ��Ҫ����ӿڸ���
    /// ������Ҫchance��ʵ��GatChance�ӿڷ���
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
                Tool.Log("ILootList ������");

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
            Tool.Log("û����Ʒ����");
            return null;
        }
    }


}