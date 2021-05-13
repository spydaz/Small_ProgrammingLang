using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using SDK.SmallProgLang.Ast_ExpressionFactory;
using SDK.SmallProgLang.Evaluator;
using SDK.SmallProgLang.GrammarFactory;



// INTERPRETOR
// 
#region INTERPRETATION
// THE AST CREATON FACTORY - ALL LANGUAGES
// 
#region AST - FACTORY
// AST FACTORY
// 
namespace SDK.SmallProgLang
{

    // THE SMALL PROGRAMMING LANGUGE - TOP LEVEL 
    // 
    #region SPL SMALL PRAGRAMING LANG
    #region MODELS
    // AST PROGRAM
    // 
    namespace Ast_ExpressionFactory
    {
        /// <summary>
        /// 
        /// 
        /// Syntax : 
        /// Based on AST Explorer output
        /// {
        /// "type": "Program",
        /// "start": 0,
        /// "end": 2,
        /// "body": [
        ///    {
        ///      "type": "ExpressionStatement",
        ///      "start": 0,
        ///      "end": 2,
        ///      "expression": {
        ///        "type": "Literal",
        ///        "start": 0,
        ///        "end": 2,
        ///        "value": 42,
        ///        "raw": "42"
        ///      }
        ///    }
        /// ],
        /// }
        /// 
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class AstProgram : AstNode
        {
            /// <summary>
            /// Expression List
            /// </summary>
            public List<AstExpression> Body;

            /// <summary>
            /// Instanciate Expression
            /// </summary>
            /// <param name="nBody">Expressions</param>
            public AstProgram(ref List<AstExpression> nBody) : base(ref AST_NODE._Program)
            {
                Body = nBody;
                _TypeStr = "_Program";
            }

            public override List<string> ToArraylist()
            {
                var lst = base.ToArraylist();
                foreach (var item in Body)
                    lst.AddRange(item.ToArraylist());
                return lst;
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    // AST NODE
    // 
    namespace Ast_ExpressionFactory
    {
        /// <summary>
        /// Ast NodeTypes Used to Describe AST Node 
        /// </summary>
        public enum AST_NODE
        {
            _array = 1,
            _boolean = 2,
            _string = 3,
            _integer = 4,
            _variable = 5,
            _null = 6,
            _endStatement = 7,
            _blockCode = 8,
            _binaryExpression = 9,
            _ParenthesizedExpresion = 10,
            _MultiplicativeExpression = 11,
            _AddativeExpression = 12,
            _assignExpression = 13,
            _Dim = 14,
            _For = 15,
            _If = 16,
            _function = 17,
            _sub = 18,
            _class = 19,
            _else = 20,
            _then = 21,
            _Do_while = 22,
            _Do_until = 23,
            _Program = 24,
            _comments = 25,
            _ExpressionStatement = 26,
            _WhiteSpace = 27,
            _UnknownStatement = 28,
            _Code_Begin = 29,
            _Code_End = 30,
            _emptyStatement = 31,
            _OperationBegin = 32,
            _OperationEnd = 33,
            _ConditionalExpression = 34,
            // Sal token_IDs
            SAL_NULL,
            SAL_REMOVE,
            SAL_RESUME,
            SAL_PUSH,
            SAL_PULL,
            SAL_PEEK,
            SAL_WAIT,
            SAL_PAUSE,
            SAL_HALT,
            SAL_DUP,
            SAL_JMP,
            SAL_JIF_T,
            SAL_JIF_F,
            SAL_JIF_EQ,
            SAL_JIF_GT,
            SAL_JIF_LT,
            SAL_LOAD,
            SAL_STORE,
            SAL_CALL,
            SAL_RET,
            SAL_PRINT_M,
            SAL_PRINT_C,
            SAL_ADD,
            SAL_SUB,
            SAL_MUL,
            SAL_DIV,
            SAL_AND,
            SAL_OR,
            SAL_NOT,
            SAL_IS_EQ,
            SAL_IS_GT,
            SAL_IS_GTE,
            SAL_IS_LT,
            SAL_IS_LTE,
            SAL_TO_POS,
            SAL_TO_NEG,
            SAL_INCR,
            SAL_DECR,
            _SAL_Expression,
            _Sal_Program_title,
            _Sal_BeginStatement,
            _ListBegin,
            _ListEnd,
            _DeclareVariable,
            _TRUE,
            Logo_Expression,
            Logo_Function,
            Logo_name,
            Logo_Value
        }
        /// <summary>
        /// Syntax: 
        /// 
        /// Root Ast node Type
        /// 
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public abstract class AstNode
        {
            /// <summary>
            /// Type Of Node
            /// </summary>
            public AST_NODE _Type;
            /// <summary>
            /// String version of the Type due to not being printed
            /// </summary>
            public string _TypeStr;
            /// <summary>
            /// Start Position
            /// </summary>
            public int _Start;
            /// <summary>
            /// End Position
            /// </summary>
            public int _End;
            /// <summary>
            /// Raw data of token
            /// </summary>
            public string _Raw;
            /// <summary>
            /// All Literals contain values 
            /// All node should be evaluated Except literals which should simply return thier value
            /// 
            /// </summary>
            /// <param name="ParentEnv"></param>
            /// <returns></returns>
            public abstract object GetValue(ref EnvironmentalMemory ParentEnv);
            /// <summary>
            /// Instanciate
            /// </summary>
            /// <param name="ntype">Type of Node</param>
            public AstNode(ref AST_NODE ntype)
            {
                _Type = ntype;
            }

            public virtual List<string> ToArraylist()
            {
                var lst = new List<string>();
                lst.Add(_TypeStr);
                return lst;
            }
            /// <summary>
            /// get raw expression
            /// </summary>
            /// <returns></returns>
            public string GetExpr()
            {
                return _Raw;
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson;
            }
        }
    }
    // AST EXPRESSION
    // 
    #endregion
    #region LITERALS
    namespace Ast_ExpressionFactory
    {

        /// <summary>
        /// Expression Model Used To Group Expressions
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public abstract class AstExpression : AstNode
        {
            public AstExpression(ref AST_NODE ntype) : base(ref ntype)
            {
            }
            /// <summary>
            /// All Expressions Return values but also need to be evaluted ; 
            /// Thier output results should be a value or the environment being return updated with its new values
            /// Expressions such as FOR/DIM/IF/WHILE 
            /// all return a value of true also to signify the completion of the evaluation; 
            /// If the node is not evaluated then it will simply return true but stay unevaluated 
            /// hence always evalating all expressions; 
            /// When designing expressions ; 
            /// thier evaluate function should use either evaluate or getvalue for its subordinate propertys
            /// We shall attempt to evaluate every expression inside of itself to return the values within.
            /// The expression uses the Environment delivered as its own global record;
            /// the environment is returned to the sender 
            /// with any values updated;
            /// This function must be overridden
            /// All 
            /// </summary>
            /// <param name="ParentEnv">sets the environment for the expression; 
            /// the environment contains the current record of variables in use by the global script </param>
            /// <returns></returns>
            public abstract object Evaluate(ref EnvironmentalMemory ParentEnv);
            /// <summary>
            /// Generates a String to be run on the sal virtual machine
            /// The node should be evaluated first to return the values for the function 
            /// then produce the code with the values -Pluged in
            /// </summary>
            /// <param name="ParentEnv"></param>
            /// <returns></returns>
            public abstract string GenerateSalCode(ref EnvironmentalMemory ParentEnv);

            private string GetDebuggerDisplay()
            {
                return ToString();
            }
        }
    }
    // AST LITERAL
    // 
    namespace Ast_ExpressionFactory
    {

        /// <summary>
        /// 
        /// Syntax: 
        /// 
        /// Used to hold Literals and values
        /// 
        /// 
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_Literal : AstNode
        {
            /// <summary>
            /// Holds value (in its type)
            /// </summary>
            public object iLiteral;

            public Ast_Literal(ref AST_NODE ntype) : base(ref ntype)
            {
            }

            public Ast_Literal(ref AST_NODE ntype, ref object nValue) : base(ref ntype)
            {
                iLiteral = nValue;
            }

            public Ast_Literal(ref int nValue) : base(ref AST_NODE._integer)
            {
                iLiteral = 0;
                iLiteral = nValue;
            }

            public Ast_Literal(ref double nValue) : base(ref AST_NODE._integer)
            {
                iLiteral = 0.0d;
                iLiteral = nValue;
            }

            public Ast_Literal(ref string nValue) : base(ref AST_NODE._string)
            {
                iLiteral = "";
                iLiteral = nValue;
            }

            public Ast_Literal(ref bool nValue) : base(ref AST_NODE._boolean)
            {
                iLiteral = false;
                iLiteral = nValue;
            }

            public Ast_Literal(ref List<object> nValue) : base(ref AST_NODE._array)
            {
                iLiteral = new List<object>();
                iLiteral = nValue;
            }

            public override List<string> ToArraylist()
            {
                var lst = base.ToArraylist();
                lst.Add(iLiteral.ToString());
                return lst;
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                switch (_Type)
                {
                    case AST_NODE._integer:
                        {
                            int Obj = 0;
                            Obj = int.Parse(Conversions.ToString(iLiteral));
                            return Obj;
                        }

                    case AST_NODE._string:
                        {
                            string Obj = "";
                            Obj = iLiteral.ToString();
                            return Obj;
                        }

                    case AST_NODE._array:
                        {
                            return iLiteral;
                        }

                    case AST_NODE._boolean:
                        {
                            bool Obj = false;
                            Obj = bool.Parse(Conversions.ToString(iLiteral));
                            return Obj;
                        }

                    default:
                        {
                            return iLiteral;
                        }
                }
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    // AST IDENTIFIER
    // 
    namespace Ast_ExpressionFactory
    {

        /// <summary>
        /// Syntax:
        /// 
        /// Used to hold Varnames or Identifiers
        /// 
        /// 
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_Identifier : Ast_Literal
        {
            public string _Name;

            public Ast_Identifier(ref string nName) : base(ref AST_NODE._variable)
            {
                _TypeStr = "Identifier";
                _Name = nName;
            }

            public override List<string> ToArraylist()
            {
                var lst = base.ToArraylist();
                lst.Add(_Name);
                return lst;
            }

            public bool CheckVar(ref EnvironmentalMemory ParentEnv)
            {
                return ParentEnv.CheckVar(ref _Name);
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                if (ParentEnv.CheckVar(ref _Name) == true)
                {
                    return ParentEnv.GetVar(ref _Name);
                }
                else
                {
                    return null;
                }
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    #endregion
    #region EXPRESSIONS
    // AST EXPRESSION
    // 
    namespace Ast_ExpressionFactory
    {
        /// <summary>
        /// Syntax:
        /// 
        /// Expression Statement Types
        /// 
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_ExpressionStatement : AstExpression
        {
            /// <summary>
            /// Literal Value
            /// </summary>
            public Ast_Literal _iLiteral;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="nValue">Literal Value to be stored </param>
            public Ast_ExpressionStatement(ref Ast_Literal nValue) : base(ref AST_NODE._ExpressionStatement)
            {
                _iLiteral = nValue;
                _TypeStr = "_PrimaryExpression";
                _Start = _iLiteral._Start;
                _End = _iLiteral._End;
                _Raw = nValue._Raw;
            }

            public override List<string> ToArraylist()
            {
                var lst = base.ToArraylist();
                lst.AddRange((IEnumerable<string>)_iLiteral.iLiteral.toarraylist);
                return lst;
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                return GetValue(ref ParentEnv);
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                return _iLiteral.GetValue(ref ParentEnv);
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    // AST VARIABLE EXPRESSION
    // 
    namespace Ast_ExpressionFactory
    {
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_VariableExpressionStatement : AstExpression
        {
            /// <summary>
            /// Literal Value
            /// </summary>
            public Ast_Identifier _iLiteral;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="nValue">Literal Value to be stored </param>
            public Ast_VariableExpressionStatement(ref Ast_Identifier nValue) : base(ref AST_NODE._ExpressionStatement)
            {
                _iLiteral = nValue;
                _TypeStr = "_PrimaryExpression";
                _Start = _iLiteral._Start;
                _End = _iLiteral._End;
                _Raw = nValue._Raw;
            }

            public override List<string> ToArraylist()
            {
                var lst = base.ToArraylist();
                lst.AddRange(_iLiteral.ToArraylist());
                return lst;
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                return GetValue(ref ParentEnv);
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                return _iLiteral.GetValue(ref ParentEnv);
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    // AST UNARY EXPRESSION
    // 
    namespace Ast_ExpressionFactory
    {
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_UnaryExpression : AstExpression
        {
            public Ast_Literal _Value;
            public Ast_Identifier _name;
            // Public _Value As AstExpression
            public Ast_UnaryExpression(ref Ast_Identifier nName, ref Ast_Literal nValue) : base(ref AST_NODE._ExpressionStatement)
            {
                _Value = nValue;
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                return GetValue(ref ParentEnv);
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                return ParentEnv;
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    // AST BINARY EXPRESSION
    // 
    namespace Ast_ExpressionFactory
    {
        /// <summary>
        /// Used for Binary Operations
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class AstBinaryExpression : AstExpression
        {
            public AstExpression _Left;
            public AstExpression _Right;
            public string _Operator;
            public EnvironmentalMemory LocalEnvironment = new EnvironmentalMemory();

            public AstBinaryExpression(ref AST_NODE nType, ref AstExpression nLeft, ref string nOperator, ref AstExpression nRight) : base(ref nType)
            {
                _Left = nLeft;
                _Right = nRight;
                _Operator = nOperator;
                _Raw = nLeft._Raw + nOperator + nRight._Raw;
                _Start = nLeft._Start;
                _End = nRight._End;
                _TypeStr = "BinaryExpression";
            }

            public override List<string> ToArraylist()
            {
                var lst = base.ToArraylist();
                lst.Add(_Operator);
                lst.AddRange(_Left.ToArraylist());
                lst.AddRange(_Right.ToArraylist());
                return lst;
            }

            public AstBinaryExpression(ref AST_NODE nType, ref Ast_VariableDeclarationExpression nLeft, ref string nOperator, ref AstExpression nRight) : base(ref nType)
            {
                _Left = nLeft;
                _Right = nRight;
                _Operator = nOperator;
                _Raw = nLeft._Raw + nOperator + nRight._Raw;
                _Start = nLeft._Start;
                _End = nRight._End;
                _TypeStr = "BinaryExpression";
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                switch (_Operator ?? "")
                {
                    // Mathmatical
                    case "+":
                        {
                            Ast_Literal argLeft = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateAddative(ref argLeft, ref _Operator, ref argRight, ref ParentEnv);
                        }

                    case "-":
                        {
                            Ast_Literal argLeft1 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight1 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateAddative(ref argLeft1, ref _Operator, ref argRight1, ref ParentEnv);
                        }

                    case "*":
                        {
                            Ast_Literal argLeft2 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight2 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateMultiplicative(ref argLeft2, ref _Operator, ref argRight2, ref ParentEnv);
                        }

                    case "/":
                        {
                            Ast_Literal argLeft3 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight3 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateMultiplicative(ref argLeft3, ref _Operator, ref argRight3, ref ParentEnv);
                        }
                    // Relational

                    case ">=":
                        {
                            Ast_Literal argLeft4 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight4 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateBoolean(ref argLeft4, ref _Operator, ref argRight4, ref ParentEnv);
                        }

                    case "<=":
                        {
                            Ast_Literal argLeft5 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight5 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateBoolean(ref argLeft5, ref _Operator, ref argRight5, ref ParentEnv);
                        }

                    case ">":
                        {
                            Ast_Literal argLeft6 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight6 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateBoolean(ref argLeft6, ref _Operator, ref argRight6, ref ParentEnv);
                        }

                    case "<":
                        {
                            Ast_Literal argLeft7 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight7 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateBoolean(ref argLeft7, ref _Operator, ref argRight7, ref ParentEnv);
                        }

                    case "=":
                        {
                            Ast_Literal argLeft8 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight8 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateBoolean(ref argLeft8, ref _Operator, ref argRight8, ref ParentEnv);
                        }

                    // Complex assign
                    case "+=":
                        {
                            Ast_Literal argLeft9 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight9 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateComplex(ref argLeft9, ref _Operator, ref argRight9, ref ParentEnv);
                        }

                    case "-=":
                        {
                            Ast_Literal argLeft10 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight10 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateComplex(ref argLeft10, ref _Operator, ref argRight10, ref ParentEnv);
                        }

                    case "*=":
                        {
                            Ast_Literal argLeft11 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight11 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateComplex(ref argLeft11, ref _Operator, ref argRight11, ref ParentEnv);
                        }

                    case "/=":
                        {
                            Ast_Literal argLeft12 = (Ast_Literal)_Left.GetValue(ref ParentEnv);
                            Ast_Literal argRight12 = (Ast_Literal)_Right.GetValue(ref ParentEnv);
                            return EvaluateComplex(ref argLeft12, ref _Operator, ref argRight12, ref ParentEnv);
                        }
                }

                return ParentEnv;
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                LocalEnvironment = ParentEnv;
                return GetValue(ref ParentEnv);
            }
            /// <summary>
            /// Allows for evaluation of the node : Imeadialty invoked expression
            /// </summary>
            /// <param name="Left"></param>
            /// <param name="iOperator"></param>
            /// <param name="Right"></param>
            /// <returns></returns>
            private int EvaluateMultiplicative(ref Ast_Literal Left, ref string iOperator, ref Ast_Literal Right, ref EnvironmentalMemory ParentEnv)
            {
                if (Left._Type == AST_NODE._integer & Right._Type == AST_NODE._integer)
                {
                    switch (iOperator ?? "")
                    {
                        case "*":
                            {
                                return Conversions.ToInteger(Operators.MultiplyObject(Left.GetValue(ref ParentEnv), Right.GetValue(ref ParentEnv)));
                            }

                        case "/":
                            {
                                return Conversions.ToInteger(Operators.DivideObject(Left.GetValue(ref ParentEnv), Right.GetValue(ref ParentEnv)));
                            }
                    }
                }

                return Conversions.ToInteger(Left.GetValue(ref ParentEnv));
            }

            private int EvaluateAddative(ref Ast_Literal Left, ref string iOperator, ref Ast_Literal Right, ref EnvironmentalMemory ParentEnv)
            {
                if (Left._Type == AST_NODE._integer & Right._Type == AST_NODE._integer)
                {
                    switch (iOperator ?? "")
                    {
                        case "+":
                            {
                                return Conversions.ToInteger(Operators.AddObject(Left.GetValue(ref ParentEnv), Right.GetValue(ref ParentEnv)));
                            }

                        case "-":
                            {
                                return Conversions.ToInteger(Operators.SubtractObject(Left.GetValue(ref ParentEnv), Right.GetValue(ref ParentEnv)));
                            }
                    }
                }

                return Conversions.ToInteger(Left.GetValue(ref ParentEnv));
            }
            /// <summary>
            /// Evaluate node values ( imeadiatly invoked expression )
            /// </summary>
            /// <param name="Left"></param>
            /// <param name="iOperator"></param>
            /// <param name="Right"></param>
            /// <returns></returns>
            private bool EvaluateBoolean(ref Ast_Literal Left, ref string iOperator, ref Ast_Literal Right, ref EnvironmentalMemory ParentEnv)
            {
                switch (iOperator ?? "")
                {
                    case ">=":
                        {
                            return Conversions.ToBoolean(Operators.ConditionalCompareObjectGreaterEqual(Left.GetValue(ref ParentEnv), Right.GetValue(ref ParentEnv), false));
                        }

                    case "<=":
                        {
                            return Conversions.ToBoolean(Operators.ConditionalCompareObjectLessEqual(Left.GetValue(ref ParentEnv), Right.GetValue(ref ParentEnv), false));
                        }

                    case ">":
                        {
                            return Conversions.ToBoolean(Operators.ConditionalCompareObjectGreater(Left.GetValue(ref ParentEnv), Right.GetValue(ref ParentEnv), false));
                        }

                    case "<":
                        {
                            return Conversions.ToBoolean(Operators.ConditionalCompareObjectLess(Left.GetValue(ref ParentEnv), Right.GetValue(ref ParentEnv), false));
                        }

                    case "=":
                        {
                            return Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(Left.GetValue(ref ParentEnv), Right.GetValue(ref ParentEnv), false));
                        }
                }

                return Conversions.ToBoolean(Left.GetValue(ref ParentEnv));
            }

            private int EvaluateComplex(ref Ast_Literal Left, ref string iOperator, ref Ast_Literal Right, ref EnvironmentalMemory ParentEnv)
            {
                if (Left._Type == AST_NODE._integer & Right._Type == AST_NODE._integer)
                {
                    switch (iOperator ?? "")
                    {
                        case "+=":
                            {
                                int lf = int.Parse(Conversions.ToString(Left.GetValue(ref ParentEnv)));
                                int rt = int.Parse(Conversions.ToString(Right.GetValue(ref ParentEnv)));
                                lf += rt;
                                return lf;
                            }

                        case "-=":
                            {
                                int lf = int.Parse(Conversions.ToString(Left.GetValue(ref ParentEnv)));
                                int rt = int.Parse(Conversions.ToString(Right.GetValue(ref ParentEnv)));
                                lf -= rt;
                                return lf;
                            }

                        case "*=":
                            {
                                int lf = int.Parse(Conversions.ToString(Left.GetValue(ref ParentEnv)));
                                int rt = int.Parse(Conversions.ToString(Right.GetValue(ref ParentEnv)));
                                lf *= rt;
                                return lf;
                            }

                        case "/=":
                            {
                                int lf = int.Parse(Conversions.ToString(Left.GetValue(ref ParentEnv)));
                                int rt = int.Parse(Conversions.ToString(Right.GetValue(ref ParentEnv)));
                                lf = (int)Math.Round(lf / (double)rt);
                                return lf;
                            }
                    }
                }

                return int.Parse(Conversions.ToString(Left.GetValue(ref ParentEnv)));
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }
        }
    }

    // AST BLOCK EXPRESSION
    // 
    namespace Ast_ExpressionFactory
    {
        /// <summary>
        /// Used for CodeBlocks
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_BlockExpression : AstExpression
        {
            public List<AstExpression> Body;
            public List<AstExpression> _ReturnValues = new List<AstExpression>();
            public bool _hasReturn = false;

            public Ast_BlockExpression(ref List<AstExpression> iBody) : base(ref AST_NODE._blockCode)
            {
                _TypeStr = "_blockCode";
                Body = iBody;
                _Start = iBody[0]._Start;
                foreach (var item in iBody)
                    _Raw += "," + item._Raw;
                _End = iBody[iBody.Count - 1]._End;
                _ReturnValues = new List<AstExpression>();
                _hasReturn = false;
            }

            public Ast_BlockExpression(ref List<AstExpression> iBody, ref List<AstExpression> iReturnValues) : base(ref AST_NODE._blockCode)
            {
                _TypeStr = "_blockCode";
                Body = iBody;
                _Start = iBody[0]._Start;
                foreach (var item in iBody)
                    _Raw += item._Raw;
                _End = iBody[iBody.Count - 1]._End;
                _ReturnValues = iReturnValues;
                _hasReturn = true;
            }

            public override List<string> ToArraylist()
            {
                var lst = base.ToArraylist();
                foreach (var item in Body)
                    lst.AddRange(item.ToArraylist());
                lst.Add(_hasReturn.ToString());
                foreach (var item in _ReturnValues)
                    lst.AddRange(item.ToArraylist());
                return lst;
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                return GetValue(ref ParentEnv);
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                if (_hasReturn == false)
                {
                    foreach (var item in Body)
                        item.Evaluate(ref ParentEnv);
                    return ParentEnv;
                }
                else
                {
                    var Values = new List<object>();
                    foreach (var item in _ReturnValues)
                        Values.Add(item.Evaluate(ref ParentEnv));
                    return _ReturnValues;
                }
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    // AST PARENTESIZED EXPRESSION   
    // 
    namespace Ast_ExpressionFactory
    {
        /// <summary>
        /// syntax:
        /// -_ParenthesizedExpresion
        /// 
        /// 
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_ParenthesizedExpresion : AstExpression
        {
            public List<AstExpression> Body;

            public Ast_ParenthesizedExpresion(ref List<AstExpression> iBody) : base(ref AST_NODE._ParenthesizedExpresion)
            {
                _TypeStr = "_ParenthesizedExpresion";
                Body = iBody;
                _Start = iBody[0]._Start;
                foreach (var item in iBody)
                    _Raw += item._Raw;
                _End = iBody[iBody.Count - 1]._End;
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                return GetValue(ref ParentEnv);
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                foreach (var item in Body)
                    item.Evaluate(ref ParentEnv);
                return ParentEnv;
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    #endregion
    #region FUNCTIONS
    // AST VARIABLE DECLARATION EXPRESSION
    // 
    namespace Ast_ExpressionFactory
    {
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_VariableDeclarationExpression : Ast_VariableExpressionStatement
        {
            public AST_NODE _LiteralType;
            public string _LiteralTypeStr;

            public Ast_VariableDeclarationExpression(ref Ast_Identifier nValue, ref AST_NODE iLiteralType) : base(ref nValue)
            {
                _LiteralType = iLiteralType;
                _Type = AST_NODE._DeclareVariable;
                _TypeStr = "_VariableDeclaration";
                switch (iLiteralType)
                {
                    case AST_NODE._string:
                        {
                            _LiteralTypeStr = "_string";
                            break;
                        }

                    case AST_NODE._array:
                        {
                            _LiteralTypeStr = "_array";
                            break;
                        }

                    case AST_NODE._integer:
                        {
                            _LiteralTypeStr = "_integer";
                            break;
                        }

                    case AST_NODE._boolean:
                        {
                            _LiteralTypeStr = "_boolean";
                            break;
                        }

                    default:
                        {
                            _LiteralTypeStr = null;
                            break;
                        }
                }
            }

            public Ast_VariableDeclarationExpression(ref Ast_VariableExpressionStatement nValue, ref AST_NODE iLiteralType) : base(ref nValue._iLiteral)
            {
                _LiteralType = iLiteralType;
                _Type = AST_NODE._DeclareVariable;
                _TypeStr = "_VariableDeclaration";
                switch (iLiteralType)
                {
                    case AST_NODE._string:
                        {
                            _LiteralTypeStr = "_string";
                            break;
                        }

                    case AST_NODE._array:
                        {
                            _LiteralTypeStr = "_array";
                            break;
                        }

                    case AST_NODE._integer:
                        {
                            _LiteralTypeStr = "_integer";
                            break;
                        }

                    default:
                        {
                            _LiteralTypeStr = "_null";
                            break;
                        }
                }
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                if (ParentEnv.CheckVar(ref _iLiteral._Name) == false)
                {
                    switch (_LiteralType)
                    {
                        case AST_NODE._null:
                            {
                                object argValue = null;
                                ParentEnv.AssignValue(ref _iLiteral._Name, ref argValue);
                                break;
                            }

                        case AST_NODE._integer:
                            {
                                object argValue1 = 0;
                                ParentEnv.AssignValue(ref _iLiteral._Name, ref argValue1);
                                break;
                            }

                        case AST_NODE._string:
                            {
                                object argValue2 = "";
                                ParentEnv.AssignValue(ref _iLiteral._Name, ref argValue2);
                                break;
                            }

                        case AST_NODE._array:
                            {
                                object argValue3 = new List<object>();
                                ParentEnv.AssignValue(ref _iLiteral._Name, ref argValue3);
                                break;
                            }
                    }
                }

                return _iLiteral.GetValue(ref ParentEnv);
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    // AST ASSIGNMENT EXPRESSION
    // 
    namespace Ast_ExpressionFactory
    {
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_AssignmentExpression : AstExpression
        {
            /// <summary>
            /// Must be var literal type
            /// </summary>
            public Ast_Identifier _Left;
            public AstExpression _Right;
            public string _Operator;

            public Ast_AssignmentExpression(ref Ast_Identifier nLeft, ref string nOperator, ref AstExpression nRight) : base(ref AST_NODE._assignExpression)
            {
                _Left = nLeft;
                _Right = nRight;
                _Operator = nOperator;
                _Raw = nLeft._Raw + nOperator + nRight._Raw;
                _Start = nLeft._Start;
                _End = nRight._End;
                _TypeStr = "_assignExpression";
            }

            public override List<string> ToArraylist()
            {
                var lst = base.ToArraylist();
                lst.Add(_Operator);
                lst.Add(_Left._Name.ToString());
                lst.AddRange(_Right.ToArraylist());
                return lst;
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                return GetValue(ref ParentEnv);
            }

            public EnvironmentalMemory.Variable GetVar(ref EnvironmentalMemory ParentEnv)
            {
                var nvar = new EnvironmentalMemory.Variable();
                nvar.Value = _Right.Evaluate(ref ParentEnv);
                nvar.Name = _Left._Name;
                nvar.Type = _Type;
                return nvar;
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                var nvar = new EnvironmentalMemory.Variable();
                nvar.Value = _Right.Evaluate(ref ParentEnv);
                nvar.Name = _Left._Name;
                nvar.Type = _Type;
                if (ParentEnv.CheckVar(ref nvar.Name) == true)
                {
                    return _Right.Evaluate(ref ParentEnv);
                }

                return null;
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    // AST DO EXPRESSION
    // 
    namespace Ast_ExpressionFactory
    {
        // DO_ While/True Until/True
        // while (x > 10) {      x -= 1;    } 
        // Until (x > 10) {      x -= 1;    } 
        /// <summary>
        /// 
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_DoExpression : AstExpression
        {
            public Ast_BlockExpression Body;
            public AstBinaryExpression Test;
            public bool Expected;

            public Ast_DoExpression(ref AST_NODE ntype, ref AstBinaryExpression iTest, ref Ast_BlockExpression iBody, bool iExpected = true) : base(ref ntype)
            {
                Body = iBody;
                Expected = iExpected;
                Test = iTest;
                switch (_Type)
                {
                    case AST_NODE._Do_until:
                        {
                            _TypeStr = "_Do_until";
                            break;
                        }

                    case AST_NODE._Do_while:
                        {
                            _TypeStr = "_Do_while";
                            break;
                        }
                }

                Body = iBody;
                Body._hasReturn = true;
                var argnValue = new Ast_Literal(ref AST_NODE._TRUE);
                Body._ReturnValues.Add(new Ast_ExpressionStatement(ref argnValue));
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                return GetValue(ref ParentEnv);
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                bool MyTest = false;
                switch (_Type)
                {
                    case AST_NODE._Do_until:
                        {
                            while (MyTest != Expected)
                            {
                                MyTest = Conversions.ToBoolean(Test.Evaluate(ref ParentEnv));
                                foreach (var item in Body.Body)
                                    item.Evaluate(ref ParentEnv);
                            }

                            break;
                        }

                    case AST_NODE._Do_while:
                        {
                            while (MyTest == Expected)
                            {
                                MyTest = Conversions.ToBoolean(Test.Evaluate(ref ParentEnv));
                                foreach (var item in Body.Body)
                                    item.Evaluate(ref ParentEnv);
                            }

                            break;
                        }
                }

                return true;
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    // AST FOR EXPRESSION
    // 
    namespace Ast_ExpressionFactory
    {

        // for (dim i = 0); (i < 10); (i += 1) {      x += i;    }
        // for (Init); (Test); (Increment) { <BODY >    x += i;    }
        /// <summary>
        /// 
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_ForExpression : AstExpression
        {
            public Ast_VariableDeclarationExpression Init;
            public AstBinaryExpression Test;
            public AstExpression Increment;
            public Ast_BlockExpression Body;

            public Ast_ForExpression(ref Ast_VariableDeclarationExpression Init, ref AstBinaryExpression Test, ref AstExpression Increment, ref Ast_BlockExpression Body) : base(ref AST_NODE._For)
            {
                Body._hasReturn = true;
                var argnValue = new Ast_Literal(ref AST_NODE._TRUE);
                Body._ReturnValues.Add(new Ast_ExpressionStatement(ref argnValue));
            }
            // Always 
            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                foreach (var item in Body.Body)
                    item.Evaluate(ref ParentEnv);
                return GetValue(ref ParentEnv);
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                return true;
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return ToString();
            }
        }
    }
    // AST IF EXPRESSION
    // 
    namespace Ast_ExpressionFactory
    {
        // if (x>7) then { x = 1; } else { x = 2; }
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_IfExpression : AstExpression
        {
            public AstExpression Test;
            public Ast_BlockExpression Consequent;
            public Ast_BlockExpression Alternate;

            public Ast_IfExpression(ref AST_NODE ntype) : base(ref ntype)
            {
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    #endregion
    #endregion


    // THE SPYDAZWEB ASSEMBLY LANGUAGE - LOW LEVEL
    // 
    #region SAL_ASSEMBLY LANG
    // AST LITERALS
    // 
    namespace Ast_ExpressionFactory
    {
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_SAL_Literal : Ast_Literal
        {
            public Ast_SAL_Literal(ref AST_NODE ntype) : base(ref ntype)
            {
            }

            public Ast_SAL_Literal(ref AST_NODE ntype, ref object nValue) : base(ref ntype, ref nValue)
            {
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    // AST EXPRESSIONS
    // 
    namespace Ast_ExpressionFactory
    {
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_SalExpression : AstExpression
        {
            public List<Ast_Literal> Program;

            public Ast_SalExpression(ref List<Ast_Literal> nProgram) : base(ref AST_NODE._SAL_Expression)
            {
                _TypeStr = "_SAL_Expression";
                Program = nProgram;
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                string Str = "";
                foreach (var item in Program)
                    Str += " " + item._Raw;
                return Str;
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }
    }
    #endregion

}

namespace SDK.SmallProgLang
{
    namespace Ast_ExpressionFactory
    {
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_Logo_Value : Ast_Literal
        {
            public Ast_Logo_Value(ref AST_NODE ntype) : base(ref ntype)
            {
            }

            public Ast_Logo_Value(ref int nValue) : base(ref nValue)
            {
                // _TypeStr = AST_NODE.Logo_Value
            }

            public Ast_Logo_Value(ref double nValue) : base(ref nValue)
            {
                // _TypeStr = AST_NODE.Logo_Value
            }

            public Ast_Logo_Value(ref string nValue) : base(ref nValue)
            {
                // _TypeStr = AST_NODE.Logo_Value
            }

            public Ast_Logo_Value(ref bool nValue) : base(ref nValue)
            {
                // _TypeStr = AST_NODE.Logo_Value
            }

            public Ast_Logo_Value(ref List<object> nValue) : base(ref nValue)
            {
                // _TypeStr = AST_NODE.Logo_Value
            }

            public Ast_Logo_Value(ref AST_NODE ntype, ref object nValue) : base(ref ntype, ref nValue)
            {
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }

        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_LogoIdentifer : Ast_Identifier
        {
            public Ast_LogoIdentifer(ref string nName) : base(ref nName)
            {
                _TypeStr = "_LogoIdentifer";
                _Type = AST_NODE.Logo_name;
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson();
            }
        }

        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_LogoCmdExpression : AstExpression
        {
            public Ast_Identifier _Left_Cmd;
            public Ast_Literal _Right_Value;

            public Ast_LogoCmdExpression(ref AST_NODE ntype, ref Ast_Identifier _left, Ast_Literal _Right) : base(ref ntype)
            {
                _TypeStr = "_LogoCmdExpression";
                _Left_Cmd = _left;
                _Right_Value = _Right;
                _Raw = _left._Raw + " " + _Right._Raw;
            }

            /// <summary>
            /// Evaluates the expression and returns the value
            /// 
            /// </summary>
            /// <param name="ParentEnv"></param>
            /// <returns></returns>
            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                return GetValue(ref ParentEnv);
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                return Operators.ConcatenateObject(_Left_Cmd._Name + " ", _Right_Value.GetValue(ref ParentEnv));
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            private string GetDebuggerDisplay()
            {
                return ToString();
            }
        }

        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_logoEvaluation : AstBinaryExpression
        {
            public Ast_logoEvaluation(ref AST_NODE nType, ref AstExpression nLeft, ref string nOperator, ref AstExpression nRight) : base(ref nType, ref nLeft, ref nOperator, ref nRight)
            {
                _TypeStr = "_logoEvaluation";
            }
        }

        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Ast_Logo_Expression : AstExpression
        {
            public Ast_Literal _value;

            public Ast_Logo_Expression(ref AstNode ivalue) : base(ref AST_NODE.Logo_Expression)
            {
                _TypeStr = "_Logo_Expression";
                _value = (Ast_Literal)ivalue;
            }

            public override object Evaluate(ref EnvironmentalMemory ParentEnv)
            {
                return GetValue(ref ParentEnv);
            }

            public override string GenerateSalCode(ref EnvironmentalMemory ParentEnv)
            {
                throw new NotImplementedException();
            }

            public override object GetValue(ref EnvironmentalMemory ParentEnv)
            {
                return _value.iLiteral;
            }

            private string GetDebuggerDisplay()
            {
                return ToString();
            }
        }
    }
}
#endregion
// GRAMMAR FACTORY
// 
// 
#region THE TOKENIZER GRAMMAR FACTORY
// GRAMMAR FACTORY
// 
// 
namespace SDK.SmallProgLang
{
    // GRAMMAR
    // 
    namespace GrammarFactory
    {
        /// <summary>
        /// GRAMMAR OBJECT ID
        /// </summary>
        public enum Type_Id
        {
            /// <summary>
            /// Literal
            /// </summary>
            _INTEGER,
            /// <summary>
            /// Literal
            /// </summary>
            _STRING,
            /// <summary>
            /// Print Literal/Value/String
            /// </summary>
            _PRINT,
            /// <summary>
            /// Declare Var
            /// </summary>
            _DIM,
            /// <summary>
            /// Begin Iteration of list
            /// For (Iterator = (Increment to Completion)
            /// </summary>
            _FOR,
            /// <summary>
            /// Additional = Step +1
            /// </summary>
            _EACH,
            /// <summary>
            /// From item in list
            /// </summary>
            _IN,
            /// <summary>
            /// End of iteration marker
            /// </summary>
            _TO,
            /// <summary>
            /// Increment Iterator
            /// </summary>
            _NEXT,
            /// <summary>
            /// If Condition = Outcome Then (code) Else (code)
            /// </summary>
            _IF,
            /// <summary>
            /// Then (block)
            /// </summary>
            _THEN,
            /// <summary>
            /// Else (Block)
            /// </summary>
            _ELSE,
            /// <summary>
            /// Until Condition = true
            /// </summary>
            _UNTIL,
            /// <summary>
            /// While Condition = true
            /// </summary>
            _WHILE,
            /// <summary>
            /// Signify begining of Do...While/Until
            /// </summary>
            _DO,
            _RETURN,
            _FUNCTION,
            _SUB,
            _CLASS,
            _NEW,
            /// <summary>
            /// Used in Declaration Assignment
            /// Left (var) assign as (LiteralType)
            /// </summary>
            _AS,
            /// <summary>
            /// End of While loop (marker)(check expression Condition)
            /// </summary>
            _LOOP,
            /// <summary>
            /// xLeft = output of right (9+4) (+= 9) (-=2) (3) (true)
            /// </summary>
            _SIMPLE_ASSIGN,
            /// <summary>
            /// xLeft assigns output of right -=(9+4) (+= 9) (-=2) (3) (true)
            /// Complex assign ... x=x+(ouput)x=x-(ouput) etc
            /// </summary>
            _COMPLEX_ASSIGN,
            /// <summary>
            /// Boolean Literal Env Variable
            /// </summary>
            _TRUE,
            /// <summary>
            /// Boolean Literal - Env Variable
            /// </summary>
            _FALSE,
            /// <summary>
            /// Boolean literal -Env Variable
            /// </summary>
            _NULL,
            /// <summary>
            /// Used for Args List (Lists) = Arrays
            /// Args are lists of Vars (function Environment Vars)
            /// </summary>
            _LIST_BEGIN,
            /// <summary>
            /// End of List
            /// </summary>
            _LIST_END,
            /// <summary>
            /// Used for Blocks of code
            /// </summary>
            _CODE_BEGIN,
            /// <summary>
            /// End of Code block
            /// </summary>
            _CODE_END,
            /// <summary>
            /// Used for operation blocks as well as 
            /// ordering prioritizing evals
            /// Begin
            /// </summary>
            _CONDITIONAL_BEGIN,
            /// <summary>
            /// End of Condition
            /// </summary>
            _CONDITIONAL_END,
            /// <summary>
            /// - AND
            /// </summary>
            _LOGICAL_AND,
            /// <summary>
            /// | OR
            /// </summary>
            _LOGICAL_OR,
            /// <summary>
            /// ! NOT
            /// </summary>
            _LOGICAL_NOT,
            /// <summary>
            /// Greater than / Less Than
            /// </summary>
            _RELATIONAL_OPERATOR,
            /// <summary>
            /// +-
            /// </summary>
            _ADDITIVE_OPERATOR,
            /// <summary>
            /// */
            /// </summary>
            _MULTIPLICATIVE_OPERATOR,
            /// <summary>
            /// ;
            /// </summary>
            _STATEMENT_END,
            /// <summary>
            /// end of file
            /// </summary>
            _EOF,
            /// <summary>
            /// Bad / Unrecognized token
            /// </summary>
            _BAD_TOKEN,
            /// <summary>
            /// Seperates items in list
            /// </summary>
            _LIST_SEPERATOR,
            /// <summary>
            /// !=
            /// </summary>
            _NOT_EQUALS,
            /// <summary>
            /// DECLARE VAR 
            /// </summary>
            _VARIABLE_DECLARE,
            // Sal token_IDs
            SAL_NULL,
            SAL_REMOVE,
            SAL_RESUME,
            SAL_PUSH,
            SAL_PULL,
            SAL_PEEK,
            SAL_WAIT,
            SAL_PAUSE,
            SAL_HALT,
            SAL_DUP,
            SAL_JMP,
            SAL_JIF_T,
            SAL_JIF_F,
            SAL_JIF_EQ,
            SAL_JIF_GT,
            SAL_JIF_LT,
            SAL_LOAD,
            SAL_STORE,
            SAL_CALL,
            SAL_RET,
            SAL_PRINT_M,
            SAL_PRINT_C,
            SAL_ADD,
            SAL_SUB,
            SAL_MUL,
            SAL_DIV,
            SAL_AND,
            SAL_OR,
            SAL_NOT,
            SAL_IS_EQ,
            SAL_IS_GT,
            SAL_IS_GTE,
            SAL_IS_LT,
            SAL_IS_LTE,
            SAL_TO_POS,
            SAL_TO_NEG,
            SAL_INCR,
            SAL_DECR,
            _SAL_PROGRAM_BEGIN,
            _SAL_EXPRESSION_BEGIN,
            _PL_PROGRAM_BEGIN,
            _Def,
            _EQUALITY,
            _JSON_digit,
            _JSON_esc,
            _JSON_int,
            _JSON_exp,
            _JSON_frac,
            _FUNCTION_DECLARE,
            _DOT,
            _OBJ_string,
            _OBJ_integer,
            _OBJ_boolean,
            _OBJ_array,
            _OBJ_null,
            /// <summary>
            /// Literal
            /// </summary>
            _VARIABLE,
            _WHITESPACE,
            _COMMENTS,
            // LOGO
            _repeat,
            LOGO_fd,
            LOGO_bk,
            LOGO_rt,
            LOGO_lt,
            LOGO_cs,
            LOGO_pu,
            LOGO_pd,
            LOGO_ht,
            LOGO_st,
            LOGO_deref,
            LOGO_home,
            LOGO_label,
            LOGO_setxy,
            LOGO_make,
            LOGO_procedureInvocation,
            LOGO_procedureDeclaration,
            LOGO_parameterDeclarations,
            LOGO_comparison,
            LOGO_comparisonOperator,
            LOGO_ife,
            LOGO_Stop,
            LOGO_fore,
            LOGO_LANG,
            LOGO_signExpression,
            LOGO_multiplyingExpression,
            LOGO_expression,
            LOGO_EOL,
            LOGO_number,
            LOGO_name
        }
        /// <summary>
        /// Simple Gramar object (Expected token Shape or from)
        /// </summary>
        public class Grammar
        {

            /// <summary>
            /// Identifier
            /// </summary>
            public Type_Id ID;
            /// <summary>
            /// RegEx Expression to search
            /// </summary>
            public string Exp;

            public static List<Grammar> GetExtendedGrammar()
            {
                var lst = new List<Grammar>();
                lst.AddRange(SALGrammar.GetSALGrammar());
                lst.AddRange(PLGrammar.GetPLGrammar());
                lst.AddRange(BaseGrammar.GetLogicGrammar());
                lst.AddRange(BaseGrammar.GetSymbolsGrammar());
                lst.AddRange(BaseGrammar.GetLiteralsGrammar());
                lst.AddRange(LogoGrammar.GetLOGOGrammar());
                return lst;
            }
            /// <summary>
            /// Still Developing grammar:
            /// </summary>
            /// <returns></returns>
            public static List<Grammar> GetJsonGrammar()
            {
                var iSpec = new List<Grammar>();
                var NewGram = new Grammar();

                // "tokens" "STRING NUMBER { } [ ] , : TRUE FALSE NULL",
                // "start": "JSONText",

                // '"JSONString" [[ "STRING", "$$ = yytext;" ]],

                // '"JSONNumber" [[ "NUMBER", "$$ = Number(yytext);" ]],

                // '"JSONNullLiteral" [[ "NULL", "$$ = null;" ]],

                // '"JSONBooleanLiteral" [[ "TRUE", "$$ = true;" ],
                // '                       [ "FALSE", "$$ = false;" ]],


                // '"JSONText" [[ "JSONValue", "return $$ = $1;" ]],

                // '"JSONValue" [[ "JSONNullLiteral",    "$$ = $1;" ],
                // '              [ "JSONBooleanLiteral", "$$ = $1;" ],
                // '              [ "JSONString",         "$$ = $1;" ],
                // '              [ "JSONNumber",         "$$ = $1;" ],
                // '              [ "JSONObject",         "$$ = $1;" ],
                // '              [ "JSONArray",          "$$ = $1;" ]],

                // '"JSONObject" [[ "{ }", "$$ = {};" ],
                // '               [ "{ JSONMemberList }", "$$ = $2;" ]],

                // '"JSONMember" [[ "JSONString : JSONValue", "$$ = [$1, $3];" ]],

                // '"JSONMemberList" [[ "JSONMember", "$$ = {}; $$[$1[0]] = $1[1];" ],
                // '                   [ "JSONMemberList , JSONMember", "$$ = $1; $1[$3[0]] = $3[1];" ]],

                // '"JSONArray" [[ "[ ]", "$$ = [];" ],
                // '              [ "[ JSONElementList ]", "$$ = $2;" ]],

                // '"JSONElementList" [[ "JSONValue", "$$ = [$1];" ],
                // '                    [ "JSONElementList , JSONValue", "$$ = $1; $1.push($3);" ]]


                // ["\\s+", "/* skip whitespace */"],
                // ["{int}{frac}?{exp}?\\b", "return 'NUMBER';"],
                // ["\"(?:{esc}[\"bfnrt/{esc}]|{esc}u[a-fA-F0-9]{4}|[^\"{esc}])*\"", "yytext = yytext.substr(1,yyleng-2); return 'STRING';"],
                // ["\\{", "return '{'"],
                // ["\\}", "return '}'"],
                // ["\\[", "return '['"],
                // ["\\]", "return ']'"],
                // [",", "return ','"],
                // [":", "return ':'"],
                // ["true\\b", "return 'TRUE'"],
                // ["false\\b", "return 'FALSE'"],
                // ["null\\b", "return 'NULL'"]


                NewGram = new Grammar();
                NewGram.ID = Type_Id._JSON_digit;
                NewGram.Exp = "^[0-9]";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._JSON_esc;
                NewGram.Exp = @"^\\\\";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._JSON_int;
                NewGram.Exp = "^-?(?:[0-9]|[1-9][0-9]+)";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._JSON_exp;
                NewGram.Exp = "(?:[eE][-+]?[0-9]+)";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._JSON_frac;
                NewGram.Exp = "(?:[eE][-+]?[0-9]+)";
                iSpec.Add(NewGram);
                return iSpec;
            }
        }
    }
    // 
    public class BaseGrammar
    {
        public static List<Grammar> GetSymbolsGrammar()
        {
            var iSpec = new List<Grammar>();
            var NewGram = new Grammar();
            #region Seperators
            // BLOCK CODE: LEFT BOUNDRY
            NewGram = new Grammar();
            NewGram.ID = Type_Id._CODE_BEGIN;
            NewGram.Exp = @"^\{";
            iSpec.Add(NewGram);
            // BLOCK CODE: RIGHT BOUNDRY
            NewGram = new Grammar();
            NewGram.ID = Type_Id._CODE_END;
            NewGram.Exp = @"^\}";
            iSpec.Add(NewGram);
            // END STATEMENT or EMPTY STATEMENT
            // EMPTY CODE BLOCKS CONTAIN (1 EMPTY STATEMENT)
            NewGram = new Grammar();
            NewGram.ID = Type_Id._STATEMENT_END;
            NewGram.Exp = @"^\;";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._LIST_SEPERATOR;
            NewGram.Exp = @"^\,";
            iSpec.Add(NewGram);
            // ARGS LIST : LEFT BOUNDRY
            NewGram = new Grammar();
            NewGram.ID = Type_Id._LIST_BEGIN;
            NewGram.Exp = @"^\[";
            iSpec.Add(NewGram);
            // ARGS LIST: RIGHT BOUNDRY
            NewGram = new Grammar();
            NewGram.ID = Type_Id._LIST_END;
            NewGram.Exp = @"^\]";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._DOT;
            NewGram.Exp = @"^\\.";
            iSpec.Add(NewGram);

            #endregion
            return iSpec;
        }

        public static List<Grammar> GetLogicGrammar()
        {
            var iSpec = new List<Grammar>();
            var NewGram = new Grammar();


            // logical(boolean) - Literal
            NewGram = new Grammar();
            NewGram.ID = Type_Id._TRUE;
            NewGram.Exp = @"^\btrue\b";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._FALSE;
            NewGram.Exp = @"^\bfalse\b";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._NULL;
            NewGram.Exp = @"^\bnull\b";
            iSpec.Add(NewGram);

            // '=
            NewGram = new Grammar();
            NewGram.ID = Type_Id._SIMPLE_ASSIGN;
            NewGram.Exp = @"^\=";
            iSpec.Add(NewGram);

            // *=, /=, +=, -=,
            NewGram = new Grammar();
            NewGram.ID = Type_Id._COMPLEX_ASSIGN;
            NewGram.Exp = @"^(\*|\/|\+|\-)=";
            iSpec.Add(NewGram);


            // Conditional BLOCK CODE: LEFT BOUNDRY
            NewGram = new Grammar();
            NewGram.ID = Type_Id._CONDITIONAL_BEGIN;
            NewGram.Exp = @"^\(";
            iSpec.Add(NewGram);
            // Conditional BLOCK CODE: RIGHT BOUNDRY
            NewGram = new Grammar();
            NewGram.ID = Type_Id._CONDITIONAL_END;
            NewGram.Exp = @"^\)";
            iSpec.Add(NewGram);

            // Logical Operators:  &&, ||
            NewGram = new Grammar();
            NewGram.ID = Type_Id._LOGICAL_AND;
            NewGram.Exp = @"^\band\b";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._LOGICAL_OR;
            NewGram.Exp = @"^\bor\b";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._LOGICAL_NOT;
            NewGram.Exp = @"^\bnot\b";
            iSpec.Add(NewGram);

            // Equality operators: ==, !=
            NewGram = new Grammar();
            NewGram.ID = Type_Id._EQUALITY;
            NewGram.Exp = @"^(=|!)=\=";
            iSpec.Add(NewGram);

            // Relational operators: >, >=, <, <=
            NewGram = new Grammar();
            NewGram.ID = Type_Id._RELATIONAL_OPERATOR;
            NewGram.Exp = @"^[><]\=?";
            iSpec.Add(NewGram);
            // Math operators: +, -, *, /
            NewGram = new Grammar();
            NewGram.ID = Type_Id._ADDITIVE_OPERATOR;
            NewGram.Exp = @"^[+\-]";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._MULTIPLICATIVE_OPERATOR;
            NewGram.Exp = "^[*/]";
            iSpec.Add(NewGram);
            return iSpec;
        }

        public static List<Grammar> GetLiteralsGrammar()
        {
            var iSpec = new List<Grammar>();
            var NewGram = new Grammar();
            // Literals
            NewGram.ID = Type_Id._INTEGER;
            NewGram.Exp = @"^\d+";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._STRING;
            NewGram.Exp = "^" + '"' + "[^" + '"' + "]*" + '"';
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._STRING;
            NewGram.Exp = "^'[^']*'";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._WHITESPACE;
            NewGram.Exp = @"^\s";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._COMMENTS;
            NewGram.Exp = @"^\/\/.*";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._COMMENTS;
            NewGram.Exp = @"^\/\*[\s\S]*?\*\/";
            iSpec.Add(NewGram);

            // Variable
            NewGram = new Grammar();
            NewGram.ID = Type_Id._VARIABLE;
            NewGram.Exp = @"^\b[a-z][a-z0-9]+\b";
            iSpec.Add(NewGram);
            #region literal Object types
            #region ARRAY
            NewGram = new Grammar();
            NewGram.ID = Type_Id._OBJ_array;
            NewGram.Exp = @"\blist\b";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._OBJ_array;
            NewGram.Exp = @"\barray\b";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._OBJ_array;
            NewGram.Exp = @"\barraylist\b";
            iSpec.Add(NewGram);
            #endregion
            #region boolean
            NewGram = new Grammar();
            NewGram.ID = Type_Id._OBJ_boolean;
            NewGram.Exp = @"\bbool\b";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._OBJ_boolean;
            NewGram.Exp = @"\bboolean\b";
            iSpec.Add(NewGram);
            #endregion
            #region NULL
            NewGram = new Grammar();
            NewGram.ID = Type_Id._OBJ_null;
            NewGram.Exp = @"\bnothing\b";
            iSpec.Add(NewGram);
            #endregion
            #region integer
            NewGram = new Grammar();
            NewGram.ID = Type_Id._OBJ_integer;
            NewGram.Exp = @"\bint\b";
            iSpec.Add(NewGram);
            NewGram = new Grammar();
            NewGram.ID = Type_Id._OBJ_integer;
            NewGram.Exp = @"\binteger\b";
            iSpec.Add(NewGram);
            #endregion
            #region string
            NewGram = new Grammar();
            NewGram.ID = Type_Id._OBJ_string;
            NewGram.Exp = @"\bstring\b";
            iSpec.Add(NewGram);

            #endregion
            #endregion
            return iSpec;
        }
    }
    // PL GRAMMAR
    // 
    // 
    namespace GrammarFactory
    {
        public class PLGrammar
        {
            /// <summary>
            /// Set OF KeyWords for Language with RegEx Search Expressions
            /// Based on basic programming languge keywords and symbols /Literals
            /// This is a preloaded Grammar (list of Grammar objects)
            /// </summary>
            /// <returns></returns>
            public static List<Grammar> GetPLGrammar()
            {
                var iSpec = new List<Grammar>();
                var NewGram = new Grammar();
                NewGram = new Grammar();
                NewGram.ID = Type_Id._PL_PROGRAM_BEGIN;
                NewGram.Exp = @"\bspl_lang\b";
                iSpec.Add(NewGram);

                #region Print
                // Prints
                NewGram = new Grammar();
                NewGram.ID = Type_Id._PRINT;
                NewGram.Exp = @"^\bprint\b";
                iSpec.Add(NewGram);
                #endregion
                #region Functions/Classes
                #region Return Value
                // Functions/Classes
                NewGram = new Grammar();
                NewGram.ID = Type_Id._RETURN;
                NewGram.Exp = @"^\breturn\b";
                iSpec.Add(NewGram);
                #endregion

                #region Declare Function
                NewGram = new Grammar();
                NewGram.ID = Type_Id._FUNCTION_DECLARE;
                NewGram.Exp = @"\bdef\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._FUNCTION_DECLARE;
                NewGram.Exp = @"^\bfunction\b";
                iSpec.Add(NewGram);
                #endregion



                NewGram = new Grammar();
                NewGram.ID = Type_Id._CLASS;
                NewGram.Exp = @"^\bclass\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._NEW;
                NewGram.Exp = @"^\bnew\b";
                iSpec.Add(NewGram);
                #endregion
                #region ASSIGNMENT
                // ASSIGNMENT : Syntax  _Variable _AS 
                // Reconsidered Using Dim (Could Still Implement by changing Assignment handler/Generator)
                NewGram = new Grammar();
                NewGram.ID = Type_Id._VARIABLE_DECLARE;
                NewGram.Exp = @"^\bdim\b\s";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._VARIABLE_DECLARE;
                NewGram.Exp = @"^\blet\s\b";
                iSpec.Add(NewGram);
                // Assignment operators: xLeft assigns output of right (9+4) (+= 9) (-=2) (3) (true)
                #endregion
                #region IF/THEN
                // IF/THEN
                NewGram = new Grammar();
                NewGram.ID = Type_Id._IF;
                NewGram.Exp = @"^\bif\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._ELSE;
                NewGram.Exp = @"^\belse\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._THEN;
                NewGram.Exp = @"^\bthen\b";
                iSpec.Add(NewGram);


                #endregion
                #region DO WHILE/UNTIL
                // DO WHILE/UNTIL
                NewGram = new Grammar();
                NewGram.ID = Type_Id._WHILE;
                NewGram.Exp = @"^\bwhile\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._VARIABLE_DECLARE;
                NewGram.Exp = @"^\bas\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._UNTIL;
                NewGram.Exp = @"^\buntil\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._LOOP;
                NewGram.Exp = @"^\bloop\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._DO;
                NewGram.Exp = @"^\bdo\b";
                iSpec.Add(NewGram);
                #endregion
                #region For/Next

                // For/To  For/Each/in /Next
                NewGram = new Grammar();
                NewGram.ID = Type_Id._FOR;
                NewGram.Exp = @"^\bfor\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._EACH;
                NewGram.Exp = @"^\beach\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._TO;
                NewGram.Exp = @"^\bto\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._NEXT;
                NewGram.Exp = @"^\bnext\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._IN;
                NewGram.Exp = @"^\bin\b";
                iSpec.Add(NewGram);

                #endregion




                iSpec.AddRange(BaseGrammar.GetLogicGrammar());
                iSpec.AddRange(BaseGrammar.GetSymbolsGrammar());
                iSpec.AddRange(BaseGrammar.GetLiteralsGrammar());
                // ARGS LIST: RIGHT BOUNDRY
                NewGram = new Grammar();
                NewGram.ID = Type_Id._EOF;
                NewGram.Exp = "EOF";
                iSpec.Add(NewGram);
                return iSpec;
            }
        }
    }
    // GRAMMAR-TOKEN
    // 
    namespace GrammarFactory
    {
        /// <summary>
        /// Token to be returned 
        /// </summary>
        public struct Token
        {
            /// <summary>
            /// Simple identifier
            /// </summary>
            public Type_Id ID;
            /// <summary>
            /// Held Data
            /// </summary>
            public string Value;
            /// <summary>
            /// Start of token(Start position)
            /// </summary>
            public int _start;
            /// <summary>
            /// End of token (end Position)
            /// </summary>
            public int _End;

            public string ToJson()
            {
                var Converter = new JavaScriptSerializer();
                return Converter.Serialize(this);
            }
        }
    }
    // SAL GRAMMAR
    // 
    namespace GrammarFactory
    {
        public class SALGrammar
        {
            public static List<Grammar> GetSALGrammar()
            {
                var iSpec = new List<Grammar>();
                var NewGram = new Grammar();
                NewGram = new Grammar();
                NewGram.ID = Type_Id._SAL_PROGRAM_BEGIN;
                NewGram.Exp = @"^\bsal_lang\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._SAL_EXPRESSION_BEGIN;
                NewGram.Exp = @"^\bsal\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._WHITESPACE;
                NewGram.Exp = @"^\s";
                iSpec.Add(NewGram);
                NewGram.ID = Type_Id._SIMPLE_ASSIGN;
                NewGram.Exp = @"^\bassigns\b";
                iSpec.Add(NewGram);


                #region SAL
                // Sal_Cmds
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_NULL;
                NewGram.Exp = @"^\bnull\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_REMOVE;
                NewGram.Exp = @"^\bremove\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_RESUME;
                NewGram.Exp = @"^\bresume\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_PUSH;
                NewGram.Exp = @"^\bpush\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_PULL;
                NewGram.Exp = @"^\bpull\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_PEEK;
                NewGram.Exp = @"^\bpeek\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_WAIT;
                NewGram.Exp = @"^\bwait\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_PAUSE;
                NewGram.Exp = @"^\bpause\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_HALT;
                NewGram.Exp = @"^\bhalt\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_DUP;
                NewGram.Exp = @"^\bdup\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_JMP;
                NewGram.Exp = @"^\bjmp\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_JIF_T;
                NewGram.Exp = @"^\bjif_t\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_JIF_F;
                NewGram.Exp = @"^\bjif_f\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_JIF_EQ;
                NewGram.Exp = @"^\bjif_eq\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_JIF_GT;
                NewGram.Exp = @"^\bjif_gt\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_JIF_LT;
                NewGram.Exp = @"^\bjif_lt\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_LOAD;
                NewGram.Exp = @"^\bload\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_STORE;
                NewGram.Exp = @"^\bstore\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_CALL;
                NewGram.Exp = @"^\bcall\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_RET;
                NewGram.Exp = @"^\bret\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_PRINT_M;
                NewGram.Exp = @"^\bprint_m\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_PRINT_C;
                NewGram.Exp = @"^\bprint_c\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_ADD;
                NewGram.Exp = @"^\badd\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_SUB;
                NewGram.Exp = @"^\bsub\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_MUL;
                NewGram.Exp = @"^\bmul\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_DIV;
                NewGram.Exp = @"^\bdiv\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_ADD;
                NewGram.Exp = @"^\band\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_OR;
                NewGram.Exp = @"^\bor\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_NOT;
                NewGram.Exp = @"^\bnot\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_JIF_LT;
                NewGram.Exp = @"^\bis_eq\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_IS_GT;
                NewGram.Exp = @"^\bis_gt\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_IS_GTE;
                NewGram.Exp = @"^\bis_gte\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_IS_LT;
                NewGram.Exp = @"^\bis_lt\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_IS_LT;
                NewGram.Exp = @"^\bis_lte\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_TO_POS;
                NewGram.Exp = @"^\bto_pos\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_TO_NEG;
                NewGram.Exp = @"^\bto_neg\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_INCR;
                NewGram.Exp = @"^\bincr\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.SAL_DECR;
                NewGram.Exp = @"^\bdecr\b";
                iSpec.Add(NewGram);
                // logical(boolean) - Literal
                NewGram = new Grammar();
                NewGram.ID = Type_Id._TRUE;
                NewGram.Exp = @"^\btrue\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._FALSE;
                NewGram.Exp = @"^\bfalse\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._NULL;
                NewGram.Exp = @"^\bnull\b";
                iSpec.Add(NewGram);

                #endregion

                iSpec.AddRange(BaseGrammar.GetLogicGrammar());
                iSpec.AddRange(BaseGrammar.GetSymbolsGrammar());
                iSpec.AddRange(BaseGrammar.GetLiteralsGrammar());
                NewGram = new Grammar();
                NewGram.ID = Type_Id._EOF;
                NewGram.Exp = "EOF";
                iSpec.Add(NewGram);
                return iSpec;
            }
        }
    }
}

namespace SDK.SmallProgLang
{
    // LOGO GRAMMAR
    // 
    namespace GrammarFactory
    {
        // https://www.tutorialspoint.com/logo/logo_introduction.htm
        // https://www.transum.org/software/Logo/ (Online logo)
        // https://www.calormen.com/jslogo/

        // FORWARD	    fd	            FORWARD(space)<number Of steps To move forward>	Moves turtle forward For number Of times specified	"forward 100" Or "fd 100"
        // BACK	        bk	            BACK(space) <number Of steps To move backward>	Moves turtle back For number Of times specified	"back 100" Or "bk 100"
        // RIGHT	        rt	            RIGHT(space) <degrees To rotate toward right	Turns turtle right For number Of degrees specified	"right 228" Or "rt 228"
        // LEFT	        lt	            LEFT(space) <degrees To rotate toward left >	Turns turtle left For number Of degrees specified	"left 228" Or "lt 228"
        // HOME	        home	        Home	Comes To screen center but does Not clear the screen	"home"
        // CLEAN	        ct cs	        Clean	Clears the screen Of trails but the turtle remains where it Is without moving	"clean"
        // CLEARSCREEN	CS	            Clearscreen	Clears the screen Of trails And comes To screen center	"cs"
        // HIDETURTLE	    HT	            Hide turtle	Hides the turtle And aids viewing a clear drawing On the screen	"ht"
        // SHOWTURTLE	    ST	            Show turtle	Shows the turtle after it Is hidden from the screen	"st"
        // PENUP	        PU(set)         Pen up	Sets the turtle To move without drawing	"pu"
        // PENDOWN	    PD(resets)      Pen	Resets To a drawing pen When ordered To move	"pd"
        // CLEARTEXT	    CT	Clear text	Clears all text In the command screen	"ct"



        // 
        // signExpression
        // (('+' | '-'))* (number | deref | func)
        // 
        // multiplyingExpression
        // : signExpression (('*' | '/') signExpression)*
        // 
        // expression
        // : multiplyingExpression (('+' | '-') multiplyingExpression)*
        // 
        // parameterDeclarations
        // : ':' name (',' parameterDeclarations)*
        // 
        // procedureDeclaration
        // : 'to' name parameterDeclarations* EOL? (line? EOL) + 'end'
        // 
        // procedureInvocation
        // : name expression*
        // 
        // deref
        // ':' name
        // 
        // fd
        // : ('fd' | 'forward') expression
        // 
        // bk
        // : ('bk' | 'backward') expression
        // 
        // rt
        // : ('rt' | 'right') expression
        // 
        // lt 
        // : ('lt' | 'left') expression
        // 
        // cs
        // : cs
        // : clearscreen
        // 
        // pu
        // : pu
        // : penup
        // 
        // pd
        // 
        // : pd
        // : pendown
        // 
        // ht
        // 
        // : ht
        // : hideturtle
        // 
        // st'
        // 
        // : st
        // : showturtle
        // 
        // : Home
        // 
        // : Stop
        // 
        // : label
        // 
        // setxy
        // : setxy expression expression
        // 
        // random
        // 
        // : random expression
        // 
        // for
        // : 'for' '[' name expression expression expression ']' block
        // 
        // value
        // String / Expression / deref
        // 
        // name
        // String
        // 
        // print
        // : 'print' (value | quotedstring)
        // 
        // make
        // : 'make' STRINGLITERAL value
        // 
        // comparison
        // : expression comparisonOperator expression
        // 
        // comparisonOperator
        // '<'
        // '>'
        // '='
        // 
        // if
        // 'if' comparison block
        // block
        // '[' cmd + ']'
        // 
        // repeat
        // : 'repeat' number block
        // 
        // func
        // : random
        // 
        // line
        // : cmd + comment?
        // : comment
        // : print comment?
        // : procedureDeclaration
        // 
        // prog
        // :(line? EOL) + line?
        // 
        // comment
        // : COMMENT
        // :  ~ [\r\n]*';'
        /// <summary>
        /// Logo Programming Language
        /// </summary>
        public class LogoGrammar
        {
            public static List<Grammar> GetLOGOGrammar()
            {
                var iSpec = new List<Grammar>();
                var NewGram = new Grammar();
                NewGram = new Grammar();
                NewGram.ID = Type_Id._COMMENTS;
                NewGram.Exp = @"^\~";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_LANG;
                NewGram.Exp = @"\blogo_lang\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_EOL;
                NewGram.Exp = @"^\;";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_deref;
                NewGram.Exp = @"^\:";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_fd;
                NewGram.Exp = @"\bfd\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_fd;
                NewGram.Exp = @"\bforward\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_bk;
                NewGram.Exp = @"\bbackward\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_bk;
                NewGram.Exp = @"\bbk\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_rt;
                NewGram.Exp = @"\brt\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_rt;
                NewGram.Exp = @"\bright\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_lt;
                NewGram.Exp = @"\blt\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_lt;
                NewGram.Exp = @"\bleft\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_cs;
                NewGram.Exp = @"\bcs\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_cs;
                NewGram.Exp = @"\bclearscreen\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_pu;
                NewGram.Exp = @"\bpu\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_pu;
                NewGram.Exp = @"\bpenup\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_pd;
                NewGram.Exp = @"\bpd\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_pd;
                NewGram.Exp = @"\bpendown\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_ht;
                NewGram.Exp = @"\bht\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_ht;
                NewGram.Exp = @"\bhideturtle\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_st;
                NewGram.Exp = @"\bst\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_st;
                NewGram.Exp = @"\bshowturtle\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_label;
                NewGram.Exp = @"\blabel\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_setxy;
                NewGram.Exp = @"\bsetxy\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_make;
                NewGram.Exp = @"\bmake\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_ife;
                NewGram.Exp = @"\bife\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_fore;
                NewGram.Exp = @"\bfore\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._STRING;
                NewGram.Exp = "^" + '"' + "[^" + '"' + "]*" + '"';
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._STRING;
                NewGram.Exp = "^'[^']*'";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._WHITESPACE;
                NewGram.Exp = @"^\s";
                iSpec.Add(NewGram);
                // logical(boolean) - Literal
                NewGram = new Grammar();
                NewGram.ID = Type_Id._TRUE;
                NewGram.Exp = @"^\btrue\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._FALSE;
                NewGram.Exp = @"^\bfalse\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id._NULL;
                NewGram.Exp = @"^\bnull\b";
                iSpec.Add(NewGram);
                // Variable
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_name;
                NewGram.Exp = @"^\b[a-z][a-z0-9]+\b";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_number;
                NewGram.Exp = @"^\d+";
                iSpec.Add(NewGram);
                // '=
                NewGram = new Grammar();
                NewGram.ID = Type_Id._SIMPLE_ASSIGN;
                NewGram.Exp = @"^\=";
                iSpec.Add(NewGram);

                // *=, /=, +=, -=,
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_signExpression;
                NewGram.Exp = @"^(\*|\/|\+|\-)=";
                iSpec.Add(NewGram);

                // Equality operators: ==, !=
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_comparisonOperator;
                NewGram.Exp = @"^(=|!)=\=";
                iSpec.Add(NewGram);
                // Relational operators: >, >=, <, <=
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_comparisonOperator;
                NewGram.Exp = @"^[><]\=?";
                iSpec.Add(NewGram);
                // Math operators: +, -, *, /
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_signExpression;
                NewGram.Exp = @"^[+\-]";
                iSpec.Add(NewGram);
                NewGram = new Grammar();
                NewGram.ID = Type_Id.LOGO_multiplyingExpression;
                NewGram.Exp = "^[*/]";
                iSpec.Add(NewGram);
                return iSpec;
            }

            public string GetLogoRef()
            {
                string str = "";
                str = My.Resources.Resources.LOGO_QUICK_REF;
                return str;
            }
        }
    }
}
#endregion
// THE TOKENIZER (LEXER)
// 
#region TOKENIZER
// LEXER - TOKENIZER - SCANNER
// 
namespace SDK.SmallProgLang
{
    namespace Compiler
    {
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Lexer
        {
            /// <summary>
            /// Cursor Position
            /// </summary>
            private int Cursor = 0;
            /// <summary>
            /// Cursor Position
            /// </summary>
            private int EoFCursor = 0;
            /// <summary>
            /// Program being Tokenized
            /// </summary>
            private string CurrentScript = "";
            private List<Token> iPastTokens = new List<Token>();
            private Token iLastToken;

            public List<Token> PastTokens
            {
                get
                {
                    return iPastTokens;
                }
            }

            private Token LastToken
            {
                get
                {
                    return iLastToken;
                }

                set
                {
                    iLastToken = value;
                    iPastTokens.Add(value);
                }
            }

            public Token GetLastToken()
            {
                return LastToken;
            }
            /// <summary>
            /// Returns from index to end of file (Universal function)
            /// </summary>
            /// <param name="Str">String</param>
            /// <param name="indx">Index</param>
            /// <returns></returns>
            public static string GetSlice(ref string Str, ref int indx)
            {
                if (indx <= Str.Length)
                {
                    Str.Substring(indx);
                    return Str.Substring(indx);
                }
                else
                {
                }

                return null;
            }

            public bool EndOfFile
            {
                get
                {
                    if (Cursor >= EoFCursor)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            /// <summary>
            /// Gets next token moves cursor forwards
            /// </summary>
            /// <returns></returns>
            public string GetNext()
            {
                if (EndOfFile == false)
                {
                    string slice = GetSlice(ref CurrentScript, ref Cursor);
                    if (slice is object)
                    {
                        Cursor += slice.Length;
                        return slice;
                    }
                    else
                    {
                        // Errors jump straight to enod of file
                        Cursor = EoFCursor;
                        return "EOF";
                    }
                }
                else
                {
                    return "EOF";
                }
            }
            /// <summary>
            /// Checks token without moving the cursor
            /// </summary>
            /// <returns></returns>
            public string ViewNext()
            {
                if (EndOfFile == false)
                {
                    string slice = GetSlice(ref CurrentScript, ref Cursor);
                    if (slice is object)
                    {
                        return slice;
                    }
                    else
                    {
                        // Does not change anything
                        return "EOF";
                    }
                }
                else
                {
                    return "EOF";
                }
            }
            /// <summary>
            /// Main Searcher
            /// </summary>
            /// <param name="Text">to be searched </param>
            /// <param name="Pattern">RegEx Search String</param>
            /// <returns></returns>
            private static List<string> RegExSearch(ref string Text, string Pattern)
            {
                var Searcher = new Regex(Pattern);
                var iMatch = Searcher.Match(Text);
                var iMatches = new List<string>();
                while (iMatch.Success)
                {
                    iMatches.Add(iMatch.Value);
                    iMatch = iMatch.NextMatch();
                }

                return iMatches;
            }
            /// <summary>
            /// Steps the tokenizer backwards
            /// </summary>
            /// <param name="TokenStr"></param>
            public void StepBack(ref string TokenStr)
            {
                // If last value was the same then do.
                if ((TokenStr ?? "") == (LastToken.Value ?? ""))
                {
                    try
                    {
                        Cursor = Cursor - TokenStr.Length;
                        // Removes last token
                        iPastTokens.RemoveAt(PastTokens.Count - 1);
                        // sets last token to newlast on the list
                        iLastToken = PastTokens[PastTokens.Count - 1];
                    }
                    catch (Exception ex)
                    {
                        // Error (cant go back)
                    }
                }
                else
                {
                    // Was not the last value in stack
                }
            }

            public Token Eat(ref Type_Id TokenType)
            {
                int Strt = Cursor;
                string argCurrentTok = ViewNext();
                if (IdentifiyToken(ref argCurrentTok) == TokenType)
                {

                    // With return "" if nothing is detected
                    string str = "";
                    // Build token until token is not the type (if token is of type then add it)

                    // Get next advances the cursor
                    str += GetNext();
                    int _end = Cursor;
                    var Toke = new Token();
                    Toke.ID = TokenType;
                    Toke.Value = str;
                    Toke._start = Strt;
                    Toke._End = _end;
                    // Preserve token returned
                    LastToken = Toke;
                    return Toke;
                }
                else
                {
                    // Not match tokentype
                    return default;
                }
            }
            /// <summary>
            /// Identifys token but due to some tokens maybe cross identifying 
            /// CheckIdentifiedToken will return the full token without moving the cursor
            /// </summary>
            /// <param name="CurrentTok"></param>
            /// <returns></returns>
            public Type_Id IdentifiyToken(ref string CurrentTok)
            {
                foreach (var item in CurrentGrammar)
                {
                    var matches = RegExSearch(ref CurrentTok, item.Exp);
                    if (matches is object & matches.Count > 0)
                    {
                        return item.ID;
                    }
                    else
                    {
                        // Check next
                    }
                }

                return Type_Id._BAD_TOKEN;
            }
            /// <summary>
            /// Identifys token
            /// </summary>
            /// <param name="CurrentTok"></param>
            /// <returns></returns>
            public Token GetIdentifiedToken(ref string CurrentTok)
            {
                foreach (var item in CurrentGrammar)
                {
                    var matches = RegExSearch(ref CurrentTok, item.Exp);
                    if (matches is object & matches.Count > 0)
                    {
                        var tok = new Token();
                        tok.ID = item.ID;
                        tok.Value = matches[0];
                        tok._start = Cursor;
                        tok._End = Cursor + tok.Value.Length;
                        Cursor = tok._End;
                        return tok;
                    }
                    else
                    {
                        // Check next
                    }
                }
                // Match not found bad token
                var btok = new Token();
                btok.ID = Type_Id._BAD_TOKEN;
                btok.Value = CurrentTok;
                btok._start = Cursor;
                btok._End = Cursor + CurrentScript.Length;
                Cursor = EoFCursor;
                return btok;
            }

            public Token CheckIdentifiedToken(ref string CurrentTok)
            {
                foreach (var item in CurrentGrammar)
                {
                    var matches = RegExSearch(ref CurrentTok, item.Exp);
                    if (matches is object & matches.Count > 0)
                    {
                        var tok = new Token();
                        tok.ID = item.ID;
                        tok.Value = matches[0];
                        tok._start = Cursor;
                        tok._End = Cursor + tok.Value.Length;
                        // Cursor = tok._End
                        return tok;
                    }
                    else
                    {
                        // Check next
                    }
                }
                // Match not found bad token
                var btok = new Token();
                btok.ID = Type_Id._BAD_TOKEN;
                btok.Value = CurrentTok;
                btok._start = Cursor;
                btok._End = Cursor + CurrentScript.Length;
                return btok;
            }

            public List<Grammar> CurrentGrammar;

            public Lexer(ref string Script)
            {
                CurrentScript = Script;
                EoFCursor = Script.Length;
                CurrentGrammar = Grammar.GetExtendedGrammar();
            }
            /// <summary>
            /// Use for Sal and OtherLangs
            /// </summary>
            /// <param name="Script"></param>
            /// <param name="Grammar"></param>
            public Lexer(ref string Script, ref List<Grammar> Grammar)
            {
                CurrentScript = Script;
                EoFCursor = Script.Length;
                CurrentGrammar = Grammar;
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson;
            }
        }
    }
}
#endregion
// => SAL TRANSPLIER
// 
#region THE TRANSPILER
namespace SDK.SmallProgLang
{
    namespace Transpiler
    {
        /// <summary>
        /// Transpiles to SAL Code; 
        /// Can be run on SAL Assembler
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class Sal_Transpiler
        {
            /// <summary>
            /// Line number counter
            /// </summary>
            private int LineNumber;
            /// <summary>
            /// Increases the current line number to track the line number in the program
            /// </summary>
            private void IncrLineNumber()
            {
                LineNumber += 1;
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson;
            }
            /// <summary>
            /// As Each Expression is consumed an program will be returned 
            /// Each Expression Should be an array 
            /// Provided by the AST toArray Function 
            /// Which only Creates the Desired nodes in the order Expected by the Transpiler(RPL)
            /// </summary>
            /// <param name="Ast"></param>
            /// <returns></returns>
            public List<string> Transpile(ref AstExpression Ast)
            {
                var Expr = Ast.ToArraylist();
                // Created for tracking 
                IncrLineNumber();

                // Detect Binary Expressions
                switch (Expr.Count)
                {
                    case 2:
                        {
                            var tmp = Expr;
                            string argStr = tmp[1];
                            return _print(ref argStr);
                            tmp[1] = argStr;
                            break;
                        }

                    case 3:
                        {
                            // Function : to get literal will be required 
                            var tmp1 = Expr;
                            int argLeft = Conversions.ToInteger(tmp1[1]);
                            var tmp2 = Expr;
                            int argRight = Conversions.ToInteger(tmp2[2]);
                            var tmp3 = Expr;
                            string argiOperator = tmp3[0];
                            return _Binary_op(ref argLeft, ref argRight, ref argiOperator);
                            tmp1[1] = argLeft.ToString();
                            tmp2[2] = argRight.ToString();
                            tmp3[0] = argiOperator;
                            break;
                        }
                }

                // If it is not a binary op
                // 'then it can be identified by its type
                switch (Ast._Type)
                {
                }

                var str = new List<string>();
                str.Add("Not Implemented Bad Expression Syntax =" + EXT.FormatJsonOutput(Expr.ToJson()) + Constants.vbNewLine + "LineNumber =" + LineNumber);
                return str;
            }

            #region Generators

            /// <summary>
            /// Generates SalCode For Binary ops
            /// -Simple Assign
            /// -Conditional
            /// -Addative
            /// -Multiplicative
            /// 
            /// </summary>
            /// <param name="Left"></param>
            /// <param name="Right"></param>
            /// <param name="iOperator"></param>
            /// <returns></returns>
            private List<string> _Binary_op(ref int Left, ref int Right, ref string iOperator)
            {
                var PROGRAM = new List<string>();
                switch (iOperator ?? "")
                {
                    case "-":
                        {
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Left.ToString());
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Right.ToString());
                            PROGRAM.Add("SUB");
                            PROGRAM.Add("PRINT_M");
                            break;
                        }

                    case "+":
                        {
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Left.ToString());
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Right.ToString());
                            PROGRAM.Add("ADD");
                            PROGRAM.Add("PRINT_M");
                            break;
                        }

                    case "/":
                        {
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Left.ToString());
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Right.ToString());
                            PROGRAM.Add("DIV");
                            PROGRAM.Add("PRINT_M");
                            break;
                        }

                    case "*":
                        {
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Left.ToString());
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Right.ToString());
                            PROGRAM.Add("MUL");
                            PROGRAM.Add("PRINT_M");
                            break;
                        }

                    case ">":
                        {
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Left.ToString());
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Right.ToString());
                            PROGRAM.Add("IS_GT");
                            PROGRAM.Add("PRINT_M");
                            break;
                        }

                    case "<":
                        {
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Left.ToString());
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Right.ToString());
                            PROGRAM.Add("IS_LT");
                            PROGRAM.Add("PRINT_M");
                            break;
                        }

                    case ">=":
                        {
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Left.ToString());
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Right.ToString());
                            PROGRAM.Add("IS_GTE");
                            PROGRAM.Add("PRINT_M");
                            break;
                        }

                    case "<=":
                        {
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Left.ToString());
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Right.ToString());
                            PROGRAM.Add("IS_LTE");
                            PROGRAM.Add("PRINT_M");
                            break;
                        }

                    case "=":
                        {
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Left.ToString());
                            PROGRAM.Add("PUSH");
                            PROGRAM.Add(Right.ToString());
                            PROGRAM.Add("IS_EQ");
                            PROGRAM.Add("PRINT_M");
                            break;
                        }
                }

                return PROGRAM;
            }

            private List<string> _print(ref string Str)
            {
                var PROGRAM = new List<string>();
                PROGRAM.Add("PUSH");
                PROGRAM.Add(Str.Replace("'", ""));
                PROGRAM.Add("PRINT_M");
                return PROGRAM;
            }
            #endregion

        }
    }
}
#endregion
#endregion
// EXECUTION
// 
#region THE EVALUATOR
// THE EVALUATOR
// 
#region EVAULATOR
// S-EXPRESSION EVALUATOR
// 
namespace SDK.SmallProgLang
{
    namespace Evaluator
    {

        /// <summary>
        /// Evaluates Arrays of tokens, 
        /// Taken from the populated AST NODES, 
        /// The Expected format is Reverse poilsh Notation. Operator/ (Operands)
        /// The Product is returned ; 
        /// 
        /// </summary>
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class S_Expression_Evaluator
        {
            private EnvironmentalMemory GlobalEnvironment;
            /// <summary>
            /// Create a new instance of the PROGRAMMING Interpretor
            /// </summary>
            /// <param name="iGlobal">Used to provide preloaded environment</param>
            public S_Expression_Evaluator(ref EnvironmentalMemory iGlobal)
            {
                GlobalEnvironment = iGlobal;
                LineNumber = 0;
            }
            /// <summary>
            /// Line number counter
            /// </summary>
            private int LineNumber;
            /// <summary>
            /// Increases the current line number to track the line number in the program
            /// </summary>
            private void IncrLineNumber()
            {
                LineNumber += 1;
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson;
            }
            /// <summary>
            /// Used for imediate Evaluations
            /// </summary>
            /// <param name="Expr"></param>
            /// <param name="Env"></param>
            /// <returns></returns>
            public object Eval(ref object Expr, EnvironmentalMemory Env = null)
            {
                // Created for tracking 
                IncrLineNumber();
                if (Env is null)
                {
                    Env = GlobalEnvironment;
                }
                #region Get Literal Literals
                // [Literals]
                // - 3 0r 5.6 [integer]
                if (IsNumberInt(ref Expr) == true)
                {
                    int num = int.Parse(Conversions.ToString(Expr));
                    return num;
                }
                // - "CAT"[String]
                if (IsString(ref Expr) == true)
                {
                    return Expr.ToString();
                }
                // - $VAR$
                if (IsVariableName(ref Expr) == true)
                {
                    string argName = Conversions.ToString(Expr((object)0));
                    return Env.GetVar(ref argName);
                }
                // - "TRUE" "FALSE"
                if (IsBoolean(ref Expr) == true)
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(Expr, "TRUE", false)))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                #endregion
                // To Identify What type of Expression
                // A Count of the Supplied elements is taken: 

                if (IsArray(ref Expr) == true)
                {
                    switch (Expr.count)
                    {
                        case 4:
                            {
                                // Case Expr(0) = "DIM"
                                // Syntax: 
                                // Dim Var = 92
                                // Dim var = False
                                // Dim var = ""
                                // Assign in Global Memory
                                string argName1 = Conversions.ToString(Expr((object)1));
                                Env.Define(ref argName1, Conversions.ToString(Expr((object)3)));
                                return true;
                            }

                        // Syntax: 
                        // 4 * 92
                        // A >= B
                        // 4 + 92
                        // A <= B
                        case 3:
                            {
                                switch (Expr(0))
                                {
                                    // Maths Expressions (Recursive)
                                    // Syntax: Basic Arithmatic + 4 6
                                    // + 3 5
                                    // + $VAR$ $VAR$
                                    // + $VAR$ 3
                                    case "+":
                                        {
                                            object localEval() { var argExpr = Expr(2); var ret = this.Eval(ref argExpr); return ret; }

                                            var argExpr = Expr(1);
                                            return Operators.AddObject(this.Eval(ref argExpr), localEval());
                                        }

                                    case "-":
                                        {
                                            object localEval1() { var argExpr = Expr(2); var ret = this.Eval(ref argExpr); return ret; }

                                            var argExpr1 = Expr(1);
                                            return Operators.SubtractObject(this.Eval(ref argExpr1), localEval1());
                                        }

                                    case "*":
                                        {
                                            object localEval2() { var argExpr = Expr(2); var ret = this.Eval(ref argExpr); return ret; }

                                            var argExpr2 = Expr(1);
                                            return Operators.MultiplyObject(this.Eval(ref argExpr2), localEval2());
                                        }

                                    case "/":
                                        {
                                            object localEval3() { var argExpr = Expr(2); var ret = this.Eval(ref argExpr); return ret; }

                                            var argExpr3 = Expr(1);
                                            return Operators.DivideObject(this.Eval(ref argExpr3), localEval3());
                                        }
                                    // Conditionals: (Recursive)
                                    // Syntax: Basic Conditionals < 4 6
                                    // < 3 5
                                    // < $VAR$ $VAR$
                                    // < $VAR$ 3
                                    case ">":
                                        {
                                            object localEval4() { var argExpr = Expr(2); var ret = this.Eval(ref argExpr); return ret; }

                                            var argExpr4 = Expr(1);
                                            return Operators.ConditionalCompareObjectGreater(this.Eval(ref argExpr4), localEval4(), false);
                                        }

                                    case "<":
                                        {
                                            object localEval5() { var argExpr = Expr(2); var ret = this.Eval(ref argExpr); return ret; }

                                            var argExpr5 = Expr(1);
                                            return Operators.ConditionalCompareObjectGreater(this.Eval(ref argExpr5), localEval5(), false);
                                        }

                                    case ">=":
                                        {
                                            object localEval6() { var argExpr = Expr(2); var ret = this.Eval(ref argExpr); return ret; }

                                            var argExpr6 = Expr(1);
                                            return Operators.ConditionalCompareObjectGreaterEqual(this.Eval(ref argExpr6), localEval6(), false);
                                        }

                                    case "<=":
                                        {
                                            object localEval7() { var argExpr = Expr(2); var ret = this.Eval(ref argExpr); return ret; }

                                            var argExpr7 = Expr(1);
                                            return Operators.ConditionalCompareObjectLessEqual(this.Eval(ref argExpr7), localEval7(), false);
                                        }

                                    case "=":
                                        {
                                            object localEval8() { var argExpr = Expr(2); var ret = this.Eval(ref argExpr); return ret; }

                                            var argExpr8 = Expr(1);
                                            return Operators.ConditionalCompareObjectEqual(this.Eval(ref argExpr8), localEval8(), false);
                                        }
                                }

                                break;
                            }

                        case 6:
                            {
                                switch (Expr(0))
                                {
                                    // Syntax: 
                                    // While(0)
                                    // (>(1)a(2)b(3)) = true(4)
                                    // {Codeblock(5)}
                                    case "WHILE":
                                        {
                                            // #while Op var1 var2 EQVAR Codeblock #loop
                                            string argName2 = "WHILE";
                                            Env.Define(ref argName2, "BOOLEAN");
                                            // Get Result expression (conditional) or (maths)
                                            object localEval9() { var argExpr = Expr(1); var ret = this.Eval(ref argExpr); return ret; }

                                            object localEval10() { var argExpr = Expr(2); var ret = this.Eval(ref argExpr); return ret; }

                                            object localEval11() { var argExpr = Expr(3); var ret = this.Eval(ref argExpr); return ret; }

                                            string[] Result = (string[])(new[] { localEval9(), localEval10(), localEval11() });
                                            // WhileCmd
                                            object localEval12() { var argExpr = Expr(4); var ret = this.Eval(ref argExpr); return ret; }

                                            object argExpr10 = Result;
                                            while (Operators.ConditionalCompareObjectEqual(Eval(ref argExpr10), localEval12(), false))
                                            {
                                                var WhileEnv = new EnvironmentalMemory(ref Env);
                                                // RETURN ENVIRONEMT? (TEST) PERHAPS NOTHING NEEDED TO BE RETURNED
                                                var argExpr9 = Expr(5);
                                                Env = this.EvalBlock(ref argExpr9, WhileEnv);
                                            }
                                            // #expr(6) = "Loop" (Signify End of Loop)
                                            return true;
                                        }
                                }

                                break;
                            }
                    }
                }

                return Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("Not Implemented Bad Expression Syntax =", Expr), Constants.vbNewLine), "LineNumber ="), LineNumber);
            }
            /// <summary>
            /// Evaluates a block of code returning the Global Environment the block environment is disposed of
            /// </summary>
            /// <param name="Expr"></param>
            /// <param name="Env"></param>
            /// <returns></returns>
            public EnvironmentalMemory EvalBlock(ref object Expr, EnvironmentalMemory Env = null)
            {
                if (Env is null)
                {
                    Env = GlobalEnvironment;
                }

                foreach (var item in (IEnumerable)Expr)
                    Eval(ref Expr, Env);
                // CHanges to the global memory enviroment need to be made? 
                return Env.GlobalMemory;
            }

            private bool _CheckCondition(ref int Left, ref int Right, ref string iOperator)
            {
                switch (iOperator ?? "")
                {
                    case ">":
                        {
                            if (Left > Right)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                            break;
                        }

                    case "<":
                        {
                            if (Left < Right)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                            break;
                        }

                    case ">=":
                        {
                            if (Left >= Right)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                            break;
                        }

                    case "<=":
                        {
                            if (Left <= Right)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                            break;
                        }

                    case "=":
                        {
                            if (Left == Right)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                            break;
                        }

                    default:
                        {
                            return false;
                        }
                }
            }


            #region Literals
            /// <summary>
            /// Checks if expr is a number, Returning number
            /// </summary>
            /// <param name="Expr"></param>
            /// <returns></returns>
            public bool IsNumberInt(ref object Expr)
            {
                try
                {
                    if (Expr is int)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            /// <summary>
            /// Checks if Expr is string returning the string
            /// </summary>
            /// <param name="Expr"></param>
            /// <returns></returns>
            public bool IsString(ref object Expr)
            {
                // ########## ############# ######
                // Align with front and back char "


                try
                {
                    if (Expr is string)
                    {
                        if (Conversions.ToBoolean(Expr.contains((object)'"')))
                        {
                            if (Conversions.ToBoolean(Expr.contains((object)'$')))
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

                return false;
            }
            /// <summary>
            /// If Epr token is variable ake then it can be handled correctlly
            /// </summary>
            /// <param name="Expr"></param>
            /// <returns></returns>
            public bool IsVariableName(ref object Expr)
            {

                // # ########################################## ##
                // #Align with front and back char $

                try
                {
                    if (Expr is string)
                    {
                        if (Conversions.ToBoolean(Expr.contains((object)'$')))
                        {
                            if (Conversions.ToBoolean(Expr.contains((object)'"')))
                            {
                                return false;
                            }
                            else
                            {
                            }

                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

                return false;
            }

            public bool IsBoolean(ref object Expr)
            {
                bool IsBooleanRet = default;
                IsBooleanRet = false;
                if (Conversions.ToBoolean(Operators.OrObject(Operators.ConditionalCompareObjectEqual(Expr, "TRUE", false), Operators.ConditionalCompareObjectEqual(Expr, "FALSE", false))))
                {
                    return true;
                }

                return IsBooleanRet;
            }
            /// <summary>
            /// if token is an array then it contains an expression
            /// </summary>
            /// <param name="Expr"></param>
            /// <returns></returns>
            public bool IsArray(ref object Expr)
            {
                try
                {
                    if (Expr is Array)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            #endregion
        }
    }
}
#endregion
// EVALUATOR - ENVIRONMANTAL MEMORY
// 
namespace SDK.SmallProgLang
{
    namespace Evaluator
    {
        [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
        public class EnvironmentalMemory
        {
            /// <summary>
            /// Structure for variable
            /// </summary>
            public struct Variable
            {
                /// <summary>
                /// Variabel name
                /// </summary>
                public string Name;
                /// <summary>
                /// Value of variable
                /// </summary>
                public object Value;
                /// <summary>
                /// Type ass string identifier
                /// </summary>
                public AST_NODE Type;
            }
            /// <summary>
            /// Memory for variables
            /// </summary>
            private List<Variable> LocalMemory;
            private EnvironmentalMemory mGlobalMemory;
            /// <summary>
            /// Global memeory passed in from parent environment
            /// </summary>
            /// <returns></returns>
            public EnvironmentalMemory GlobalMemory
            {
                get
                {
                    return mGlobalMemory;
                }
            }
            /// <summary>
            /// A global memeory is contained within environmemt for referencing
            /// </summary>
            /// <param name="GlobalMemory"></param>
            public EnvironmentalMemory(ref EnvironmentalMemory GlobalMemory)
            {
                LocalMemory = new List<Variable>();
                mGlobalMemory = GlobalMemory;
            }

            private List<Variable> _AddInternalLiterals()
            {
                var Lst = new List<Variable>();
                var BTrue = new Variable();
                BTrue.Name = "TRUE";
                BTrue.Value = true;
                BTrue.Type = AST_NODE._boolean;
                Lst.Add(BTrue);
                var BFalse = new Variable();
                BFalse.Name = "FALSE";
                BFalse.Value = false;
                BFalse.Type = AST_NODE._boolean;
                Lst.Add(BFalse);
                var BNull = new Variable();
                BNull.Name = "NULL";
                BNull.Value = null;
                BNull.Type = AST_NODE._null;
                Lst.Add(BNull);
                return Lst;
            }
            /// <summary>
            /// Has no Global Memory
            /// </summary>
            public EnvironmentalMemory()
            {
                LocalMemory = _AddInternalLiterals();
                // mGlobalMemory = New EnvironmentalMemory


                // Me.GlobalMemory = GlobalMemory
            }
            /// <summary>
            /// Defines variable in environemnt if not previoulsy exisiting
            /// </summary>
            /// <param name="Name">Variable name</param>
            /// <param name="nType">stype such as string/ integer etc</param>
            /// <returns></returns>
            public object Define(ref string Name, string nType)
            {
                bool detected = false;
                var var = new Variable();
                var.Name = Name;
                var.Type = (AST_NODE)Conversions.ToInteger(nType);
                if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(GetVar(ref Name), "NULL", false)))
                {
                    LocalMemory.Add(var);
                    return true;
                }

                return "ERROR : Unable to define Variable Name: -: -" + Name + " -: -:Exists Previously in memory";
            }
            /// <summary>
            /// Assigns a value to the variable
            /// </summary>
            /// <param name="Name">Variable name(Previoulsy Exisiting)</param>
            /// <param name="Value">Value to be stored</param>
            /// <returns></returns>
            public object AssignValue(ref string Name, ref object Value)
            {
                foreach (var item in LocalMemory)
                {
                    if ((item.Name ?? "") == (Name ?? ""))
                    {
                        // IN LOCAL
                        item.Value = Value;
                        return true;
                    }
                }

                if (GlobalMemory is object)
                {
                    foreach (var item in GlobalMemory.LocalMemory)
                    {
                        if ((item.Name ?? "") == (Name ?? ""))
                        {
                            // 'Found in Global
                            item.Value = Value;
                            return true;
                        }
                    }
                }
                else
                {
                    // THIS IS THE GLOBAL MEMORY
                }

                return Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject("ERROR : Unable to Assign Value  :=", Value), " -: Not in Memory :- Variable Unknown :="), Name);
            }
            /// <summary>
            /// Returns the value from the stored variable
            /// </summary>
            /// <param name="Name"></param>
            /// <returns></returns>
            public object GetVar(ref string Name)
            {
                foreach (var item in LocalMemory)
                {
                    if ((item.Name ?? "") == (Name ?? ""))
                    {
                        // Found in Local Memeory
                        return item.Value;
                    }
                }

                if (GlobalMemory is object)
                {
                    if (Conversions.ToBoolean(Operators.ConditionalCompareObjectNotEqual(GlobalMemory.GetVar(ref Name), "NULL", false)))
                    {
                        return GlobalMemory.GetVar(ref Name);
                    }
                }
                else
                {
                    // THIS IS THE GLOBAL MEMORY
                }

                return "NULL";
            }

            public bool CheckVar(ref string VarName)
            {
                bool CheckVarRet = default;
                foreach (var item in LocalMemory)
                {
                    if ((item.Name ?? "") == (VarName ?? ""))
                    {
                        return true;
                    }
                }

                CheckVarRet = false;
                return CheckVarRet;
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson;
            }
        }
    }
}
#endregion
// HELPER EXTENSIONS
// 
#region EXTENSIONS
namespace SDK.SmallProgLang
{
    /// <summary>
    /// Minor Extension Methods; Required for json formatting
    /// </summary>
    public static class EXT
    {
        public static string FormatJsonOutput(this string jsonString)
        {
            var stringBuilder = new StringBuilder();
            bool escaping = false;
            bool inQuotes = false;
            int indentation = 0;
            foreach (char character in jsonString)
            {
                if (escaping)
                {
                    escaping = false;
                    stringBuilder.Append(character);
                }
                else if (character == '\\')
                {
                    escaping = true;
                    stringBuilder.Append(character);
                }
                else if (character == '"')
                {
                    inQuotes = !inQuotes;
                    stringBuilder.Append(character);
                }
                else if (!inQuotes)
                {
                    if (character == ',')
                    {
                        stringBuilder.Append(character);
                        stringBuilder.Append(Constants.vbCrLf);
                        stringBuilder.Append(Conversions.ToChar(Constants.vbTab), indentation);
                    }
                    else if (character == '[' || character == '{')
                    {
                        stringBuilder.Append(character);
                        stringBuilder.Append(Constants.vbCrLf);
                        stringBuilder.Append(Conversions.ToChar(Constants.vbTab), Interlocked.Increment(ref indentation));
                    }
                    else if (character == ']' || character == '}')
                    {
                        stringBuilder.Append(Constants.vbCrLf);
                        stringBuilder.Append(Conversions.ToChar(Constants.vbTab), Interlocked.Decrement(ref indentation));
                        stringBuilder.Append(character);
                    }
                    else if (character == ':')
                    {
                        stringBuilder.Append(character);
                        stringBuilder.Append(Constants.vbTab);
                    }
                    else if (!char.IsWhiteSpace(character))
                    {
                        stringBuilder.Append(character);
                    }
                }
                else
                {
                    stringBuilder.Append(character);
                }
            }

            return stringBuilder.ToString();
        }

        public static string ToJson(this ref object item)
        {
            var Converter = new JavaScriptSerializer();
            return Converter.Serialize(item);
        }

        public static IEnumerable<string> SplitAtNewLine(this string input)
        {
            return input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public static string ExtractLastChar(this ref string InputStr)
        {
            string ExtractLastCharRet = default;
            ExtractLastCharRet = Strings.Right(InputStr, 1);
            return ExtractLastCharRet;
        }

        public static string ExtractFirstChar(this ref string InputStr)
        {
            string ExtractFirstCharRet = default;
            ExtractFirstCharRet = Strings.Left(InputStr, 1);
            return ExtractFirstCharRet;
        }
    }
}
#endregion
// SPYDAZWEB AL VIRTUAL MACHINE
// 
// 
#region SPYDAZWEB ASSEMBLY LANGUAGE VIRTUAL MACHINE
// EXTENSIONS
// 
namespace SDK.SAL
{
    public static class Ext
    {
        public static IEnumerable<string> SplitAtNewLine(this string input)
        {
            return input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public static string ExtractLastChar(this ref string InputStr)
        {
            string ExtractLastCharRet = default;
            ExtractLastCharRet = Strings.Right(InputStr, 1);
            return ExtractLastCharRet;
        }

        public static string ExtractFirstChar(this ref string InputStr)
        {
            string ExtractFirstCharRet = default;
            ExtractFirstCharRet = Strings.Left(InputStr, 1);
            return ExtractFirstCharRet;
        }
    }
}
// HELPER FUNCTIONS
// 
namespace SDK.SAL
{
    public static class SalCode_Helpers
    {
        private static List<string> _Binary_op(ref int Left, ref int Right, ref string iOperator)
        {
            var PROGRAM = new List<string>();
            switch (iOperator ?? "")
            {
                case "-":
                    {
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Left.ToString());
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Right.ToString());
                        PROGRAM.Add("SUB");
                        PROGRAM.Add("PRINT_M");
                        break;
                    }

                case "+":
                    {
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Left.ToString());
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Right.ToString());
                        PROGRAM.Add("ADD");
                        PROGRAM.Add("PRINT_M");
                        break;
                    }

                case "/":
                    {
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Left.ToString());
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Right.ToString());
                        PROGRAM.Add("DIV");
                        PROGRAM.Add("PRINT_M");
                        break;
                    }

                case "*":
                    {
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Left.ToString());
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Right.ToString());
                        PROGRAM.Add("MUL");
                        PROGRAM.Add("PRINT_M");
                        break;
                    }

                case ">":
                    {
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Left.ToString());
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Right.ToString());
                        PROGRAM.Add("IS_GT");
                        PROGRAM.Add("PRINT_M");
                        break;
                    }

                case "<":
                    {
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Left.ToString());
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Right.ToString());
                        PROGRAM.Add("IS_LT");
                        PROGRAM.Add("PRINT_M");
                        break;
                    }

                case ">=":
                    {
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Left.ToString());
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Right.ToString());
                        PROGRAM.Add("IS_GTE");
                        PROGRAM.Add("PRINT_M");
                        break;
                    }

                case "<=":
                    {
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Left.ToString());
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Right.ToString());
                        PROGRAM.Add("IS_LTE");
                        PROGRAM.Add("PRINT_M");
                        break;
                    }

                case "=":
                    {
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Left.ToString());
                        PROGRAM.Add("PUSH");
                        PROGRAM.Add(Right.ToString());
                        PROGRAM.Add("IS_EQ");
                        PROGRAM.Add("PRINT_M");
                        break;
                    }
            }

            return PROGRAM;
        }

        private static bool _CheckCondition(ref int Left, ref int Right, ref string iOperator)
        {
            switch (iOperator ?? "")
            {
                case ">":
                    {
                        if (Left > Right)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                        break;
                    }

                case "<":
                    {
                        if (Left < Right)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                        break;
                    }

                case ">=":
                    {
                        if (Left >= Right)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                        break;
                    }

                case "<=":
                    {
                        if (Left <= Right)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                        break;
                    }

                case "=":
                    {
                        if (Left == Right)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                        break;
                    }

                default:
                    {
                        return false;
                    }
            }
        }

        private static List<string> _print(ref string Str)
        {
            var PROGRAM = new List<string>();
            PROGRAM.Add("PUSH");
            PROGRAM.Add(Str.Replace("'", ""));
            PROGRAM.Add("PRINT_M");
            return PROGRAM;
        }
        #region IF
        /// <summary>
        ///       If ["condition"] Then ["If-True"]  End
        /// </summary>
        /// <param name="_If">If ["condition"]</param>
        /// <param name="_Then">Then ["If-True"]  End</param>
        private static List<string> _if_then(ref List<string> _If, ref List<string> _Then)
        {
            var PROGRAM = new List<string>();
            // ADD CONDITION
            PROGRAM.AddRange(_If);
            // JUMP TO END - IF FALSE
            PROGRAM.Add("JIF_F");
            PROGRAM.Add(_Then.Count.ToString());
            PROGRAM.AddRange(_Then);
            // END

            return PROGRAM;
        }
        /// <summary>
        ///     If ["condition"] Then ["If-True"] ELSE ["If-False"] End
        /// </summary>
        /// <param name="_If">If ["condition"]</param>
        /// <param name="_Then">Then ["If-True"]</param>
        /// <param name="_Else">ELSE ["If-False"]</param>
        private static List<string> _if_then_else(ref List<string> _If, ref List<string> _Then, ref List<string> _Else)
        {
            var PROGRAM = new List<string>();
            // ADD CONDITION
            PROGRAM.AddRange(_If);
            // JUMP TO ELSE IF FALSE
            PROGRAM.Add("JIF_F");
            PROGRAM.Add((_If.Count + _Then.Count + 2).ToString());
            // THEN PART
            PROGRAM.AddRange(_Then);
            // JUMP TO END
            PROGRAM.Add("JMP");
            PROGRAM.Add((_If.Count + _Then.Count + 4 + _Else.Count).ToString());
            // ELSE PART
            PROGRAM.AddRange(_Else);
            // END
            return PROGRAM;
        }
        #endregion
    }
}
// STACK-MEMORY-FRAME
// 
namespace SDK.SAL
{
    /// <summary>
    /// Memory frame for Variables 
    /// </summary>
    public class StackMemoryFrame
    {
        public struct Var
        {
            public int Value;
            public string VarNumber;
        }

        public int ReturnAddress;
        public List<Var> Variables;

        public StackMemoryFrame(ref int ReturnAddress)
        {
            ReturnAddress = ReturnAddress;
            Variables = new List<Var>();
        }

        public int GetReturnAddress()
        {
            return ReturnAddress;
        }

        public int GetVar(ref string VarName)
        {
            foreach (var item in Variables)
            {
                if ((item.VarNumber ?? "") == (VarName ?? ""))
                {
                    return item.Value;
                }
            }

            return 0;
        }

        public void SetVar(ref string VarName, ref object value)
        {
            var item = new Var();
            item.VarNumber = VarName;
            item.Value = Conversions.ToInteger(value);
            Variables.Add(item);
        }
    }
}
// API
// 
namespace SDK.SAL
{
    public class X86API
    {
        public static string RunMachineCode(ref string Code)
        {
            Code = Strings.UCase(Code);
            var PROG = Strings.Split(Code.Replace(Constants.vbCrLf, " "), " ");
            var InstructionLst = new List<string>();
            var ROOT = new TreeNode();
            ROOT.Text = "ASSEMBLY_CODE";
            int Count = 0;
            foreach (var item in PROG)
            {
                Count += 1;
                if (!string.IsNullOrEmpty(item))
                {
                    var NDE = new TreeNode();
                    NDE.Text = Count + ": " + item;
                    ROOT.Nodes.Add(NDE);
                    InstructionLst.Add(item);
                }
            }
            // cpu - START



            string argThreadName = "Test";
            var CPU = new ZX81_CPU(ref argThreadName, ref InstructionLst);
            CPU.RUN();
            Tree = ROOT;
            return "CURRENT POINTER = " + CPU.Get_Instruction_Pointer_Position + Constants.vbCr + "CONTAINED DATA = " + CPU.Peek();
        }

        public static TreeNode Tree = new TreeNode();
    }
}
// CPU
// 
namespace SDK.SAL
{
    /// <summary>
    /// SpydazWeb X86 Assembly language Virtual X86 Processor
    /// </summary>
    public class ZX81_CPU
    {
        public ZX81_GPU GPU = new ZX81_GPU();

        #region CPU


        /// <summary>
        /// Used to monitor the Program status ; 
        /// If the program is being executed then the cpu must be running
        /// the Property value can only be changed within the program
        /// </summary>
        public bool RunningState
        {
            get
            {
                if (mRunningState == State.RUN)
                {
                    return true;
                }
                else
                {
                    return false;
                }

                return Conversions.ToBoolean(mRunningState);
            }
        }

        private State mRunningState = State.HALT;
        /// <summary>
        /// This is the cpu stack memory space; 
        /// Items being interrogated will be placed in this memeory frame
        /// calling functions will access this frame ; 
        /// the cpu stack can be considered to be a bus; Functions are devices / 
        /// or gate logic which is connected to the bus via the cpu; 
        /// </summary>
        private Stack CPU_CACHE = new Stack();
        /// <summary>
        /// Returns the Current position of the instruction Pointer 
        /// in the Program being executed The instruction Pionet can be manipulated 
        /// Jumping backwards and forwards in the program code.
        /// </summary>
        /// <returns></returns>
        private int GetInstructionAddress
        {
            get
            {
                return InstructionAdrress;
            }
        }

        public int Get_Instruction_Pointer_Position
        {
            get
            {
                return GetInstructionAddress;
            }
        }
        /// <summary>
        /// Returns the current data in the stack
        /// </summary>
        /// <returns></returns>
        public List<string> Get_Current_Stack_Data
        {
            get
            {
                List<string> Get_Current_Stack_DataRet = default;
                Get_Current_Stack_DataRet = new List<string>();
                foreach (var item in CPU_CACHE)
                    Get_Current_Stack_DataRet.Add(item.ToString());
                return Get_Current_Stack_DataRet;
            }
        }
        /// <summary>
        /// Returns the Current Cache (the stack)
        /// </summary>
        /// <returns></returns>
        public Stack View_C_P_U
        {
            get
            {
                return CPU_CACHE;
            }
        }
        /// <summary>
        /// Returns the current object on top of the stack
        /// </summary>
        /// <returns></returns>
        public object Get_Current_Stack_Item
        {
            get
            {
                return Peek();
            }
        }
        /// <summary>
        /// Used to pass the intensive error messaging required; 
        /// </summary>
        private VM_ERR CPU_ERR;
        /// <summary>
        /// Used to hold the Program being sent to the CPU
        /// A list of objects has been chosen to allow for a Richer CPU
        /// enabling for objects to be passed instead of strings; 
        /// due to this being a compiler as well as a morenized CPU
        /// converting strings to string or integers or booleans etc 
        /// makes it much harder to create quick easy code;
        /// the sender is expeected to understand the logic of the items in the program
        /// the decoder only decodes bassed on what is expected; 
        /// </summary>
        private List<object> ProgramData = new List<object>();
        /// <summary>
        /// the InstructionAdrress is the current position in the program; 
        /// it could be considered to be the line numbe
        /// </summary>
        private int InstructionAdrress;
        /// <summary>
        /// Name of current program or process running in CPU thread
        /// </summary>
        public string PROCESS_NAME = "";
        /// <summary>
        /// Used for local memory frame
        /// </summary>
        private StackMemoryFrame CurrentCache;
        /// <summary>
        /// Used to Store memory frames (The Heap)
        /// </summary>
        private Stack R_A_M = new Stack();
        /// <summary>
        /// Returns the Ram as a Stack of Stack Memeory frames;
        /// </summary>
        /// <returns></returns>
        public Stack View_R_A_M
        {
            get
            {
                return R_A_M;
            }
        }

        private int WaitTime = 0;


        /// <summary>
        /// Each Program can be considered to be a task or thread; 
        /// A name should be assigned to the Process; 
        /// Processes themselves can be stacked in a higher level processor,
        /// allowing for paralel processing of code
        /// This process allows for the initialization of the CPU; THe Prgram will still need to be loaded
        /// </summary>
        /// <param name="ThreadName"></param>
        public ZX81_CPU(ref string ThreadName)
        {
            PROCESS_NAME = ThreadName;
            // Initializes a Stack for use (Memory for variables in code can be stored here)
            int argReturnAddress = 0;
            CurrentCache = new StackMemoryFrame(ref argReturnAddress);
        }
        /// <summary>
        /// Load Program and Executes Code on CPU
        /// </summary>
        /// <param name="ThreadName">A name is required to Identify the Process</param>
        /// <param name="Program"></param>
        public ZX81_CPU(ref string ThreadName, ref List<string> Program)
        {
            PROCESS_NAME = ThreadName;
            // Initializes a Stack for use (Memory for variables in code can be stored here)
            int argReturnAddress = 0;
            CurrentCache = new StackMemoryFrame(ref argReturnAddress);
            LoadProgram(ref Program);
            mRunningState = State.RUN;
            RUN();
        }
        /// <summary>
        /// Loads items in to the program cache; 
        /// this has been added to allow for continuious running of the VM
        /// the run/wait Command will be added to the assembler 
        /// enabling for the pausing of the program and restarting of the program stack
        /// </summary>
        /// <param name="Prog"></param>
        public void LoadProgram(ref List<string> Prog)
        {
            ProgramData.AddRange(Prog);
        }
        /// <summary>
        /// Loads items in to the program cache; 
        /// this has been added to allow for continuious running of the VM
        /// the run/wait Command will be added to the assembler 
        /// enabling for the pausing of the program and restarting of the program stack
        /// </summary>
        /// <param name="Prog"></param>
        public void LoadProgram(ref List<object> Prog)
        {
            ProgramData.AddRange(Prog);
            // Initializes a Stack for use (Memory for variables in code can be stored here)
            int argReturnAddress = 0;
            CurrentCache = new StackMemoryFrame(ref argReturnAddress);
        }
        /// <summary>
        /// Begins eexecution of the instructions held in program data
        /// </summary>
        public void RUN()
        {
            while (IsHalted == false)
            {
                if (IsWait == true)
                {
                    for (int I = 0, loopTo = WaitTime; I <= loopTo; I++)
                    {
                    }

                    EXECUTE();
                    mRunningState = State.RUN;
                }
                else
                {
                    EXECUTE();
                }
            }
        }
        /// <summary>
        /// Checks the status of the cpu
        /// </summary>
        /// <returns></returns>
        public bool IsHalted
        {
            get
            {
                if (mRunningState == State.HALT == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool IsWait
        {
            get
            {
                if (Conversions.ToInteger(RunningState) == (int)State.PAUSE)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Executes the next instruction in the Program
        /// Each Instruction is fed individually to the decoder : 
        /// The Execute cycle Checks the Current State to determine 
        /// if to fetch the next instruction to be decoded;(or EXECUTED) -
        /// The decoder contains the Chip logic
        /// </summary>
        public void EXECUTE()
        {
            // The HALT command is used to STOP THE CPU
            if (IsHalted == false)
            {
                DECODE(ref Fetch());
            }
            else
            {
                // CPU_ERR = New VM_ERR("CPU HALTED", Me)
                // CPU_ERR.RaiseErr()
                // ERR - state stopped
            }
        }
        /// <summary>
        /// Program Instructions can be boolean/String or integer 
        /// so an object is assumed enabling for later classification
        /// of the instructions at the decoder level : 
        /// The Fetch Cycle Fetches the next Instruction in the Program to be executed:
        /// It is fed to the decoder to be decoded and executed
        /// </summary>
        /// <returns></returns>
        private object Fetch()
        {
            // Check that it is not the end of the program
            if (InstructionAdrress >= ProgramData.Count)
            {
                string argErr = "End of instruction list reached! No more instructions in Program data Error Invalid Address -FETCH(Missing HALT command)";
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                // End of instruction list reached no more instructions in program data
                // HALT CPU!!
                mRunningState = State.HALT;
            }
            // 
            else
            {
                // Each Instruction is considered to be a string 
                // or even a integer or boolean the string is the most universal
                var CurrentInstruction = ProgramData[InstructionAdrress];
                // Move to next instruction
                InstructionAdrress += 1;
                return CurrentInstruction;
            }

            return null;
        }
        /// <summary>
        /// Contains MainInstruction Set: Decode Cycle Decodes program instructions from the program 
        /// the Insruction pointer points to the current Instruction being feed into the decoder: 
        /// Important Note : the stack will always point to the data at top of the CPU CACHE (Which is Working Memory);
        ///                 THe memory frames being used are Extensions of this memeory and can be seen as registers, 
        ///                 itself being a memory stack (stack of memory frames)
        /// </summary>
        /// <param name="ProgramInstruction"></param>
        public void DECODE(ref object ProgramInstruction)
        {
            switch (ProgramInstruction)
            {

                #region Basic Assembly
                case "WAIT":
                    {
                        WaitTime = int.Parse(Fetch().ToString());
                        mRunningState = State.PAUSE;
                        break;
                    }

                case "HALT":
                    {
                        // Stop the CPU
                        mRunningState = State.HALT;
                        break;
                    }

                case "PAUSE":
                    {
                        WaitTime = int.Parse(Fetch().ToString());
                        mRunningState = State.PAUSE;
                        break;
                    }

                case "RESUME":
                    {
                        if (mRunningState == State.PAUSE)
                        {
                            mRunningState = State.RUN;
                            RUN();
                        }

                        break;
                    }
                // A Wait time Maybe Neccasary
                case "DUP":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 1)
                            {
                                // Get Current item on the stack
                                string n = Peek();
                                // Push another copy onto the stack
                                object argValue = n;
                                Push(ref argValue);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr = "STACK ERROR : Stack Not intialized - DUP";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - DUP";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "POP":
                    {
                        CheckStackHasAtLeastOneItem();
                        Pop();
                        break;
                    }

                case "PUSH":
                    {
                        // Push
                        Push(ref Fetch());
                        break;
                    }

                case "JMP":
                    {
                        CheckStackHasAtLeastOneItem();
                        // "Should have the address after the JMP instruction"
                        // ' The word after the instruction will contain the address to jump to
                        int address = int.Parse(Fetch().ToString());
                        JUMP(ref address);
                        break;
                    }

                case "JIF_T":
                    {
                        CheckStackHasAtLeastOneItem();
                        // "Should have the address after the JIF instruction"
                        // ' The word after the instruction will contain the address to jump to
                        int address = int.Parse(Fetch().ToString());
                        JumpIf_TRUE(ref address);
                        break;
                    }

                case "JIF_F":
                    {
                        CheckStackHasAtLeastOneItem();
                        // "Should have the address after the JIF instruction"
                        // ' The word after the instruction will contain the address to jump to
                        int address = int.Parse(Fetch().ToString());
                        JumpIf_False(ref address);
                        break;
                    }

                case "LOAD":
                    {
                        CheckStackHasAtLeastOneItem();
                        // lOADS A VARIABLE
                        int varNumber = int.Parse(Fetch().ToString());
                        string localGetVar() { string argVarNumber = varNumber.ToString(); var ret = GetCurrentFrame().GetVar(ref argVarNumber); return ret; }

                        CPU_CACHE.Push(localGetVar());
                        break;
                    }

                case "REMOVE":
                    {
                        // lOADS A VARIABLE
                        int varNumber = int.Parse(Fetch().ToString());
                        string argVarNumber = varNumber.ToString();
                        GetCurrentFrame().RemoveVar(ref argVarNumber);
                        break;
                    }

                case "STORE":
                    {
                        // "Should have the variable number after the STORE instruction"
                        int varNumber = int.Parse(Fetch().ToString());
                        CheckStackHasAtLeastOneItem();
                        string argVarNumber1 = varNumber.ToString();
                        string argvalue = Conversions.ToString(CPU_CACHE.Peek());
                        CurrentCache.SetVar(ref argVarNumber1, ref argvalue);
                        R_A_M.Push(CurrentCache);
                        break;
                    }

                case "CALL":
                    {
                        // The word after the instruction will contain the function address
                        int address = int.Parse(Fetch().ToString());
                        CheckJumpAddress(ref address);
                        R_A_M.Push(new StackMemoryFrame(ref InstructionAdrress));  // // Push a New stack frame on to the memory heap
                        InstructionAdrress = address; // // And jump!
                        break;
                    }

                case "RET":
                    {
                        // Pop the stack frame And return to the previous address from the memory heap
                        CheckThereIsAReturnAddress();
                        int returnAddress = GetCurrentFrame().GetReturnAddress();
                        InstructionAdrress = returnAddress;
                        R_A_M.Pop();
                        break;
                    }
                #endregion
                #region PRINT
                // PRINT TO MONITOR
                case "PRINT_M":
                    {
                        string argStr = Conversions.ToString(Pop());
                        GPU.ConsolePrint(ref argStr);
                        break;
                    }

                case "CLS":
                    {
                        GPU.Console_CLS();
                        break;
                    }
                // PRINT TO CONSOLE
                case "PRINT_C":
                    {
                        // System.Console.WriteLine("------------ ZX 81 ----------" & vbNewLine & Peek())
                        Console.ReadKey();
                        break;
                    }
                #endregion
                #region Operations
                case "ADD":
                    {
                        // ADD
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue1 = localBINARYOP();
                                Push(ref argValue1);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr1 = "Error Decoding Invalid Instruction - ADD";
                                CPU_ERR = new VM_ERR(ref argErr1, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - ADD";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "SUB":
                    {
                        // SUB
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP1() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue2 = localBINARYOP1();
                                Push(ref argValue2);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr2 = "Error Decoding Invalid Instruction - SUB";
                                CPU_ERR = new VM_ERR(ref argErr2, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - SUB";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "MUL":
                    {
                        // MUL
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP2() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue3 = localBINARYOP2();
                                Push(ref argValue3);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr3 = "Error Decoding Invalid Instruction - MUL";
                                CPU_ERR = new VM_ERR(ref argErr3, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - MUL";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "DIV":
                    {
                        // DIV
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP3() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue4 = localBINARYOP3();
                                Push(ref argValue4);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr4 = "Error Decoding Invalid Instruction - DIV";
                                CPU_ERR = new VM_ERR(ref argErr4, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - DIV";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "NOT":
                    {
                        CheckStackHasAtLeastOneItem();
                        int localNOT_ToBool() { int argVal = Conversions.ToInteger(Pop()); var ret = NOT_ToBool(ref argVal); return ret; }

                        string localToInt() { bool argBool = Conversions.ToBoolean(hs064eea10285542069406c75256a7dff5()); var ret = ToInt(ref argBool); return ret; }

                        object argValue5 = localToInt();
                        Push(ref argValue5);
                        break;
                    }

                case "AND":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP4() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue6 = localBINARYOP4();
                                Push(ref argValue6);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr5 = "Error Decoding Invalid Instruction - AND";
                                CPU_ERR = new VM_ERR(ref argErr5, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - AND";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "OR":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP5() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue7 = localBINARYOP5();
                                Push(ref argValue7);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr6 = "Error Decoding Invalid Instruction - OR";
                                CPU_ERR = new VM_ERR(ref argErr6, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - OR";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "IS_EQ":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP6() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue8 = localBINARYOP6();
                                Push(ref argValue8);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr7 = "Error Decoding Invalid Instruction - ISEQ";
                                CPU_ERR = new VM_ERR(ref argErr7, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - OR";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "IS_GTE":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP7() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue9 = localBINARYOP7();
                                Push(ref argValue9);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr8 = "Error Decoding Invalid Instruction - IS_GTE";
                                CPU_ERR = new VM_ERR(ref argErr8, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - IS_GTE";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "IS_GT":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP8() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue10 = localBINARYOP8();
                                Push(ref argValue10);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr9 = "Error Decoding Invalid Instruction - IS_GT";
                                CPU_ERR = new VM_ERR(ref argErr9, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - IS_GT";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "IS_LT":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP9() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue11 = localBINARYOP9();
                                Push(ref argValue11);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr10 = "Error Decoding Invalid Instruction - IS_LT";
                                CPU_ERR = new VM_ERR(ref argErr10, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - IS_LT";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "IS_LTE":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 2)
                            {
                                string localBINARYOP10() { string argINSTRUCTION = Conversions.ToString(ProgramInstruction); var ret = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop()))); return ret; }

                                object argValue12 = localBINARYOP10();
                                Push(ref argValue12);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr11 = "Error Decoding Invalid Instruction - IS_LTE";
                                CPU_ERR = new VM_ERR(ref argErr11, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - IS_LTE";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }
                #endregion
                #region POSITIVE VS NEGATIVE
                case "TO_POS":
                    {
                        if (CPU_CACHE.Count >= 1)
                        {
                            var argValue13 = ToPositive(int.Parse(Conversions.ToString(Pop())));
                            Push(ref argValue13);
                        }
                        else
                        {
                            mRunningState = State.HALT;
                            string argErr12 = "Error Decoding Invalid arguments - POS";
                            CPU_ERR = new VM_ERR(ref argErr12, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "TO_NEG":
                    {
                        if (CPU_CACHE.Count >= 1)
                        {
                            var argValue14 = ToNegative(int.Parse(Conversions.ToString(Pop())));
                            Push(ref argValue14);
                        }
                        else
                        {
                            mRunningState = State.HALT;
                            string argErr13 = "Error Decoding Invalid arguments - NEG";
                            CPU_ERR = new VM_ERR(ref argErr13, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }
                #endregion
                #region Extended JmpCmds
                case "JIF_GT":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 3)
                            {
                                int address = int.Parse(Fetch().ToString());
                                string argINSTRUCTION = "IS_GT";
                                object argValue15 = BINARYOP(ref argINSTRUCTION, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop())));
                                Push(ref argValue15);
                                JumpIf_TRUE(ref address);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr14 = "Error Decoding Invalid arguments - JIF_GT";
                                CPU_ERR = new VM_ERR(ref argErr14, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - JIF_GT";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "JIF_LT":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 3)
                            {
                                int address = int.Parse(Fetch().ToString());
                                string argINSTRUCTION1 = "IS_LT";
                                object argValue16 = BINARYOP(ref argINSTRUCTION1, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop())));
                                Push(ref argValue16);
                                JumpIf_TRUE(ref address);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr15 = "Error Decoding Invalid arguments - JIF_LT";
                                CPU_ERR = new VM_ERR(ref argErr15, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - JIF_LT";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }

                case "JIF_EQ":
                    {
                        try
                        {
                            if (CPU_CACHE.Count >= 3)
                            {
                                int address = int.Parse(Fetch().ToString());
                                string argINSTRUCTION2 = "IS_EQ";
                                object argValue17 = BINARYOP(ref argINSTRUCTION2, int.Parse(Conversions.ToString(Pop())), int.Parse(Conversions.ToString(Pop())));
                                Push(ref argValue17);
                                JumpIf_TRUE(ref address);
                            }
                            else
                            {
                                mRunningState = State.HALT;
                                string argErr16 = "Error Decoding Invalid arguments - JIF_EQ";
                                CPU_ERR = new VM_ERR(ref argErr16, this);
                                CPU_ERR.RaiseErr();
                            }
                        }
                        catch (Exception ex)
                        {
                            mRunningState = State.HALT;
                            string argErr = "Error Decoding Invalid Instruction - JIF_EQ";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                        }

                        break;
                    }
                #endregion
                #region INCREMENT/DECREMENT
                case "INCR":
                    {
                        CheckStackHasAtLeastOneItem();
                        object argValue18 = int.Parse(Conversions.ToString(Pop())) + 1;
                        Push(ref argValue18);
                        break;
                    }

                case "DECR":
                    {
                        CheckStackHasAtLeastOneItem();
                        #endregion

                        object argValue19 = int.Parse(Conversions.ToString(Pop())) - 1;
                        Push(ref argValue19);
                        break;
                    }

                default:
                    {
                        mRunningState = State.HALT;
                        break;
                    }
                    // CPU_ERR = New VM_ERR("Error Decoding Invalid Instruction", Me)
                    // CPU_ERR.RaiseErr()

            }
        }
        #endregion
        #region functions required by cpu and assembly language
        #region Handle Boolean
        private string ToInt(ref bool Bool)
        {
            if (Bool == false)
            {
                return 0.ToString();
            }
            else
            {
                return 1.ToString();
            }
        }

        private bool ToBool(ref int Val)
        {
            if (Val == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int NOT_ToBool(ref int Val)
        {
            if (Val == 1)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        #endregion
        #region Functional Parts
        /// <summary>
        /// Checks if there is a jump address available
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private bool CheckJumpAddress(ref int address)
        {
            try
            {
                // Check if it is in range
                if (address < 0 | address >= ProgramData.Count)
                {
                    // Not in range
                    mRunningState = State.HALT;
                    string argErr = string.Format("Invalid jump address %d at %d", address, GetInstructionAddress);
                    CPU_ERR = new VM_ERR(ref argErr, this);
                    CPU_ERR.RaiseErr();
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                mRunningState = State.HALT;
                string argErr = string.Format("Invalid jump address %d at %d", address, GetInstructionAddress);
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                return false;
            }
        }
        /// <summary>
        /// Function used by the internal functions to check if there is a return address
        /// </summary>
        /// <returns></returns>
        private bool CheckThereIsAReturnAddress()
        {
            try
            {
                if (R_A_M.Count >= 1)
                {
                    return true;
                }
                else
                {
                    mRunningState = State.HALT;
                    string argErr = string.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress);
                    CPU_ERR = new VM_ERR(ref argErr, this);
                    CPU_ERR.RaiseErr();
                    return false;
                }
            }
            catch (Exception ex)
            {
                mRunningState = State.HALT;
                string argErr = string.Format("Invalid RET instruction: no current function call %d", GetInstructionAddress);
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                return false;
            }
        }
        /// <summary>
        /// RAM is a STACK MEMORY - Here we can take a look at the stack item
        /// </summary>
        /// <returns></returns>
        public StackMemoryFrame GetCurrentFrame()
        {
            if (R_A_M.Count > 0)
            {
                return (StackMemoryFrame)R_A_M.Pop();
            }
            else
            {
                return null;
                mRunningState = State.HALT;
                string argErr = "Error Decoding STACK MEMORY FRAME - GetCurrentFrame";
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
            }
        }
        /// <summary>
        /// Outputs stack data for verbose output
        /// </summary>
        /// <returns></returns>
        public string GetStackData()
        {
            string Str = "";
            object argOBJ = this;
            Str = ToJson(ref argOBJ);
            return Str;
        }

        private string ToJson(ref object OBJ)
        {
            var Converter = new JavaScriptSerializer();
            return Converter.Serialize(OBJ);
        }
        #endregion
        #region Operational Functions
        /// <summary>
        /// REQUIRED TO SEE IN-SIDE CURRENT POINTER LOCATION
        /// ----------Public For Testing Purposes-----------
        /// </summary>
        /// <returns></returns>
        public string Peek()
        {
            try
            {
                if (CPU_CACHE.Count > 0)
                {
                    return CPU_CACHE.Peek().ToString();
                }
                else
                {
                    mRunningState = State.HALT;
                    return "NULL";
                }
            }
            catch (Exception ex)
            {
                mRunningState = State.HALT;
                string argErr = "NULL POINTER CPU HALTED";
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                return "NULL";
            }
        }

        private string BINARYOP(ref string INSTRUCTION, int LEFT, int RIGHT)
        {
            if (INSTRUCTION is object)
            {
                switch (INSTRUCTION ?? "")
                {
                    case "IS_EQ":
                        {
                            try
                            {
                                string localToInt() { bool argBool = ToBool(ref LEFT) == ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal = Conversions.ToInteger(localToInt());
                                return Conversions.ToString(ToBool(ref argVal));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation - isEQ";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "IS_GT":
                        {
                            try
                            {
                                string localToInt1() { bool argBool = ToBool(ref LEFT) < ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal1 = Conversions.ToInteger(localToInt1());
                                return Conversions.ToString(ToBool(ref argVal1));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation - isGT";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "IS_GTE":
                        {
                            try
                            {
                                string localToInt2() { bool argBool = ToBool(ref LEFT) <= ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal2 = Conversions.ToInteger(localToInt2());
                                return Conversions.ToString(ToBool(ref argVal2));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation isGTE";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "IS_LT":
                        {
                            try
                            {
                                string localToInt3() { bool argBool = ToBool(ref LEFT) > ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal3 = Conversions.ToInteger(localToInt3());
                                return Conversions.ToString(ToBool(ref argVal3));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation isLT";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "IS_LE":
                        {
                            try
                            {
                                string localToInt4() { bool argBool = ToBool(ref LEFT) >= ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal4 = Conversions.ToInteger(localToInt4());
                                return Conversions.ToString(ToBool(ref argVal4));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation isLTE";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "ADD":
                        {
                            try
                            {
                                return (LEFT + RIGHT).ToString();
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -add";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "SUB":
                        {
                            try
                            {
                                return (RIGHT - LEFT).ToString();
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -sub";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "MUL":
                        {
                            try
                            {
                                return (RIGHT * LEFT).ToString();
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -mul";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "DIV":
                        {
                            try
                            {
                                return (RIGHT / (double)LEFT).ToString();
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -div";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "OR":
                        {
                            try
                            {
                                bool argBool = ToBool(ref LEFT) | ToBool(ref RIGHT);
                                return ToInt(ref argBool);
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -or";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "AND":
                        {
                            try
                            {
                                string localToInt5() { bool argBool = ToBool(ref LEFT) & ToBool(ref RIGHT); var ret = ToInt(ref argBool); return ret; }

                                int argVal5 = Conversions.ToInteger(localToInt5());
                                return Conversions.ToString(ToBool(ref argVal5));
                            }
                            catch (Exception ex)
                            {
                                mRunningState = State.HALT;
                                string argErr = "Invalid Operation -and";
                                CPU_ERR = new VM_ERR(ref argErr, this);
                                CPU_ERR.RaiseErr();
                            }

                            break;
                        }

                    case "NOT":
                        {
                            CheckStackHasAtLeastOneItem();
                            int localNOT_ToBool() { int argVal = Conversions.ToInteger(Pop()); var ret = NOT_ToBool(ref argVal); return ret; }

                            string localToInt6() { bool argBool = Conversions.ToBoolean(hs81fb00ba895d4f38a1a0361835449e83()); var ret = ToInt(ref argBool); return ret; }

                            object argValue = localToInt6();
                            Push(ref argValue);
                            break;
                        }

                    default:
                        {
                            mRunningState = State.HALT;
                            string argErr = "Invalid Operation -not";
                            CPU_ERR = new VM_ERR(ref argErr, this);
                            CPU_ERR.RaiseErr();
                            break;
                        }
                }
            }
            else
            {
                mRunningState = State.HALT;
            }

            return "NULL";
        }

        private bool CheckStackHasAtLeastOneItem(ref Stack Current)
        {
            if (Current.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckStackHasAtLeastOneItem()
        {
            if (CPU_CACHE.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckRamHasAtLeastOneItem()
        {
            if (CPU_CACHE.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void JumpIf_TRUE(ref int Address)
        {
            if (CheckJumpAddress(ref Address) == true & CheckStackHasAtLeastOneItem() == true)
            {
                int argVal = Conversions.ToInteger(Pop());
                if (ToBool(ref argVal))
                {
                    InstructionAdrress = Address;
                }
                else
                {
                }
            }
            else
            {
            }
        }

        private void JUMP(ref int Address)
        {
            if (CheckJumpAddress(ref Address) == true)
            {
                InstructionAdrress = Address;
            }
        }

        private void JumpIf_False(ref int Address)
        {
            if (CheckJumpAddress(ref Address) == true & CheckStackHasAtLeastOneItem() == true)
            {
                int argVal = Conversions.ToInteger(Pop());
                if (Conversions.ToBoolean(NOT_ToBool(ref argVal)))
                {
                    InstructionAdrress = Address;
                }
                else
                {
                }
            }
            else
            {
            }
        }
        /// <summary>
        /// Puts a value on the cpu stack to be available to funcitons
        /// </summary>
        /// <param name="Value"></param>
        private void Push(ref object Value)
        {
            try
            {
                CPU_CACHE.Push(Value);
            }
            catch (Exception ex)
            {
                string argErr = "STACK ERROR - CPU HALTED -push";
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                mRunningState = State.HALT;
            }
        }
        /// <summary>
        /// Pops a value of the cpu_Stack (current workspace)
        /// </summary>
        /// <returns></returns>
        private object Pop()
        {
            try
            {
                if (CPU_CACHE.Count >= 1)
                {
                    return CPU_CACHE.Pop();
                }
                else
                {
                    string argErr = "STACK ERROR - NULL POINTER CPU HALTED -pop";
                    CPU_ERR = new VM_ERR(ref argErr, this);
                    CPU_ERR.RaiseErr();
                    mRunningState = State.HALT;
                    return "NULL";
                }
            }
            catch (Exception ex)
            {
                string argErr = "STACK ERROR - NULL POINTER CPU HALTED -pop";
                CPU_ERR = new VM_ERR(ref argErr, this);
                CPU_ERR.RaiseErr();
                mRunningState = State.HALT;
                return "NULL";
            }

            return "NULL";
        }
        #endregion
        private object ToPositive(int Number)
        {
            return Math.Abs(Number);
        }

        private object ToNegative(int Number)
        {
            return Math.Abs(Number) * -1;
        }
        #endregion
        #region CPU _ INTERNAL _ Components
        private enum State
        {
            RUN,
            HALT,
            PAUSE
        }
        /// <summary>
        /// Memory frame for Variables
        /// </summary>
        public class StackMemoryFrame
        {
            public struct Var
            {
                public string Value;
                public string VarNumber;
            }

            public int ReturnAddress;
            public List<Var> Variables;

            public StackMemoryFrame(ref int ReturnAddress)
            {
                ReturnAddress = ReturnAddress;
                Variables = new List<Var>();
            }

            public int GetReturnAddress()
            {
                return ReturnAddress;
            }

            public string GetVar(ref string VarNumber)
            {
                foreach (var item in Variables)
                {
                    if ((item.VarNumber ?? "") == (VarNumber ?? ""))
                    {
                        return item.Value;
                    }
                }

                return 0.ToString();
            }

            public void SetVar(ref string VarNumber, ref string value)
            {
                var item = new Var();
                bool added = false;
                item.VarNumber = VarNumber;
                item.Value = value;
                foreach (var ITM in Variables)
                {
                    if ((ITM.VarNumber ?? "") == (VarNumber ?? ""))
                    {
                        ITM.Value = value;
                    }
                }

                Variables.Add(item);
            }

            public void RemoveVar(ref string VarNumber)
            {
                foreach (var item in Variables)
                {
                    if ((item.VarNumber ?? "") == (VarNumber ?? "") == true)
                    {
                        Variables.Remove(item);
                        break;
                    }
                    else
                    {
                    }
                }
            }
        }

        public class VM_ERR
        {
            private string ErrorStr = "";
            private FormDisplayConsole frm = new FormDisplayConsole();
            private ZX81_CPU CpuCurrentState;

            public VM_ERR(ref string Err, ZX81_CPU CPUSTATE)
            {
                ErrorStr = Err;
                CpuCurrentState = CPUSTATE;
            }

            public void RaiseErr()
            {
                if (frm is null)
                {
                    frm = new FormDisplayConsole();
                    frm.Show();
                    string argUserinput = ErrorStr + Constants.vbNewLine + CpuCurrentState.GetStackData();
                    frm.Print(ref argUserinput);
                }
                else
                {
                    frm.Show();
                    string argUserinput1 = ErrorStr + Constants.vbNewLine + CpuCurrentState.GetStackData();
                    frm.Print(ref argUserinput1);
                }
            }
        }
        #endregion
        /// <summary>
        /// COMMANDS FOR ASSEMBLY LANGUAGE FOR THIS CPU
        /// SPYDAZWEB_VM_X86
        /// </summary>
        public enum VM_x86_Cmds
        {
            _NULL,
            _REMOVE,
            _RESUME,
            _PUSH,
            _PULL,
            _PEEK,
            _WAIT,
            _PAUSE,
            _HALT,
            _DUP,
            _JMP,
            _JIF_T,
            _JIF_F,
            _JIF_EQ,
            _JIF_GT,
            _JIF_LT,
            _LOAD,
            _STORE,
            _CALL,
            _RET,
            _PRINT_M,
            _PRINT_C,
            _ADD,
            _SUB,
            _MUL,
            _DIV,
            _AND,
            _OR,
            _NOT,
            _IS_EQ,
            _IS_GT,
            _IS_GTE,
            _IS_LT,
            _IS_LTE,
            _TO_POS,
            _TO_NEG,
            _INCR,
            _DECR
        }
    }
}
// GPU
// 
namespace SDK.SAL
{
    public class ZX81_GPU
    {
        private FormDisplayConsole iMonitorConsole;

        public ZX81_GPU()
        {
            iMonitorConsole = new FormDisplayConsole();
        }

        public void ConsolePrint(ref string Str)
        {
            if (iMonitorConsole.Visible == false)
            {
                iMonitorConsole.Show();
            }
            else
            {
            }

            iMonitorConsole.Print(ref Str);
        }

        public void Console_CLS()
        {
            if (iMonitorConsole.Visible == false)
            {
                iMonitorConsole.Show();
            }
            else
            {
            }

            iMonitorConsole.CLS();
        }
    }
}
// RAM
// 
namespace SDK.SAL
{
    public class ZX81_RAM
    {
        public struct Variable
        {
            public string iName;
            public string iValue;
            public string iType;
        }
        /// <summary>
        /// Currently only Variables can be stored
        /// </summary>
        public List<Variable> CurrentVars;

        public ZX81_RAM()
        {
            CurrentVars = new List<Variable>();
        }

        // Variables

        public void UpdateVar(ref string VarName, ref string iVALUE)
        {
            foreach (var item in CurrentVars)
            {
                if ((item.iName ?? "") == (VarName ?? ""))
                {
                    var NiTEM = item;
                    NiTEM.iValue = iVALUE;
                    CurrentVars.Remove(item);
                    CurrentVars.Add(NiTEM);
                    break;
                }
                else
                {
                }
            }
        }

        public object RemoveVar(ref Variable Var)
        {
            foreach (var item in CurrentVars)
            {
                if ((item.iName ?? "") == (Var.iName ?? ""))
                {
                    CurrentVars.Remove(item);
                }
            }

            return Var;
        }

        public void AddVar(ref Variable Var)
        {
            if (CheckVar(ref Var.iName) == false)
            {
                CurrentVars.Add(Var);
            }
        }

        public bool CheckVar(ref string VarName)
        {
            bool CheckVarRet = default;
            foreach (var item in CurrentVars)
            {
                if ((item.iName ?? "") == (VarName ?? ""))
                {
                    return true;
                }
            }

            CheckVarRet = false;
            return CheckVarRet;
        }

        public string GetVar(ref string VarName)
        {
            foreach (var item in CurrentVars)
            {
                if ((item.iName ?? "") == (VarName ?? "") == true)
                {
                    if (item.iType == "BOOLEAN")
                    {
                        switch (item.iValue ?? "")
                        {
                            case 0:
                                {
                                    return "False";
                                }

                            case 1:
                                {
                                    return "True";
                                }

                            default:
                                {
                                    return item.iValue;
                                }
                        }
                    }
                    else
                    {
                        return item.iValue;
                    }
                }
                else
                {
                }
            }

            return VarName;
        }
    }
}
#endregion
#region PARSER

// MAIN PARSER
namespace SDK.SmallProgLang
{
    namespace Compiler
    {
        /// <summary>
        /// Programming Language Parser to AST
        /// </summary>
        public class ParserFactory
        {
            #region Propertys
            public List<string> ParserErrors = new List<string>();
            /// <summary>
            /// Currently held script
            /// </summary>
            public string iScript = "";
            /// <summary>
            /// To hold the look ahead value without consuming the value
            /// </summary>
            public string Lookahead;
            /// <summary>
            /// Tokenizer !
            /// </summary>
            private Lexer Tokenizer;
            private AstProgram iProgram;

            public AstProgram Program
            {
                get
                {
                    return iProgram;
                }
            }
            #endregion
            #region PARSER FACTORY
            /// <summary>
            /// Main Parser Function  
            /// Parses whole Script into a AST tree ; 
            /// Which can be used later for evaluation to be run on a vm 
            /// or to generate code for a different language (interpretor) 
            /// or (evaluator - Compiler(Executor)
            /// </summary>
            /// <param name="nScript">Script to be compiled </param>
            /// <returns>AST PROGRAM</returns>
            [System.ComponentModel.Description("Main Parser Function Parses whole Script into a AST tree ; Which can be used later for evaluation to be run on a vm or to generate code for a different language (interpretor) or (evaluator - Compiler(Executor)")]
            public AstProgram _Parse(ref string nScript)
            {
                var Body = new List<Ast_ExpressionStatement>();
                ParserErrors = new List<string>();
                iScript = nScript.Replace(Constants.vbNewLine, ";");
                iScript = Strings.RTrim(iScript);
                iScript = Strings.LTrim(iScript);

                // iScript = nScript.Replace(" ", "")
                // iScript = nScript.Replace(";", "")
                Tokenizer = new Lexer(ref iScript);
                // Dim TokType As GrammarFactory.Grammar.Type_Id
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (tok)
                {
                    case Type_Id._SAL_PROGRAM_BEGIN:
                        {
                            // Get title
                            var Decl = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            Lookahead = Tokenizer.ViewNext();
                            Tokenizer.IdentifiyToken(ref Lookahead);
                            // GetEmptystatement
                            var empt = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            Tokenizer.IdentifiyToken(ref Lookahead);
                            Lookahead = Tokenizer.ViewNext();
                            // GetProgram
                            iProgram = _SAL_ProgramNode();
                            break;
                        }

                    case Type_Id._PL_PROGRAM_BEGIN:
                        {
                            var Decl = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            Lookahead = Tokenizer.ViewNext();
                            Tokenizer.IdentifiyToken(ref Lookahead);
                            iProgram = _ProgramNode();
                            break;
                        }

                    case Type_Id.LOGO_LANG:
                        {
                            var Decl = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            Lookahead = Tokenizer.ViewNext();
                            Tokenizer.IdentifiyToken(ref Lookahead);
                            iProgram = _LOGO_ProgramNode();
                            break;
                        }

                    default:
                        {
                            // GetProgram
                            iProgram = _ProgramNode();
                            break;
                        }
                }
                // Preserve InClass
                return iProgram;
            }

            public AstProgram _LOGO_ProgramNode()
            {
                var nde = new AstProgram(ref _LogoStatements());
                nde._Raw = iScript;
                nde._Start = 0;
                nde._End = iScript.Length;
                nde._TypeStr = "LOGO PROGRAM";
                return nde;
            }
            // Syntax
            // 
            // 
            // Logo Expression; Logo expression;
            // LogoEvaluation
            // LogoFunction
            // Literal
            public List<AstExpression> _LogoStatements()
            {
                var lst = new List<AstExpression>();
                Type_Id tok;
                Lookahead = Tokenizer.ViewNext();
                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                while (tok != Type_Id._EOF)
                {
                    switch (tok)
                    {
                        // End of line
                        case Type_Id.LOGO_EOL:
                            {
                                __EndStatementNode();
                                break;
                            }

                        case Type_Id._VARIABLE:
                            {
                                var _Left = _LogoIdentiferLiteralNode();
                                // Check if it is a left hand cmd
                                switch (Strings.LCase(_Left._Name) ?? "")
                                {
                                    case "ht":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "hideturtle":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "fd":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "forward":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "bk":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "backward":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "rt":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "right":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "lt":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "label":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "if":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "for":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "deref":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "setxy":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "st":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "stop":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "pu":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "pd":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    case "make":
                                        {
                                            lst.Add(_ComandFunction(ref _Left));
                                            break;
                                        }

                                    default:
                                        {
                                            // Must be a variable
                                            AstNode argivalue = _Left;
                                            lst.Add(new Ast_Logo_Expression(ref argivalue));
                                            break;
                                        }
                                }

                                break;
                            }
                        // lst.Add(New Ast_Logo_Expression())
                        case Type_Id._STRING:
                            {
                                AstNode argivalue1 = _StringLiteralNode();
                                lst.Add(new Ast_Logo_Expression(ref argivalue1));
                                break;
                            }

                        case Type_Id._INTEGER:
                            {
                                lst.Add(_EvaluationExpression());
                                break;
                            }

                        case Type_Id._WHITESPACE:
                            {
                                _WhitespaceNode();
                                break;
                            }

                        case Type_Id._STATEMENT_END:
                            {
                                __EndStatementNode();
                                break;
                            }
                    }

                    Lookahead = Tokenizer.ViewNext();
                    tok = Tokenizer.IdentifiyToken(ref Lookahead);
                }

                return lst;
            }

            // syntax
            // 
            // 
            // literal +-*/<>= Literal 
            // 
            public AstExpression _EvaluationExpression()
            {
                AstExpression _left;
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                // Remove Erronious WhiteSpaces
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Primary
                AstNode argivalue = _NumericLiteralNode();
                _left = new Ast_Logo_Expression(ref argivalue);
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._ADDITIVE_OPERATOR:
                        {
                            return (AstExpression)_Evaluation(ref _left);
                        }

                    case Type_Id._MULTIPLICATIVE_OPERATOR:
                        {
                            return (AstExpression)_Evaluation(ref _left);
                        }

                    case Type_Id._RELATIONAL_OPERATOR:
                        {
                            return (AstExpression)_Evaluation(ref _left);
                        }
                }
                // Simple Number
                return _left;
            }
            // syntax
            // 
            // 
            // literal +-*/<>= Literal 
            // Literal
            public object _Evaluation(ref AstExpression _left)
            {
                string _Operator = "";
                AstExpression _Right;
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                _Operator = _GetAssignmentOperator();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                _Right = _EvaluationExpression();
                _left = new Ast_logoEvaluation(ref AST_NODE.Logo_Expression, ref _left, ref _Operator, ref _Right);
                _left._TypeStr = "EvaluationExpression";
                return _left;
            }
            // syntax
            // 
            // identifier Value
            // 
            // 
            // 
            public Ast_LogoCmdExpression _ComandFunction(ref Ast_Identifier _Left)
            {
                _WhitespaceNode();
                switch (Strings.LCase(_Left._Name) ?? "")
                {
                    case "ht":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "hideturtle":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "fd":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLogoLiteralNode());
                        }

                    case "forward":
                        {
                            var xde = new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLogoLiteralNode());
                            return xde;
                        }

                    case "bk":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLogoLiteralNode());
                        }

                    case "backward":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLogoLiteralNode());
                        }

                    case "rt":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLogoLiteralNode());
                        }

                    case "right":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLogoLiteralNode());
                        }

                    case "lt":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLogoLiteralNode());
                        }

                    case "label":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "if":
                        {
                            break;
                        }

                    case "for":
                        {
                            break;
                        }

                    case "deref":
                        {
                            break;
                        }

                    case "setxy":
                        {
                            break;
                        }

                    case "st":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "stop":
                        {
                            break;
                        }

                    case "pu":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "pd":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "make":
                        {
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
                // Must be a variable
                // Return Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left)
                return null;
            }
            // syntax
            // 
            // Identifier
            // 
            // 
            public Ast_Identifier _LogoIdentiferLiteralNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                var nde = new Ast_LogoIdentifer(ref tok.Value);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_Identifer";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Syntax:
            /// 
            /// Numeric Literal:
            /// -Number
            /// </summary>
            /// <returns></returns>
            public Ast_Logo_Value _NumericLogoLiteralNode()
            {
                int Str = 0;
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._INTEGER)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                if (int.TryParse(tok.Value, out Str))
                {
                    var nde = new Ast_Logo_Value(ref Str);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_integer";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
                else
                {
                    // Unable to parse default 0 to preserve node listeral as integer
                    object argnValue = 0;
                    var nde = new Ast_Logo_Value(ref AST_NODE._integer, ref argnValue);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_integer";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
            }
            /// <summary>
            /// Main Parser Function  
            /// Parses whole Script into a AST tree ; 
            /// Which can be used later for evaluation to be run on a vm 
            /// or to generate code for a different language (interpretor) 
            /// or (evaluator - Compiler(Executor)
            /// </summary>
            /// <param name="nScript">Script to be compiled </param>
            /// <param name="nGrammar">Uses Custom Grammar to create tokens based on Stored Grammar ID's</param>
            /// <returns>AST PROGRAM</returns>
            public AstProgram ParseGrammar(ref string nScript, ref List<Grammar> nGrammar)
            {
                var Body = new List<Ast_ExpressionStatement>();
                ParserErrors = new List<string>();
                iScript = nScript.Replace(Constants.vbNewLine, ";");
                iScript = Strings.RTrim(iScript);
                iScript = Strings.LTrim(iScript);
                Tokenizer = new Lexer(ref iScript, ref nGrammar);
                // Dim TokType As GrammarFactory.Grammar.Type_Id
                // uses the first token to determine the program type
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (tok)
                {
                    case Type_Id._SAL_PROGRAM_BEGIN:
                        {
                            // Get title
                            var Decl = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            Lookahead = Tokenizer.ViewNext();
                            Tokenizer.IdentifiyToken(ref Lookahead);
                            // GetEmptystatement
                            var empt = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            Tokenizer.IdentifiyToken(ref Lookahead);
                            Lookahead = Tokenizer.ViewNext();
                            // GetProgram
                            iProgram = _SAL_ProgramNode();
                            break;
                        }

                    case Type_Id._PL_PROGRAM_BEGIN:
                        {
                            var Decl = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            Lookahead = Tokenizer.ViewNext();
                            Tokenizer.IdentifiyToken(ref Lookahead);
                            iProgram = _ProgramNode();
                            break;
                        }

                    default:
                        {
                            // GetProgram
                            iProgram = _ProgramNode();
                            break;
                        }
                }
                // Preserve InClass
                return iProgram;
            }

            public AstProgram ParseFactory(ref string nScript, ProgramLangs PL = default)
            {
                var Body = new List<Ast_ExpressionStatement>();
                ParserErrors = new List<string>();
                iScript = nScript.Replace(Constants.vbNewLine, ";");
                iScript = Strings.RTrim(iScript);
                iScript = Strings.LTrim(iScript);
                switch (PL)
                {
                    case ProgramLangs.SAL:
                        {
                            // Get title
                            var Decl = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            Lookahead = Tokenizer.ViewNext();
                            Tokenizer.IdentifiyToken(ref Lookahead);
                            // GetEmptystatement
                            var empt = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            Tokenizer.IdentifiyToken(ref Lookahead);
                            Lookahead = Tokenizer.ViewNext();
                            // GetProgram
                            iProgram = _SAL_ProgramNode();
                            break;
                        }

                    case ProgramLangs.Small_PL:
                        {
                            iProgram = _ParsePL(ref nScript);
                            break;
                        }

                    case 0:
                        {
                            iProgram = _Parse(ref nScript);
                            break;
                        }

                    case ProgramLangs.Unknown:
                        {
                            iProgram = _Parse(ref nScript);
                            break;
                        }

                    default:
                        {
                            iProgram = _Parse(ref nScript);
                            break;
                        }
                }

                return iProgram;
            }

            public AstProgram _ParsePL(ref string nScript)
            {
                var Body = new List<Ast_ExpressionStatement>();
                ParserErrors = new List<string>();
                iScript = nScript.Replace(Constants.vbNewLine, ";");
                iScript = Strings.RTrim(iScript);
                iScript = Strings.LTrim(iScript);
                Tokenizer = new Lexer(ref iScript);
                // Dim TokType As GrammarFactory.Grammar.Type_Id
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);

                // GetProgram
                iProgram = _ProgramNode();


                // Preserve InClass
                return iProgram;
            }

            public AstProgram _ParseSAL(ref string nScript)
            {
                var Body = new List<Ast_ExpressionStatement>();
                ParserErrors = new List<string>();
                iScript = nScript.Replace(Constants.vbNewLine, ";");
                iScript = Strings.RTrim(iScript);
                iScript = Strings.LTrim(iScript);
                Tokenizer = new Lexer(ref iScript);
                // Dim TokType As GrammarFactory.Grammar.Type_Id
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);

                // Get title
                var Decl = Tokenizer.GetIdentifiedToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                Tokenizer.IdentifiyToken(ref Lookahead);
                // GetEmptystatement
                var empt = Tokenizer.GetIdentifiedToken(ref Lookahead);
                Tokenizer.IdentifiyToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                // GetProgram
                iProgram = _SAL_ProgramNode();


                // Preserve InClass
                return iProgram;
            }

            public AstProgram _ParseLOGO(ref string nScript)
            {
                var Body = new List<Ast_ExpressionStatement>();
                ParserErrors = new List<string>();
                iScript = nScript.Replace(Constants.vbNewLine, ";");
                iScript = Strings.RTrim(iScript);
                iScript = Strings.LTrim(iScript);
                Tokenizer = new Lexer(ref iScript);
                // GetEmptystatement
                var empt = Tokenizer.GetIdentifiedToken(ref Lookahead);
                Tokenizer.IdentifiyToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                iProgram = _LOGO_ProgramNode();
                // Preserve InClass
                return iProgram;
            }
            #endregion
            #region AstNode Handlers/Generators
            #region Main Program
            /// <summary>
            /// Main Entry Point. 
            /// Syntax:
            /// 
            /// Program:
            /// -Literals
            /// 
            /// </summary>
            /// <returns></returns>
            public AstProgram _ProgramNode()
            {
                var nde = new AstProgram(ref _StatementList());
                nde._Raw = iScript;
                nde._Start = 0;
                nde._End = iScript.Length;
                nde._TypeStr = "PL PROGRAM";
                return nde;
            }

            public AstProgram _SAL_ProgramNode()
            {
                var nde = new AstProgram(ref _SAL_StatementList());
                nde._Raw = iScript;
                nde._Start = 0;
                nde._End = iScript.Length;
                nde._TypeStr = "SAL PROGRAM";
                return nde;
            }
            /// <summary>
            /// 
            /// Syntax
            /// -Statement
            /// -Statementlist Statement -> Statement Statement Statement
            /// </summary>
            /// <returns></returns>
            public List<AstExpression> _StatementList()
            {
                var lst = new List<AstExpression>();
                Type_Id tok;
                Lookahead = Tokenizer.ViewNext();
                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                while (Lookahead != "EOF")
                {
                    switch (tok)
                    {
                        case Type_Id._SAL_EXPRESSION_BEGIN:
                            {
                                var nde = _SAL_Expression();
                                if (nde is object)
                                {
                                    lst.Add(nde);
                                    Lookahead = Tokenizer.ViewNext();
                                }

                                break;
                            }

                        case Type_Id._VARIABLE:
                            {
                                // Check If Logo Statement - Internal
                                if (CheckLogoStatement(ref Tokenizer.CheckIdentifiedToken(ref Lookahead).Value))
                                {
                                    var _Left = _IdentifierLiteralNode();
                                    lst.Add(_ComandFunction(ref _Left));
                                }
                                else
                                {
                                    lst.Add(_LeftHandExpression());
                                }

                                break;
                            }

                        default:
                            {
                                var nde = _Statement();
                                if (nde is object)
                                {
                                    lst.Add(nde);
                                    Lookahead = Tokenizer.ViewNext();
                                }

                                break;
                            }
                    }

                    Lookahead = Tokenizer.ViewNext();
                    tok = Tokenizer.IdentifiyToken(ref Lookahead);
                }

                return lst;
            }
            #endregion
            public bool CheckLogoStatement(ref string _left)
            {
                // Check if it is a left hand cmd
                switch (Strings.LCase(_left) ?? "")
                {
                    case "ht":
                        {
                            return true;
                        }

                    case "hideturtle":
                        {
                            return true;
                        }

                    case "fd":
                        {
                            return true;
                        }

                    case "forward":
                        {
                            return true;
                        }

                    case "bk":
                        {
                            return true;
                        }

                    case "backward":
                        {
                            return true;
                        }

                    case "rt":
                        {
                            return true;
                        }

                    case "right":
                        {
                            return true;
                        }

                    case "lt":
                        {
                            return true;
                        }

                    case "label":
                        {
                            return true;
                        }

                    case "if":
                        {
                            return true;
                        }

                    case "for":
                        {
                            return true;
                        }

                    case "deref":
                        {
                            return true;
                        }

                    case "setxy":
                        {
                            return true;
                        }

                    case "st":
                        {
                            return true;
                        }

                    case "stop":
                        {
                            return true;
                        }

                    case "pu":
                        {
                            return true;
                        }

                    case "pd":
                        {
                            return true;
                        }

                    case "make":
                        {
                            return true;
                        }

                    default:
                        {
                            // Must be a variable
                            return false;
                        }
                }

                return default;
            }
            #region SAL_LITERALS
            /// <summary>
            /// Sal Literals 
            /// The SAL Assembly language is of Pure Literals;
            /// Operators Also need to be handled as literals ;
            /// Each Expresion Statement needs to be terminated with a HALT command
            /// Sal Expressions are Inititiated as Statring with a "SAL" ending in "HALT"
            /// All Captured between will be Directly by the SAL Virtual Machine Interpretor
            /// 
            /// </summary>
            /// <returns></returns>
            public Ast_SalExpression _SAL_Expression()
            {
                var lst = new List<Ast_Literal>();
                // First token SAL BEGIN
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                // End of Expression is "HALT"
                while (tok != Type_Id._STATEMENT_END && tok != Type_Id.SAL_HALT)
                {
                    Lookahead = Tokenizer.ViewNext();
                    tok = Tokenizer.IdentifiyToken(ref Lookahead);
                    switch (tok)
                    {
                        case Type_Id._WHITESPACE:
                            {
                                _WhitespaceNode();
                                break;
                            }

                        case Type_Id._INTEGER:
                            {
                                var fnd = _NumericLiteralNode();
                                fnd._TypeStr = "_Integer";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._STRING:
                            {
                                var fnd = _StringLiteralNode();
                                fnd._TypeStr = "_string";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._STATEMENT_END:
                            {
                                lst.Add(__EmptyStatementNode());
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._SAL_EXPRESSION_BEGIN:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE._Sal_BeginStatement, ref argnValue);
                                fnd._End = nTok._End;
                                fnd._Raw = "_Sal_BeginStatement";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "_Sal_BeginStatement";
                                // lst.Add(fnd)
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._SAL_PROGRAM_BEGIN:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue1 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE._Sal_Program_title, ref argnValue1);
                                fnd._End = nTok._End;
                                fnd._Raw = "_SAL_PROGRAM_BEGIN";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "_SAL_PROGRAM_BEGIN";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_ADD:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue2 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_ADD, ref argnValue2);
                                fnd._End = nTok._End;
                                fnd._Raw = "ADD";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "ADD";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_AND:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue3 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_AND, ref argnValue3);
                                fnd._End = nTok._End;
                                fnd._Raw = "AND";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "AND";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_CALL:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue4 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_CALL, ref argnValue4);
                                fnd._End = nTok._End;
                                fnd._Raw = "CALL";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "CALL";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_DECR:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue5 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_DECR, ref argnValue5);
                                fnd._End = nTok._End;
                                fnd._Raw = "DECR";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "DECR";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_DIV:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue6 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_DIV, ref argnValue6);
                                fnd._End = nTok._End;
                                fnd._Raw = "DIV";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "DIV";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_DUP:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue7 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_DUP, ref argnValue7);
                                fnd._End = nTok._End;
                                fnd._Raw = "DUP";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "DUP";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_HALT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue8 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_HALT, ref argnValue8);
                                fnd._End = nTok._End;
                                fnd._Raw = "HALT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "HALT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_INCR:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue9 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_INCR, ref argnValue9);
                                fnd._End = nTok._End;
                                fnd._Raw = "INCR";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "INCR";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_IS_EQ:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue10 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_IS_EQ, ref argnValue10);
                                fnd._End = nTok._End;
                                fnd._Raw = "IS_EQ";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "IS_EQ";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_IS_GT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue11 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_IS_GT, ref argnValue11);
                                fnd._End = nTok._End;
                                fnd._Raw = "IS_GT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "IS_GT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_IS_GTE:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue12 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_IS_GTE, ref argnValue12);
                                fnd._End = nTok._End;
                                fnd._Raw = "IS_GTE";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "IS_GTE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_IS_LT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue13 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_IS_LT, ref argnValue13);
                                fnd._End = nTok._End;
                                fnd._Raw = "IS_LT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "IS_LT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_IS_LTE:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue14 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_IS_EQ, ref argnValue14);
                                fnd._End = nTok._End;
                                fnd._Raw = "IS_LTE";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "IS_LTE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JIF_EQ:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue15 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_EQ, ref argnValue15);
                                fnd._End = nTok._End;
                                fnd._Raw = "JIF_EQ";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JIF_EQ";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JIF_F:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue16 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_F, ref argnValue16);
                                fnd._End = nTok._End;
                                fnd._Raw = "JIF_F";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JIF_F";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JIF_GT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue17 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_GT, ref argnValue17);
                                fnd._End = nTok._End;
                                fnd._Raw = "JIF_GT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JIF_GT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JIF_LT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue18 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_LT, ref argnValue18);
                                fnd._End = nTok._End;
                                fnd._Raw = "JIF_LT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JIF_LT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JIF_T:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue19 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_T, ref argnValue19);
                                fnd._End = nTok._End;
                                fnd._Raw = "JIF_T";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JIF_T";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JMP:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue20 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JMP, ref argnValue20);
                                fnd._End = nTok._End;
                                fnd._Raw = "JMP";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JMP";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_LOAD:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue21 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_LOAD, ref argnValue21);
                                fnd._End = nTok._End;
                                fnd._Raw = "LOAD";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "LOAD";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_MUL:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue22 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_MUL, ref argnValue22);
                                fnd._End = nTok._End;
                                fnd._Raw = "MUL";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "MUL";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_NOT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue23 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_NOT, ref argnValue23);
                                fnd._End = nTok._End;
                                fnd._Raw = "NOT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "NOT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_NULL:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue24 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_NULL, ref argnValue24);
                                fnd._End = nTok._End;
                                fnd._Raw = "NULL";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "NULL";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_OR:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue25 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_OR, ref argnValue25);
                                fnd._End = nTok._End;
                                fnd._Raw = "OR";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "OR";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PAUSE:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue26 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PAUSE, ref argnValue26);
                                fnd._End = nTok._End;
                                fnd._Raw = "PAUSE";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PAUSE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PEEK:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue27 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PEEK, ref argnValue27);
                                fnd._End = nTok._End;
                                fnd._Raw = "PEEK";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PEEK";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PRINT_C:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue28 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PRINT_C, ref argnValue28);
                                fnd._End = nTok._End;
                                fnd._Raw = "PRINT_C";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PRINT_C";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PRINT_M:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue29 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PRINT_M, ref argnValue29);
                                fnd._End = nTok._End;
                                fnd._Raw = "PRINT_M";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PRINT_M";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PULL:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue30 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PULL, ref argnValue30);
                                fnd._End = nTok._End;
                                fnd._Raw = "PULL";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PULL";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PUSH:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue31 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PUSH, ref argnValue31);
                                fnd._End = nTok._End;
                                fnd._Raw = "PUSH";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PUSH";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_REMOVE:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue32 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_REMOVE, ref argnValue32);
                                fnd._End = nTok._End;
                                fnd._Raw = "REMOVE";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "REMOVE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_RESUME:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue33 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_RESUME, ref argnValue33);
                                fnd._End = nTok._End;
                                fnd._Raw = "RESUME";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "RESUME";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_RET:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue34 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_RET, ref argnValue34);
                                fnd._End = nTok._End;
                                fnd._Raw = "RET";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "RET";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_STORE:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue35 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_T, ref argnValue35);
                                fnd._End = nTok._End;
                                fnd._Raw = "STORE";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "STORE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_SUB:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue36 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_SUB, ref argnValue36);
                                fnd._End = nTok._End;
                                fnd._Raw = "SUB";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "STORE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_TO_NEG:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue37 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_TO_NEG, ref argnValue37);
                                fnd._End = nTok._End;
                                fnd._Raw = "TO_NEG";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "TO_NEG";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_TO_POS:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue38 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_TO_POS, ref argnValue38);
                                fnd._End = nTok._End;
                                fnd._Raw = "TO_POS";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "TO_POS";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case var @case when @case == Type_Id._WHITESPACE:
                            {
                                _WhitespaceNode();
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._BAD_TOKEN:
                            {
                                // Technically badtoken try capture
                                var etok = __UnknownStatementNode();
                                ParserErrors.Add("Unknown Statement/Expression Uncountered" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput() + Constants.vbNewLine);
                                lst.Add(etok);
                                Lookahead = Tokenizer.ViewNext();
                                Lookahead = "EOF";
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                // Set End of File
                                return new Ast_SalExpression(ref lst);
                            }

                        default:
                            {
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                return new Ast_SalExpression(ref lst);
                            }
                    }
                }

                Lookahead = Tokenizer.ViewNext();
                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                if (tok == Type_Id._STATEMENT_END)
                {
                    __EndStatementNode();
                    var stat = new Ast_SalExpression(ref lst);
                    foreach (var item in lst)
                        stat._Raw += item._Raw + " ";
                    Lookahead = Tokenizer.ViewNext();
                    tok = Tokenizer.IdentifiyToken(ref Lookahead);
                    return stat;
                }
                else
                {
                    if (tok == Type_Id.SAL_HALT)
                    {
                        var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                        object argnValue39 = nTok.Value;
                        var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_HALT, ref argnValue39);
                        fnd._End = nTok._End;
                        fnd._Raw = "HALT";
                        fnd._Start = nTok._start;
                        fnd._TypeStr = "HALT";
                        lst.Add(fnd);
                        Lookahead = Tokenizer.ViewNext();
                        tok = Tokenizer.IdentifiyToken(ref Lookahead);
                        var istat = new Ast_SalExpression(ref lst);
                        foreach (var item in lst)
                            istat._Raw += item._Raw + " ";
                        Lookahead = Tokenizer.ViewNext();
                        tok = Tokenizer.IdentifiyToken(ref Lookahead);
                        return istat;
                    }


                    // Technically badtoken try capture
                    var etok = __UnknownStatementNode();
                    ParserErrors.Add("Missing Halt maker" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput());
                    lst.Add(etok);
                    var stat = new Ast_SalExpression(ref lst);
                    foreach (var item in lst)
                        stat._Raw += item._Raw + " ";
                    return stat;
                }
            }

            public List<AstExpression> _SAL_StatementList()
            {
                var lst = new List<AstExpression>();
                while (Tokenizer.ViewNext() != "EOF")
                {
                    var nde = _SAL_Expression();
                    if (nde is object)
                    {
                        lst.Add(nde);
                    }
                }

                return lst;
            }
            #endregion
            #region Literals
            /// <summary>
            /// Syntax
            /// 
            /// -Literal => (_PrimaryExpression)
            /// -EatExtra WhiteSpace
            /// -EatExtra ";"
            /// </summary>
            /// <returns></returns>
            public AstExpression _PrimaryExpression()
            {
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (tok)
                {
                    case Type_Id._WHITESPACE:
                        {
                            _WhitespaceNode();
                            break;
                        }

                    case Type_Id._STATEMENT_END:
                        {
                            // __EmptyStatementNode()
                            var temp = new Ast_ExpressionStatement(ref __EmptyStatementNode());
                            temp._TypeStr = "_PrimaryExpression";
                            return temp;
                        }

                    default:
                        {
                            // Literal - Node!
                            Ast_ExpressionStatement Expr;
                            var nde = _literalNode();
                            if (nde is object)
                            {
                                Expr = new Ast_ExpressionStatement(ref nde);
                                // Advances to the next cursor
                                Lookahead = Tokenizer.ViewNext();
                                Expr._TypeStr = "_PrimaryExpression";
                                return Expr;
                            }
                            else
                            {
                                // Technically badtoken try capture
                                var etok = __UnknownStatementNode();
                                Lookahead = "EOF";
                                ParserErrors.Add("Unknown Statement/Expression/Function Uncountered" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput().Replace("  ", "") + Constants.vbNewLine);
                                var Lit = new Ast_ExpressionStatement(ref etok);
                                Lit._TypeStr = "_PrimaryExpression";
                            }

                            break;
                        }
                }
                // Technically badtoken try capture
                var ertok = __UnknownStatementNode();
                Lookahead = "EOF";
                ParserErrors.Add("Unknown Statement/LiteralExpression Uncountered" + Constants.vbNewLine + ertok.ToJson().FormatJsonOutput().Replace("  ", "") + Constants.vbNewLine);
                return new Ast_ExpressionStatement(ref ertok);
            }
            /// <summary>
            /// 
            /// Syntax:
            /// -EatWhiteSpace
            /// -SalExpression
            /// -ParenthesizedExpresion
            /// -_VariableExpression
            /// -_COMMENTS
            /// _CommandFunction
            /// -_BinaryExpression
            /// 
            /// 'Added Glitch(Select case on tokenvalue) ..... Not sure if it is the right way
            /// as the variables are blocking the keywords?
            /// </summary>
            /// <returns></returns>
            public AstExpression _LeftHandExpression()
            {
                Lookahead = Tokenizer.ViewNext();
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }
                else
                {
                }

                switch (toktype)
                {
                    case Type_Id._VARIABLE:
                        {
                            // Check if misIdentified
                            var iTok = Tokenizer.CheckIdentifiedToken(ref Lookahead);
                            if (CheckFunction(ref iTok.Value))
                            {
                                return _CommandFunction();
                            }
                            else
                            {
                                // Do Variable Expression
                                var arg_left = _VariableExpression();
                                return _BinaryExpression(ref arg_left);
                            }

                            break;
                        }

                    case Type_Id._COMMENTS:
                        {
                            return _CommentsListExpression();
                        }

                    case Type_Id._SAL_EXPRESSION_BEGIN:
                        {
                            return _SAL_Expression();
                        }

                    case Type_Id._CONDITIONAL_BEGIN:
                        {
                            return _ParenthesizedExpression();
                        }

                    default:
                        {
                            // Must be a primaryExpression With binary
                            return _BinaryExpression();
                        }
                }

                // Technically badtoken try capture
                var etok = __UnknownStatementNode();
                ParserErrors.Add("Unknown Statement/_LeftHandExpression Uncountered" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput() + Constants.vbNewLine);
                return new Ast_ExpressionStatement(ref etok);
            }
            /// <summary>
            /// -Literals
            /// Syntax:
            ///     
            ///     -Numeric Literal
            ///     -String Literal
            ///     -Comments
            ///     -Nullable
            ///     -BooleanLiteral
            ///     -ArrayLiteral
            ///     -EatWhiteSpace
            ///     -EatEmptyStatment
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _literalNode()
            {
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (tok)
                {
                    case Type_Id._INTEGER:
                        {
                            return _NumericLiteralNode();
                        }

                    case Type_Id._STRING:
                        {
                            return _StringLiteralNode();
                        }

                    // Case GrammarFactory.Grammar.Type_Id._VARIABLE
                    // Dim ntok = Tokenizer.GetIdentifiedToken(Lookahead)
                    // Dim xc = New Ast_Literal(AST_NODE._variable, ntok.Value)
                    // xc._Start = ntok._start
                    // xc._End = ntok._End
                    // xc._Raw = ntok.Value
                    case Type_Id._LIST_BEGIN:
                        {
                            return _ArrayListLiteral();
                        }

                    case Type_Id._NULL:
                        {
                            return _NullableNode();
                        }

                    case Type_Id._TRUE:
                        {
                            return _BooleanNode();
                        }

                    case Type_Id._FALSE:
                        {
                            return _BooleanNode();
                        }

                    case Type_Id._WHITESPACE:
                        {
                            return _WhitespaceNode();
                        }

                    case Type_Id._STATEMENT_END:
                        {
                            return __EmptyStatementNode();
                        }

                    case Type_Id.SAL_HALT:
                        {
                            var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            object argnValue = nTok.Value;
                            var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_HALT, ref argnValue);
                            fnd._End = nTok._End;
                            fnd._Raw = "HALT";
                            fnd._Start = nTok._start;
                            fnd._TypeStr = "HALT";
                            Lookahead = Tokenizer.ViewNext();
                            tok = Tokenizer.IdentifiyToken(ref Lookahead);
                            return fnd;
                            break;
                        }

                    default:
                        {
                            // Technically badtoken try capture
                            var etok = __UnknownStatementNode();
                            ParserErrors.Add("Unknown Literal Uncountered" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput().Replace("  ", "") + Constants.vbNewLine);
                            Lookahead = "EOF";
                            return etok;
                        }
                }
                // Technically badtoken try capture
                var itok = __UnknownStatementNode();
                Lookahead = "EOF";
                ParserErrors.Add("Unknown Literal Uncountered" + Constants.vbNewLine + itok.ToJson().FormatJsonOutput().Replace("  ", "") + Constants.vbNewLine);
                return itok;
            }
            /// <summary>
            /// Syntax:
            /// 
            /// Numeric Literal:
            /// -Number
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _NumericLiteralNode()
            {
                int Str = 0;
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._INTEGER)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                if (int.TryParse(tok.Value, out Str))
                {
                    var nde = new Ast_Literal(ref Str);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_integer";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
                else
                {
                    // Unable to parse default 0 to preserve node listeral as integer
                    int argnValue = 0;
                    var nde = new Ast_Literal(ref argnValue);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_integer";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
            }
            /// <summary>
            /// Syntax:
            /// 
            /// Nullable Literal:
            /// -Null
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _NullableNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._null, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_null";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used for end of statement
            /// </summary>
            /// <returns></returns>
            public Ast_Literal __EmptyStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._emptyStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_emptyStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Literal __EndStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._endStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_endStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Collects bad token
            /// </summary>
            /// <returns></returns>
            public Ast_Literal __UnknownStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._UnknownStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_UnknownStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used when data has already been collected
            /// </summary>
            /// <param name="ErrorTok"></param>
            /// <returns></returns>
            public Ast_Literal __UnknownStatementNode(ref Token ErrorTok)
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = ErrorTok;
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._UnknownStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_UnknownStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used to denote white space as it is often important later
            /// Some Parsers ignore this token ; 
            /// It is thought also; to be prudent to collect all tokens to let the Evaluator deal with this later
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _WhitespaceNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._WhiteSpace, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_whitespace";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used to Eat Node
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _CodeBeginNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._Code_Begin, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_Code_Begin";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Literal _ConditionalBeginNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._OperationBegin, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_OperationBegin";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Literal _ListEndNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                var x = Tokenizer.GetIdentifiedToken(ref Lookahead);
                // Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                // nde._Start = tok._start
                // nde._End = tok._End
                // nde._Raw = tok.Value
                // nde._TypeStr = "_Code_End"
                // Lookahead = Tokenizer.ViewNext
                var xDC = new Ast_Literal(ref AST_NODE._ListEnd);
                xDC._Start = x._start;
                xDC._End = x._End;
                xDC._Raw = x.Value;
                Lookahead = Tokenizer.ViewNext();
                return xDC;
            }

            public Ast_Literal _ListBeginNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                var x = Tokenizer.GetIdentifiedToken(ref Lookahead);
                // Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                // nde._Start = tok._start
                // nde._End = tok._End
                // nde._Raw = tok.Value
                // nde._TypeStr = "_Code_End"
                // Lookahead = Tokenizer.ViewNext
                var xDC = new Ast_Literal(ref AST_NODE._ListEnd);
                xDC._Start = x._start;
                xDC._End = x._End;
                xDC._Raw = x.Value;
                Lookahead = Tokenizer.ViewNext();
                return xDC;
            }
            /// <summary>
            /// Used to Eat Node 
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _CodeEndNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                var x = Tokenizer.GetIdentifiedToken(ref Lookahead);
                // Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                // nde._Start = tok._start
                // nde._End = tok._End
                // nde._Raw = tok.Value
                // nde._TypeStr = "_Code_End"
                // Lookahead = Tokenizer.ViewNext
                var xDC = new Ast_Literal(ref AST_NODE._Code_End);
                xDC._Start = x._start;
                xDC._End = x._End;
                xDC._Raw = x.Value;
                Lookahead = Tokenizer.ViewNext();
                return xDC;
            }

            public Ast_Literal _ConditionalEndNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._OperationEnd, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_OperationEnd";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used to return boolean literals if badly detected it will return false
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _BooleanNode()
            {
                bool Str = false;
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                if (bool.TryParse(tok.Value, out Str) == true)
                {
                    var nde = new Ast_Literal(ref Str);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_boolean";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
                else
                {
                    // Default to false
                    bool argnValue = false;
                    var nde = new Ast_Literal(ref argnValue);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_boolean";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
            }
            /// <summary>
            /// Syntax:
            /// 
            /// Comments Literal:
            /// -Comments
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _CommentsNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._COMMENTS)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._comments, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_comments";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public AstExpression _CommentsListExpression()
            {
                var Body = new List<Ast_Literal>();
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                while (tok == Type_Id._COMMENTS)
                    Body.Add(_CommentsNode());
                object argnValue = Body;
                var argnValue1 = new Ast_Literal(ref AST_NODE._comments, ref argnValue);
                var x = new Ast_ExpressionStatement(ref argnValue1);
                x._TypeStr = "_CommentsExpression";
                return x;
            }
            /// <summary>
            /// Syntax:
            /// "hjk"
            /// String Literal:
            /// -String
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _StringLiteralNode()
            {
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                string str = "";
                if (tok.Value.Contains("'"))
                {
                    str = tok.Value.Replace("'", "");
                }
                else
                {
                }

                if (tok.Value.Contains(Conversions.ToString('"')))
                {
                    str = tok.Value.Replace(Conversions.ToString('"'), "");
                }

                object argnValue = str;
                var nde = new Ast_Literal(ref AST_NODE._string, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_string";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Identifier _IdentifierLiteralNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                var nde = new Ast_Identifier(ref tok.Value);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_variable";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }


            #endregion
            #region STATEMENTS
            /// <summary>
            /// 
            /// Syntax
            /// -ExpressionStatement
            /// -BlockStatement
            /// -IterationStatement
            /// </summary>
            /// <returns></returns>
            public AstExpression _Statement()
            {
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (tok)
                {
                    // Begin Block
                    case Type_Id._CODE_BEGIN:
                        {
                            return _BlockStatement();
                        }
                    // due to most tokens detecting as variable (they can also be function names)
                    // we must check if is a fucntion command

                    case Type_Id._WHITESPACE:
                        {
                            while (tok == Type_Id._WHITESPACE)
                                _WhitespaceNode();
                            break;
                        }
                    // enable machine code in script;
                    // 'when Evaluating can be executed on VM
                    case Type_Id._SAL_EXPRESSION_BEGIN:
                        {
                            return _SAL_Expression();
                        }

                    case Type_Id._SAL_PROGRAM_BEGIN:
                        {
                            // Standard Expression
                            return _SAL_Expression();
                        }

                    default:
                        {
                            return _ExpressionStatement();
                        }
                }
                // Technically badtoken try capture
                var etok = __UnknownStatementNode();
                ParserErrors.Add("Unknown Statement syntax" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput());
                return new Ast_ExpressionStatement(ref etok);
            }
            /// <summary>
            /// Gets Expression Statement All functions etc are some form of Expression
            /// Syntax
            /// -Expression ";"
            /// 
            /// 
            /// </summary>
            /// <returns></returns>
            public AstExpression _ExpressionStatement()
            {
                return _Expression();
            }
            /// <summary>
            /// 
            /// Syntax:
            /// -_PrimaryExpression(literal)
            /// -_MultiplicativeExpression
            /// -_AddativeExpression
            /// -_RelationalExpression
            /// 
            /// </summary>
            /// <returns></returns>
            public AstExpression _Expression()
            {
                return _LeftHandExpression();
            }
            /// <summary>
            /// 
            /// Syntax: 
            /// Could be Empty list So Prefix Optional
            /// { OptionalStatmentList } 
            /// 
            /// </summary>
            /// <returns></returns>
            public Ast_BlockExpression _BlockStatement()
            {
                Type_Id toktype;
                var Body = new List<AstExpression>();
                _CodeBeginNode();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Detect Empty List
                if (toktype == Type_Id._CODE_END)
                {
                    Body.Add(new Ast_ExpressionStatement(ref __EmptyStatementNode()));
                    _CodeEndNode();
                    return new Ast_BlockExpression(ref Body);
                }
                else
                {
                    while (toktype != Type_Id._CODE_END)
                    {
                        Body.Add(_LeftHandExpression());
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }

                    _CodeEndNode();
                    return new Ast_BlockExpression(ref Body);
                }

                return new Ast_BlockExpression(ref Body);
            }

            public Ast_Literal _ArrayListLiteral()
            {
                Lookahead = Tokenizer.ViewNext();
                Type_Id toktype;
                var Body = new List<AstNode>();
                _ListBeginNode();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._LIST_END == true)
                    Body.Add(__EmptyStatementNode());
                while (toktype != Type_Id._LIST_END)
                {
                    switch (toktype)
                    {
                        case Type_Id._WHITESPACE:
                            {
                                _WhitespaceNode();
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._VARIABLE:
                            {
                                Body.Add(_VariableInitializer(ref _IdentifierLiteralNode()));
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._LIST_END:
                            {
                                _ListEndNode();
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                object argnValue = Body;
                                var de = new Ast_Literal(ref AST_NODE._array, ref argnValue);
                                de._TypeStr = "_array";
                                Lookahead = Tokenizer.ViewNext();
                                return de;
                            }

                        case Type_Id._LIST_SEPERATOR:
                            {
                                _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        default:
                            {
                                Body.Add(_literalNode());
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }
                    }
                }

                _ListEndNode();
                // Error at this point
                object argnValue1 = Body;
                var nde = new Ast_Literal(ref AST_NODE._array, ref argnValue1);
                nde._TypeStr = "_array";
                return nde;
            }

            public List<Ast_Identifier> _IdentifierList()
            {
                Lookahead = Tokenizer.ViewNext();
                Type_Id toktype;
                var Body = new List<Ast_Identifier>();
                _ListBeginNode();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                while (toktype != Type_Id._LIST_END)
                {
                    switch (toktype)
                    {
                        case Type_Id._WHITESPACE:
                            {
                                _WhitespaceNode();
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._VARIABLE:
                            {
                                Body.Add(_IdentifierLiteralNode());
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._LIST_END:
                            {
                                break;
                            }

                        case Type_Id._LIST_SEPERATOR:
                            {
                                _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        default:
                            {
                                // Technically badtoken try capture
                                var etok = __UnknownStatementNode();
                                ParserErrors.Add("Unknown _Identifier Uncountered" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput() + Constants.vbNewLine);
                                string argnName = "Error";
                                Body.Add(new Ast_Identifier(ref argnName));
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                return Body;
                            }
                    }
                }

                _ListEndNode();
                return Body;
            }

            public List<Ast_VariableDeclarationExpression> _VariableDeclarationList()
            {
                Lookahead = Tokenizer.ViewNext();
                Type_Id toktype;
                var Body = new List<Ast_VariableDeclarationExpression>();
                _ListBeginNode();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                while (toktype != Type_Id._LIST_END)
                {
                    switch (toktype)
                    {
                        case Type_Id._WHITESPACE:
                            {
                                _WhitespaceNode();
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._VARIABLE:
                            {
                                Body.Add(_VariableDeclaration(ref _IdentifierLiteralNode()));
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._LIST_END:
                            {
                                break;
                            }

                        case Type_Id._LIST_SEPERATOR:
                            {
                                _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        default:
                            {
                                // Technically badtoken try capture
                                var etok = __UnknownStatementNode();
                                ParserErrors.Add("Unknown _VariableDeclaration Uncountered" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput() + Constants.vbNewLine);
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                return Body;
                            }
                    }
                }

                _ListEndNode();
                return Body;
            }
            #endregion
            #region Expressions
            /// <summary>
            /// Syntax:
            /// Variable: -Identifier as expression
            /// - identifer = binaryExpression
            /// </summary>
            /// <returns></returns>
            public AstExpression _VariableExpression()
            {
                Type_Id toktype;
                Ast_Identifier _left = null;
                // Token ID
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);

                // Get Identifier (All Variable statements start with a Left)
                _left = _IdentifierLiteralNode();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // if the next operation is here then do it
                switch (toktype)
                {
                    case Type_Id._SIMPLE_ASSIGN:
                        {
                            return _VariableInitializer(ref _left);
                        }

                    default:
                        {
                            // Carry Variable forwards to binary function
                            AstExpression arg_left = new Ast_VariableExpressionStatement(ref _left);
                            return _BinaryExpression(ref arg_left);
                        }
                }
            }

            public AstBinaryExpression _VariableInitializer(ref Ast_Identifier _left)
            {
                Lookahead = Tokenizer.ViewNext();
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                try
                {
                    AstExpression arg_left = new Ast_VariableExpressionStatement(ref _left);
                    return (AstBinaryExpression)_BinaryExpression(ref arg_left);
                }
                catch (Exception ex)
                {
                    ParserErrors.Add(ex.ToString());
                    return null;
                }
            }

            public AstExpression _VariableInitializer(ref Ast_VariableDeclarationExpression _left)
            {
                Lookahead = Tokenizer.ViewNext();
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                AstExpression arg_left = new Ast_VariableExpressionStatement(ref _left._iLiteral);
                return _BinaryExpression(ref arg_left);
            }
            /// <summary>
            /// Variable declaration, no init:OR INIT
            /// DIM A 
            /// DIM A AS STRING / INTEGER / BOOLEAN / LIST
            /// let a as string
            ///  
            /// </summary>
            /// <param name="_left">IDENTIFIER</param>
            /// <returns></returns>
            public Ast_VariableDeclarationExpression _VariableDeclaration(ref Ast_Identifier _left)
            {
                _WhitespaceNode();
                Lookahead = Tokenizer.ViewNext();
                var Tok = Tokenizer.CheckIdentifiedToken(ref Lookahead);

                // SELECT lITERAL TYPE
                switch (Strings.UCase(Tok.Value) ?? "")
                {
                    case var @case when @case == Strings.UCase("string"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._string);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case1 when case1 == Strings.UCase("boole"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._boolean);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case2 when case2 == Strings.UCase("bool"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._boolean);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case3 when case3 == Strings.UCase("boolean"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._boolean);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case4 when case4 == Strings.UCase("array"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._array);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case5 when case5 == Strings.UCase("array"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._array);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case6 when case6 == Strings.UCase("list"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._array);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case7 when case7 == Strings.UCase("integer"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._integer);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case8 when case8 == Strings.UCase("nothing"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._null);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case9 when case9 == Strings.UCase("null"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._null);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case10 when case10 == Strings.UCase("int"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._integer);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    default:
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._null);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                return X;
                            }

                            break;
                        }
                }

                return new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._null);
            }

            /// <summary>
            /// _Simple Assign (variable)
            /// _Complex Assign (variable)
            /// 
            /// </summary>
            /// <param name="_left"></param>
            /// <returns></returns>
            public AstExpression _AssignmentExpression(ref Ast_Identifier _left)
            {
                Lookahead = Tokenizer.ViewNext();
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._SIMPLE_ASSIGN:
                        {
                            return _VariableInitializer(ref _left);
                        }
                }

                return _VariableInitializer(ref _left);
            }

            public AstExpression _AssignmentExpression(ref AstExpression _left)
            {
                Lookahead = Tokenizer.ViewNext();
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }
                else
                {
                }

                switch (toktype)
                {
                    case Type_Id._COMPLEX_ASSIGN:
                        {
                            // Complex Assignments are 
                            string _operator = _GetAssignmentOperator();
                            var x = new AstBinaryExpression(ref AST_NODE._assignExpression, ref _left, ref _operator, ref _LeftHandExpression());
                            x._TypeStr = "_COMPLEX_ASSIGN";
                            return x;
                        }
                }

                return _BinaryExpression(ref _left);
            }

            /// <summary>
            /// 
            /// Syntax: 
            /// 
            /// ( OptionalStatmentList; )
            /// 
            /// </summary>
            /// <returns></returns>
            public Ast_ParenthesizedExpresion _ParenthesizedExpression()
            {
                Type_Id toktype;
                var Body = new List<AstExpression>();
                _ConditionalBeginNode();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Detect Empty List
                if (toktype == Type_Id._CONDITIONAL_END)
                {
                    Body.Add(new Ast_ExpressionStatement(ref __EmptyStatementNode()));
                    _ConditionalEndNode();
                }
                else
                {
                    while (toktype != Type_Id._CONDITIONAL_END)
                    {
                        Body.Add(_ExpressionStatement());
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }

                    _ConditionalEndNode();
                }

                return new Ast_ParenthesizedExpresion(ref Body);
            }
            #region Binary Operations/Expressions
            /// <summary>
            /// Syntax:
            ///      -Multiplicative Expression
            /// Literal */ Literal
            /// </summary>
            /// <returns></returns>
            public AstExpression _MultiplicativeExpression()
            {
                return _BinaryExpression(ref Type_Id._MULTIPLICATIVE_OPERATOR, AST_NODE._MultiplicativeExpression, "_MultiplicativeExpression");
            }
            /// <summary>
            /// Syntax:
            ///      -Addative Expression
            /// Literal +- Literal
            /// </summary>
            /// <returns></returns>
            public AstExpression _AddativeExpression()
            {
                return _BinaryExpression(ref Type_Id._ADDITIVE_OPERATOR, AST_NODE._AddativeExpression, "_AddativeExpression");
            }
            /// <summary>
            /// Syntax: 
            /// 
            /// _RelationalExpression
            /// </summary>
            /// <returns></returns>
            public object _RelationalExpression()
            {
                return _BinaryExpression(ref Type_Id._RELATIONAL_OPERATOR, AST_NODE._ConditionalExpression, "_ConditionalExpression");
            }
            /// <summary>
            /// syntax:
            /// 
            /// 
            /// -Literal(Primary Expression)
            /// -Multiplicative Expression
            /// -Addative Expression
            /// -ConditionalExpression(OperationalExpression)
            /// _LeftHandExpression
            /// __BinaryExpression
            /// </summary>
            /// <param name="NType"></param>
            /// <param name="AstType"></param>
            /// <param name="AstTypeStr"></param>
            /// <returns></returns>
            public AstExpression _BinaryExpression(ref Type_Id NType, AST_NODE AstType, string AstTypeStr)
            {
                AstExpression _left;
                string _Operator = "";
                AstExpression _Right;
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Remove Erronious WhiteSpaces
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }
                else
                {
                }

                _left = _PrimaryExpression();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._SIMPLE_ASSIGN:
                        {
                            while (toktype == Type_Id._SIMPLE_ASSIGN)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._assignExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "AssignmentExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._ADDITIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._ADDITIVE_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._AddativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._MULTIPLICATIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._MULTIPLICATIVE_OPERATOR)
                            {
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                _Operator = _GetAssignmentOperator();

                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._MultiplicativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                            }

                            break;
                        }

                    case Type_Id._RELATIONAL_OPERATOR:
                        {
                            while (toktype == Type_Id._RELATIONAL_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._ConditionalExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._WHITESPACE:
                        {
                            _WhitespaceNode();
                            break;
                        }
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._STATEMENT_END)
                {
                    var x = __EmptyStatementNode();
                    return _left;
                }
                else
                {
                    return _left;
                }
                // End of file Marker
                return _left;
            }

            public AstExpression _BinaryExpression()
            {
                AstExpression _left;
                string _Operator = "";
                AstExpression _Right;
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Remove Erronious WhiteSpaces
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }
                else
                {
                }

                _left = _PrimaryExpression();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._SIMPLE_ASSIGN:
                        {
                            while (toktype == Type_Id._SIMPLE_ASSIGN)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._assignExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "AssignmentExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._ADDITIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._ADDITIVE_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._AddativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._MULTIPLICATIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._MULTIPLICATIVE_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._MultiplicativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._RELATIONAL_OPERATOR:
                        {
                            while (toktype == Type_Id._RELATIONAL_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._ConditionalExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._WHITESPACE:
                        {
                            _WhitespaceNode();
                            break;
                        }
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._STATEMENT_END)
                {
                    var x = __EmptyStatementNode();
                    return _left;
                }
                else
                {
                    return _left;
                }
                // End of file Marker
                return _left;
            }

            public AstExpression _BinaryExpression(ref AstExpression _left)
            {
                string _Operator = "";
                AstExpression _Right;
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Remove Erronious WhiteSpaces
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }
                else
                {
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._COMPLEX_ASSIGN:
                        {
                            while (toktype == Type_Id._COMPLEX_ASSIGN)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._assignExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "AssignmentExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._SIMPLE_ASSIGN:
                        {
                            while (toktype == Type_Id._SIMPLE_ASSIGN)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._assignExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "AssignmentExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._ADDITIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._ADDITIVE_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._AddativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._MULTIPLICATIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._MULTIPLICATIVE_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._MultiplicativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._RELATIONAL_OPERATOR:
                        {
                            while (toktype == Type_Id._RELATIONAL_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._ConditionalExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._WHITESPACE:
                        {
                            _WhitespaceNode();
                            break;
                        }
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._STATEMENT_END)
                {
                    var x = __EmptyStatementNode();
                    return _left;
                }
                else
                {
                    return _left;
                }
                // End of file Marker
                return _left;
            }

            #endregion
            #endregion
            public string _GetAssignmentOperator()
            {
                string str = Tokenizer.GetIdentifiedToken(ref Lookahead).Value;
                str = str.Replace(@"\u003c", " Less than");
                str = str.Replace(@"\u003e", " Greater Than ");
                // \U003c < Less-than sign
                // \U003e > Greater-than sign
                str = str.Replace("<=", " Less than equals ");
                str = str.Replace(">=", " Greater Than equals ");
                str = str.Replace("<", " Less than ");
                str = str.Replace(">", " Greater Than ");
                return Strings.UCase(str);
            }
            #endregion

            #region Functions
            /// <summary>
            /// syntax : 
            /// -Functions
            /// _DimFunction
            /// FOR
            /// WHILE
            /// UNTIL
            /// IF
            /// </summary>
            /// <returns></returns>
            public AstExpression _CommandFunction()
            {
                var iTok = Tokenizer.CheckIdentifiedToken(ref Lookahead);
                switch (Strings.UCase(iTok.Value) ?? "")
                {
                    // Check Fucntion name
                    case "DIM":
                        {
                            var nde = _DimFunction();
                            Lookahead = Tokenizer.ViewNext();
                            return nde;
                        }

                    case "FOR":
                        {
                            return _IterationStatment();
                        }

                    case "WHILE":
                        {
                            return _IterationStatment();
                        }

                    case "UNTIL":
                        {
                            return _IterationStatment();
                        }

                    case "IF":
                        {
                            return _IterationStatment();
                        }
                }

                return null;
            }

            public bool CheckFunction(ref string Str)
            {
                switch (Strings.UCase(Str) ?? "")
                {
                    // Check Fucntion name
                    case "DIM":
                        {
                            return true;
                        }

                    case "FOR":
                        {
                            return true;
                        }

                    case "WHILE":
                        {
                            return true;
                        }

                    case "UNTIL":
                        {
                            return true;
                        }
                }

                return false;
            }

            public AstExpression _DimFunction()
            {
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // GET the identified token as it is a command but detected as variable
                // DIM
                Tokenizer.GetIdentifiedToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                // _
                _WhitespaceNode();
                var _left = _IdentifierLiteralNode();
                _WhitespaceNode();
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.CheckIdentifiedToken(ref Lookahead);
                if ((Strings.UCase(tok.Value) ?? "") == (Strings.UCase("AS") ?? ""))
                {
                    Ast_VariableDeclarationExpression DecNode;
                    // Eat as
                    Tokenizer.GetIdentifiedToken(ref Lookahead);
                    Lookahead = Tokenizer.ViewNext();
                    // GetVar
                    DecNode = _VariableDeclaration(ref _left);
                    // nde = _VariableInitializer(_left)
                    Lookahead = Tokenizer.ViewNext();
                    tok = Tokenizer.CheckIdentifiedToken(ref Lookahead);
                    if (tok.ID == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        tok = Tokenizer.CheckIdentifiedToken(ref Lookahead);
                        if (tok.ID == Type_Id._SIMPLE_ASSIGN)
                        {
                            var lst = new List<AstExpression>();
                            lst.Add(DecNode);
                            var argnValue = new Ast_Literal(ref AST_NODE._emptyStatement);
                            var Empt = new Ast_ExpressionStatement(ref argnValue);
                            Empt._TypeStr = "_emptyStatement";
                            Empt._iLiteral._Raw = ";";
                            Empt._iLiteral.iLiteral = "";
                            Empt._iLiteral.iLiteral = ";";
                            lst.Add(Empt);
                            AstExpression local_BinaryExpression() { AstExpression arg_left = new Ast_VariableExpressionStatement(ref _left); var ret = _BinaryExpression(ref arg_left); return ret; }

                            lst.Add(local_BinaryExpression());
                            // Return 
                            var block = new Ast_BlockExpression(ref lst);
                            block._hasReturn = true;
                            block._ReturnValues.Add(new Ast_VariableExpressionStatement(ref _left));
                            return block;
                        }
                    }
                    else
                    {
                        return DecNode;
                    }
                }
                else
                {
                    // Complex
                    // View next (for next function)
                    AstExpression nde;
                    Lookahead = Tokenizer.ViewNext();
                    nde = _VariableInitializer(ref _left);
                    return nde;
                }

                return new Ast_VariableExpressionStatement(ref _left);
            }
            /// <summary>
            /// Syntax 
            /// -DoWhile
            /// -DoUntil
            /// _ForNext
            /// 
            /// </summary>
            /// <returns></returns>
            public AstExpression _IterationStatment()
            {
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._WHITESPACE:
                        {
                            _WhitespaceNode();
                            break;
                        }

                    case Type_Id._WHILE:
                        {
                            break;
                        }

                    case Type_Id._UNTIL:
                        {
                            break;
                        }

                    case Type_Id._FOR:
                        {
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }

                return null;
            }
            #endregion

        }
    }
}

// SAL PARSER
namespace SDK.SmallProgLang
{
    namespace Compiler
    {
        /// <summary>
        /// Known Langs
        /// </summary>
        public enum ProgramLangs
        {
            SAL = 1,
            Small_PL = 2,
            LOGO = 3,
            Unknown = 4
        }

        public class SalParser
        {
            public AstProgram _Parse(ref string nScript)
            {
                var Body = new List<Ast_ExpressionStatement>();
                ParserErrors = new List<string>();
                iScript = nScript.Replace(Constants.vbNewLine, ";");
                iScript = Strings.RTrim(iScript);
                iScript = Strings.LTrim(iScript);
                Tokenizer = new Lexer(ref iScript);
                // Dim TokType As GrammarFactory.Grammar.Type_Id
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);

                // GetProgram
                iProgram = _SAL_ProgramNode();


                // Preserve InClass
                return iProgram;
            }
            #region Propertys
            public List<string> ParserErrors = new List<string>();
            /// <summary>
            /// Currently held script
            /// </summary>
            public string iScript = "";
            /// <summary>
            /// To hold the look ahead value without consuming the value
            /// </summary>
            public string Lookahead;
            /// <summary>
            /// Tokenizer !
            /// </summary>
            private Lexer Tokenizer;
            private AstProgram iProgram;

            public AstProgram Program
            {
                get
                {
                    return iProgram;
                }
            }
            #endregion
            #region Standard Nodes
            /// <summary>
            /// Used to Eat Node
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _CodeBeginNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._Code_Begin, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_Code_Begin";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Literal _ConditionalBeginNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._OperationBegin, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_OperationBegin";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Literal _ListEndNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                var x = Tokenizer.GetIdentifiedToken(ref Lookahead);
                // Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                // nde._Start = tok._start
                // nde._End = tok._End
                // nde._Raw = tok.Value
                // nde._TypeStr = "_Code_End"
                // Lookahead = Tokenizer.ViewNext
                var xDC = new Ast_Literal(ref AST_NODE._ListEnd);
                xDC._Start = x._start;
                xDC._End = x._End;
                xDC._Raw = x.Value;
                Lookahead = Tokenizer.ViewNext();
                return xDC;
            }

            public Ast_Literal _ListBeginNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                var x = Tokenizer.GetIdentifiedToken(ref Lookahead);
                // Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                // nde._Start = tok._start
                // nde._End = tok._End
                // nde._Raw = tok.Value
                // nde._TypeStr = "_Code_End"
                // Lookahead = Tokenizer.ViewNext
                var xDC = new Ast_Literal(ref AST_NODE._ListEnd);
                xDC._Start = x._start;
                xDC._End = x._End;
                xDC._Raw = x.Value;
                Lookahead = Tokenizer.ViewNext();
                return xDC;
            }
            /// <summary>
            /// Syntax:
            /// 
            /// Numeric Literal:
            /// -Number
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _NumericLiteralNode()
            {
                int Str = 0;
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._INTEGER)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                if (int.TryParse(tok.Value, out Str) == true)
                {
                    object argnValue = Str;
                    var nde = new Ast_Literal(ref AST_NODE._integer, ref argnValue);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_integer";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
                else
                {
                    // Unable to parse default 0 to preserve node listeral as integer
                    object argnValue1 = 0;
                    var nde = new Ast_Literal(ref AST_NODE._integer, ref argnValue1);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_integer";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
            }
            /// <summary>
            /// Syntax:
            /// 
            /// Nullable Literal:
            /// -Null
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _NullableNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._null, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_null";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used for end of statement
            /// </summary>
            /// <returns></returns>
            public Ast_Literal __EmptyStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._emptyStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_emptyStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Literal __EndStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._endStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_endStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Collects bad token
            /// </summary>
            /// <returns></returns>
            public Ast_Literal __UnknownStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._UnknownStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_UnknownStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used when data has already been collected
            /// </summary>
            /// <param name="ErrorTok"></param>
            /// <returns></returns>
            public Ast_Literal __UnknownStatementNode(ref Token ErrorTok)
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = ErrorTok;
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._UnknownStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_UnknownStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used to denote white space as it is often important later
            /// Some Parsers ignore this token ; 
            /// It is thought also; to be prudent to collect all tokens to let the Evaluator deal with this later
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _WhitespaceNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._WhiteSpace, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_whitespace";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Syntax:
            /// "hjk"
            /// String Literal:
            /// -String
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _StringLiteralNode()
            {
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                string str = "";
                if (tok.Value.Contains("'"))
                {
                    str = tok.Value.Replace("'", "");
                }
                else
                {
                }

                if (tok.Value.Contains(Conversions.ToString('"')))
                {
                    str = tok.Value.Replace(Conversions.ToString('"'), "");
                }

                object argnValue = str;
                var nde = new Ast_Literal(ref AST_NODE._string, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_string";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Identifier _IdentifierLiteralNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                var nde = new Ast_Identifier(ref tok.Value);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_variable";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            #endregion
            #region SAL_LITERALS
            /// <summary>
            /// Sal Literals 
            /// The SAL Assembly language is of Pure Literals;
            /// Operators Also need to be handled as literals ;
            /// Each Expresion Statement needs to be terminated with a HALT command
            /// Sal Expressions are Inititiated as Statring with a "SAL" ending in "HALT"
            /// All Captured between will be Directly by the SAL Virtual Machine Interpretor
            /// 
            /// </summary>
            /// <returns></returns>
            public Ast_SalExpression _SAL_Expression()
            {
                var lst = new List<Ast_Literal>();
                // First token SAL BEGIN
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                // End of Expression is "HALT"
                while (tok != Type_Id._STATEMENT_END && tok != Type_Id.SAL_HALT)
                {
                    Lookahead = Tokenizer.ViewNext();
                    tok = Tokenizer.IdentifiyToken(ref Lookahead);
                    switch (tok)
                    {
                        case Type_Id._WHITESPACE:
                            {
                                _WhitespaceNode();
                                break;
                            }

                        case Type_Id._INTEGER:
                            {
                                var fnd = _NumericLiteralNode();
                                fnd._TypeStr = "_Integer";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._STRING:
                            {
                                var fnd = _StringLiteralNode();
                                fnd._TypeStr = "_string";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._STATEMENT_END:
                            {
                                lst.Add(__EmptyStatementNode());
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._SAL_EXPRESSION_BEGIN:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE._Sal_BeginStatement, ref argnValue);
                                fnd._End = nTok._End;
                                fnd._Raw = "_Sal_BeginStatement";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "_Sal_BeginStatement";
                                // lst.Add(fnd)
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._SAL_PROGRAM_BEGIN:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue1 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE._Sal_Program_title, ref argnValue1);
                                fnd._End = nTok._End;
                                fnd._Raw = "_SAL_PROGRAM_BEGIN";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "_SAL_PROGRAM_BEGIN";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_ADD:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue2 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_ADD, ref argnValue2);
                                fnd._End = nTok._End;
                                fnd._Raw = "ADD";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "ADD";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_AND:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue3 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_AND, ref argnValue3);
                                fnd._End = nTok._End;
                                fnd._Raw = "AND";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "AND";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_CALL:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue4 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_CALL, ref argnValue4);
                                fnd._End = nTok._End;
                                fnd._Raw = "CALL";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "CALL";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_DECR:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue5 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_DECR, ref argnValue5);
                                fnd._End = nTok._End;
                                fnd._Raw = "DECR";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "DECR";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_DIV:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue6 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_DIV, ref argnValue6);
                                fnd._End = nTok._End;
                                fnd._Raw = "DIV";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "DIV";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_DUP:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue7 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_DUP, ref argnValue7);
                                fnd._End = nTok._End;
                                fnd._Raw = "DUP";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "DUP";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_HALT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue8 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_HALT, ref argnValue8);
                                fnd._End = nTok._End;
                                fnd._Raw = "HALT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "HALT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_INCR:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue9 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_INCR, ref argnValue9);
                                fnd._End = nTok._End;
                                fnd._Raw = "INCR";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "INCR";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_IS_EQ:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue10 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_IS_EQ, ref argnValue10);
                                fnd._End = nTok._End;
                                fnd._Raw = "IS_EQ";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "IS_EQ";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_IS_GT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue11 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_IS_GT, ref argnValue11);
                                fnd._End = nTok._End;
                                fnd._Raw = "IS_GT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "IS_GT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_IS_GTE:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue12 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_IS_GTE, ref argnValue12);
                                fnd._End = nTok._End;
                                fnd._Raw = "IS_GTE";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "IS_GTE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_IS_LT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue13 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_IS_LT, ref argnValue13);
                                fnd._End = nTok._End;
                                fnd._Raw = "IS_LT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "IS_LT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_IS_LTE:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue14 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_IS_EQ, ref argnValue14);
                                fnd._End = nTok._End;
                                fnd._Raw = "IS_LTE";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "IS_LTE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JIF_EQ:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue15 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_EQ, ref argnValue15);
                                fnd._End = nTok._End;
                                fnd._Raw = "JIF_EQ";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JIF_EQ";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JIF_F:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue16 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_F, ref argnValue16);
                                fnd._End = nTok._End;
                                fnd._Raw = "JIF_F";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JIF_F";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JIF_GT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue17 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_GT, ref argnValue17);
                                fnd._End = nTok._End;
                                fnd._Raw = "JIF_GT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JIF_GT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JIF_LT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue18 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_LT, ref argnValue18);
                                fnd._End = nTok._End;
                                fnd._Raw = "JIF_LT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JIF_LT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JIF_T:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue19 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_T, ref argnValue19);
                                fnd._End = nTok._End;
                                fnd._Raw = "JIF_T";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JIF_T";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_JMP:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue20 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JMP, ref argnValue20);
                                fnd._End = nTok._End;
                                fnd._Raw = "JMP";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "JMP";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_LOAD:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue21 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_LOAD, ref argnValue21);
                                fnd._End = nTok._End;
                                fnd._Raw = "LOAD";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "LOAD";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_MUL:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue22 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_MUL, ref argnValue22);
                                fnd._End = nTok._End;
                                fnd._Raw = "MUL";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "MUL";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_NOT:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue23 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_NOT, ref argnValue23);
                                fnd._End = nTok._End;
                                fnd._Raw = "NOT";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "NOT";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_NULL:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue24 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_NULL, ref argnValue24);
                                fnd._End = nTok._End;
                                fnd._Raw = "NULL";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "NULL";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_OR:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue25 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_OR, ref argnValue25);
                                fnd._End = nTok._End;
                                fnd._Raw = "OR";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "OR";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PAUSE:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue26 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PAUSE, ref argnValue26);
                                fnd._End = nTok._End;
                                fnd._Raw = "PAUSE";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PAUSE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PEEK:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue27 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PEEK, ref argnValue27);
                                fnd._End = nTok._End;
                                fnd._Raw = "PEEK";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PEEK";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PRINT_C:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue28 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PRINT_C, ref argnValue28);
                                fnd._End = nTok._End;
                                fnd._Raw = "PRINT_C";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PRINT_C";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PRINT_M:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue29 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PRINT_M, ref argnValue29);
                                fnd._End = nTok._End;
                                fnd._Raw = "PRINT_M";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PRINT_M";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PULL:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue30 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PULL, ref argnValue30);
                                fnd._End = nTok._End;
                                fnd._Raw = "PULL";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PULL";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_PUSH:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue31 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_PUSH, ref argnValue31);
                                fnd._End = nTok._End;
                                fnd._Raw = "PUSH";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "PUSH";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_REMOVE:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue32 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_REMOVE, ref argnValue32);
                                fnd._End = nTok._End;
                                fnd._Raw = "REMOVE";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "REMOVE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_RESUME:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue33 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_RESUME, ref argnValue33);
                                fnd._End = nTok._End;
                                fnd._Raw = "RESUME";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "RESUME";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_RET:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue34 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_RET, ref argnValue34);
                                fnd._End = nTok._End;
                                fnd._Raw = "RET";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "RET";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_STORE:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue35 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_JIF_T, ref argnValue35);
                                fnd._End = nTok._End;
                                fnd._Raw = "STORE";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "STORE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_SUB:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue36 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_SUB, ref argnValue36);
                                fnd._End = nTok._End;
                                fnd._Raw = "SUB";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "STORE";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_TO_NEG:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue37 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_TO_NEG, ref argnValue37);
                                fnd._End = nTok._End;
                                fnd._Raw = "TO_NEG";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "TO_NEG";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id.SAL_TO_POS:
                            {
                                var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                                object argnValue38 = nTok.Value;
                                var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_TO_POS, ref argnValue38);
                                fnd._End = nTok._End;
                                fnd._Raw = "TO_POS";
                                fnd._Start = nTok._start;
                                fnd._TypeStr = "TO_POS";
                                lst.Add(fnd);
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case var @case when @case == Type_Id._WHITESPACE:
                            {
                                _WhitespaceNode();
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._BAD_TOKEN:
                            {
                                // Technically badtoken try capture
                                var etok = __UnknownStatementNode();
                                ParserErrors.Add("Unknown Statement/Expression Uncountered" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput() + Constants.vbNewLine);
                                lst.Add(etok);
                                Lookahead = Tokenizer.ViewNext();
                                Lookahead = "EOF";
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                // Set End of File
                                return new Ast_SalExpression(ref lst);
                            }

                        default:
                            {
                                Lookahead = Tokenizer.ViewNext();
                                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                                return new Ast_SalExpression(ref lst);
                            }
                    }
                }

                Lookahead = Tokenizer.ViewNext();
                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                if (tok == Type_Id._STATEMENT_END)
                {
                    __EndStatementNode();
                    var stat = new Ast_SalExpression(ref lst);
                    foreach (var item in lst)
                        stat._Raw += item._Raw + " ";
                    Lookahead = Tokenizer.ViewNext();
                    tok = Tokenizer.IdentifiyToken(ref Lookahead);
                    return stat;
                }
                else
                {
                    if (tok == Type_Id.SAL_HALT)
                    {
                        var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                        object argnValue39 = nTok.Value;
                        var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_HALT, ref argnValue39);
                        fnd._End = nTok._End;
                        fnd._Raw = "HALT";
                        fnd._Start = nTok._start;
                        fnd._TypeStr = "HALT";
                        lst.Add(fnd);
                        Lookahead = Tokenizer.ViewNext();
                        tok = Tokenizer.IdentifiyToken(ref Lookahead);
                        var istat = new Ast_SalExpression(ref lst);
                        foreach (var item in lst)
                            istat._Raw += item._Raw + " ";
                        Lookahead = Tokenizer.ViewNext();
                        tok = Tokenizer.IdentifiyToken(ref Lookahead);
                        return istat;
                    }


                    // Technically badtoken try capture
                    var etok = __UnknownStatementNode();
                    ParserErrors.Add("Missing Halt maker" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput());
                    lst.Add(etok);
                    var stat = new Ast_SalExpression(ref lst);
                    foreach (var item in lst)
                        stat._Raw += item._Raw + " ";
                    return stat;
                }
            }

            public List<AstExpression> _SAL_StatementList()
            {
                var lst = new List<AstExpression>();
                while (Tokenizer.ViewNext() != "EOF")
                {
                    var nde = _SAL_Expression();
                    if (nde is object)
                    {
                        lst.Add(nde);
                    }
                }

                return lst;
            }
            #endregion

            public string _GetAssignmentOperator()
            {
                string str = Tokenizer.GetIdentifiedToken(ref Lookahead).Value;
                str = str.Replace(@"\u003c", "_IS_LT");
                str = str.Replace(@"\u003e", "_IS_GT");
                // \U003c < Less-than sign
                // \U003e > Greater-than sign
                str = str.Replace("<=", "_IS_LTE");
                str = str.Replace("+=", "_INCR");
                str = str.Replace("-=", "_DECR");
                str = str.Replace(">=", "_IS_GTE");
                str = str.Replace("<", "_IS_LT");
                str = str.Replace(">", "_IS_GT");
                str = str.Replace("==", "_IS_EQ");
                str = str.Replace("+", "_ADD");
                str = str.Replace("-", "_SUB");
                str = str.Replace("/", "_DIV");
                str = str.Replace("*", "_MUL");
                str = str.Replace("=", "_ASSIGNS");
                return Strings.UCase(str);
            }

            public AstProgram _SAL_ProgramNode()
            {
                var nde = new AstProgram(ref _SAL_StatementList());
                nde._Raw = iScript;
                nde._Start = 0;
                nde._End = iScript.Length;
                nde._TypeStr = "SAL PROGRAM";
                return nde;
            }
        }
    }
}

// LOGO _ PARSER 
namespace SDK.SmallProgLang
{
    // LOGO
    // 
    namespace Compiler
    {
        // Logo Parser
        // 
        public class LogoParser
        {
            #region Propertys
            public List<string> ParserErrors = new List<string>();
            /// <summary>
            /// Currently held script
            /// </summary>
            public string iScript = "";
            /// <summary>
            /// To hold the look ahead value without consuming the value
            /// </summary>
            public string Lookahead;
            /// <summary>
            /// Tokenizer !
            /// </summary>
            private Lexer Tokenizer;
            private AstProgram iProgram;

            public AstProgram Program
            {
                get
                {
                    return iProgram;
                }
            }
            #endregion
            // Syntax
            // 
            // Program : Body = StatementLists
            // 
            public AstProgram _Parse(ref string nScript)
            {
                List<Ast_ExpressionStatement> Body;
                Body = new List<Ast_ExpressionStatement>();
                ParserErrors = new List<string>();
                iScript = nScript.Replace(Constants.vbNewLine, ";");
                iScript = Strings.RTrim(iScript);
                iScript = Strings.LTrim(iScript);
                Tokenizer = new Lexer(ref iScript);
                // Dim TokType As GrammarFactory.Grammar.Type_Id
                // GetProgram
                iProgram = _LOGO_ProgramNode();
                // Preserve InClass
                return iProgram;
            }

            public AstProgram _LOGO_ProgramNode()
            {
                var nde = new AstProgram(ref _StatementList());
                nde._Raw = iScript;
                nde._Start = 0;
                nde._End = iScript.Length;
                nde._TypeStr = "LOGO PROGRAM";
                return nde;
            }
            // Syntax;
            // 
            // Logo Expression; Logo expression;
            // 
            public List<AstExpression> _StatementList()
            {
                var lst = new List<AstExpression>();
                lst.AddRange(_LogoStatements());
                return lst;
            }

            public bool CheckLogoStatement(ref string _left)
            {
                // Check if it is a left hand cmd
                switch (Strings.LCase(_left) ?? "")
                {
                    case "ht":
                        {
                            return true;
                        }

                    case "hideturtle":
                        {
                            return true;
                        }

                    case "fd":
                        {
                            return true;
                        }

                    case "forward":
                        {
                            return true;
                        }

                    case "bk":
                        {
                            return true;
                        }

                    case "backward":
                        {
                            return true;
                        }

                    case "rt":
                        {
                            return true;
                        }

                    case "right":
                        {
                            return true;
                        }

                    case "lt":
                        {
                            return true;
                        }

                    case "label":
                        {
                            return true;
                        }

                    case "if":
                        {
                            return true;
                        }

                    case "for":
                        {
                            return true;
                        }

                    case "deref":
                        {
                            return true;
                        }

                    case "setxy":
                        {
                            return true;
                        }

                    case "st":
                        {
                            return true;
                        }

                    case "stop":
                        {
                            return true;
                        }

                    case "pu":
                        {
                            return true;
                        }

                    case "pd":
                        {
                            return true;
                        }

                    case "make":
                        {
                            return true;
                        }

                    default:
                        {
                            // Must be a variable
                            return false;
                        }
                }

                return default;
            }
            // Syntax
            // 
            // 
            // Logo Expression; Logo expression;
            // LogoEvaluation
            // LogoFunction
            // Literal
            public List<AstExpression> _LogoStatements()
            {
                var lst = new List<AstExpression>();
                Type_Id tok;
                Lookahead = Tokenizer.ViewNext();
                tok = Tokenizer.IdentifiyToken(ref Lookahead);
                while (tok != Type_Id._EOF)
                {
                    switch (tok)
                    {
                        // End of line
                        case Type_Id.LOGO_EOL:
                            {
                                __EndStatementNode();
                                break;
                            }

                        case Type_Id._VARIABLE:
                            {
                                var _Left = _IdentifierLiteralNode();
                                // Check if it is a left hand cmd
                                if (CheckLogoStatement(ref _Left._Name) == true)
                                {
                                    lst.Add(_ComandFunction(ref _Left));
                                }
                                else
                                {
                                    AstNode argivalue = _Left;
                                    lst.Add(new Ast_Logo_Expression(ref argivalue));
                                }

                                break;
                            }

                        case Type_Id._STRING:
                            {
                                AstNode argivalue1 = _StringLiteralNode();
                                lst.Add(new Ast_Logo_Expression(ref argivalue1));
                                break;
                            }

                        case Type_Id._INTEGER:
                            {
                                lst.Add(_EvaluationExpression());
                                break;
                            }

                        case Type_Id._WHITESPACE:
                            {
                                _WhitespaceNode();
                                break;
                            }

                        case Type_Id._STATEMENT_END:
                            {
                                __EndStatementNode();
                                break;
                            }
                    }

                    Lookahead = Tokenizer.ViewNext();
                    tok = Tokenizer.IdentifiyToken(ref Lookahead);
                }

                return lst;
            }
            // syntax
            // 
            // 
            // literal +-*/<>= Literal 
            // 
            public AstExpression _EvaluationExpression()
            {
                AstExpression _left;
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                // Remove Erronious WhiteSpaces
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Primary
                AstNode argivalue = _NumericLiteralNode();
                _left = new Ast_Logo_Expression(ref argivalue);
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._ADDITIVE_OPERATOR:
                        {
                            return (AstExpression)_Evaluation(ref _left);
                        }

                    case Type_Id._MULTIPLICATIVE_OPERATOR:
                        {
                            return (AstExpression)_Evaluation(ref _left);
                        }

                    case Type_Id._RELATIONAL_OPERATOR:
                        {
                            return (AstExpression)_Evaluation(ref _left);
                        }
                }
                // Simple Number
                return _left;
            }
            // syntax
            // 
            // 
            // literal +-*/<>= Literal 
            // Literal
            public object _Evaluation(ref AstExpression _left)
            {
                string _Operator = "";
                AstExpression _Right;
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                _Operator = _GetAssignmentOperator();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                Lookahead = Tokenizer.ViewNext();
                _Right = _EvaluationExpression();
                _left = new Ast_logoEvaluation(ref AST_NODE.Logo_Expression, ref _left, ref _Operator, ref _Right);
                _left._TypeStr = "EvaluationExpression";
                return _left;
            }
            // syntax
            // 
            // identifier Value
            // 
            // 
            // 
            public Ast_LogoCmdExpression _ComandFunction(ref Ast_Identifier _Left)
            {
                _WhitespaceNode();
                switch (Strings.LCase(_Left._Name) ?? "")
                {
                    case "ht":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "hideturtle":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "fd":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLiteralNode());
                        }

                    case "forward":
                        {
                            var xde = new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLiteralNode());
                            return xde;
                        }

                    case "bk":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLiteralNode());
                        }

                    case "backward":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLiteralNode());
                        }

                    case "rt":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLiteralNode());
                        }

                    case "right":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLiteralNode());
                        }

                    case "lt":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, _NumericLiteralNode());
                        }

                    case "label":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "if":
                        {
                            break;
                        }

                    case "for":
                        {
                            break;
                        }

                    case "deref":
                        {
                            break;
                        }

                    case "setxy":
                        {
                            break;
                        }

                    case "st":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "stop":
                        {
                            break;
                        }

                    case "pu":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "pd":
                        {
                            return new Ast_LogoCmdExpression(ref AST_NODE.Logo_Function, ref _Left, __EndStatementNode());
                        }

                    case "make":
                        {
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
                // Must be a variable
                // Return Ast_LogoCmdExpression(AST_NODE.Logo_Function, _Left)
                return null;
            }
            // syntax
            // 
            // +-*/=<>
            // 
            // 
            public string _GetAssignmentOperator()
            {
                string str = Tokenizer.GetIdentifiedToken(ref Lookahead).Value;
                str = str.Replace(@"\u003c", "<");
                str = str.Replace(@"\u003e", ">");
                // \U003c < Less-than sign
                // \U003e > Greater-than sign


                return Strings.UCase(str);
            }
            // syntax
            // 
            // Identifier
            // 
            // 
            public Ast_Identifier _IdentifierLiteralNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                var nde = new Ast_LogoIdentifer(ref tok.Value);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_Identifer";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            // syntax
            // 
            // ;
            // 
            public Ast_Literal __EndStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._endStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_endStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            // syntax:
            // 
            // Unknown
            // 
            /// <summary>
            /// Collects bad token
            /// </summary>
            /// <returns></returns>
            public Ast_Literal __UnknownStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._UnknownStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_UnknownStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used when data has already been collected
            /// </summary>
            /// <param name="ErrorTok"></param>
            /// <returns></returns>
            public Ast_Literal __UnknownStatementNode(ref Token ErrorTok)
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = ErrorTok;
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._UnknownStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_UnknownStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            // syntax
            // 
            // " "
            /// <summary>
            /// Used to denote white space as it is often important later
            /// Some Parsers ignore this token ; 
            /// It is thought also; to be prudent to collect all tokens to let the Evaluator deal with this later
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _WhitespaceNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._WhiteSpace, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_whitespace";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            // Syntax:
            // 
            // String Literal
            // 
            /// <summary>
            /// Syntax:
            /// "hjk"
            /// String Literal:
            /// -String
            /// </summary>
            /// <returns></returns>
            public Ast_Logo_Value _StringLiteralNode()
            {
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                string str = "";
                if (tok.Value.Contains("'"))
                {
                    str = tok.Value.Replace("'", "");
                }
                else
                {
                }

                if (tok.Value.Contains(Conversions.ToString('"')))
                {
                    str = tok.Value.Replace(Conversions.ToString('"'), "");
                }

                object argnValue = str;
                var nde = new Ast_Logo_Value(ref AST_NODE._string, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_string";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            // syntax
            // 
            // numeric integer
            /// <summary>
            /// Syntax:
            /// 
            /// Numeric Literal:
            /// -Number
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _NumericLiteralNode()
            {
                int Str = 0;
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._INTEGER)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                if (int.TryParse(tok.Value, out Str) == true)
                {
                    var nde = new Ast_Logo_Value(ref Str);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_integer";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
                else
                {
                    // Unable to parse default 0 to preserve node listeral as integer
                    object argnValue = 0;
                    var nde = new Ast_Logo_Value(ref AST_NODE._integer, ref argnValue);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_integer";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
            }

            private string GetDebuggerDisplay()
            {
                return base.ToJson;
            }
        }
    }
}

// ROOT PARSER 
namespace SDK.SmallProgLang
{
    namespace Compiler
    {
        public class BaseParser
        {
            public AstProgram _Parse(ref string nScript)
            {
                var Body = new List<Ast_ExpressionStatement>();
                ParserErrors = new List<string>();
                iScript = nScript.Replace(Constants.vbNewLine, ";");
                iScript = Strings.RTrim(iScript);
                iScript = Strings.LTrim(iScript);
                Tokenizer = new Lexer(ref iScript);
                // Dim TokType As GrammarFactory.Grammar.Type_Id
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);

                // GetProgram
                iProgram = _ProgramNode();


                // Preserve InClass
                return iProgram;
            }
            /// <summary>
            /// Main Entry Point. 
            /// Syntax:
            /// 
            /// Program:
            /// -Literals
            /// 
            /// </summary>
            /// <returns></returns>
            public AstProgram _ProgramNode()
            {
                var nde = new AstProgram(ref _StatementList());
                nde._Raw = iScript;
                nde._Start = 0;
                nde._End = iScript.Length;
                nde._TypeStr = "PL PROGRAM";
                return nde;
            }

            private List<AstExpression> _StatementList()
            {
                throw new NotImplementedException();
            }

            #region Propertys
            public List<string> ParserErrors = new List<string>();
            /// <summary>
            /// Currently held script
            /// </summary>
            public string iScript = "";
            /// <summary>
            /// To hold the look ahead value without consuming the value
            /// </summary>
            public string Lookahead;
            /// <summary>
            /// Tokenizer !
            /// </summary>
            private Lexer Tokenizer;
            private AstProgram iProgram;

            public AstProgram Program
            {
                get
                {
                    return iProgram;
                }
            }
            #endregion

            #region Basic Nodes
            /// <summary>
            /// Syntax:
            /// 
            /// Numeric Literal:
            /// -Number
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _NumericLiteralNode()
            {
                int Str = 0;
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._INTEGER)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                if (int.TryParse(tok.Value, out Str) == true)
                {
                    object argnValue = Str;
                    var nde = new Ast_Literal(ref AST_NODE._integer, ref argnValue);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_integer";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
                else
                {
                    // Unable to parse default 0 to preserve node listeral as integer
                    object argnValue1 = 0;
                    var nde = new Ast_Literal(ref AST_NODE._integer, ref argnValue1);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_integer";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
            }
            /// <summary>
            /// Syntax:
            /// 
            /// Nullable Literal:
            /// -Null
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _NullableNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._null, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_null";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Syntax:
            /// "hjk"
            /// String Literal:
            /// -String
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _StringLiteralNode()
            {
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                string str = "";
                if (tok.Value.Contains("'"))
                {
                    str = tok.Value.Replace("'", "");
                }
                else
                {
                }

                if (tok.Value.Contains(Conversions.ToString('"')))
                {
                    str = tok.Value.Replace(Conversions.ToString('"'), "");
                }

                object argnValue = str;
                var nde = new Ast_Literal(ref AST_NODE._string, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_string";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Identifier _IdentifierLiteralNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                var nde = new Ast_Identifier(ref tok.Value);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_variable";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public string _GetAssignmentOperator()
            {
                string str = Tokenizer.GetIdentifiedToken(ref Lookahead).Value;
                str = str.Replace(@"\u003c", " Less than");
                str = str.Replace(@"\u003e", " Greater Than ");
                // \U003c < Less-than sign
                // \U003e > Greater-than sign
                str = str.Replace("<=", " Less than equals ");
                str = str.Replace(">=", " Greater Than equals ");
                str = str.Replace("<", " Less than ");
                str = str.Replace(">", " Greater Than ");
                return Strings.UCase(str);
            }
            /// <summary>
            /// Collects bad token
            /// </summary>
            /// <returns></returns>
            public Ast_Literal __UnknownStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._UnknownStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_UnknownStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used when data has already been collected
            /// </summary>
            /// <param name="ErrorTok"></param>
            /// <returns></returns>
            public Ast_Literal __UnknownStatementNode(ref Token ErrorTok)
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = ErrorTok;
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._UnknownStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_UnknownStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Used to denote white space as it is often important later
            /// Some Parsers ignore this token ; 
            /// It is thought also; to be prudent to collect all tokens to let the Evaluator deal with this later
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _WhitespaceNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._WhiteSpace, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_whitespace";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            #endregion
            #region Expressions
            /// <summary>
            /// Syntax:
            /// Variable: -Identifier as expression
            /// - identifer = binaryExpression
            /// </summary>
            /// <returns></returns>
            public AstExpression _VariableExpression()
            {
                Type_Id toktype;
                Ast_Identifier _left = null;
                // Token ID
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);

                // Get Identifier (All Variable statements start with a Left)
                _left = _IdentifierLiteralNode();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // if the next operation is here then do it
                switch (toktype)
                {
                    case Type_Id._SIMPLE_ASSIGN:
                        {
                            return _VariableInitializer(ref _left);
                        }

                    default:
                        {
                            // Carry Variable forwards to binary function
                            AstExpression arg_left = new Ast_VariableExpressionStatement(ref _left);
                            return _BinaryExpression(ref arg_left);
                        }
                }
            }

            public AstBinaryExpression _VariableInitializer(ref Ast_Identifier _left)
            {
                Lookahead = Tokenizer.ViewNext();
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                try
                {
                    AstExpression arg_left = new Ast_VariableExpressionStatement(ref _left);
                    return (AstBinaryExpression)_BinaryExpression(ref arg_left);
                }
                catch (Exception ex)
                {
                    ParserErrors.Add(ex.ToString());
                    return null;
                }
            }

            public AstExpression _VariableInitializer(ref Ast_VariableDeclarationExpression _left)
            {
                Lookahead = Tokenizer.ViewNext();
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                AstExpression arg_left = new Ast_VariableExpressionStatement(ref _left._iLiteral);
                return _BinaryExpression(ref arg_left);
            }
            /// <summary>
            /// Variable declaration, no init:OR INIT
            /// DIM A 
            /// DIM A AS STRING / INTEGER / BOOLEAN / LIST
            /// let a as string
            ///  
            /// </summary>
            /// <param name="_left">IDENTIFIER</param>
            /// <returns></returns>
            public Ast_VariableDeclarationExpression _VariableDeclaration(ref Ast_Identifier _left)
            {
                _WhitespaceNode();
                Lookahead = Tokenizer.ViewNext();
                var Tok = Tokenizer.CheckIdentifiedToken(ref Lookahead);

                // SELECT lITERAL TYPE
                switch (Strings.UCase(Tok.Value) ?? "")
                {
                    case var @case when @case == Strings.UCase("string"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._string);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case1 when case1 == Strings.UCase("boole"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._boolean);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case2 when case2 == Strings.UCase("bool"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._boolean);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case3 when case3 == Strings.UCase("boolean"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._boolean);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case4 when case4 == Strings.UCase("array"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._array);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case5 when case5 == Strings.UCase("array"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._array);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case6 when case6 == Strings.UCase("list"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._array);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case7 when case7 == Strings.UCase("integer"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._integer);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case8 when case8 == Strings.UCase("nothing"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._null);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case9 when case9 == Strings.UCase("null"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._null);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    case var case10 when case10 == Strings.UCase("int"):
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._integer);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }

                            break;
                        }

                    default:
                        {
                            Tokenizer.GetIdentifiedToken(ref Lookahead);
                            var X = new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._null);
                            Lookahead = Tokenizer.ViewNext();
                            if (Lookahead == ";" == true)
                            {
                                __EndStatementNode();
                                Lookahead = Tokenizer.ViewNext();
                                return X;
                            }
                            else
                            {
                                return X;
                            }

                            break;
                        }
                }

                return new Ast_VariableDeclarationExpression(ref _left, ref AST_NODE._null);
            }

            public Ast_Literal __EndStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._endStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_endStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// _Simple Assign (variable)
            /// _Complex Assign (variable)
            /// 
            /// </summary>
            /// <param name="_left"></param>
            /// <returns></returns>
            public AstExpression _AssignmentExpression(ref Ast_Identifier _left)
            {
                Lookahead = Tokenizer.ViewNext();
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._SIMPLE_ASSIGN:
                        {
                            return _VariableInitializer(ref _left);
                        }
                }

                return _VariableInitializer(ref _left);
            }

            public AstExpression _AssignmentExpression(ref AstExpression _left)
            {
                Lookahead = Tokenizer.ViewNext();
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }
                else
                {
                }

                switch (toktype)
                {
                    case Type_Id._COMPLEX_ASSIGN:
                        {
                            // Complex Assignments are 
                            string _operator = _GetAssignmentOperator();
                            var x = new AstBinaryExpression(ref AST_NODE._assignExpression, ref _left, ref _operator, ref _LeftHandExpression());
                            x._TypeStr = "_COMPLEX_ASSIGN";
                            return x;
                        }
                }

                return _BinaryExpression(ref _left);
            }

            /// <summary>
            /// 
            /// Syntax: 
            /// 
            /// ( OptionalStatmentList; )
            /// 
            /// </summary>
            /// <returns></returns>
            public Ast_ParenthesizedExpresion _ParenthesizedExpression()
            {
                Type_Id toktype;
                var Body = new List<AstExpression>();
                _ConditionalBeginNode();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Detect Empty List
                if (toktype == Type_Id._CONDITIONAL_END)
                {
                    Body.Add(new Ast_ExpressionStatement(ref __EmptyStatementNode()));
                    _ConditionalEndNode();
                }
                else
                {
                    while (toktype != Type_Id._CONDITIONAL_END)
                    {
                        Body.Add(_ExpressionStatement());
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }

                    _ConditionalEndNode();
                }

                return new Ast_ParenthesizedExpresion(ref Body);
            }
            #region Binary Operations/Expressions
            /// <summary>
            /// Syntax:
            ///      -Multiplicative Expression
            /// Literal */ Literal
            /// </summary>
            /// <returns></returns>
            public AstExpression _MultiplicativeExpression()
            {
                return _BinaryExpression(ref Type_Id._MULTIPLICATIVE_OPERATOR, AST_NODE._MultiplicativeExpression, "_MultiplicativeExpression");
            }
            /// <summary>
            /// Syntax:
            ///      -Addative Expression
            /// Literal +- Literal
            /// </summary>
            /// <returns></returns>
            public AstExpression _AddativeExpression()
            {
                return _BinaryExpression(ref Type_Id._ADDITIVE_OPERATOR, AST_NODE._AddativeExpression, "_AddativeExpression");
            }
            /// <summary>
            /// Syntax: 
            /// 
            /// _RelationalExpression
            /// </summary>
            /// <returns></returns>
            public object _RelationalExpression()
            {
                return _BinaryExpression(ref Type_Id._RELATIONAL_OPERATOR, AST_NODE._ConditionalExpression, "_ConditionalExpression");
            }
            /// <summary>
            /// syntax:
            /// 
            /// 
            /// -Literal(Primary Expression)
            /// -Multiplicative Expression
            /// -Addative Expression
            /// -ConditionalExpression(OperationalExpression)
            /// _LeftHandExpression
            /// __BinaryExpression
            /// </summary>
            /// <param name="NType"></param>
            /// <param name="AstType"></param>
            /// <param name="AstTypeStr"></param>
            /// <returns></returns>
            public AstExpression _BinaryExpression(ref Type_Id NType, AST_NODE AstType, string AstTypeStr)
            {
                AstExpression _left;
                string _Operator = "";
                AstExpression _Right;
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Remove Erronious WhiteSpaces
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }
                else
                {
                }

                _left = _PrimaryExpression();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._SIMPLE_ASSIGN:
                        {
                            while (toktype == Type_Id._SIMPLE_ASSIGN)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._assignExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "AssignmentExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._ADDITIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._ADDITIVE_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._AddativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._MULTIPLICATIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._MULTIPLICATIVE_OPERATOR)
                            {
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                _Operator = _GetAssignmentOperator();

                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._MultiplicativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                            }

                            break;
                        }

                    case Type_Id._RELATIONAL_OPERATOR:
                        {
                            while (toktype == Type_Id._RELATIONAL_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._ConditionalExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._WHITESPACE:
                        {
                            _WhitespaceNode();
                            break;
                        }
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._STATEMENT_END)
                {
                    var x = __EmptyStatementNode();
                    return _left;
                }
                else
                {
                    return _left;
                }
                // End of file Marker
                return _left;
            }

            public AstExpression _BinaryExpression()
            {
                AstExpression _left;
                string _Operator = "";
                AstExpression _Right;
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Remove Erronious WhiteSpaces
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }
                else
                {
                }

                _left = _PrimaryExpression();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._SIMPLE_ASSIGN:
                        {
                            while (toktype == Type_Id._SIMPLE_ASSIGN)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._assignExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "AssignmentExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._ADDITIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._ADDITIVE_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._AddativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._MULTIPLICATIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._MULTIPLICATIVE_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._MultiplicativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._RELATIONAL_OPERATOR:
                        {
                            while (toktype == Type_Id._RELATIONAL_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._ConditionalExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._WHITESPACE:
                        {
                            _WhitespaceNode();
                            break;
                        }
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._STATEMENT_END)
                {
                    var x = __EmptyStatementNode();
                    return _left;
                }
                else
                {
                    return _left;
                }
                // End of file Marker
                return _left;
            }

            public AstExpression _BinaryExpression(ref AstExpression _left)
            {
                string _Operator = "";
                AstExpression _Right;
                Type_Id toktype;
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                // Remove Erronious WhiteSpaces
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }
                else
                {
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (toktype)
                {
                    case Type_Id._COMPLEX_ASSIGN:
                        {
                            while (toktype == Type_Id._COMPLEX_ASSIGN)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._assignExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "AssignmentExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._SIMPLE_ASSIGN:
                        {
                            while (toktype == Type_Id._SIMPLE_ASSIGN)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._assignExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "AssignmentExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._ADDITIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._ADDITIVE_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._AddativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._MULTIPLICATIVE_OPERATOR:
                        {
                            while (toktype == Type_Id._MULTIPLICATIVE_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._MultiplicativeExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._RELATIONAL_OPERATOR:
                        {
                            while (toktype == Type_Id._RELATIONAL_OPERATOR)
                            {
                                _Operator = _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                // NOTE: When adding further binary expressions maybe trickle down with this side
                                // the final level will need to be primary expression? 
                                _Right = _LeftHandExpression();
                                _left = new AstBinaryExpression(ref AST_NODE._ConditionalExpression, ref _left, ref _Operator, ref _Right);
                                _left._TypeStr = "BinaryExpression";
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                            }

                            break;
                        }

                    case Type_Id._WHITESPACE:
                        {
                            _WhitespaceNode();
                            break;
                        }
                }

                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._STATEMENT_END)
                {
                    var x = __EmptyStatementNode();
                    return _left;
                }
                else
                {
                    return _left;
                }
                // End of file Marker
                return _left;
            }
            /// <summary>
            /// Syntax
            /// 
            /// -Literal => (_PrimaryExpression)
            /// -EatExtra WhiteSpace
            /// -EatExtra ";"
            /// </summary>
            /// <returns></returns>
            public AstExpression _PrimaryExpression()
            {
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (tok)
                {
                    case Type_Id._WHITESPACE:
                        {
                            _WhitespaceNode();
                            break;
                        }

                    case Type_Id._STATEMENT_END:
                        {
                            // __EmptyStatementNode()
                            var temp = new Ast_ExpressionStatement(ref __EmptyStatementNode());
                            temp._TypeStr = "_PrimaryExpression";
                            return temp;
                        }

                    default:
                        {
                            // Literal - Node!
                            Ast_ExpressionStatement Expr;
                            var nde = _literalNode();
                            if (nde is object)
                            {
                                Expr = new Ast_ExpressionStatement(ref nde);
                                // Advances to the next cursor
                                Lookahead = Tokenizer.ViewNext();
                                Expr._TypeStr = "_PrimaryExpression";
                                return Expr;
                            }
                            else
                            {
                                // Technically badtoken try capture
                                var etok = __UnknownStatementNode();
                                Lookahead = "EOF";
                                ParserErrors.Add("Unknown Statement/Expression/Function Uncountered" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput().Replace("  ", "") + Constants.vbNewLine);
                                var Lit = new Ast_ExpressionStatement(ref etok);
                                Lit._TypeStr = "_PrimaryExpression";
                            }

                            break;
                        }
                }
                // Technically badtoken try capture
                var ertok = __UnknownStatementNode();
                Lookahead = "EOF";
                ParserErrors.Add("Unknown Statement/LiteralExpression Uncountered" + Constants.vbNewLine + ertok.ToJson().FormatJsonOutput().Replace("  ", "") + Constants.vbNewLine);
                return new Ast_ExpressionStatement(ref ertok);
            }
            /// <summary>
            /// -Literals
            /// Syntax:
            ///     
            ///     -Numeric Literal
            ///     -String Literal
            ///     -Comments
            ///     -Nullable
            ///     -BooleanLiteral
            ///     -ArrayLiteral
            ///     -EatWhiteSpace
            ///     -EatEmptyStatment
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _literalNode()
            {
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                switch (tok)
                {
                    case Type_Id._INTEGER:
                        {
                            return _NumericLiteralNode();
                        }

                    case Type_Id._STRING:
                        {
                            return _StringLiteralNode();
                        }

                    // Case GrammarFactory.Grammar.Type_Id._VARIABLE
                    // Dim ntok = Tokenizer.GetIdentifiedToken(Lookahead)
                    // Dim xc = New Ast_Literal(AST_NODE._variable, ntok.Value)
                    // xc._Start = ntok._start
                    // xc._End = ntok._End
                    // xc._Raw = ntok.Value
                    case Type_Id._LIST_BEGIN:
                        {
                            return _ArrayListLiteral();
                        }

                    case Type_Id._NULL:
                        {
                            return _NullableNode();
                        }

                    case Type_Id._TRUE:
                        {
                            return _BooleanNode();
                        }

                    case Type_Id._FALSE:
                        {
                            return _BooleanNode();
                        }

                    case Type_Id._WHITESPACE:
                        {
                            return _WhitespaceNode();
                        }

                    case Type_Id._STATEMENT_END:
                        {
                            return __EmptyStatementNode();
                        }

                    case Type_Id.SAL_HALT:
                        {
                            var nTok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                            object argnValue = nTok.Value;
                            var fnd = new Ast_SAL_Literal(ref AST_NODE.SAL_HALT, ref argnValue);
                            fnd._End = nTok._End;
                            fnd._Raw = "HALT";
                            fnd._Start = nTok._start;
                            fnd._TypeStr = "HALT";
                            Lookahead = Tokenizer.ViewNext();
                            tok = Tokenizer.IdentifiyToken(ref Lookahead);
                            return fnd;
                            break;
                        }

                    default:
                        {
                            // Technically badtoken try capture
                            var etok = __UnknownStatementNode();
                            ParserErrors.Add("Unknown Literal Uncountered" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput().Replace("  ", "") + Constants.vbNewLine);
                            Lookahead = "EOF";
                            return etok;
                        }
                }
                // Technically badtoken try capture
                var itok = __UnknownStatementNode();
                Lookahead = "EOF";
                ParserErrors.Add("Unknown Literal Uncountered" + Constants.vbNewLine + itok.ToJson().FormatJsonOutput().Replace("  ", "") + Constants.vbNewLine);
                return itok;
            }

            public Ast_Literal _BooleanNode()
            {
                bool Str = false;
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                if (bool.TryParse(tok.Value, out Str) == true)
                {
                    var nde = new Ast_Literal(ref Str);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_boolean";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
                else
                {
                    // Default to false
                    bool argnValue = false;
                    var nde = new Ast_Literal(ref argnValue);
                    nde._Start = tok._start;
                    nde._End = tok._End;
                    nde._Raw = tok.Value;
                    nde._TypeStr = "_boolean";
                    Lookahead = Tokenizer.ViewNext();
                    return nde;
                }
            }

            public Ast_Literal _ArrayListLiteral()
            {
                Lookahead = Tokenizer.ViewNext();
                Type_Id toktype;
                var Body = new List<AstNode>();
                _ListBeginNode();
                Lookahead = Tokenizer.ViewNext();
                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._LIST_END == true)
                    Body.Add(__EmptyStatementNode());
                while (toktype != Type_Id._LIST_END)
                {
                    switch (toktype)
                    {
                        case Type_Id._WHITESPACE:
                            {
                                _WhitespaceNode();
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._VARIABLE:
                            {
                                Body.Add(_VariableInitializer(ref _IdentifierLiteralNode()));
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        case Type_Id._LIST_END:
                            {
                                _ListEndNode();
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                object argnValue = Body;
                                var de = new Ast_Literal(ref AST_NODE._array, ref argnValue);
                                de._TypeStr = "_array";
                                Lookahead = Tokenizer.ViewNext();
                                return de;
                            }

                        case Type_Id._LIST_SEPERATOR:
                            {
                                _GetAssignmentOperator();
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }

                        default:
                            {
                                Body.Add(_literalNode());
                                Lookahead = Tokenizer.ViewNext();
                                toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                                break;
                            }
                    }
                }

                _ListEndNode();
                // Error at this point
                object argnValue1 = Body;
                var nde = new Ast_Literal(ref AST_NODE._array, ref argnValue1);
                nde._TypeStr = "_array";
                return nde;
            }
            /// <summary>
            /// Syntax:
            /// 
            /// Comments Literal:
            /// -Comments
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _CommentsNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._COMMENTS)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._comments, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_comments";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// Gets Expression Statement All functions etc are some form of Expression
            /// Syntax
            /// -Expression ";"
            /// 
            /// 
            /// </summary>
            /// <returns></returns>
            public AstExpression _ExpressionStatement()
            {
                return _Expression();
            }
            /// <summary>
            /// 
            /// Syntax:
            /// -_PrimaryExpression(literal)
            /// -_MultiplicativeExpression
            /// -_AddativeExpression
            /// -_RelationalExpression
            /// 
            /// </summary>
            /// <returns></returns>
            public AstExpression _Expression()
            {
                return _LeftHandExpression();
            }
            /// <summary>
            /// Used for end of statement
            /// </summary>
            /// <returns></returns>
            public Ast_Literal __EmptyStatementNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._EMPTY_STATEMENT)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._emptyStatement, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_emptyStatement";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public AstExpression _CommentsListExpression()
            {
                var Body = new List<Ast_Literal>();
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                while (tok == Type_Id._COMMENTS)
                    Body.Add(_CommentsNode());
                object argnValue = Body;
                var argnValue1 = new Ast_Literal(ref AST_NODE._comments, ref argnValue);
                var x = new Ast_ExpressionStatement(ref argnValue1);
                x._TypeStr = "_CommentsExpression";
                return x;
            }
            /// <summary>
            /// Used to Eat Node
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _CodeBeginNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._Code_Begin, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_Code_Begin";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Literal _ConditionalBeginNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._OperationBegin, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_OperationBegin";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }

            public Ast_Literal _ListEndNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                var x = Tokenizer.GetIdentifiedToken(ref Lookahead);
                // Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                // nde._Start = tok._start
                // nde._End = tok._End
                // nde._Raw = tok.Value
                // nde._TypeStr = "_Code_End"
                // Lookahead = Tokenizer.ViewNext
                var xDC = new Ast_Literal(ref AST_NODE._ListEnd);
                xDC._Start = x._start;
                xDC._End = x._End;
                xDC._Raw = x.Value;
                Lookahead = Tokenizer.ViewNext();
                return xDC;
            }

            public Ast_Literal _ListBeginNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                var x = Tokenizer.GetIdentifiedToken(ref Lookahead);
                // Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                // nde._Start = tok._start
                // nde._End = tok._End
                // nde._Raw = tok.Value
                // nde._TypeStr = "_Code_End"
                // Lookahead = Tokenizer.ViewNext
                var xDC = new Ast_Literal(ref AST_NODE._ListEnd);
                xDC._Start = x._start;
                xDC._End = x._End;
                xDC._Raw = x.Value;
                Lookahead = Tokenizer.ViewNext();
                return xDC;
            }
            /// <summary>
            /// Used to Eat Node 
            /// </summary>
            /// <returns></returns>
            public Ast_Literal _CodeEndNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                Lookahead = Tokenizer.ViewNext();
                var tok = Tokenizer.IdentifiyToken(ref Lookahead);
                var x = Tokenizer.GetIdentifiedToken(ref Lookahead);
                // Dim nde = New Ast_Literal(AST_NODE._Code_End, tok.Value)
                // nde._Start = tok._start
                // nde._End = tok._End
                // nde._Raw = tok.Value
                // nde._TypeStr = "_Code_End"
                // Lookahead = Tokenizer.ViewNext
                var xDC = new Ast_Literal(ref AST_NODE._Code_End);
                xDC._Start = x._start;
                xDC._End = x._End;
                xDC._Raw = x.Value;
                Lookahead = Tokenizer.ViewNext();
                return xDC;
            }

            public Ast_Literal _ConditionalEndNode()
            {
                // Dim tok As Token = Tokenizer.Eat(GrammarFactory.Grammar.Type_Id._NULL)
                var tok = Tokenizer.GetIdentifiedToken(ref Lookahead);
                object argnValue = tok.Value;
                var nde = new Ast_Literal(ref AST_NODE._OperationEnd, ref argnValue);
                nde._Start = tok._start;
                nde._End = tok._End;
                nde._Raw = tok.Value;
                nde._TypeStr = "_OperationEnd";
                Lookahead = Tokenizer.ViewNext();
                return nde;
            }
            /// <summary>
            /// 
            /// Syntax:
            /// -EatWhiteSpace
            /// -SalExpression
            /// -ParenthesizedExpresion
            /// -_VariableExpression
            /// -_COMMENTS
            /// _CommandFunction
            /// -_BinaryExpression
            /// 
            /// 'Added Glitch(Select case on tokenvalue) ..... Not sure if it is the right way
            /// as the variables are blocking the keywords?
            /// </summary>
            /// <returns></returns>
            public AstExpression _LeftHandExpression()
            {
                Lookahead = Tokenizer.ViewNext();
                var toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                if (toktype == Type_Id._WHITESPACE)
                {
                    while (toktype == Type_Id._WHITESPACE)
                    {
                        _WhitespaceNode();
                        Lookahead = Tokenizer.ViewNext();
                        toktype = Tokenizer.IdentifiyToken(ref Lookahead);
                    }
                }
                else
                {
                }

                switch (toktype)
                {
                    case Type_Id._VARIABLE:
                        {
                            var arg_left = _VariableExpression();
                            return _BinaryExpression(ref arg_left);
                        }

                    case Type_Id._COMMENTS:
                        {
                            return _CommentsListExpression();
                        }

                    case Type_Id._CONDITIONAL_BEGIN:
                        {
                            return _ParenthesizedExpression();
                        }

                    default:
                        {
                            // Must be a primaryExpression With binary
                            return _BinaryExpression();
                        }
                }

                // Technically badtoken try capture
                var etok = __UnknownStatementNode();
                ParserErrors.Add("Unknown Statement/_LeftHandExpression Uncountered" + Constants.vbNewLine + etok.ToJson().FormatJsonOutput() + Constants.vbNewLine);
                return new Ast_ExpressionStatement(ref etok);
            }
            #endregion
            #endregion
        }
    }
}

#endregion


namespace SDK.SmallProgLang
{
    public class TURTLE
    {
        public int X_axis;
        public int Y_axis;
        private const int MAX_CANVAS_WIDTH = 3000;
        private const int MAX_CANVAS_HEIGHT = 3000;
        private Image drawingImage;
        private Graphics drawingGraphics;
        public Image turtleImage;
        private double m_Angle = 90d;
        private Color Color = Color.Red;
        private double penWidth = 1d;
        private readonly PictureBox turtlePicture = new PictureBox();
        public Panel _CONTROL;
        public PenStatus _PenStatus = PenStatus.Down;
        public Color PenColor;

        public enum PenStatus
        {
            Up,
            Down
        }

        public TURTLE(Panel control, Image turtleImage)
        {
            // Me.MAX_CANVAS_WIDTH = _Screen.Width
            // Me.MAX_CANVAS_HEIGHT = _Screen.Height
            this.turtleImage = turtleImage;
            _CONTROL = control;
            int argmaximumCanvasWidth = control.Width;
            int argmaximumCanvasHeight = control.Height;
            InitalizeTurtle(ref argmaximumCanvasWidth, ref argmaximumCanvasHeight);
            control.Width = argmaximumCanvasWidth;
            control.Height = argmaximumCanvasHeight;
            ShowHideTurtle = true;
        }

        private void InvalidateControl()
        {
            _CONTROL.Invalidate();
            _CONTROL.Update();
            Thread.Sleep(100);
            Application.DoEvents();
        }

        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            if (_CONTROL is object)
            {
                FileSystem.Reset();
                _CONTROL.Invalidate();
            }
        }

        public int _Angle
        {
            get
            {
                return (int)Math.Round(m_Angle);
            }

            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > 360)
                {
                    value = 360;
                }

                m_Angle = value;
            }
        }

        private void InitalizeTurtle(ref int maximumCanvasWidth, ref int maximumCanvasHeight)
        {
            m_Angle = 90.0d;
            Color = Color.Black;
            penWidth = 1.0d;
            _CONTROL.Controls.Add(turtlePicture);
            _CONTROL.Paint += OnControlPaint;
            _CONTROL.ClientSizeChanged += OnClientSizeChanged;
            drawingImage = new Bitmap(maximumCanvasWidth, maximumCanvasHeight);
            drawingGraphics = Graphics.FromImage(drawingImage);
            drawingGraphics.Clear(Color.LightGray);
            drawingGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            _CONTROL.Invalidate();
        }

        private void _CenterScreen()
        {
            _Reset();
        }

        private static PointF WorldPositionToControl(Control control, PointF original)
        {
            int w = control.ClientSize.Width;
            int h = control.ClientSize.Height;
            return new PointF(w / 2.0f + original.X, h / 2.0f - original.Y);
        }

        private static PointF ControlPositionToWorld(Control control, PointF original)
        {
            return new PointF(original.X - control.ClientSize.Width / 2.0f, control.ClientSize.Height / 2.0f - original.Y);
        }

        public PointF Position
        {
            get
            {
                var centerPoint = new PointF(turtlePicture.Left + turtlePicture.Width / 2.0f, turtlePicture.Top + turtlePicture.Height / 2.0f);
                return ControlPositionToWorld(_CONTROL, centerPoint);
            }

            set
            {
                var centerInControl = WorldPositionToControl(_CONTROL, value);
                double left = centerInControl.X - turtlePicture.Width / 2.0d;
                double top = centerInControl.Y - turtlePicture.Height / 2.0d;
                turtlePicture.Left = Convert.ToInt32(left);
                turtlePicture.Top = Convert.ToInt32(top);
            }
        }

        public void _Reset()
        {
            _Angle = (int)Math.Round(90.0);
            _Rotate();
            Position = new Point(0, 0);
        }

        private object ShowHideTurtle = false;

        public void ShowTurtle()
        {
            ShowHideTurtle = true;
        }

        public void HideTurtle()
        {
            ShowHideTurtle = false;
        }

        public void SetPenWidth(float width)
        {
            penWidth = width;
        }

        public void SetPenColor(Color color)
        {
            PenColor = color;
        }

        public void SetPenColor(int r, int g, int b)
        {
            PenColor = Color.FromArgb(r, g, b);
        }

        public void DrawLine(PointF _from, PointF _to)
        {
            using (var pen = new Pen(PenColor, (float)penWidth)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            })
            {
                var fromPoint = WorldPositionToControl(_CONTROL, _from);
                var toPoint = WorldPositionToControl(_CONTROL, _to);
                drawingGraphics.DrawLine(pen, fromPoint, toPoint);
            }

            _CONTROL.Invalidate();
        }

        private void OnControlPaint(object sender, PaintEventArgs e)
        {
            if (_CONTROL is object)
            {
                e.Graphics.DrawImage(drawingImage, 0, 0);
            }
        }
        /// <summary>
        /// Forwards Command
        /// </summary>
        /// <param name="Amt"></param>
        public void _forward(ref int Amt)
        {
            float toX = Convert.ToSingle(Position.X + Amt * Math.Cos(_Angle * Math.PI / 180d));
            float toY = Convert.ToSingle(Position.Y + Amt * Math.Sin(_Angle * Math.PI / 180d));
            var origPosition = Position;
            Position = new PointF(toX, toY);
            if (_PenStatus == PenStatus.Down)
            {
                DrawLine(origPosition, new PointF(toX, toY));
            }

            _CONTROL.Invalidate();
        }

        public void _backward(ref int Amt)
        {
            int argAmt = -Amt;
            _forward(ref argAmt);
        }

        public void _Right(ref int Degrees)
        {
            _Angle += Degrees;
            _Rotate();
        }

        public void _Left(ref int Degrees)
        {
            _Angle -= Degrees;
            _Rotate();
        }

        public Bitmap RotateImage(Image bmp, float angleDegrees)
        {
            var rotatedImage = new Bitmap(bmp.Width, bmp.Height);
            using (var g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(bmp.Width / 2.0f, bmp.Height / 2.0f);
                g.RotateTransform(90f - angleDegrees);
                g.TranslateTransform(-bmp.Width / 2.0f, -bmp.Height / 2.0f);
                g.DrawImage(bmp, new Point(0, 0));
            }

            return rotatedImage;
        }

        public void _Rotate()
        {
            turtlePicture.Image = RotateImage(turtleImage, _Angle);
            turtlePicture.Width = turtlePicture.Image.Width;
            turtlePicture.Height = turtlePicture.Image.Height;
            _CONTROL.Invalidate();
        }

        public void _Clear()
        {
            drawingGraphics.Clear(Color.White);
            _Reset();
        }
    }
}