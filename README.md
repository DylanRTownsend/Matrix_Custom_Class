IMatrix, a custom class for easily creating Matrices of varying sizes and types.

Includes:

- Generic Type for any variable type usage
- Several access and assigment functions/options
- Matrix Manipulation Functions (dot product multiplication, addition, subtraction)
- Equality overrides (including the == and != operators)
- A string table builder (saves time rewriting loops to console write strings with line breaks) return function
- IEnumerator/GetNumerator functionaliities for clean foreach looping
- GetHasCode override (helps immensely when layered custom classes meet dictionaries)

Why?

Well, I was coding in Unity, and was going to use the Matrix, but realised it had a limit to it's size. So I wrote a quick little custom Matrix class (IMatrix - since Matrix is a global variable in Unity) to make up for this.
