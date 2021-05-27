'---------------------------------------------------------------------------------------------------
' file:		CodeAnalysis\DiagnosticBag.vb
'
' summary:	Diagnostic bag class
'---------------------------------------------------------------------------------------------------

Option Explicit On
Option Strict On
Option Infer On

Imports Basic.CodeAnalysis.Symbols
Imports Basic.CodeAnalysis.Syntax
Imports Basic.CodeAnalysis.Text
Imports Mono.Cecil

Namespace Global.Basic.CodeAnalysis
    ''' <summary> . </summary>

    Friend NotInheritable Class DiagnosticBag
        Implements IEnumerable(Of Diagnostic)
        ''' <summary>   The diagnostics. </summary>

        Private ReadOnly m_diagnostics As New List(Of Diagnostic)

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Returns an enumerator that iterates through the collection. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   An enumerator that can be used to iterate through the collection. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Function GetEnumerator() As IEnumerator(Of Diagnostic) Implements IEnumerable(Of Diagnostic).GetEnumerator
            Return m_diagnostics.GetEnumerator
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Enumerable get enumerator. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <returns>   An IEnumerator. </returns>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="message">  The message. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Private Sub Report(location As TextLocation, message As String)
            Dim diagnostic = New Diagnostic(location, message)
            m_diagnostics.Add(diagnostic)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Adds a range. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="diagnostics">  An IEnumerable(OfDiagnostic) of items to append to this. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub AddRange(diagnostics As IEnumerable(Of Diagnostic))
            m_diagnostics.AddRange(diagnostics)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Concatenates the given diagnostics. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="diagnostics">  The diagnostics. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub Concat(diagnostics As DiagnosticBag)
            m_diagnostics.Concat(diagnostics.m_diagnostics)
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports invalid number. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="text">     The text. </param>
        ''' <param name="type">     The type. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportInvalidNumber(location As TextLocation, text As String, type As TypeSymbol)
            Report(location, $"The number {text} isn't valid {type}.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports bad character. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location">     The location. </param>
        ''' <param name="character">    The character. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportBadCharacter(location As TextLocation, character As Char)
            Report(location, $"Bad character input: '{character}'.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports unterminated string. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportUnterminatedString(location As TextLocation)
            Report(location, $"Unterminated string literal.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports unterminated multi line comment. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportUnterminatedMultiLineComment(location As TextLocation)
            Report(location, $"Unterminated mult-line comment.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports unexpected token. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location">     The location. </param>
        ''' <param name="actualKind">   The actual kind. </param>
        ''' <param name="expectedKind"> The expected kind. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportUnexpectedToken(location As TextLocation, actualKind As SyntaxKind, expectedKind As SyntaxKind)
            Report(location, $"Unexpected token <{actualKind}>, expected <{expectedKind}>.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports undefined unary operator. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location">     The location. </param>
        ''' <param name="operatorText"> The operator text. </param>
        ''' <param name="operandType">  Type of the operand. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportUndefinedUnaryOperator(location As TextLocation, operatorText As String, operandType As TypeSymbol)
            Report(location, $"Unary operator '{operatorText}' is not defined for type '{operandType}'.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports undefined binary operator. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location">     The location. </param>
        ''' <param name="operatorText"> The operator text. </param>
        ''' <param name="leftType">     Type of the left. </param>
        ''' <param name="rightType">    Type of the right. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportUndefinedBinaryOperator(location As TextLocation, operatorText As String, leftType As TypeSymbol, rightType As TypeSymbol)
            Report(location, $"Binary operator '{operatorText}' is not defined for type '{leftType}' and '{rightType}'.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports parameter already declared. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location">         The location. </param>
        ''' <param name="parameterName">    Name of the parameter. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportParameterAlreadyDeclared(location As TextLocation, parameterName As String)
            Report(location, $"A parameter with the name '{parameterName}' already exists.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports undefined variable. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="name">     The name. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        'Public Sub ReportUndefinedName(location As TextLocation, name As String)
        Public Sub ReportUndefinedVariable(location As TextLocation, name As String)
            Report(location, $"Variable '{name}' doesn't exist.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports not a variable. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="name">     The name. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportNotAVariable(location As TextLocation, name As String)
            Report(location, $"'{name}' is not a variable.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports undefined type. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="name">     The name. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Friend Sub ReportUndefinedType(location As TextLocation, name As String)
            Report(location, $"Type '{name}' doesn't exist.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports cannot convert. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="fromType"> Type of from. </param>
        ''' <param name="toType">   Type of to. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportCannotConvert(location As TextLocation, fromType As TypeSymbol, toType As TypeSymbol)
            Report(location, $"Cannot convert type '{fromType}' to '{toType}'.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports cannot convert implicitly. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="fromType"> Type of from. </param>
        ''' <param name="toType">   Type of to. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportCannotConvertImplicitly(location As TextLocation, fromType As TypeSymbol, toType As TypeSymbol)
            Report(location, $"Cannot convert type '{fromType}' to '{toType}'. An explicit conversion exists (are you missing a cast?)")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports symbol already declared. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="name">     The name. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportSymbolAlreadyDeclared(location As TextLocation, name As String)
            Report(location, $"'{name}' is already declared.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports cannot assign. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="name">     The name. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportCannotAssign(location As TextLocation, name As String)
            Report(location, $"Variable '{name}' is read-only and cannot be assigned to.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports undefined function. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="name">     The name. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportUndefinedFunction(location As TextLocation, name As String)
            Report(location, $"Function '{name}' doesn't exist.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports expression must have value. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportExpressionMustHaveValue(location As TextLocation)
            Report(location, "Expression must have a value.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports not a function. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="name">     The name. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportNotAFunction(location As TextLocation, name As String)
            Report(location, $"'{name}' is not a function.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports wrong argument count. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location">         The location. </param>
        ''' <param name="name">             The name. </param>
        ''' <param name="expectedCount">    Number of expected. </param>
        ''' <param name="actualCount">      Number of actuals. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportWrongArgumentCount(location As TextLocation, name As String, expectedCount As Integer, actualCount As Integer)
            Report(location, $"Function '{name}' requires {expectedCount} arguments but was given {actualCount}.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports wrong argument type. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location">     The location. </param>
        ''' <param name="name">         The name. </param>
        ''' <param name="expectedType"> Type of the expected. </param>
        ''' <param name="actualType">   Type of the actual. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportWrongArgumentType(location As TextLocation, name As String, expectedType As TypeSymbol, actualType As TypeSymbol)
            Report(location, $"Parameter '{name}' requires a value of type '{expectedType}' but was given a value of type '{actualType}'.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports invalid break or continue. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        ''' <param name="text">     The text. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportInvalidBreakOrContinue(location As TextLocation, text As String)
            Report(location, $"The keyword '{text}' can only be used inside of loops.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports all paths must return. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportAllPathsMustReturn(location As TextLocation)
            Report(location, "Not all code paths return a value.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports invalid return expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location">     The location. </param>
        ''' <param name="functionName"> Name of the function. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportInvalidReturnExpression(location As TextLocation, functionName As String)
            Report(location, $"Since the function '{functionName}' does not return a value, the 'return' keyword cannot be followed by an expression.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports invalid return with value in global statements. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportInvalidReturnWithValueInGlobalStatements(location As TextLocation)
            Report(location, $"The 'return' keyword cannot be followed by an expression in global statements.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports missing return expression. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location">     The location. </param>
        ''' <param name="returnType">   Type of the return. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportMissingReturnExpression(location As TextLocation, returnType As TypeSymbol)
            Report(location, $"An expression of type '{returnType}' is expected.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports invalid expression statement. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportInvalidExpressionStatement(location As TextLocation)
            Report(location, "Only assignment and call epxressions can be used as a statement.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports only one file can have global statements. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportOnlyOneFileCanHaveGlobalStatements(location As TextLocation)
            Report(location, "At most one file can have global statements.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports main must have correct signature. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportMainMustHaveCorrectSignature(location As TextLocation)
            Report(location, "main must not take arguments and not return anything.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports cannot mix main and global statements. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="location"> The location. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportCannotMixMainAndGlobalStatements(location As TextLocation)
            Report(location, "Cannot declare main function when global statements are used.")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports invalid reference. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="path"> Full pathname of the file. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportInvalidReference(path As String)
            Report(Nothing, $"The reference is not a valid .NET assembly: '{path}'")
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports required type not found. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="internalName"> Name of the internal. </param>
        ''' <param name="metadataName"> Name of the metadata. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportRequiredTypeNotFound(internalName As String, metadataName As String)
            Report(Nothing, If(internalName Is Nothing,
                               $"The required type '{metadataName}' cannot be resolved among the given references.",
                               $"The required type '{internalName}' ('{metadataName}') cannot be resolved among the given references."))
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports required type ambiguous. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="internalName"> Name of the internal. </param>
        ''' <param name="metadataName"> Name of the metadata. </param>
        ''' <param name="foundTypes">   List of types of the founds. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportRequiredTypeAmbiguous(internalName As String, metadataName As String, foundTypes() As TypeDefinition)
            Dim assemblyNames = foundTypes.Select(Function(t) t.Module.Assembly.Name.Name)
            Dim assemblyNameList = String.Join(", ", assemblyNames)
            Report(Nothing, If(internalName Is Nothing,
                               $"The required type '{metadataName}' was found in multiple references: {assemblyNameList}",
                               $"The required type '{internalName}' ('{metadataName}') was found in multiple references: {assemblyNameList}"))
        End Sub

        '''////////////////////////////////////////////////////////////////////////////////////////////////////
        ''' <summary>   Reports required method not found. </summary>
        '''
        ''' <remarks>   Leroy, 27/05/2021. </remarks>
        '''
        ''' <param name="typeName">             Name of the type. </param>
        ''' <param name="methodName">           Name of the method. </param>
        ''' <param name="parameterTypeNames">   List of names of the parameter types. </param>
        '''////////////////////////////////////////////////////////////////////////////////////////////////////

        Public Sub ReportRequiredMethodNotFound(typeName As String, methodName As String, parameterTypeNames() As String)
      Dim parameterTypeNameList = String.Join(", ", parameterTypeNames)
      Report(Nothing, $"The required method '{typeName}.{methodName}({parameterTypeNameList})' cann be resolved among the given references.")
    End Sub

  End Class

End Namespace