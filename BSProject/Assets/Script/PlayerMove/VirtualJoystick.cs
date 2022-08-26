using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
	private Image imageBackground;  // �츮�� ���� ��ġ�ϴ� ��Ʈ�ѷ��� ��� �̹���
	private Image imageController;  // ��ġ ������ ���� ��ġ�� �ٲ�� ��Ʈ�ѷ� �̹���
	private Vector2 touchPosition;      // ��ġ ��ġ�� �ܺη� ������ ���� ���������� �ƴ� ��������� ����

	Animator ani;

	// x, y ���� ���� �ܺο��� ������ �� �ֵ��� Get ���� ������Ƽ ����
	public float Horizontal => touchPosition.x;
	public float Vertical => touchPosition.y;

	private void Awake()
	{
		imageBackground = GetComponent<Image>();
		imageController = transform.GetChild(0).GetComponent<Image>();
	}

	/// <summary>
	/// ��ġ�ϴ� ���� 1ȸ ȣ��
	/// </summary>
	public void OnPointerDown(PointerEventData eventData)
	{
		Debug.Log("Touch Began : " + eventData);
	}

	/// <summary>
	/// ��ġ ���·� �巡���� �� �� ������ ȣ��
	/// </summary>
	public void OnDrag(PointerEventData eventData)
	{
		//Vector2 touchPosition = Vector2.zero;
		touchPosition = Vector2.zero;

		// ���̽�ƽ�� ��ġ�� ��� �ֵ� ������ ���� �����ϱ� ����
		// touchPosition�� ��ġ ���� �̹����� ���� ��ġ�� ��������
		// �󸶳� ������ �ִ����� ���� �ٸ��� ���´�
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
			imageBackground.rectTransform, eventData.position, eventData.pressEventCamera, out touchPosition))
		{
			// touchPosition ���� ����ȭ [0 ~ 1]
			// touchPosition�� �̹��� ũ��� ����
			touchPosition.x = (touchPosition.x / imageBackground.rectTransform.sizeDelta.x);
			touchPosition.y = (touchPosition.y / imageBackground.rectTransform.sizeDelta.y);

			// touchPosition ���� ����ȭ [-n ~ n]
			// ����(-1), �߽�(0), ������(1)�� �����ϱ� ���� touchPosition.x*2-1
			// �Ʒ�(-1), �߽�(0), ��(1)�� �����ϱ� ���� touchPosition.y*2-1
			// �� ������ Pivot�� ���� �޶�����. (���ϴ� Pivot ����)
			touchPosition = new Vector2(touchPosition.x * 2 - 1, touchPosition.y * 2 - 1);

			// touchPosition ���� ����ȭ [-1 ~ 1]
			// ���� ���̽�ƽ ��� �̹��� ������ ��ġ�� ������ �Ǹ� -1 ~ 1���� ū ���� ���� �� �ִ�
			// �� �� normailzed�� �̿��� -1 ~ 1������ ������ ����ȭ
			touchPosition = (touchPosition.magnitude > 1) ? touchPosition.normalized : touchPosition;

			// ���� ���̽�ƽ ��Ʈ�ѷ� �̹��� �̵�
			imageController.rectTransform.anchoredPosition = new Vector2(
				touchPosition.x * imageBackground.rectTransform.sizeDelta.x / 3,
				touchPosition.y * imageBackground.rectTransform.sizeDelta.y / 3);
		}
	}

	/// <summary>
	/// ��ġ�� �����ϴ� ���� 1ȸ ȣ��
	/// </summary>
	public void OnPointerUp(PointerEventData eventData)
	{
		// ��ġ ����� �̹����� ��ġ�� �߾����� �̵�
		imageController.rectTransform.anchoredPosition = Vector2.zero;
		// ��ġ ����� touchPosition ���� (0, 0)���� �ʱ�ȭ
		touchPosition = Vector2.zero;
	}
}