using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[System.Serializable]
public class IMatrix<T> : IEnumerable<T>
{
    [SerializeField] public T[,] data;

    public int Rows { get { return data.GetLength(0); } }

    public int Cols { get { return data.GetLength(1); } }


    
    //Function for easy value assignment and retrieval
    public T this[int i, int j]
    {
        get { return data[i, j]; }
        set { data[i, j] = value; }
    }

    //Create a new data array with a new size
    public IMatrix(int rows, int cols)
    {
        data = new T[rows, cols];
    }
    //Matrix values assigned from another
    public IMatrix(IMatrix<T> matrix)
    {
        data = matrix.data;
    }
    //Initialize with values
    public IMatrix(int rows, int cols, T[][] initialValues)
    {
        if (initialValues.Length != rows)
        {
            throw new ArgumentException("Number of initial value arrays must match rows");
        }

        this.data = new T[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            if (initialValues[i].Length != cols)
            {
                throw new ArgumentException("Number of elements in each initial value array must match columns");
            }

            for (int j = 0; j < cols; j++)
            {
                this.data[i, j] = initialValues[i][j];
            }
        }
    }

    public T GetValue(int row, int col)
    {
        return data[row, col];
    }

    public void SetValue(int row, int col, T value)
    {
        if (value.GetType() != data[row, col].GetType()) { return; }

        data[row, col] = value;
    }



    //Function to quickly create a string table of values
    public string MatrixToTableString(string delimiter = "", bool skipTop = true)
    {
        StringBuilder stringBuild = new StringBuilder();

        for (int i = 0; i < data.GetLength(0); i++)
        {
            if (skipTop) { stringBuild.Append("\n"); }
            for (int j = 0; j < data.GetLength(1); j++)
            {
                if (j == data.GetLength(1) - 1) { stringBuild.Append(data[i, j]); }
                else { stringBuild.Append(data[i, j]).Append(delimiter); }

            }
            if (!skipTop) { stringBuild.Append("\n"); }
        }

        return stringBuild.ToString();

    }



    //Comparison Functions
    public override bool Equals(object obj)
    {
        //Check type match
        if (obj.GetType() != this.GetType()) { return false; }

        //Get our Matrix
        IMatrix<T> mat = (IMatrix<T>)obj;

        //Check the lengths match
        if (mat.data.GetLength(0) != this.data.GetLength(0) || mat.data.GetLength(1) != this.data.GetLength(1)) { return false; }

        //Check the data in the the matrix positions
        for (int i = 0; i < this.data.GetLength(0); i++)
        {
            for (int j = 0; j < this.data.GetLength(1); j++)
            {
                //Debug.Log($"Comparing data[{i},{j}] ({data[i, j]}) with mat[{i},{j}] ({mat.data[i, j]})");
                if (!EqualityComparer<T>.Default.Equals(data[i, j], mat.data[i, j]))
                {
                    //Debug.Log("Mismatch found at position ({i},{j})!");
                    return false;
                }
            }
        }

        return true;
    }

    //Override the == operator as it is not being called properly with an Equals override
    public static bool operator ==(IMatrix<T> m1, IMatrix<T> m2)
    {
        // Call the Equals method for value comparison
        return m1.Equals(m2);
    }
    public static bool operator !=(IMatrix<T> m1, IMatrix<T> m2)
    {
        // Negate the result of the == operator
        return !(m1 == m2);
    }
    //Hash Code
    public override int GetHashCode()
    {
        // Combine hash codes of stateString and each KeyValuePair in the list
        int hash = 0;
        foreach (var pair in data)
        {
            hash ^= pair.GetHashCode() ^ pair.GetHashCode();
        }
        return hash;
    }





    //Mathmatical Functions

    //Matrix Multiplication
    public IMatrix<int> MatriceMultiplication(IMatrix<int> mat)
    {

        //Ensure class instance type matches Matrix Passed In
        if (data.GetType().GetElementType() != typeof(int))
        {
            return null;
        }
        

        //Get Our data sets
        int[,] A = data as int[,];
        int[,] B = mat.data;

        //Check to see our rows our not less than B's rows
        if (A.GetLength(0) < B.GetLength(0)) { Debug.Log("Incompatible"); return null; }

        //Get our new Matrix Class
        IMatrix<int> newMatrix = null;

        int m = A.GetLength(0); // Number of rows in A
        int n = A.GetLength(1); // Number of columns in A (also number of rows in B)
        int p = B.GetLength(1); // Number of columns in B

        // Initialize the resulting matrix C
        int[,] C = new int[m, p];

        // Perform matrix multiplication
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < p; j++)
            {
                int sum = 0;
                for (int k = 0; k < n; k++)
                {
                    sum += A[i, k] * B[k, j];
                }
                C[i, j] = sum;
            }
        }

        //Assign our new matrix
        newMatrix = new IMatrix<int>(C.GetLength(0), C.GetLength(1));

        //Addisgn it's data to C
        newMatrix.data = C;

        //Return Matrix C
        return newMatrix;

    }
    public IMatrix<float> MatriceMultiplication(IMatrix<float> mat)
    {
        //Ensure class instance type matches Matrix Passed In
        if (data.GetType().GetElementType() != typeof(float))
        {
            return null;
        }

        //Get Our data sets
        float[,] A = data as float[,];
        float[,] B = mat.data;

        //Check to see our rows our not less than B's rows
        if (A.GetLength(0) < B.GetLength(0)) { Debug.Log("Incompatible"); return null; }

        //Get our new Matrix Class
        IMatrix<float> newMatrix = null;

        int m = A.GetLength(0); // Number of rows in A
        int n = A.GetLength(1); // Number of columns in A (also number of rows in B)
        int p = B.GetLength(1); // Number of columns in B

        // Initialize the resulting matrix C
        float[,] C = new float[m, p];

        // Perform matrix multiplication
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < p; j++)
            {
                float sum = 0;
                for (int k = 0; k < n; k++)
                {
                    sum += A[i, k] * B[k, j];
                }
                C[i, j] = sum;
            }
        }

        //Assign our new matrix
        newMatrix = new IMatrix<float>(C.GetLength(0), C.GetLength(1));

        //Addisgn it's data to C
        newMatrix.data = C;

        //Return Matrix C
        return newMatrix;

    }
    public IMatrix<double> MatriceMultiplication(IMatrix<double> mat)
    {
        //Ensure class instance type matches Matrix Passed In
        if (data.GetType().GetElementType() != typeof(double))
        {
            return null;
        }

        //Get Our data sets
        double[,] A = data as double[,];
        double[,] B = mat.data;

        //Check to see our rows our not less than B's rows
        if (A.GetLength(0) < B.GetLength(0)) { Debug.Log("Incompatible"); return null; }

        //Get our new Matrix Class
        IMatrix<double> newMatrix = null;

        int m = A.GetLength(0); // Number of rows in A
        int n = A.GetLength(1); // Number of columns in A (also number of rows in B)
        int p = B.GetLength(1); // Number of columns in B

        // Initialize the resulting matrix C
        double[,] C = new double[m, p];

        // Perform matrix multiplication
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < p; j++)
            {
                double sum = 0;
                for (int k = 0; k < n; k++)
                {
                    sum += A[i, k] * B[k, j];
                }
                C[i, j] = sum;
            }
        }

        //Assign our new matrix
        newMatrix = new IMatrix<double>(C.GetLength(0), C.GetLength(1));

        //Addisgn it's data to C
        newMatrix.data = C;

        //Return Matrix C
        return newMatrix;

    }

    //Matrix Additions
    public IMatrix<float> MatriceAddition(IMatrix<float> mat)
    {
        //Ensure both types are types that we can use for arithmetic operations 
        if (data.GetType().GetElementType() != typeof(float)) { return null; }

        // Define matrices A and B
        float[,] A = data as float[,];
        float[,] B = mat.data;

        // Ensure that both matrices have the same dimensions
        if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
        {
            // Matrices have different dimensions, subtraction is not possible
            Debug.Log("Matrices have different dimensions, subtraction is not possible");
            return null;
        }

        //Our New Matrix
        IMatrix<float> newMatrix = null;

        // Get the dimensions of the matrices
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);

        // Initialize the resulting matrix C
        float[,] C = new float[rows, cols];

        // Perform matrix subtraction
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                C[i, j] = A[i, j] + B[i, j];
            }
        }

        newMatrix = new IMatrix<float>(C.GetLength(0), C.GetLength(1));
        newMatrix.data = C;

        return newMatrix;
    }
    public IMatrix<int> MatriceAddition(IMatrix<int> mat)
    {
        //Ensure both types are types that we can use for arithmetic operations 
        if (data.GetType().GetElementType() != typeof(int)) { return null; }

        // Define matrices A and B
        int[,] A = data as int[,];
        int[,] B = mat.data;

        // Ensure that both matrices have the same dimensions
        if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
        {
            // Matrices have different dimensions, subtraction is not possible
            Debug.Log("Matrices have different dimensions, subtraction is not possible");
            return null;
        }

        //Our New Matrix
        IMatrix<int> newMatrix = null;

        // Get the dimensions of the matrices
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);

        // Initialize the resulting matrix C
        int[,] C = new int[rows, cols];

        // Perform matrix subtraction
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                C[i, j] = A[i, j] + B[i, j];
            }
        }

        newMatrix = new IMatrix<int>(C.GetLength(0), C.GetLength(1));
        newMatrix.data = C;

        return newMatrix;
    }
    public IMatrix<double> MatriceAddition(IMatrix<double> mat)
    {
        //Ensure both types are types that we can use for arithmetic operations 
        if (data.GetType().GetElementType() != typeof(double)) { return null; }

        // Define matrices A and B
        double[,] A = data as double[,];
        double[,] B = mat.data;

        // Ensure that both matrices have the same dimensions
        if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
        {
            // Matrices have different dimensions, subtraction is not possible
            Debug.Log("Matrices have different dimensions, subtraction is not possible");
            return null;
        }

        //Our New Matrix
        IMatrix<double> newMatrix = null;

        // Get the dimensions of the matrices
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);

        // Initialize the resulting matrix C
        double[,] C = new double[rows, cols];

        // Perform matrix subtraction
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                C[i, j] = A[i, j] + B[i, j];
            }
        }

        newMatrix = new IMatrix<double>(C.GetLength(0), C.GetLength(1));
        newMatrix.data = C;

        return newMatrix;
    }

    //Matrix Subtractions
    public IMatrix<int> MatriceSubtraction(IMatrix<int> mat)
    {
        //Ensure both types are types that we can use for arithmetic operations 
        if (data.GetType().GetElementType() != typeof(int)) { return null; }

        // Define matrices A and B
        int[,] A = data as int[,];
        int[,] B = mat.data as int[,];

        // Ensure that both matrices have the same dimensions
        if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
        {
            // Matrices have different dimensions, subtraction is not possible
            Debug.Log("Matrices have different dimensions, subtraction is not possible");
            return null;
        }

        //Our New Matrix
        IMatrix<int> newMatrix = null;

        // Get the dimensions of the matrices
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);

        // Initialize the resulting matrix C
        int[,] C = new int[rows, cols];

        // Perform matrix subtraction
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                C[i, j] = A[i, j] - B[i, j];
            }
        }

        newMatrix = new IMatrix<int>(C.GetLength(0), C.GetLength(1));
        newMatrix.data = C;

        return newMatrix;
    }
    public IMatrix<float> MatriceSubtraction(IMatrix<float> mat)
    {
        //Ensure both types are types that we can use for arithmetic operations 
        if (data.GetType().GetElementType() != typeof(float)) { return null; }

        // Define matrices A and B
        float[,] A = data as float[,];
        float[,] B = mat.data;

        // Ensure that both matrices have the same dimensions
        if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
        {
            // Matrices have different dimensions, subtraction is not possible
            Debug.Log("Matrices have different dimensions, subtraction is not possible");
            return null;
        }

        //Our New Matrix
        IMatrix<float> newMatrix = null;

        // Get the dimensions of the matrices
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);

        // Initialize the resulting matrix C
        float[,] C = new float[rows, cols];

        // Perform matrix subtraction
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                C[i, j] = A[i, j] - B[i, j];
            }
        }

        newMatrix = new IMatrix<float>(C.GetLength(0), C.GetLength(1));
        newMatrix.data = C;

        return newMatrix;
    }
    public IMatrix<double> MatriceSubtraction(IMatrix<double> mat)
    {
        //Ensure both types are types that we can use for arithmetic operations 
        if (data.GetType().GetElementType() != typeof(double)) { return null; }

        // Define matrices A and B
        double[,] A = data as double[,];
        double[,] B = mat.data;

        // Ensure that both matrices have the same dimensions
        if (A.GetLength(0) != B.GetLength(0) || A.GetLength(1) != B.GetLength(1))
        {
            // Matrices have different dimensions, subtraction is not possible
            Debug.Log("Matrices have different dimensions, subtraction is not possible");
            return null;
        }

        //Our New Matrix
        IMatrix<double> newMatrix = null;

        // Get the dimensions of the matrices
        int rows = A.GetLength(0);
        int cols = A.GetLength(1);

        // Initialize the resulting matrix C
        double[,] C = new double[rows, cols];

        // Perform matrix subtraction
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                C[i, j] = A[i, j] - B[i, j];
            }
        }

        newMatrix = new IMatrix<double>(C.GetLength(0), C.GetLength(1));
        newMatrix.data = C;

        return newMatrix;
    }






    //For IEnumeration operations over the Matrix
    public IEnumerator<T> GetEnumerator()
    {
        var enumerator = new MatrixEnumerator<T>(data);
        enumerator.Reset(); // Reset before returning
        return enumerator;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator(); // Cast to non-generic IEnumerator for base interface
    }

    private class MatrixEnumerator<U> : IEnumerator<T>
    {
        private readonly T[,] data;
        private int i = 0;
        private int j = -1;

        public MatrixEnumerator(T[,] data)
        {
            this.data = data;
        }

        public T Current => data[i, j];

        object IEnumerator.Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            if (data == null || data.Length == 0)
            {
                return false;
            }

            j++;
            if (j >= data.GetLength(1))
            {
                j = 0;
                i++;
            }

            return (i < data.GetLength(0) && j < data.GetLength(1));
        }

        public void Reset()
        {
            i = 0;
            j = -1;
        }

        public void Dispose() { } // Optional for cleanup, not strictly necessary here
    }



}
