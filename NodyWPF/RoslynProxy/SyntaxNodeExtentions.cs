using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NodyWPF.RoslynProxy;

public static class SyntaxNodeExtentions
{
    public static IEnumerable<TopLevelDeclarationSyntax> GetPropsAndFieldsDeclarationSyntax(this SyntaxNode root)
	{
        var props = root.DescendantNodes().OfType<PropertyDeclarationSyntax>().Select(p => 
            new TopLevelDeclarationSyntax(p.Identifier.ValueText, p.Type.ToString(), p.Type.ToString())
        );
        var field = root.DescendantNodes().OfType<FieldDeclarationSyntax>().Select(p => 
            new TopLevelDeclarationSyntax(p.Declaration.Variables.First().Identifier.ValueText, p.Declaration.Type.ToString(), p.Declaration.Type.ToString())
        );

        return Enumerable.Concat(props, field);
        //File.WriteAllText(sourceFilePath + ".ro", string.Join(" ;", root.DescendantNodes().OfType<PropertyDeclarationSyntax>().Select(p => p.GetText() + p.Identifier.ValueText + "")));
        //File.AppendAllText(sourceFilePath + ".ro", $"{Environment.NewLine}{Environment.NewLine}-----------Fine properties {Environment.NewLine}");
        //File.AppendAllText(sourceFilePath + ".ro", string.Join(" ;", root.DescendantNodes().OfType<FieldDeclarationSyntax>().Select(p => p.GetText() + string.Join(" _ ", p.Declaration.Variables.Select(v => v.Identifier.ValueText)) + "")));
        //File.AppendAllText(sourceFilePath + ".ro", $"{Environment.NewLine}{Environment.NewLine}-----------Fine fields {Environment.NewLine}");
        //File.AppendAllText(sourceFilePath + ".ro", string.Join(" ;", root.DescendantNodes().OfType<MemberDeclarationSyntax>().Select(p => p.GetText() + "" + /*string.Join(" _ ", p.ToFullString()) +*/ ""))); // questo ritorna, definizione della classe, dei field, prop e metodi, può essere utile ma vanno filtrati.
        //File.AppendAllText(sourceFilePath + ".ro", $"{Environment.NewLine}{Environment.NewLine}-----------Fine base types {Environment.NewLine}");
    }
    
    public static IEnumerable<ClassDeclarationSyntax> GetClassesDeclarationSyntax(this SyntaxNode root)
	{
        return root.DescendantNodes().OfType<ClassDeclarationSyntax>();
    }
}
