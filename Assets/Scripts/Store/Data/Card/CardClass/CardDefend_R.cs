
using UnityEngine;
using QFramework;

namespace Frag
{

    public class CradDefend_R : BaseCard
    {
       
        public override void Apply(Fighter creator, Fighter target)
        {
            this.SendCommand<DefenseCommand>(new DefenseCommand(creator, this.CardDefense));
        }
    
    }
}