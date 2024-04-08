
using UnityEngine;
using QFramework;

namespace Frag
{

    public class CardBash : BaseCard
    {

        public override void Apply(Fighter creator, Fighter target)
        {
            this.SendCommand<HurtCommand>(new HurtCommand(new DamageInfo(creator, target, this.CardAttack)));

            this.SendCommand<BuffCommand>(new BuffCommand(target,this.CardBuff));
        }

    }
}