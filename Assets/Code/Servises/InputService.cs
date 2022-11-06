using Assets.Code.Infrastrucrure;
using UnityEngine;


public class InputService : IService
{

    private Vector3 _inputVector = Vector3.zero;

    public Vector3 MousePosition => Input.mousePosition;

    public Vector3 GetInputVector()
    {
        _inputVector.z = Input.GetAxis("Horizontal");
        _inputVector.x = Input.GetAxis("Vertical");

        return _inputVector;
    }

    public bool GetShootButton() 
        => Input.GetMouseButtonDown(0);

    internal bool GetJumpButton()
    {
        if (Input.GetAxis("Jump") != 0)
            return true;
        else
            return false;
    }
}
