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
        public List<BaseCard> deck = new List<BaseCard>();                         //���� 

        public List<BaseCard> drawPile = new List<BaseCard>();                     // �鿨��  


        public List<BaseCard> cardsInHand = new List<BaseCard>();                  // ����  


        public List<BaseCard> discardPile = new List<BaseCard>();                  // ���ƶ�  

        public List<BaseCard> cemeteryPile = new List<BaseCard>();                 //Ĺ�ض�

        public Enegry enegry = new Enegry();


        protected override void OnInit()
        {
            EventCenter.GetInstance().AddEventListener("FightUpdate", UpdateData);

            return;

        }
        /// <summary>
        /// ����
        /// </summary>
        public void UpdateData()
        {
            UpdateInfo();
        }


        /// <summary>
        /// ֪ͨview�����
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
