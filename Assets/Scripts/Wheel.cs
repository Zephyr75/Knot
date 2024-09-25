using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    RIGHT, LEFT
}

public class Wheel : Module
{
    public Direction direction;
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        SetMainInput(KeyCode.UpArrow);
        SetSecondInput(KeyCode.DownArrow);
    }

    // Update is called once per frame
    void Update()
    {
        if (machine.GetComponent<Editor>().start)
        {
            //Controls for driving
            bool up = Input.GetKey(GetMainInput());
            bool down = Input.GetKey(GetSecondInput());
            bool left = Input.GetKey(GetThirdInput());
            bool right = Input.GetKey(GetFourthInput());

            Debug.Log(GetThirdInput().ToString());

            if (up)
            {
                RollForward();
            }
            if (down)
            {
                RollBackward();
            }
            if (right)
            {
                Debug.Log("right");
                if (direction == Direction.RIGHT)
                {
                    RollBackward();
                }
                if (direction == Direction.LEFT)
                {
                    RollForward();
                }
            }
            if (left)
            {
                if (direction == Direction.LEFT)
                {
                    RollBackward();
                }
                if (direction == Direction.RIGHT)
                {
                    RollForward();
                }
            }
        }
    }

    private void RollForward()
    {
        GetComponent<Rigidbody>().AddForceAtPosition(transform.parent.forward * 15 * speed, transform.position + new Vector3(0, .5f, 0));
    }

    private void RollBackward()
    {
        GetComponent<Rigidbody>().AddForceAtPosition(-transform.parent.forward * 10 * speed, transform.position + new Vector3(0, .5f, 0));
    }
}
