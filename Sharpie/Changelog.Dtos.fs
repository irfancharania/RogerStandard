module Changelog.Dtos


open System
open Changelog.Domain

// ============================== 
// Bindings
// ============================== 

let (<!>) = Result.map
let (<*>) = Result.apply

// ============================== 
// Enums
// ============================== 
type WorkItemTypeEnum = 
    | Bug = 1
    | Feature = 2
    | Miscellaneous = 3


// ============================== 
// DTOs
// ============================== 

[<AllowNullLiteralAttribute>]
type WorkItemDto() = 
    member val Id = 0 with get, set
    member val Type = 0 with get, set
    member val Description : string = null with get, set


[<AllowNullLiteralAttribute>]
type ReleaseDto() = 
    member val Version: string = null with get, set
    member val ReleaseDate = DateTime.Now with get, set
    member val Authors : string[] = null with get, set
    member val WorkItems : WorkItemDto[]  = null with get, set


// ============================== 
// DTO Converters
// ============================== 
let WorkItemTypeDtoToDomain dto :Result<WorkItemType, _> =
    match dto with
    | WorkItemTypeEnum.Bug -> Ok (WorkItemType.Bug)
    | WorkItemTypeEnum.Miscellaneous -> Ok (WorkItemType.Miscellaneous)
    | WorkItemTypeEnum.Feature -> Ok (WorkItemType.Feature)
    | _ -> Error ([WorkItemTypeInvalidValueProvided])


let WorkItemDtoToDomain (dto:WorkItemDto) =
    if dto = null then 
        Error([WorkItemIsRequired])
    else
        // Get each validated component
        let workItemIdOrError = createWorkItemId dto.Id
        let workItemTypeOrError = WorkItemTypeDtoToDomain (enum<WorkItemTypeEnum> dto.Type)
        let workItemDescriptionOrError = createWorkItemDescription dto.Description

        // Combine the components
        createWorkItem
        <!> workItemIdOrError
        <*> workItemTypeOrError
        <*> workItemDescriptionOrError
