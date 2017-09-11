module Changelog.Domain

open System
open Util.Types
open FSharpx.Collections


// TODO: constrain 10 to 100 characters
type Description = Description of string
// TODO: constrain 10 to 300 characters
type Author = Author of string


module VersionNumber = 
    type T = VersionNumber of int

    let create x = 
        match x >= 0, x <= 100 with
        | true, true -> Ok (VersionNumber x)
        | false, _ -> Error(MustBeGreaterThanOrEqualTo 0)
        | _, false -> Error(MustBeLessThanOrEqualTo 100)


type WorkItemType = 
| Bug
| Feature
| Miscellaneous


type WorkItem = {
    Type: WorkItemType
    Description: Description
}


module Version =
    type T = {
                Major: VersionNumber.T
                Minor: VersionNumber.T
                Revision: VersionNumber.T
            }
    type T with 
        member this.isLowerThan (x:T) =
            match this.Major < x.Major
                , this.Minor < x.Minor
                , this.Revision < x.Revision with
            | true, _, _ -> true
            | _, true, _ -> true
            | _, _, true -> true
            | _, _, _ -> false

        member this.isHigherThan (x:T) =
            match this.Major > x.Major
                , this.Minor > x.Minor
                , this.Revision > x.Revision with
            | true, _, _ -> true
            | _, true, _ -> true
            | _, _, true -> true
            | _, _, _ -> false
  

type Release = {
    Version: Version
    Date: Date.PastDate
    Authors: NonEmptyList<Author>
    WorkItems: NonEmptyList<WorkItem>
}
