module Changelog.Domain

open System
open FSharpx.Collections
open Changelog.DomainPrimitiveTypes


// ============================== 
// Domain models
// ============================== 

type WorkItemType = 
| Bug
| Feature
| Miscellaneous


type WorkItem = {
    Type: WorkItemType
    Description: WorkItemDescription.T
}


type Release = {
    Version: Version
    Date: ReleaseDate.T
    Authors: NonEmptyList<ReleaseAuthor.T>
    WorkItems: NonEmptyList<WorkItem>
}


// All possible things that can happen in the use-cases
type DomainMessage =

    // Validation errors
    | VersionNumberUnableToParse
    | VersionNumberIsRequired
    | VersionNumberHasFewerThanTwoOrMoreThanFour
    | VersionNumberIncorrectFormat
    | VersionNumberLessThanZero
    | VersionNumberGreaterThanMaximum
    | WorkItemDescriptionIsRequired
    | WorkItemDescriptionMustNotBeShorterThan10Chars
    | WorkItemDescriptionMustNotBeLongerThan100Chars
    | ReleaseDateMustBeNewerThan2017
    | ReleaseDateMustBeEqualToOrOlderThanToday

// ============================== 
// Utility functions
// ============================== 

let mapErrorToList fn result =
    result |> Result.mapError (fun err -> [fn err])


let createWorkItemDescription description =
    let map = function
        | StringError.Missing       -> WorkItemDescriptionIsRequired
        | MustNotBeLongerThan _     -> WorkItemDescriptionMustNotBeLongerThan100Chars
        | MustNotBeShorterThan _    -> WorkItemDescriptionMustNotBeShorterThan10Chars

    WorkItemDescription.create description
    |> mapErrorToList map


let createVersion version =
    let couldparse, parsed = Version.TryParse version
    if couldparse then
        Ok (parsed)
    else
        Error([VersionNumberUnableToParse])
  

let createReleaseDate date =
    let map = function
        | MustBeNewerThan _ -> ReleaseDateMustBeNewerThan2017
        | MustBeOlderThan _ -> ReleaseDateMustBeEqualToOrOlderThanToday

    ReleaseDate.create date
    |> mapErrorToList map


