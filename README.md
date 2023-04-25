# CodePaper
CodePaper is a command-line interface (CLI) program written in F# that generates a single Markdown file containing code blocks for all files in a given directory.

## Usage

To use CodePaper, run the following command:

```ps1
CodePaper.exe [-e ext1] [-e ext2] [-e ext3] [-o output] [directory]
```

example
```ps1
.\CodePaper.exe -e ".kt" -e ".yaml"  -o "E:\output.md" "D:\Project\test\src\main"
```

The following options are available:

- `[directory]`: The path to the directory containing the files to be processed.
- `-e [ext]`: An optional file extensions to filter the files by.
- `-o [output]`: An optional output file name.
- `-h`: Display help information.

By default, CodePaper will generate an `output.md` file in the current directory

## Supported Languages

CodePaper supports the following programming languages:

- kotlin
- java
- csharp
- fsharp
- javascript
- typescript
- html
- css
- python
- bash
- powershell
- batch
- c
- cpp
- go
- rust
- php
- sql
- json
- xml
- yaml
- clojure
- lua
- ruby
- r
- scala
- swift
- vbnet
- coffeescript
- elm
