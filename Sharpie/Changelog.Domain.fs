module Changelog.Domain

open System
open Changelog.DomainPrimitiveTypes


// ============================== 
// Domain models
// ============================== 

type WorkItemType = 
| Bug
| Feature
| Miscellaneous


type WorkItem = {
    Id: WorkItemId.T
    Type: WorkItemType
    Description: WorkItemDescription.T
}


type Release = {
    Version: Version
    ReleaseDate: ReleaseDate.T
    Authors: List<ReleaseAuthor.T>
    WorkItems: List<WorkItem>
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
    | WorkItemIsRequired
    | WorkItemIdMustBePositive
    | WorkItemIdUnknownError
    | WorkItemTypeInvalidValueProvided
    | WorkItemDescriptionIsRequired
    | WorkItemDescriptionMustNotBeShorterThan10Chars
    | WorkItemDescriptionMustNotBeLongerThan100Chars
    | ReleaseIsRequired
    | ReleaseDateMustBeNewerThan2017
    | ReleaseDateMustBeEqualToOrOlderThanToday
    | ReleaseAuthorIsRequired

// ============================== 
// Utility functions
// ============================== 

let mapErrorToList fn result =
    result |> Result.mapError (fun err -> [fn err])


let createWorkItemId workItemId =
    let map = function
        | IntegerError.Missing      -> WorkItemIsRequired
        | MustBePositiveInteger _   -> WorkItemIdMustBePositive
        | _                         -> WorkItemIdUnknownError
    
    WorkItemId.create workItemId
    |> mapErrorToList map


let createWorkItemDescription description =
    let map = function
        | StringError.Missing       -> WorkItemDescriptionIsRequired
        | MustNotBeLongerThan _     -> WorkItemDescriptionMustNotBeLongerThan100Chars
        | MustNotBeShorterThan _    -> WorkItemDescriptionMustNotBeShorterThan10Chars

    WorkItemDescription.create description
    |> mapErrorToList map


let createWorkItem workItemId workItemType workItemDescription =
    {Id = workItemId; Type = workItemType; Description = workItemDescription}


let createVersion version =
    if String.IsNullOrWhiteSpace version then
        Error([VersionNumberIsRequired])
    else
        let couldparse, parsed = Version.TryParse version
        if couldparse then
            Ok(parsed)
        else
            Error([VersionNumberUnableToParse])
  

let fromVersion (version:Version) =
    sprintf "%d.%d.%d.%d" version.Major version.Minor version.Build version.Revision


let createReleaseDate date =
    let map = function
        | MustBeNewerThan _ -> ReleaseDateMustBeNewerThan2017
        | MustBeOlderThan _ -> ReleaseDateMustBeEqualToOrOlderThanToday

    ReleaseDate.create date
    |> mapErrorToList map


let createAuthors (authors:string[]) =
    if authors.Length = 0 then
        Error([ReleaseAuthorIsRequired])
    else
        let result = authors
                    |> Seq.cast<ReleaseAuthor.T>
                    |> Seq.toList
        Ok(result)


let createWorkItems (workItems:List<WorkItem>) = 
    if workItems.Length = 0 then
        Error([WorkItemIsRequired])
    else
        let result = workItems
        Ok(result)


let createRelease version releaseDate authors workitems =
    {   Version = version; 
        ReleaseDate = releaseDate; 
        Authors = authors; 
        WorkItems = workitems
    }
