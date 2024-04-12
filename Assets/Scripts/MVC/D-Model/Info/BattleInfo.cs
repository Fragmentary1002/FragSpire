using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{

    [SerializeField]
    public class BattleInfo : AbstractModel
    {

        //model
        public List<BaseCard> deck = new List<BaseCard>();                         //卡组 

        public List<BaseCard> drawPile = new List<BaseCard>();                     // 抽卡堆  


        public List<BaseCard> cardsInHand = new List<BaseCard>();                  // 手牌  


        public List<BaseCard> discardPile = new List<BaseCard>();                  // 弃牌堆  

        public List<BaseCard> cemeteryPile = new List<BaseCard>();                 //墓地堆

        public Enegry enegry = new Enegry();


        protected override void OnInit()
        {
            EventCenter.GetInstance().AddEventListener("FightUpdate", UpdateData);

            return;

        }
        /// <summary>
        /// 更新
        /// </summary>
        public void UpdateData()
        {
            UpdateInfo();
        }


        /// <summary>
        /// 通知view层更新
        /// </summary>
        private void UpdateInfo()
        {
            EventCenter.GetInstance().EventTrigger("Battle");
        }

        public void OnDestroy()
        {
            EventCenter.GetInstance().RemoveEventListener("Battle", UpdateData);
        }


    }
}
