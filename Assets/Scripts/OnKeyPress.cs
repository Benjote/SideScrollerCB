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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
