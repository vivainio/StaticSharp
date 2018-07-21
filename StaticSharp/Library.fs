namespace StaticSharp

open System.IO
open Giraffe

module Mat =
    open Giraffe.GiraffeViewEngine
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
