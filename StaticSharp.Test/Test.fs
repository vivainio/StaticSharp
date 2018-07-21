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
        |> Renderer.PrettyPrint "Header"
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
        |> Renderer.PrettyPrint "Navigation"

    [<Case>]
    static member Buttons() =
        div [] [
            Mat.Button.Link "nolink" "sometext" ++ _id "linkbutton"
        ]
        |> Renderer.PrettyPrint "Buttons"
    [<Case>]
    static member Cards() =
        Mat.Card.WithImage "Card title" "mypic.jpg" "Some supplementary text"
         ++ _id "mycard"
         ++ _class "extraclass"
        |> Renderer.PrettyPrint "Cards"
[<EntryPoint>]
let main argv =
    TRunner.AddTests<MaterialTest>()

    TRunner.RunTests()
    TRunner.ExitStatus
