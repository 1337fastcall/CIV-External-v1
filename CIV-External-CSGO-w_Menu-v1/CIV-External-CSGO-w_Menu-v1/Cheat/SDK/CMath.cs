//
// CMath
// Pure C#      // may change cause of nospread
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;


class CMath
{
    public partial class Aimbot
    {
        const double M_RADPI = 57.295779513082;
        const double M_PI = 3.14159265358979323846; // source: gccmath (c++)


        private static void MakeVector(float[] pfIn, float[] pfOut)
        {
            float pitch;
            float yaw;
            float tmp;

            if (pfIn == null)
            {
                pfIn = new float[3];
            }

            if (pfOut == null)
            {
                for (int i = 0; i < 3; i++)
                    pfOut[i] = 0;
            }

            pitch = (float)(pfIn[0] * M_PI / 180);
            yaw = (float)(pfIn[1] * M_PI / 180);
            tmp = (float)Math.Cos(pitch);

            pfOut[0] = (float)(-tmp * -Math.Cos(yaw));
            pfOut[1] = (float)(Math.Sin(yaw) * tmp);
            pfOut[2] = (float)-Math.Sin(pitch);
        }

        public static void CalcAimAngles(float[] src, float[] dst, float[] angles)
        {
            double[] delta = { (src[0] - dst[0]), (src[1] - dst[1]), (src[2] - dst[2]) };
            double hyp = Math.Sqrt(delta[0] * delta[0] + delta[1] * delta[1]);

            angles[0] = (float)(Math.Atan(delta[2] / hyp) * M_RADPI);
            angles[1] = (float)(Math.Atan(delta[1] / delta[0]) * M_RADPI);
            angles[2] = 0.0f;

            if (delta[0] >= 0.0) { angles[1] += 180.0f; }
        }

        public static void VectorNormalize(float[] v) // From SDK but rewritten in C# // to do
        {
            double FLT_EPSILON = 1.192092896e-07F;

            // still to do
        }

        public static void NormalizeAngles(float[] angles) // From SDK
        {
            int i;

            // Normalize angles to -180 to 180 range
            for (i = 0; i < 3; i++)
            {
                if (angles[i] > 180.0)
                {
                    angles[i] -= (float)360.0;
                }
                else if (angles[i] < -180.0)
                {
                    angles[i] += (float)360.0;
                }
            }
        }

        public static float Get_FOV(float[] angle, float[] src, float[] dst)
        {
            float[] ang = new float[3];
            float[] aim = new float[3];
            double fov;

            CalcAimAngles(src, dst, ang);
            MakeVector(angle, aim);
            MakeVector(ang, ang);

            double mag_s = Math.Sqrt((aim[0] * aim[0]) + (aim[1] * aim[1]) + (aim[2] * aim[2]));
            double mag_d = Math.Sqrt((aim[0] * aim[0]) + (aim[1] * aim[1]) + (aim[2] * aim[2]));

            double u_dot_v = aim[0] * ang[0] + aim[1] * ang[1] + aim[2] * ang[2];

            fov = Math.Acos(u_dot_v / (mag_s * mag_d)) * (180.0f / M_PI);

            return (float)fov;
        }

        public static void Smooth(float[] AimAngles, float[] SmoothedAngles, float[] vAngs, int SmoothValue)
        {
            float[] smoothdiff = new float[2]; ;
            AimAngles[0] -= vAngs[0];
            AimAngles[1] -= vAngs[1];
            if (AimAngles[0] > 180) AimAngles[0] -= 360;
            if (AimAngles[1] > 180) AimAngles[1] -= 360;
            if (AimAngles[0] < -180) AimAngles[0] += 360;
            if (AimAngles[1] < -180) AimAngles[1] += 360;
            smoothdiff[0] = AimAngles[0] / SmoothValue;
            smoothdiff[1] = AimAngles[1] / SmoothValue;
            SmoothedAngles[0] = vAngs[0] + smoothdiff[0];
            SmoothedAngles[1] = vAngs[1] + smoothdiff[1];
            SmoothedAngles[2] = vAngs[2];
            if (SmoothedAngles[0] > 180) SmoothedAngles[0] -= 360;
            if (SmoothedAngles[1] > 180) SmoothedAngles[1] -= 360;
            if (SmoothedAngles[0] < -180) SmoothedAngles[0] += 360;
            if (SmoothedAngles[1] < -180) SmoothedAngles[1] += 360;
        }
    }

    public partial class ESP // to do
    {

    }
}