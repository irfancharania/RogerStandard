namespace Changelog.Test

open Xunit
open Swensen.Unquote.Assertions

open System
open Changelog.DomainPrimitiveTypes
open Changelog.Domain
open Changelog.Dtos


module WorkItem =
    [<Fact>]
    let ``Convert valid WorkItemDto to Domain``() =    
        let dto = WorkItemDto()
        dto.Id <- 1
        dto.Type <- 1
        dto.Description <- "Convert valid WorkItemDto to Domain"

        let sut = WorkItemDto.toDomain dto


        let expected = {
            Id = WorkItemId.T.WorkItemId 1;
            Type = Bug;
            Description = WorkItemDescription.T.WorkItemDescription "Convert valid WorkItemDto to Domain";}
        
        let (expectedResult:Result<WorkItem, DomainMessage list>) = Ok expected
           

        test<@ sut = expectedResult @>


    [<Fact>]
    let ``Convert invalid WorkItemDto to Domain``() =    
        let sut = WorkItemDto()
        sut.Id <- 0
        sut.Type <- 5
        sut.Description <- "hi"

        let numErrors item = 
            match (WorkItemDto.toDomain item) with
            | Ok _ -> 0
            | Error x -> x.Length

        test<@ numErrors sut = 3 @>


    [<Fact>]
    let ``Convert invalid ReleaseDto to Domain``() =    
        let sut = ReleaseDto()
        sut.ReleaseDate <- DateTime.UtcNow.AddDays(-10.0)
        sut.Version <- "1.1.1"
        
        Console.WriteLine(sut)

        let numErrors item = 
            match (ReleaseDto.toDomain item) with
            | Ok _ -> 0
            | Error x -> x.Length

        test<@ numErrors sut = 2 @>

