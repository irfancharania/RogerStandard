// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#r @"C:\Users\irfan\Documents\GitHub\RogerStandard\packages\FSharpx.Collections.1.17.0\lib\net40\FSharpx.Collections.dll"


#load "Changelog.DomainPrimitiveTypes.fs"
#load "Changelog.Domain.fs"
#load "Global.fs"


open System
open FSharpx.Collections
open Changelog.DomainPrimitiveTypes
open Changelog.Domain


let a = VersionNumber.create(9);;
let b = VersionNumber.create(100);;
let c = VersionNumber.create(001);;
let d = VersionNumber.create(230);;
let e = VersionNumber.create(-2);;

let f = Result.lift3 ReleaseVersion.create a d c
