
using UnityEngine;
using QFramework;

namespace Frag
{
    [CreateAssetMenu(fileName = "CradStrike_R", menuName = "ScriptableObject/Card/CradStrike_R ")]
    public class CradStrike_R : BaseCard
    {
       
        public override void Apply(Fighter creator, Fighter target)
        {
            this.SendCommand<DataDamageCommand>(new DataDamageCommand(new DamageInfo(creator, target, this.CardAttack)));
        }

    
    }
}