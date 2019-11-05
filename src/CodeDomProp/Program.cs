using System;
using System.IO;
using System.CodeDom;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection; // TypeAttributes

namespace CodeDomProp
{
    class Program
    {
        static void Main(string[] args)
        {
            Generate(CreateCode());
        }
        // ファイル出力
        private static void Generate(CodeCompileUnit compileunit)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            string sourceFile = @"Hello.cs";
            using (StreamWriter sw = new StreamWriter(sourceFile, false))
            {
                IndentedTextWriter tw = new IndentedTextWriter(sw, "    ");
                provider.GenerateCodeFromCompileUnit(compileunit, tw,
                    new CodeGeneratorOptions());
                tw.Close();
            }
        }
        // コード生成
        private static CodeCompileUnit CreateCode()
        {
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeNamespace samples = new CodeNamespace("MyNamespace");
            samples.Imports.Add(new CodeNamespaceImport("System"));
            CodeTypeDeclaration targetClass = new CodeTypeDeclaration("MyClass");
            targetClass.IsClass = true;
            targetClass.TypeAttributes =
                TypeAttributes.Public | TypeAttributes.Sealed;
            samples.Types.Add(targetClass);
            targetUnit.Namespaces.Add(samples);
            return targetUnit;
        }
    }
}
