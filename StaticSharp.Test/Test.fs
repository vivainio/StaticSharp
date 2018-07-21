open TrivialTestRunner
open StaticSharp
open Giraffe.GiraffeViewEngine

type MaterialTest() =
    [<Case>]
    static member Header() =

        Mat.Boilerplates.MaterialCss ::
        Mat.Boilerplates.Roboto ::
        Mat.Boilerplates.BasicMeta
        |> head []
        |> Renderer.Print "Header"
    [<Case>]
    static member Nav() =
        let navLink url text =
            a [_class Mat.NavC.Link; _href url] [
                EncodedText text
            ]

        let navPart =
            nav [_class Mat.NavC.Nav] [
                navLink "index.html" "portfolio"
                navLink "blog.html" "Blog"
                navLink "about.html" "About"
                navLink "contact.html" "Contact"
            ]
        // render separate nav for small and large screen

        div [] [
            div [_classes [Mat.LayoutC.LargeOnly; Mat.LayoutC.HeaderRow]] [navPart]
            div [_class Mat.LayoutC.SmallOnly] [navPart]

        ]
        |> Renderer.Print "Navigation"

    [<Case>]
    static member Buttons() =
        div [] [
            Mat.Button.Link "nolink" "sometext"
        ]
        |> Renderer.Print "Buttons"


[<EntryPoint>]
let main argv =
    TRunner.AddTests<MaterialTest>()

    TRunner.RunTests()
    TRunner.ExitStatus
