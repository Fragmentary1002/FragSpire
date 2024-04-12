using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{

    public class DataDefenseCommand : AbstractCommand
    {
        int amount;
        Fighter target;
        public DataDefenseCommand(Fighter target, int amount)
        {
            this.amount = amount;
            this.target = target;   
        }
        protected override void OnExecute()
        {
            target.DoAddBlock(amount);
        }
    }

}

