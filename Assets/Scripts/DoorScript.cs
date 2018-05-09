using UnityEngine;

public class DoorScript : MonoBehaviour {

    private Animator _animator;

    public bool open;
    bool doorOpen;

	// Use this for initialization
	void Start ()
    {
        _animator = GetComponent<Animator>();
        doorOpen = true;	
	}

    void Update ()
    {
        if (doorOpen == false)
        {
            if (open == true)
            {
                _animator.SetBool("open", true);
                doorOpen = true;
            }
        }
        else if (doorOpen == true)
        {
            if (open == false)
            {
                _animator.SetBool("open", false);
                doorOpen = false;
            }
        }
	}
}
