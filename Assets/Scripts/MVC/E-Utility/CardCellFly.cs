using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Frag
{
    public class CardFly : MonoBehaviour
    {
        // ���幫��Transform������ã��������ÿ�Ƭ�ķ���Ŀ��λ�á�
        public Transform targetPosition;
        private void Awake()
        {
            // ��targetPosition��������ΪbattleSceneManager��discardPileCountText��Transform��������á�
            //targetPosition = battleSceneManager.discardPileCountText.transform;
            targetPosition.position = transform.position;
            // ��targetPosition��������ΪbattleSceneManager��discardPileCountText��Transform��������á�
            GetComponent<Animator>().Play("Disappear");
        }
        public void Update()
        {
            // ʹ��Vector3.Lerp����ƽ���ظ��µ�ǰ��Ϸ�����Transform�����λ�ã�ʹ����Ŀ��λ���ƶ����ƶ��ٶ���Time.deltaTime*10������
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition.position, Time.deltaTime * 10);
            // �����ǰ��Ϸ�����λ����Ŀ��λ�õľ���С��1����λ��һ����λ��Unity��Ĭ�ϳ��ȵ�λ���������ٵ�ǰ��Ϸ���� 
            if (Vector3.Distance(this.transform.position, targetPosition.position) < 1f)
                Destroy(this.gameObject);
        }
    }
}
