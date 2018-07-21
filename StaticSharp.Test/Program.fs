// Learn more about F# at http://fsharp.org

open System
open TrivialTestRunner
open StaticSharp
open Giraffe.GiraffeViewEngine

type MaterialTest() =
    [<Case>]
    static member Header() =
        Mat.Head [
            meta [_name "description"; _content "My test case"]
        ]
        |> Renderer.Print

type BasicTests() =
    [<Case>]
    static member Generate() =
        let c = div [] []
        Renderer.WriteDoc "testout.txt" c
        ()


[<EntryPoint>]
let main argv =
    TRunner.AddTests<MaterialTest>()
    TRunner.AddTests<BasicTests>()

    TRunner.RunTests()
    TRunner.ExitStatus
