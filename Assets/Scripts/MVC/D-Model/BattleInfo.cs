using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{

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
            return;
  
        }


    }
}
