
using UnityEngine;
using QFramework;

namespace Frag
{
    [CreateAssetMenu(fileName = "CardBash", menuName = "ScriptableObject/Card/CardBash ")]
    public class CardBash : BaseCard
    {

        public override void Apply(Fighter creator, Fighter target)
        {
            this.SendCommand<DataDamageCommand>(new DataDamageCommand(new DamageInfo(creator, target, this.CardAttack)));

            this.SendCommand<DataBuffCommand>(new DataBuffCommand(target,this.CardBuff));
        }

    }
}