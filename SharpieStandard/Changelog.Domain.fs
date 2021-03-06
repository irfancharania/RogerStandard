module Changelog.Domain

open System
open Changelog.DomainPrimitiveTypes
open FSharpx.Collections


// ============================== 
// Domain models
// ============================== 

type WorkItemType = 
| Bug
| Feature
| Miscellaneous


type WorkItem = {
    Id: WorkItemId.T
    WorkItemType: WorkItemType
    Description: WorkItemDescription.T
}


type Release = {
    ReleaseId: ReleaseId 
    ReleaseVersion: Version
    ReleaseDate: ReleaseDate.T
    Authors: NonEmptyList<ReleaseAuthor.T>
    WorkItems: NonEmptyList<WorkItem>
    RecordVersion: RecordVersion
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
    | ReleaseIdIsRequired
    | ReleaseDateMustBeNewerThan2017
    | ReleaseDateMustBeEqualToOrOlderThanToday
    | ReleaseAuthorIsRequired
    | ReleaseAuthorUnknownError

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
    {Id = workItemId; WorkItemType = workItemType; Description = workItemDescription}


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
    match version.Revision > 0, version.Build > 0, version.Minor > 0 with
    | false, false, false -> sprintf "%d" version.Major
    | false, false, true -> sprintf "%d.%d" version.Major version.Minor
    | false, _, _ ->  sprintf "%d.%d.%d" version.Major version.Minor version.Build
    | _, _, _ -> sprintf "%d.%d.%d.%d" version.Major version.Minor version.Build version.Revision


let createReleaseDate date =
    let map = function
        | MustBeNewerThan _ -> ReleaseDateMustBeNewerThan2017
        | MustBeOlderThan _ -> ReleaseDateMustBeEqualToOrOlderThanToday

    ReleaseDate.create date
    |> mapErrorToList map


let createAuthors (authors:string[]) =
    if isNull authors || authors.Length = 0 then
        Error([ReleaseAuthorIsRequired])
    else
        let stringToError = function
            | StringError.Missing -> ReleaseAuthorIsRequired
            | _ -> ReleaseAuthorUnknownError

        authors
            |> Seq.map (ReleaseAuthor.create >> (mapErrorToList stringToError))
            |> Result.sequence
            |> Result.map NonEmptyList.ofList


let createReleaseId (releaseId:Guid) :Result<ReleaseId, DomainMessage list> =
    let result:Guid = 
        match releaseId = Guid.Empty with
        | true -> Guid.NewGuid()
        | false -> releaseId

    Ok(ReleaseId result)


let fromReleaseId (releaseId:ReleaseId) =
    let (ReleaseId result) = releaseId
    result


let createRecordVersion (recordVersion:byte[]) :Result<RecordVersion, DomainMessage list> =
    let result:byte[] = 
        match isNull recordVersion with
        | true -> [||]
        | false -> recordVersion

    Ok(RecordVersion result)


let fromRecordVersion (recordVersion:RecordVersion) =
    let (RecordVersion result) = recordVersion
    result


let createRelease id version releaseDate authors workItems recordVersion =
    {   ReleaseId = id;
        ReleaseVersion = version; 
        ReleaseDate = releaseDate; 
        Authors = authors; 
        WorkItems = workItems;
        RecordVersion = recordVersion;
    }
