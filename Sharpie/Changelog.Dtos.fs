module Changelog.Dtos


open System
open Changelog.Domain
open Changelog.DomainPrimitiveTypes
open FSharpx.Collections

// ============================== 
// Bindings
// ============================== 

let (<!>) = Result.map
let (<*>) = Result.apply


// ============================== 
// WorkItemType
// ============================== 
type WorkItemTypeDto = 
    | Bug = 1
    | Feature = 2
    | Miscellaneous = 3


// Convertors
module WorkItemTypeDto =
    let toDomain (dto:WorkItemTypeDto) :Result<WorkItemType, _> =
        match dto with
        | WorkItemTypeDto.Bug -> Ok (WorkItemType.Bug)
        | WorkItemTypeDto.Feature -> Ok (WorkItemType.Feature)
        | WorkItemTypeDto.Miscellaneous -> Ok (WorkItemType.Miscellaneous)
        | _ -> Error ([WorkItemTypeInvalidValueProvided])


    let fromDomain (workItemType:WorkItemType) :WorkItemTypeDto =
        match workItemType with
        | WorkItemType.Bug -> WorkItemTypeDto.Bug
        | WorkItemType.Feature -> WorkItemTypeDto.Feature
        | WorkItemType.Miscellaneous -> WorkItemTypeDto.Miscellaneous


// ============================== 
// WorkItem
// ============================== 
[<AllowNullLiteralAttribute>]
type WorkItemDto() = 
    member val Id = 0 with get, set
    member val Type = 0 with get, set
    member val Description : string = null with get, set


// Convertors
module WorkItemDto =
    let toDomain (dto:WorkItemDto) :Result<WorkItem, _> =
        if dto = null then 
            Error([WorkItemIsRequired])
        else
            // Get each validated component
            let workItemIdOrError = createWorkItemId dto.Id
            let workItemTypeOrError = WorkItemTypeDto.toDomain (enum<WorkItemTypeDto> dto.Type)
            let workItemDescriptionOrError = createWorkItemDescription dto.Description

            // Combine the components
            createWorkItem
            <!> workItemIdOrError
            <*> workItemTypeOrError
            <*> workItemDescriptionOrError

    
    let fromDomain (workItem:WorkItem) :WorkItemDto =
        let item = WorkItemDto()
        item.Id <- workItem.Id |> WorkItemId.apply id
        item.Type <- workItem.Type |> WorkItemTypeDto.fromDomain |> int
        item.Description <- workItem.Description |> WorkItemDescription.apply id

        item


let createWorkItems (workItems:WorkItemDto[]) = 
    if workItems = null || workItems.Length = 0 then
        Error([WorkItemIsRequired])
    else
        workItems
        |> Seq.map WorkItemDto.toDomain
        |> Result.sequence
        |> Result.map NonEmptyList.ofList
         

// ============================== 
// Release
// ============================== 
[<AllowNullLiteralAttribute>]
type ReleaseDto() = 
    member val Version: string = null with get, set
    member val ReleaseDate = DateTime.Now with get, set
    member val Authors : string[] = null with get, set
    member val WorkItems : WorkItemDto[]  = null with get, set
    member val RecordVersion : byte[] = null with get, set


// Convertors
module ReleaseDto =
    let toDomain (dto:ReleaseDto) :Result<Release, _> =
        if dto = null then 
            Error([ReleaseIsRequired])
        else
            // Get each validated component
            let releaseVersionOrError = createVersion dto.Version
            let releaseDateOrError = createReleaseDate dto.ReleaseDate
            let releaseAuthorsOrError = createAuthors dto.Authors
            let releaseWorkItems = createWorkItems dto.WorkItems
            let releaseRecordVersion = createRecordVersion dto.RecordVersion

            // Combine the components
            createRelease
            <!> releaseVersionOrError
            <*> releaseDateOrError
            <*> releaseAuthorsOrError
            <*> releaseWorkItems
            <*> releaseRecordVersion

    
    let fromDomain (release:Release) :ReleaseDto =
        let item = ReleaseDto()

        item.Version    <- release.Version |> fromVersion
        item.ReleaseDate <- release.ReleaseDate |> ReleaseDate.apply id
        item.Authors    <- release.Authors
                            |> NonEmptyList.map (ReleaseAuthor.apply id)
                            |> NonEmptyList.toArray
        item.WorkItems  <- release.WorkItems 
                            |> NonEmptyList.map WorkItemDto.fromDomain
                            |> NonEmptyList.toArray
        item.RecordVersion <- release.RecordVersion |> fromRecordVersion

        item

