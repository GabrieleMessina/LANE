using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using NodyWPF.RoslynProxy;
using Nodify;

namespace NodyWPF;

public class EditorViewModel
{
    public NodifyEditor MainEditor { get; set; }
    public ObservableCollection<ClassSyntaxNodeViewModel> ClassSyntaxNodes { get; } = new ObservableCollection<ClassSyntaxNodeViewModel>();
    public ObservableCollection<ConnectionViewModel> ClassConnections { get; } = new ObservableCollection<ConnectionViewModel>();

    public EditorViewModel(NodifyEditor mainEditor)
    {
        MainEditor = mainEditor;
        FindClasses();
        FindLink();

        CalculateNextPointInTable();
        //CalculateNextPointInSpiral();
    }

    public void FindClasses()
    {
        var projectFilePath = @"D:\Users\gabry\Lavoro\neotek\rivaleat-web";
        //var projectFilePath = @"D:\Users\gabry\Lavoro\universita\TestSyntax\dotnet";
        //var projectFilePath = @"D:\Users\gabry\Lavoro\universita\TestSyntax\SimpleAuthentication";
        //var projectFilePath = @"D:\Users\gabry\Lavoro\universita\TestSyntax\TinyHelpers";
        //var projectFilePath = @"D:\Users\gabry\Lavoro\universita\TestSyntax\ValeatImporter";

        var projectFiles = Directory.EnumerateFiles(projectFilePath, "*.cs", new EnumerationOptions() { RecurseSubdirectories = true, MaxRecursionDepth = 30});
        foreach (var sourceFilePath in projectFiles)
        {
            var programText = File.ReadAllText(sourceFilePath);

            SyntaxTree tree = CSharpSyntaxTree.ParseText(text: programText, path: sourceFilePath);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();
            var test = root.ToFullString();

            AddClassNodeToView(root.GetClassesDeclarationSyntax());
        }
    }
    public void AddClassNodeToView(IEnumerable<ClassDeclarationSyntax> classes)
    {
        var nodeToAdd = new List<ClassSyntaxNodeViewModel>();
        //for(int i = 0; i < classes.Count(); i++)
        foreach(var @class in classes)
        {
            //var @class = classes.ElementAt(i);
            //trovo prop e field della classe
            var member = @class.GetPropsAndFieldsDeclarationSyntax();

            //trovo i nomi di questi membri
            var membersNames = member.Select(member => member.ToString()).ToArray();
            //creo dei connettori di output che dovranno poi essere legati alle classi o ai tipi con cui sono stati dichiarati
            var membersOutputConnectors = member.Select(member => new TypeConnectorViewModel(member.Type, member.ToString())).ToArray();

            //creo il nodo della classe aggiungendo i dati sui membri e i connettori
            var classSyntaxNode = new ClassSyntaxNodeViewModel(@class, membersNames, membersNames.Length)
            {
                Output = new ObservableCollection<TypeConnectorViewModel>(membersOutputConnectors),
                //Location = new System.Windows.Point(Random.Shared.NextDouble() * App.Current.MainWindow.Width, Random.Shared.NextDouble() * App.Current.MainWindow.Height),
                //Location = CalculateNextPointInSpiral(i),
            };


            //aggiungo un connettore d'ingresso alla classe per poter essere referenziata dai connettori di output dei membri
            classSyntaxNode.Input.Add(new TypeConnectorViewModel(classSyntaxNode.Name));

            //aggiungo i nuovi nodi di classe alla lista globale
            ClassSyntaxNodes.Add(classSyntaxNode);
            //nodeToAdd.Add(classSyntaxNode);
        }        
    }


    //galassia
    //float circleSpeed = 1f;
    //float circleSize = 1f;
    //float circleSizeGrowSpeed = 100f;
    //public Point CalculateNextPointInSpiral(int currentNodeIndex)
    //{
    //    var xPos = Math.Sin(currentNodeIndex * circleSpeed) * circleSize;
    //    var yPos = Math.Cos(currentNodeIndex * circleSpeed) * circleSize;

    //    circleSize += circleSizeGrowSpeed;
    //    return new Point(xPos, yPos);
    //}


    //float circleSpeed = float.Pi * 20;
    //float circleSize = 250f;
    //float circleSizeGrowSpeed = 40f;
    //public Point CalculateNextPointInSpiral(int currentNodeIndex)
    //{
    //    var xPos = Math.Sin(ToRadians(currentNodeIndex * circleSpeed)) * circleSize;
    //    var yPos = Math.Cos(ToRadians(currentNodeIndex * circleSpeed)) * circleSize;

    //    circleSize += circleSizeGrowSpeed;
    //    return new Point(xPos, yPos);
    //}
    

    /*
        ordiniamo i nodi in base al grado, prima quelli senza input ne output, poi quelli con nessun input e molti output (più output in alto)
        poi quelli con input ed output, poi quelli con soli input
     
     */
    public void CalculateNextPointInSpiral()
    {
        //int i = 0;

        //var nodeToAdd = ClassSyntaxNodes.OrderByDescending(n => n.Weight).ToList();

        //foreach (var node in nodeToAdd)
        //{
        //    node.Location = CalculateNextPointInSpiral(i++);
        //}
    }
    public void CalculateNextPointInTable()
    {
        double baseX = 1000d;
        double baseY = -1000d;

        var nodeToAdd = ClassSyntaxNodes
            .OrderBy(n => n.ConnectedOutputsCount)
            .GroupBy(n => n.ConnectedInputsCount)
            .ToList();

        foreach (var nodeInLevel in nodeToAdd)
        {
            foreach (var node in nodeInLevel)
            {
                if(node.ConnectedInputsCount == 0 && node.ConnectedOutputsCount == 0)
                {
                    node.Location = new(baseX - 1000d, baseY);
                    baseY += 100d;
                }
            }
            baseY /= 2d;
        }

        foreach (var nodeInLevel in nodeToAdd)
        {
            foreach (var node in nodeInLevel)
            {
                if (node.ConnectedInputsCount != 0 || node.ConnectedOutputsCount != 0)
                {
                    node.Location = new(baseX + (1000d * nodeInLevel.Key), baseY);
                    baseY += 100d;
                }
            }
            baseY /= 2d;
        }
    }

    public void FindLink()
    {
        foreach(var currentNode in ClassSyntaxNodes){
            var @class = currentNode.ClassDeclaration;
            //per ogni nodo ottengo la lista dei suoi mebri
            var currentNodeMembers = @class.GetPropsAndFieldsDeclarationSyntax();

            //per ogni membro controllo se ci sono classi di quel tipo in modo da creare eventuali connessioni fra nodi
            foreach (var currentNodeMember in currentNodeMembers)
            {   
                //ci possono essere molte variabili dello stesso tipo, in quel caso dobbiamo connetterle tutte alla classe corretta.
                var sourceConnectors = currentNode.Output.Where(o => o.Type == currentNodeMember.Type);

                //TODO: può esistere solo una classe con lo stesso nome al momento, piccola semplificazione che va risolta usando i namespace.
                var targetConnector = ClassSyntaxNodes.SelectMany(n => n.Input).FirstOrDefault(i => i.Type == currentNodeMember.Type);

                if (targetConnector == null)
                {
                    //TODO: ci troviamo probabilmente con un tipo base o con una classe non mappata, gestire i casi.
                    continue;
                }

                //creo le connessioni
                foreach(var connector in sourceConnectors)
                {
                    ClassConnections.Add(new ConnectionViewModel(connector, targetConnector));
                }
            }
        }
    }
}
