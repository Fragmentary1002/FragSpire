using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    public class CardFly : MonoBehaviour
    {
        // 定义公共Transform组件引用，用于设置卡片的飞行目标位置。
        public Transform targetPosition;
        // 定义一个BattleSceneManager类型的私有成员变量，用于管理战斗场景。  
        BattleSceneManager battleSceneManager;
        private void Awake()
        {
            // 通过FindObjectOfType方法获取当前场景中类型为BattleSceneManager的实例，并将其赋值给battleSceneManager变量。  
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            // 将targetPosition变量设置为battleSceneManager中discardPileCountText的Transform组件的引用。
            targetPosition = battleSceneManager.discardPileCountText.transform;
            // 将targetPosition变量设置为battleSceneManager中discardPileCountText的Transform组件的引用。
            GetComponent<Animator>().Play("Disappear");
        }
        public void Update()
        {
            // 使用Vector3.Lerp方法平滑地更新当前游戏对象的Transform组件的位置，使其向目标位置移动。移动速度由Time.deltaTime*10决定。
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition.position, Time.deltaTime * 10);
            // 如果当前游戏对象的位置与目标位置的距离小于1个单位（一个单位是Unity的默认长度单位），则销毁当前游戏对象。 
            if (Vector3.Distance(this.transform.position, targetPosition.position) < 1f)
                Destroy(this.gameObject);
        }
    }
}
