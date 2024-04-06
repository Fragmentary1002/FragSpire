using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{

    public class BattleInfo : AbstractModel
    {

        //model
        public List<BaseCard> deck = new List<BaseCard>();                         //¿¨×é 

        public List<BaseCard> drawPile = new List<BaseCard>();                     // ³é¿¨¶Ñ  


        public List<BaseCard> cardsInHand = new List<BaseCard>();                  // ÊÖÅÆ  


        public List<BaseCard> discardPile = new List<BaseCard>();                  // ÆúÅÆ¶Ñ  

        public List<BaseCard> cemeteryPile = new List<BaseCard>();                 //Ä¹µØ¶Ñ

        public Enegry enegry = new Enegry();

        protected override void OnInit()
        {
            return;
  
        }


    }
}
