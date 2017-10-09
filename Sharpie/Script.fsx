// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#r @"C:\Users\irfan\Documents\GitHub\RogerStandard\packages\FSharpx.Collections.1.17.0\lib\net40\FSharpx.Collections.dll"


#load "Global.fs"
#load "Changelog.DomainPrimitiveTypes.fs"
#load "Changelog.Domain.fs"
#load "Changelog.Dtos.fs"

open System
open FSharpx.Collections
open Changelog.DomainPrimitiveTypes
open Changelog.Domain
open Changelog.Dtos

//let a = createWorkItemDescription "hi uihiul hljkk";;
//let u = createVersion String.Empty;;
//let v = createVersion "hi.0";;
//let w = createVersion "1";;
//let x = createVersion "-234";;
//let y = createVersion "2.0.0";;
//let z = createVersion "22222222.8888888.23";;

//let h = createReleaseDate DateTime.Now;;
//let i = createReleaseDate DateTime.UtcNow;;

let j = WorkItemDto()
j.Id <- 0
j.Type <- 2
j.Description <- "hi"

let k = WorkItemDtoToDomain j
