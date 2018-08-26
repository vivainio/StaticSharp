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

    // add new attributes to root

    let addAttrs (attr: XmlAttribute[]) (node: XmlNode) =
        match node with
        | ParentNode((name, oldattrs), children) ->
            let newEl = name,Array.concat [oldattrs; attr]
            ParentNode(newEl, children)
        | VoidElement(name, oldattrs) ->
            let newEl = name,Array.concat [oldattrs; attr]
            VoidElement(newEl)
        | _ ->
            failwithf "Can't add attribute to node %A" node


    // operator to add attributes to node
    let inline (++) (node: XmlNode) (attr: XmlAttribute) =
        addAttrs [|attr|] node


// material design helpers
module Mdl =
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
        let Title = "mdl-layout__title"


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
            let attrs = [_classes [GridC.Cell; CardC.Card; ShahowC.S4 ]]
            div attrs  [
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
        let Link href text =
            a [_href href; _class ButtonC.Fancy] [ EncodedText text]


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
        let MaterialIcons =
            link [_rel "stylesheet"; _href "https://fonts.googleapis.com/icon?family=Material+Icons"]



module C =
    type Css = Css of string
    module Util =
        let Pretty (parts: Css seq) =
            parts
            |> Seq.map (fun (Css s) -> sprintf "  %s;" s)
            |> String.concat "\n"
            |> fun s -> s+"\n"

    let propFn (key: string) (value:string) = sprintf "%s: %s" key value |> Css


    let Color = propFn "color"
    let Of frags =
        (frags: Css seq)
        |> Seq.map (fun (Css s) -> s)
        |> String.concat "; "
        |> _style

    module Flex =
        let Flex = Css "display: flex"
        module Wrap =
            let Wrap = Css "flex-wrap: wrap"
            let No = Css "flex-wrap: nowrap"
        module Align =
            let SpaceBetween = Css "align-content: space-between"
            let Start = Css "align-content: flex-start"
            let End = Css "align-content: flex-start"
    module Text =

        module Align =
            let Center = Css "text-align: center"
            let Left = Css "text-align: left"
            let Right = Css "text-align: right"


    let Width = propFn "width"
    let Height = propFn "height"


    let MarginTRBL = propFn "margin"

    module Font =
        let Size = propFn "font-size"
        let Bold = Css "font-weight: bold"
        let LineHeight = propFn "line-height"
        let Weight = propFn "font-weight"


module StyleDefs =
    type Bem = Bem of string
    type ClassName = ClassName of string

    let (?) (Bem(block)) (el:string) =
        sprintf "%s--%s" block el |> ClassName

    type Rule = {selector: string; body: string }
    type RuleSet = Rule[]

    type Collector() =
        let rules = ResizeArray<Rule>()
        let mutable locked = false

        let write rule =
            if locked then failwith "Attempt to modify written css collector"
            rules.Add rule

        member x.Class (klass: ClassName) (block: C.Css seq) =
            let (ClassName kl) = klass
            write { selector = "." + kl; body = C.Util.Pretty block }
            kl
        member x.AsText() =
            locked <- true
            rules
            |> Seq.map (fun r -> sprintf "%s {\n%s}\n" r.selector r.body)
            |> String.concat "\n"



module Renderer =
    let internal filterProcess pname =
        let psi = ProcessStartInfo(pname)
        psi.RedirectStandardInput <- true
        psi.RedirectStandardOutput <- true
        psi.CreateNoWindow <- true
        psi.UseShellExecute <- false
        let p = Process.Start psi
        p.StandardInput, p.StandardOutput
    let FormatHtml (html: string) =
        let input, output = filterProcess "formathtml.exe"
        input.Write(html)
        input.Close()
        output.ReadToEnd()

    let WriteDoc fname nodes =
        let cont =
            nodes
            |> GiraffeViewEngine.renderHtmlDocument

        let formatted = FormatHtml cont
        File.WriteAllText(fname,formatted)

    let PrettyPrint title node =

        let html =
            node
            |> GiraffeViewEngine.renderHtmlNode

        let input, output = filterProcess "formathtml.exe"
        input.Write(html)
        input.Close()
        let cont = output.ReadToEnd()
        printfn "** %s **" title
        printfn "%s" cont
