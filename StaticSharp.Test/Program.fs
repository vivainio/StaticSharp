// Learn more about F# at http://fsharp.org

open System
open TrivialTestRunner
open StaticSharp
open Giraffe.GiraffeViewEngine

type BasicTests() =
    [<Case>]
    static member Generate() =
        let c = div [] []
        renderHtmlDocument c
        |> printfn "%s"
        ()


[<EntryPoint>]
let main argv =
    TRunner.AddTests<BasicTests>()
    TRunner.RunTests()
    TRunner.ExitStatus
