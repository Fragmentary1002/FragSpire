
using UnityEngine;
using QFramework;

namespace Frag
{

    [CreateAssetMenu(fileName = "CradDefend_R", menuName = "ScriptableObject/Card/CradDefend_R ")]
    public class CardDefend_R : BaseCard
    {
       
        public override void Apply(Fighter creator, Fighter target)
        {
            this.SendCommand<DataDefenseCommand>(new DataDefenseCommand(creator, this.CardDefense));
        }
    
    }
}