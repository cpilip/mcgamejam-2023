using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashableGlass : Dashable
{
    int counter = 0;
    bool canDash = false;

    public void SetCanDash(bool value)
    {
        canDash = value;
    }

    public override void DashThrough()
    {
        if (canDash)
        {
            canDash = false;
            Debug.Log("Dashing through glass" + this.gameObject.name);
            counter++;

            switch (counter)
            {
                case 1:
                    // Sprite a lil cracked
                    Debug.Log("cracked the glass a bit" + this.gameObject.name);
                    break;
                case 2:
                    // Sprite cracked glass
                    Debug.Log("cracked the glass a lot" + this.gameObject.name);
                    break;
                case 3:
                    this.transform.parent.gameObject.SetActive(false);
                    this.gameObject.SetActive(false);
                    break;
            }
        }
    }
}
