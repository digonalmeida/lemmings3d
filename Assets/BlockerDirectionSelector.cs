using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerDirectionSelector : MonoBehaviour {

    private Transform followingTransform;


    public void Setup(Transform following)
    {
        followingTransform = following;

    }

	
	// Update is called once per frame
	void Update () {

        transform.position = followingTransform.position;

	}

    public void ClickDirection(int direction)
    {
        switch(direction)
        {
            
        }

        Hide();

    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
