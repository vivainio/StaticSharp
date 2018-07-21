namespace StaticSharp

open System.IO
open Giraffe

open Giraffe.GiraffeViewEngine



[<AutoOpen>]
module ViewExtensions =
    let _classes classes =
        classes
        |> String.concat " "
        |> _class



module Mat =


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
    module ButtonC =
        let Fancy = "mdl-button mdl-button--colored mdl-js-button mdl-js-ripple-effect mdl-button--accent"

    module Button =
        let Link href text = a [_href href; _class ButtonC.Fancy] [ EncodedText text]

    module Card =

    let Head (extraEnts: XmlNode list) =
        [
            meta [_charset "UTF-8"]

            meta [_httpEquiv "X-UA-Compatible"; _content "IE=edge"]
            meta [ _name "viewport"; _content "width=device-width, initial-scale=1.0, minimum-scale=1.0"]
            link [_rel "stylesheet"; _href "https://fonts.googleapis.com/css?family=Roboto:regular,bold,italic,thin,light,bolditalic,black,medium&amp;lang=en"]
            link [_rel "stylesheet"; _href "https://code.getmdl.io/1.3.0/material.grey-pink.min.css"]
            link [_rel "stylesheet"; _href "styles.css"]
        ]
        |> List.append extraEnts
        |> head []


module Renderer =
    let WriteDoc fname nodes =
        let cont =
            nodes
            |> GiraffeViewEngine.renderHtmlDocument
        printfn "%s\n%s" fname cont
        File.WriteAllText(fname,cont)
    let Print node =
        node
        |> GiraffeViewEngine.renderHtmlNode
        |> printfn "%s"
