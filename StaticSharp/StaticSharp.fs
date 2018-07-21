namespace StaticSharp

open System.IO
open Giraffe
open Giraffe.GiraffeViewEngine
open System.Diagnostics

[<AutoOpen>]
module ViewExtensions =
    let _classes classes =
        classes
        |> String.concat " "
        |> _class


// material design helpers
module Mat =
    module ShahowC =
        let S4 = "mdl-shadow--4dp"

    module LayoutC =
        let Layout = "mdl-layout"
        let Header = "mdl-layout__header"
        let HeaderWaterfall = "mdl-layout__header--waterfall"
        let HeaderRow = "mdl-layout__header-row"
        let LargeOnly = "mdl-layout--large-screen-only"
        let SmallOnly = "mdl-layout--small-screen-only"

        let Content = "mdl-layout__content"


    module NavC =
        let Nav = "mdl-navigation"
        let Link = "mdl-navigation__link"

    module GridC =
        let Grig = "mdl-grid"
        let Cell = "mdl-cell"

    module CardC =
        let Card = "mdl-card"
        let Media = "mdl-card__media"
        let Title = "mdl-card__title"
        let TitleText = "mdl-card__title-text"
        let SupText = "mdl-card__supporting-text"
        let Actions = "mdl-card__actions"
        let Border = "mdl-card--border"

    module Card =
        let WithImage title imgSrc supText =
            div [_classes [GridC.Cell; CardC.Card; ShahowC.S4 ]] [
                div [_class CardC.Media] [
                    img [_src imgSrc; _border "0"; _alt "" ]
                ]
                div [_class CardC.Title] [
                    h2 [_class CardC.TitleText] [EncodedText title]
                ]
                div [_class CardC.SupText] [EncodedText supText]
            ]

    module ButtonC =
        let Fancy = "mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect mdl-button--accent"

    module Button =
        let Link href text = a [_href href; _class ButtonC.Fancy] [ EncodedText text]


    module Boilerplates =
        let BasicMeta =
            [
                meta [_charset "UTF-8"]
                meta [_httpEquiv "X-UA-Compatible"; _content "IE=edge"]
                meta [ _name "viewport"; _content "width=device-width, initial-scale=1.0, minimum-scale=1.0"]
            ]
        let Roboto =
            link [_rel "stylesheet"; _href "https://fonts.googleapis.com/css?family=Roboto:regular,bold,italic,thin,light,bolditalic,black,medium&amp;lang=en"]
        let MaterialCss =
            link [_rel "stylesheet"; _href "https://code.getmdl.io/1.3.0/material.grey-pink.min.css"]


module Renderer =

    let filterProcess pname =
        let psi = ProcessStartInfo(pname)
        psi.RedirectStandardInput <- true
        psi.RedirectStandardOutput <- true
        psi.CreateNoWindow <- true
        psi.UseShellExecute <- false
        let p = Process.Start psi
        p.StandardInput, p.StandardOutput




    let WriteDoc fname nodes =
        let cont =
            nodes
            |> GiraffeViewEngine.renderHtmlDocument
        printfn "%s\n%s" fname cont
        File.WriteAllText(fname,cont)
    let Print title node =

        let html =
            node
            |> GiraffeViewEngine.renderHtmlNode

        let input, output = filterProcess "formathtml.exe"
        input.Write(html)
        input.Close()
        let cont = output.ReadToEnd()
        printfn "** %s **" title
        printfn "%s" cont
