open System.IO

type CodeBlock =
    {
        File: string
        Lang: string
        Content: string
    }

type ParsedArgs =
    {
        OutputPath: string
        FilterExts: string list
        Help: bool
        Files: string list
    }

let parseArgs args =
    let rec loop args acc =
        match args with
        | [] -> acc
        | "-o"::value::tail -> loop tail { acc with OutputPath = value }
        | "-e"::value::tail -> loop tail { acc with FilterExts = acc.FilterExts @ [value] }
        | "-h"::tail -> { acc with Help = true }
        | path::tail -> 
            if Directory.Exists(path) then
                let files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories) |> Array.toList
                loop tail { acc with Files = acc.Files @ files }
            else
                loop tail acc
    loop args { OutputPath = "output.md"; FilterExts = []; Help = false; Files = [] }

let getFileContent (path: string) =
    use streamReader = new StreamReader(path)
    streamReader.ReadToEnd()

let getLangByExt ext =
    match ext with
    | ".kt" -> "kotlin" 
    | ".java" -> "java"
    | ".cs" -> "csharp"
    | ".fs" -> "fsharp"
    | ".js" -> "javascript"
    | ".ts" -> "typescript"
    | ".html" -> "html"
    | ".css" -> "css"
    | ".py" -> "python"
    | ".sh" -> "bash"
    | ".ps1" -> "powershell"
    | ".bat" -> "batch"
    | ".c" -> "c"
    | ".cpp" -> "cpp"
    | ".h" -> "cpp"
    | ".hpp" -> "cpp"
    | ".go" -> "go"
    | ".rs" -> "rust"
    | ".php" -> "php"
    | ".sql" -> "sql"
    | ".json" -> "json"
    | ".xml" -> "xml"
    | ".yml" -> "yaml"
    | ".yaml" -> "yaml"
    | ".clj" -> "clojure"
    | ".cljs" -> "clojure"
    | ".cljc" -> "clojure"
    | ".edn" -> "clojure"
    | ".lua" -> "lua"
    | ".rb" -> "ruby"
    | ".r" -> "r"
    | ".scala" -> "scala"
    | ".swift" -> "swift"
    | ".vb" -> "vbnet"
    | ".vbnet" -> "vbnet"
    | ".coffee" -> "coffeescript"
    | ".elm" -> "elm"
    | _ -> ""

let getCodeBlock ext path : CodeBlock =
    let lang = getLangByExt ext
    let content = getFileContent path
    { File = path; Lang = lang; Content = content }


let generateOutput files (filterExts:string list) (outputPath:string) =
    let codeBlocks = 
        files
        |> Seq.filter (fun (file:string) -> filterExts |> List.exists (fun ext -> Path.GetExtension(file) = ext) || filterExts = [])
        |> Seq.map (fun file -> getCodeBlock (Path.GetExtension file) file)
    use streamWriter = new StreamWriter(outputPath)
    for block in codeBlocks do
        let header = sprintf "### %s\n" block.File
        let codeBlock = sprintf "```%s\n%s\n```\n" block.Lang block.Content
        streamWriter.WriteLine(header)
        streamWriter.WriteLine(codeBlock)

let printHelp () =
    printfn "Help:\n-h: Help\n-o: Output path\n-e: File extension filter\n"


// Main entry point
[<EntryPoint>]
let main args =
    let parsedArgs = args |> Array.toList |> parseArgs 
    if parsedArgs.Help then
        printHelp ()
    else
        generateOutput parsedArgs.Files parsedArgs.FilterExts parsedArgs.OutputPath
    0
