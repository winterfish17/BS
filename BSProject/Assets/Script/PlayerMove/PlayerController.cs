using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private VirtualJoystick virtualJoystick;
	
	public float moveSpeed = 5;

	SpriteRenderer spriteRenderer;
	Animator anim;

    private void Awake()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}
    private void Update()
	{
		float x = virtualJoystick.Horizontal;   // 좌/우 이동
		float y = virtualJoystick.Vertical;     // 위/아래 이동

		if (x != 0 || y != 0)
		{
			anim.SetBool("isWalking", true);
			transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;
			if (x > 0)
            {
				transform.localScale = new Vector3(-2, 2, 2);
			}
			if (x < 0)
			{
				transform.localScale = new Vector3(2, 2, 2);
			}
		}
        else
        {
			anim.SetBool("isWalking", false);
		}
	}
}

