
using UnityEngine;

namespace Frag
{
    [CreateAssetMenu(fileName = "BuffDexterity", menuName = "ScriptableObject/BuffModel/BuffDexterity ")]
    public class BuffDexterity : BuffModel
    {
       //基础回调点

        //创造回调点
        public override void OnCreate(BuffInfo buff) { }

        //移除回调点
        public override void OnRemoved(BuffInfo buff) { }

    }
}