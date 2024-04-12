
using UnityEngine;
using QFramework;

namespace Frag
{

    public class CardBash : BaseCard
    {

        public override void Apply(Fighter creator, Fighter target)
        {
            this.SendCommand<DataAttackCommand>(new DataAttackCommand(new DamageInfo(creator, target, this.CardAttack)));

            this.SendCommand<DataBuffCommand>(new DataBuffCommand(target,this.CardBuff));
        }

    }
}