using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightManager : MonoBehaviour
{
    public float height = 685.5f;
    public float originalYPosCropBox = -46f;

    public float width = 675.75f;
    public float originalXPosCropBox = 0;


    //Rescaling right handle using Top handle

    public float reScaleRightHandleHeight = 645;
    public float reScaleRightHandleWidth = 32.916f;
    public float reScaleTopBottomHeight = 27;
    public float reScaleTopBottomHandleWidth = 603.85f;





    //For VideoCropping

    public float videoCropperWidth = 583;
    public float originalXPosVideoCropper = 0;
}
