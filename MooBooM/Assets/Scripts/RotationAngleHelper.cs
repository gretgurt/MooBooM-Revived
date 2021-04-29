// https://forum.unity.com/members/joshuamckenzie.866650/
// https://forum.unity.com/threads/solved-how-to-get-rotation-value-that-is-in-the-inspector.460310/
/*
 * transform.rotation.x is a complex number. 
 * its not a angle and oscillates from -1 to 1. 
 * same goes for y,z,and w components
 * as for converting to +/- 180:
 * 
 * so WrapAngle will convert 270 to -90
 */
public static class RotationAngleHelper {

    private static float WrapAngle(float angle) {
        angle %= 360;
        if (angle > 180)
            return angle - 360;

        return angle;
    }

    private static float UnwrapAngle(float angle) {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;

        return 360 - angle;
    }
}
