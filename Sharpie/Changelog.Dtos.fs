module Changelog.Dtos


open System
open Changelog.Domain
open Changelog.DomainPrimitiveTypes

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


// ============================== 
// Release
// ============================== 
[<AllowNullLiteralAttribute>]
type ReleaseDto() = 
    member val Version: string = null with get, set
    member val ReleaseDate = DateTime.Now with get, set
    member val Authors : string[] = null with get, set
    member val WorkItems : WorkItemDto[]  = null with get, set





