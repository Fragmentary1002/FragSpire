//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEditor.UI;

//public class UIRaycaster : MonoBehaviour
//{
//    // Update is called once per frame
//    void Update()
//    {
//        // ����������Ƿ���
//        if (Input.GetMouseButtonDown(0))
//        {
//            // ����һ�����ߴ����������
//            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;

//            // ������߻�����UIԪ��
//            if (Physics.Raycast(ray, out hit))
//            {
//                // �����еĶ����Ƿ���UIԪ��
//                if (hit.collider != null && hit.collider.GetComponent<RectTransform>() != null)
//                {
//                    // ����PointerEventData
//                    PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
//                    pointerEventData.position = Input.mousePosition;

//                    // ����RaycastResults�б��Խ������߼����
//                    var raycastResults = new List<RaycastResult>();

//                    // �������߼��
//                    EventSystem.current.RaycastAll(pointerEventData, raycastResults);

//                    // �������߼����
//                    foreach (var result in raycastResults)
//                    {
//                        // ����Ƿ��пɵ����UIԪ��
//                        if (result.gameObject.GetComponent<Button>() != null)
//                        {
//                            // �����ﴦ��UIԪ�صĵ��
//                            Debug.Log("Clicked on UI element: " + result.gameObject.name);
//                            break; // �˳�ѭ����ֻ�������ϲ��UIԪ��
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
