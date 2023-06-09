using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnKeyPress : MonoBehaviour
{
    public UnityEvent onKeyDown;
    public UnityEvent onKeyUp;
    public SpriteRenderer mySR;
    public Sprite changeToSprite;

    [SerializeField] KeyCode myKey;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(myKey))
        {
            onKeyDown.Invoke();
        }
        else if(Input.GetKeyUp(myKey))
        {
            onKeyUp.Invoke();

            mySR.sprite = changeToSprite;
        }
    }
}
