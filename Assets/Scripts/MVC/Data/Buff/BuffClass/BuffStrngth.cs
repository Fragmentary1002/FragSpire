
using UnityEngine;

namespace Frag
{
    [CreateAssetMenu(fileName = "BuffStrngth", menuName = "ScriptableObject/BuffModel/BuffStrngth ")]
    public class BuffStrngth : BuffModel
    {
       //基础回调点

        //创造回调点
        public override void OnCreate(BuffInfo buff) { }

        //移除回调点
        public override void OnRemoved(BuffInfo buff) { }

    }
}