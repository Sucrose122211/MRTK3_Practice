using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Utils
{
    public static class Utils
    {
        public static Vector2 GetRelativePosition(GameObject target, Vector3 worldPos, Vector3 origin)
        {
            if(target == null) return Vector2.zero;

            Vector3 targetPos = target.transform.position - origin;
            Vector3 normY = Vector3.Cross(targetPos, Vector3.up);
            Vector3 normX = Vector3.Cross(normY, targetPos);
            float x = Vector3.SignedAngle(targetPos, worldPos, normX);
            float y = Vector3.SignedAngle(targetPos, worldPos, normY);
            return new Vector2(x, y);
        }
    }

    public static class Matrixf
    {
        public static float Det(float[,] A)
        {
            /*2x2 only*/
            if(A.GetLength(0) != A.GetLength(1)) 
                throw new InvalidOperatorException("Input matrix of Determinant should be NxN matrix", typeof(float[,]));
            return A[0,0]*A[1,1] - A[0,1]*A[1,0];
        }

        public static float[,] Trans(float[,] A)
        {
            int row = A.GetLength(0);
            int col = A.GetLength(1);
            float[,] result = new float[col, row];

            for(int i = 0; i < col; i++)
                for(int j = 0; j < row; j++)
                {
                    result[i, j] = A[j, i];
                }
            return result;
        }

        public static float[,] Inverse(float[,] A)
        {
            /*2x2 only*/
            if(A.GetLength(0) != A.GetLength(1))
                throw new ArgumentException("Input matrix should be NxN matrix");

            float det = Det(A);
            if(det == 0)
                throw new InvalidOperatorException("Determination of the matrix is zero", typeof(float[,]));
            float[,] result = new float[2,2];

            result[0,0] = A[1,1] / det;
            result[1,1] = A[0,0] / det;
            result[1,0] = -A[1,0] / det;
            result[0,1] = -A[0,1] / det;

            return result;
        }

        public static float[,] Add(float[,] A, float[,] B)
        {
            int row = A.GetLength(0);
            int col = A.GetLength(1);
            if(row != B.GetLength(0) || col != B.GetLength(1))
                throw new InvalidOperatorException("Dimension of input matrix should be equal", typeof(float[,]));

            float[,] result = new float[row, col];

            for(int i = 0; i < row; i++)
                for(int j = 0; j < col; j++)
                    result[i, j] = A[i, j] + B[i, j];

            return result;
        }

        public static float[,] Sub(float[,] A, float[,] B)
        {
            int row = A.GetLength(0);
            int col = A.GetLength(1);
            if(row != B.GetLength(0) || col != B.GetLength(1))
                throw new InvalidOperatorException("Dimension of input matrix should be equal", typeof(float[,]));

            float[,] result = new float[row, col];

            for(int i = 0; i < row; i++)
                for(int j = 0; j < col; j++)
                    result[i, j] = A[i, j] - B[i, j];

            return result;
        }

        public static float[,] Mult(float[,] A, float[,] B)
        {
            int rowA = A.GetLength(0);
            int colA = A.GetLength(1);
            int rowB = B.GetLength(0);
            int colB = B.GetLength(1);
            if(colA != rowB)
                throw new InvalidOperatorException("Dimension mismatch in matrix multiplication", typeof(float[,]));

            float[,] result = new float[rowA,colB];

            for(int i = 0; i < rowA; i++)
                for(int j = 0; j < colB; j++)
                {
                    float sum = 0;
                    for(int k = 0; k < colA; k ++)
                        sum += A[i,k] * B[k,j];
                    result[i,j] = sum;
                }

            return result;
        }
    }
}
