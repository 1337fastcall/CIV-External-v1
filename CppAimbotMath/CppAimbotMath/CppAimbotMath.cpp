// CppAimbotMath.cpp : Definiert die exportierten Funktionen für die DLL-Anwendung.
//

#include "stdafx.h"


#include <windows.h>
#include <math.h>

#ifndef M_PI
#define M_PI		3.14159265358979323846f	// matches value in gcc v2 math.h
#endif

#define M_RADPI 57.295779513082f

#define DotProduct(x,y) ((x)[0]*(y)[0]+(x)[1]*(y)[1]+(x)[2]*(y)[2])

#define VectorSubtract(a,b,c) {(c)[0]=(a)[0]-(b)[0];(c)[1]=(a)[1]-(b)[1];(c)[2]=(a)[2]-(b)[2];}

extern bool g_bIsInFullScreen;

class CMath
{
	float VectorLength(float *v)
	{
		return (float)sqrt(v[0]*v[0]+v[1]*v[1]+v[2]*v[2]);
	}
	float VectorAngle(float *a, float *b)
	{
		float length_a = VectorLength(a);
		float length_b = VectorLength(b);
		float length_ab = length_a*length_b;
		if( length_ab==0.0 ){ return 0.0; }
		else 
			return (float) (acos(DotProduct(a,b)/length_ab) * (180.f/M_PI));
	}
	void AngleVectors( float *angles, float *forward, float *right, float *up ) 
	{
		float angle;
		static float sp, sy, cp, cy;

		angle = angles[0] * ( M_PI / 180 );
		sp = sin( angle );
		cp = cos( angle );

		angle = angles[1] * ( M_PI / 180 );
		sy = sin( angle );
		cy = cos( angle );

		if( forward ) {
			forward[0] = cp*cy;
			forward[1] = cp*sy;
			forward[2] = -sp;
		}
		if( right || up ) {
			static float sr, cr;

			angle = angles[2] * ( M_PI / 180 );
			sr = sin( angle );
			cr = cos( angle );

			if( right ) {
				right[0] = -1*sr*sp*cy+-1*cr*-sy;
				right[1] = -1*sr*sp*sy+-1*cr*cy;
				right[2] = -1*sr*cp;
			}
			if( up ) {
				up[0] = cr*sp*cy+-sr*-sy;
				up[1] = cr*sp*sy+-sr*cy;
				up[2] = cr*cp;
			}
		}
	}
	float GetDistance( float *vecA, float *vecB )
	{
		float diff[3] = { vecB[0] - vecA[0], vecB[1] - vecA[1], vecB[2] - vecA[2] };
		return (float)( sqrt( ( diff[0] * diff[0] ) + ( diff[1] * diff[1] ) + ( diff[2] * diff[2] ) ) );
	}
	void MakeVector(float *pfIn, float *pfOut)
	{
		float pitch;
		float yaw;
		float tmp;		

		pitch = (float) (pfIn[0] * M_PI/180);
		yaw = (float) (pfIn[1] * M_PI/180);
		tmp = (float) cos(pitch);

		pfOut[0] = (float) (-tmp * -cos(yaw));
		pfOut[1] = (float) (sin(yaw)*tmp);
		pfOut[2] = (float) -sin(pitch);
	}
	void VectorRotateX(float *pfIn, float fAngle, float *pfOut)
	{
		float a,c,s;

		a = (float) (fAngle * M_PI/180);
		c = (float) cos(a);
		s = (float) sin(a);
		pfOut[0] = pfIn[0];
		pfOut[1] = c*pfIn[1] - s*pfIn[2];
		pfOut[2] = s*pfIn[1] + c*pfIn[2];	
	}
	void VectorRotateY(float *pfIn, float fAngle, float *pfOut)
	{
		float a,c,s;

		a = (float) (fAngle * M_PI/180);
		c = (float) cos(a);
		s = (float) sin(a);
		pfOut[0] = c*pfIn[0] + s*pfIn[2];
		pfOut[1] = pfIn[1];
		pfOut[2] = -s*pfIn[0] + c*pfIn[2];
	}
	void VectorRotateZ(float *pfIn, float fAngle, float *pfOut)
	{
		float a,c,s;

		a = (float) (fAngle * M_PI/180);
		c = (float) cos(a);
		s = (float) sin(a);
		pfOut[0] = c*pfIn[0] - s*pfIn[1];
		pfOut[1] = s*pfIn[0] + c*pfIn[1];
		pfOut[2] = pfIn[2];
	}
	void CalcAngle( float *src, float *dst, float *angles )
	{
		double delta[3] = { (src[0]-dst[0]), (src[1]-dst[1]), (src[2]-dst[2]) };
		double hyp = sqrt(delta[0]*delta[0] + delta[1]*delta[1]);

		angles[0] = (float) (atan(delta[2]/hyp) * M_RADPI);
		angles[1] = (float) (atan(delta[1]/delta[0]) * M_RADPI);
		angles[2] = 0.0f;

		if(delta[0] >= 0.0) { angles[1] += 180.0f; }
	}
	float GetFOV( float *angle, float *src, float *dst) 
	{ 
		float ang[3],aim[3]; 
		float fov; 

		CalcAngle(src, dst, ang); 
		MakeVector(angle, aim); 
		MakeVector(ang, ang);      

		float mag_s = sqrt((aim[0]*aim[0]) + (aim[1]*aim[1]) + (aim[2]*aim[2])); 
		float mag_d = sqrt((aim[0]*aim[0]) + (aim[1]*aim[1]) + (aim[2]*aim[2])); 

		float u_dot_v = aim[0]*ang[0] + aim[1]*ang[1] + aim[2]*ang[2]; 

		fov = acos(u_dot_v / (mag_s*mag_d)) * (180.0f / M_PI); 

		return fov; 
	}
	bool WorldToScreen(float *pfIn, float *pfViewOrigin, float *pfViewAngle, int FOV, int *piScreenSize, int *piOut)
	{
		float fAim[3];
		float fNewAim[3];
		float fView[3];
		float fTmp[3];
		float num;

		if(!pfIn||!piOut){ return false; }

		VectorSubtract(pfIn,pfViewOrigin,fAim);	
		MakeVector(pfViewAngle,fView);	

		//not in fov#!@#!@$#@!$
		if (VectorAngle(fView,fAim) > (FOV/1.8))
		{
			return false;
		}		

		VectorRotateZ(fAim,-pfViewAngle[1],fNewAim);// yaw
		VectorRotateY(fNewAim,-pfViewAngle[0],fTmp);// pitch
		VectorRotateX(fTmp,-pfViewAngle[2],fNewAim);// roll

		//they are behind us!@~!#@!$@!$
		if(fNewAim[0] <= 0)
			return false;

		if(FOV == 0.0f)
		{
			return false;
		}
		num = (float)(((piScreenSize[0] / 2) / fNewAim[0]) * (120.0 / FOV - 1.0 / 3.0));

		piOut[0] = (int)((piScreenSize[0] / 2) - num*fNewAim[1]);
		piOut[1] = (int)((piScreenSize[1] / 2) - num * fNewAim[2]);

		//if( !g_bIsInFullScreen )
		//{
		piOut[0] = piOut[0] + 3;
		piOut[1] = piOut[1] + 29;
		//}

		return true;
	}
};