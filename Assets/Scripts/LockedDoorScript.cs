using UnityEngine;

public class LockedDoorScript : MonoBehaviour
{
    private Animator _animator;
    public GameObject key;

    public bool open;
    bool doorOpen;

    void Start()
    {
        _animator = GetComponent<Animator>();
        doorOpen = true;
    }

    void Update()
    {
        switch (key.name)
        {
            case "Key1":
                if (ItemManager.hasKey[0] == true)
                    OpenDoor();
                break;
            case "Key2":
                if (ItemManager.hasKey[1] == true)            
                    OpenDoor();               
                break;
            case "Key3":
                if (ItemManager.hasKey[2] == true)
                    OpenDoor();
                break;
            default:
                break;
        }
    }

    void OpenDoor()
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