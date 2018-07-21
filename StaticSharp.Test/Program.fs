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
    [<Case>]
    static member Nav() =
        let navLink url text =
            a [_class Mat.Nav.Link; _href url] [
                EncodedText text
            ]

        let navPart =
            nav [_class Mat.Nav.Nav] [
                navLink "index.html" "portfolio"
                navLink "blog.html" "Blog"
                navLink "about.html" "About"
                navLink "contact.html" "Contact"
            ]
        // render separate nav for small and large screen

        div [] [
            div [_classes [Mat.Layout.LargeOnly; Mat.Layout.HeaderRow]] [navPart]
            div [_class Mat.Layout.SmallOnly] [navPart]

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
