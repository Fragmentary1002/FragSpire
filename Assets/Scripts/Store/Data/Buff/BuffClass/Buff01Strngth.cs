
using UnityEngine;

namespace Frag
{
    [CreateAssetMenu(fileName = " Buff01Strngth ", menuName = "ScriptableObject/BuffModel/Buff01Strngth ")]
    public class Buff01Strngth : BuffModel
    {
       //基础回调点

        //创造回调点
        public override void OnCreate(BuffInfo buff) { }

        //移除回调点
        public override void OnRemoved(BuffInfo buff) { }

    }
}