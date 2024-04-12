using Frag;
using QFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAttackCommand : AbstractCommand
{

    DamageInfo damageInfo = null;

    public DataAttackCommand(DamageInfo damageInfo)  
    {
        this.damageInfo = damageInfo;
    }
    
    protected override void OnExecute()
    {
        this.SendCommand<DamageCommand>(new DamageCommand(damageInfo));
    }
}
