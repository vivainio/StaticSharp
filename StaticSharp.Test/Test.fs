﻿open TrivialTestRunner
open StaticSharp
open Giraffe.GiraffeViewEngine
open StaticSharp.C

type MaterialLiteTest() =
    [<Case>]
    static member Header() =

        Mdl.Boilerplates.MaterialCss ::
        Mdl.Boilerplates.Roboto ::
        Mdl.Boilerplates.BasicMeta
        |> head []
        |> Renderer.PrettyPrint "Header"
    [<Case>]
    static member Nav() =
        let navLink url text =
            a [_class Mdl.NavC.Link; _href url] [
                EncodedText text
            ]

        let navPart =
            nav [_class Mdl.NavC.Nav] [
                navLink "index.html" "portfolio"
                navLink "blog.html" "Blog"
                navLink "about.html" "About"
                navLink "contact.html" "Contact"
            ]
        // render separate nav for small and large screen

        div [] [
            div [_classes [Mdl.LayoutC.LargeOnly; Mdl.LayoutC.HeaderRow]] [navPart]
            div [_class Mdl.LayoutC.SmallOnly] [navPart]

        ]
        |> Renderer.PrettyPrint "Navigation"

    [<Case>]
    static member Buttons() =
        div [] [
            Mdl.Button.Link "nolink" "sometext" ++ _id "linkbutton"
        ]
        |> Renderer.PrettyPrint "Buttons"
    [<Case>]
    static member Cards() =
        Mdl.Card.WithImage "Card title" "mypic.jpg" "Some supplementary text"
         ++ _id "mycard"
         ++ _class "extraclass"
        |> Renderer.PrettyPrint "Cards"
    [<Case>]
    static member FullPortfolio() =
        let headerElement =
            Mdl.Boilerplates.MaterialCss ::
            Mdl.Boilerplates.Roboto ::
            Mdl.Boilerplates.MaterialIcons ::
            Mdl.Boilerplates.BasicMeta
            |>
            head []

        Renderer.PrettyPrint "Head element" headerElement
        let pageHeader =

            header [_classes [Mdl.LayoutC.Header; Mdl.LayoutC.HeaderWaterfall; "portfolio-header"]] [
                div [_classes [ Mdl.LayoutC.HeaderRow; "portfolio-logo-row"]] [
                    span [_class Mdl.LayoutC.Title ] []
                    div [_class "portfolio-logo"] []
                    span [_class Mdl.LayoutC.Title ] [EncodedText "Simple portfolio website"]
                ]

                div [
                    _classes [
                        Mdl.LayoutC.HeaderRow
                        "portfolio-navigation-row"
                        Mdl.LayoutC.LargeOnly
                    ]] [

                ]

            ]
        ()
    [<Case>]
    static member AddClass() =
        // phase 2 merge classes
        let c = div [_class "hello"] []
        let b = c ++ _class "world"
        Renderer.PrettyPrint "two classes" b

        printfn "%A" b
        ()
    [<Case>]
    static member InlineStyles() =
        let plain = div [][]
        let styled =
            plain
            ++ _style "display:flex"

        let styled2 =
            div [] []
            ++ C.Of [C.Flex.Align.SpaceBetween]

        Renderer.PrettyPrint "inline style" styled2

        ()



[<EntryPoint>]
let main argv =
    TRunner.AddTests<MaterialLiteTest>()

    TRunner.RunTests()
    TRunner.ReportAll()
    TRunner.ExitStatus
