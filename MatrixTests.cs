using System;
using UnityEngine;

public class MatrixTests : MonoBehaviour
{
    //Lets try some different types of Matrices for testing

    [SerializeField]
    public IMatrix<int> myMatrix = new IMatrix<int>(3, 3); // Our main matrix - we compare the others to here for testing

    public IMatrix<int> matchingMatrix = new IMatrix<int>(3, 3); //A matching matrix

    public IMatrix<int> notMatchingVars = new IMatrix<int>(3, 3); //A same sized matrix with a single different value

    public IMatrix<int> notMatchingSize = new IMatrix<int>(4, 3); //A matrix with a different size

    public IMatrix<float> notMatchingType = new IMatrix<float>(4, 3); //A matrix with non-matching types

    public IMatrix<string> RuntimeInitialized = new IMatrix<string>(4, 3); //A matix whose variable we will add by hand in the Start

    //A matrix preloaded globally
    public IMatrix<string> GloblalInitialized = new IMatrix<string>(4, 3, new string[][] 
    { 
        new string[]{ "Hello", "H", "h" },
        new string[]{ "World", "i", "?" }, 
        new string[]{ "World", "i", "?" }, 
        new string[]{ "World", "i", "?" } 
    });




    // Start is called before the first frame update
    void Start()
    {
        //Setting an IMatrix at runtime
        string[] row1 = { "Hello", "H", "h" };
        string[] row2 = { "World", "i", "?" };
        string[] row3 = { "!", "!", "!" };
        string[] row4 = { "There", ".", "." };

        RuntimeInitialized = new IMatrix<string>(4, 3, new string[][] { row1, row2, row3, row4 });


        TestMatrix();
    }

    public void TestMatrix()
    {

        //Assigning variables into our matrices
        AssignTestMatricesVariables();

        //A Comparison Test
        ComparisonTest();

        //Test the IENumerator works
        IENumeratorTest();

        //Test Arithmetic Functions work
        ArithmeticTest();



        //Get different string results with our matrix to string table function - Success
        //As Unity puts the top string next to the time of Debug (throwing the table out of whack
        //A bool has been used to skip the first line (default true)
        //Also, a delimiter can be used to seperatre the table values in a row

        string useDefaultCall = notMatchingSize.MatrixToTableString();
        string useDelimiter = notMatchingVars.MatrixToTableString(", ");
        string useDelimiterAndBool = RuntimeInitialized.MatrixToTableString(" || ", true);



    }

    public void AssignTestMatricesVariables()
    {
        //First lets assign the main, matching and not matching (single var) together to save time
        for (int i = 0; i < myMatrix.Rows; i++)
        {
            for (int j = 0; j < myMatrix.Cols; j++)
            {
                //Assign to our matrices
                myMatrix[i, j] = i + j;
                matchingMatrix[i, j] = i + j;

                //If we are in a specific position - give the unmatching matrix it's single different variables
                if (i == myMatrix.Rows - 2 && j == myMatrix.Cols - 1)
                {
                    notMatchingVars[i, j] = 7;
                }
                else
                {
                    notMatchingVars[i, j] = i + j;
                }
            }
        }

        //Next we can fill the other two Matrices
        for (int i = 0; i < notMatchingSize.Rows; i++)
        {
            for (int j = 0; j < notMatchingSize.Cols; j++)
            {
                notMatchingSize[i, j] = i + j;
            }
        }

        for (int i = 0; i < notMatchingType.Rows; i++)
        {
            for (int j = 0; j < notMatchingType.Cols; j++)
            {
                notMatchingType[i, j] = i + j;
            }
        }
    }

    public void ComparisonTest()
    {
        Debug.Log("-- Comparisons -- ");

        //Debugs for testing equals and comparison opertaions work - success
        if (myMatrix == matchingMatrix) { Debug.Log("Success: Matching Size and Variables."); }
        else { Debug.Log("Failed: Matching Size and Variables."); }

        if (myMatrix == notMatchingVars) { Debug.Log("Failed: We are matching Different Variables."); }
        else { Debug.Log("Success: Not matching different variables."); }

        if (myMatrix == notMatchingSize) { Debug.Log("Failed: We are matching different sizes."); }
        else { Debug.Log("Success: Not matching different sizes."); }

        //Success here, as they cannot be compared due to their different type value
        /*if (myMatrix == notMatchingType) { Debug.Log("Failed: We are matching different variable types."); }
        else { Debug.Log("Success: Not matching different variable types."); }*/
    }

    public void IENumeratorTest()
    {
        Debug.Log("-- IENumerator -- ");

        //For each loop to ensure IEnumerator is working correctly - sucess
        foreach (var d in myMatrix)
        {
            Debug.Log(d.ToString());
        }
    }

    public void ArithmeticTest()
    {
        Debug.Log("-- Arithmetic -- ");

        //String tables for the tables we will be using to demnostrate
        string myMat = myMatrix.MatrixToTableString("-", true);
        string matchMat = matchingMatrix.MatrixToTableString("-");

        //Matrix Multiplication - Success
        Debug.Log("-- -- Arithmetic - Multiplication -- -- ");
        //Multiplication test
        IMatrix<int> C = myMatrix.MatriceMultiplication(matchingMatrix);
        //String Out put of results

        Debug.Log(myMat);
        Debug.Log(matchMat);

        Debug.Log("A * B =");

        string cTable = C.MatrixToTableString("-");

        Debug.Log(cTable);

        //Matrix Addition - Success
        Debug.Log("-- -- Arithmetic - Addition -- -- ");
        IMatrix<int> c2 = myMatrix.MatriceAddition(matchingMatrix);

        Debug.Log(myMat);
        Debug.Log(matchMat);

        Debug.Log("A + B =");

        string c2Table = c2.MatrixToTableString("-");

        Debug.Log(c2Table);

        //Matrix subtraction
        Debug.Log("-- -- Arithmetic - Subtraction -- -- ");
        IMatrix<int> c3 = myMatrix.MatriceSubtraction(matchingMatrix);

        Debug.Log(myMat);
        Debug.Log(matchMat);

        Debug.Log("A - B =");

        string c3Table = c3.MatrixToTableString("-");

        Debug.Log(c3Table);
    } 
    
}
