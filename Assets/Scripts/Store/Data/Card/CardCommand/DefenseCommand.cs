using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frag
{

    public class DefenseCommand : AbstractCommand
    {
        int amount;
        Fighter target;
        public DefenseCommand(Fighter target, int amount)
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

