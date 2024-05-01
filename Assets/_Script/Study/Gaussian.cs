
using System;
using MixedReality.Toolkit.UX.Experimental;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

public class Gaussian
{
    private float _width;
    private float _angle;
    private float[,] _mean;
    private float[,] _covariance;
    private float denom;

    private const float a = 0.1243f;
    private const float b = 0.4115f;
    private const float c = 0.0078f;
    private const float d = 0.371f;
    private const float e = -0.1541f;
    private const float f = 0.0084f;
    private const float g = -0.1702f;

    public Gaussian(float width, float angle)
    {
        _width = width;
        _angle = angle;

        _mean = new float[,]{
            {e*_width + f*_angle + g}, 
            {0}
        };
        _covariance = new float[2,2]{
            {Mathf.Pow(a*_width + b, 2), 0}, 
            {0, Mathf.Pow(c*_angle + d, 2)}
        };

        denom = Mathf.Sqrt(Mathf.Pow(2 * Mathf.PI, 2) * Matrixf.Det(_covariance));

        if(denom == 0) throw new InvalidOperationException("Denominator of Gaussian is zero");
    }

    public float GetProbability(float x, float y)
    {
        float[,] input = new float[,]{
            {x}, {y}
        };

        var V = Matrixf.Sub(input, _mean);
        var Vt = Matrixf.Trans(V);
        var invSig = Matrixf.Inverse(_covariance);
        var e1 = Matrixf.Mult(Vt, invSig);
        var e2 = Matrixf.Mult(e1, V);
        float numer = Mathf.Exp(-0.5f * e2[0, 0]);

        return numer/denom;
    }
}
