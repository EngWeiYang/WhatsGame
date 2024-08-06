using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightManager : MonoBehaviour
{
    public float height = 857.08f;
    public float originalYPosCropBox = 0;

    public float width = 685.5f;
    public float originalXPosCropBox = 0;


    //Rescaling right handle using Top handle

    public float reScaleRightHandleHeight = 645;
    public float reScaleRightHandleWidth = 32;
    public float reScaleTopBottomHeight = 27;
    public float reScaleTopBottomHandleWidth = 808;





    //For VideoCropping

    public float videoCropperWidth = 583;
    public float originalXPosVideoCropper = 0;
}
