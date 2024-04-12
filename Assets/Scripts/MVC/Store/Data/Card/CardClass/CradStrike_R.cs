
using UnityEngine;
using QFramework;

namespace Frag
{

    public class CradStrike_R : BaseCard
    {
       
        public override void Apply(Fighter creator, Fighter target)
        {
            this.SendCommand<DataAttackCommand>(new DataAttackCommand(new DamageInfo(creator, target, this.CardAttack)));
        }

    
    }
}