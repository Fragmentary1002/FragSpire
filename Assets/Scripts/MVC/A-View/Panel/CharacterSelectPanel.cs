using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XFramework;
using XFramework.Extend;


namespace Frag
{
    /// <summary>
    /// ��ʼ���
    /// </summary>
    public class CharacterSelectPanel : BasePanel
    {
        /// <summary>
        /// ·��
        /// </summary>
        static readonly string path = "Prefabs/UI/Panel/CharacterSelectPanel";

        /// <summary>
        /// ��ʼ���
        /// </summary>
        public CharacterSelectPanel() : base(new UIType(path))
        {

        }

        protected override void InitEvent()
        {

            ActivePanel.GetOrAddComponentInChildren<Button>("Embark").onClick.AddListener(() =>
            {
                Game.LoadScene(new FightScene());
            });

        }
    }
}