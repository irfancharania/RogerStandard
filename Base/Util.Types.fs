module Util

open System


/// http://www.taimila.com/blog/fsharp-pure-time-dependent-domain/
type PastDate = PastDate of DateTime
type CurrentDate = CurrentDate of DateTime
type FutureDate = FutureDate of DateTime

type Date =
  | Past of PastDate
  | Current of CurrentDate
  | Future of FutureDate

let create (dt: DateTime) =
  match dt with
  | dt when dt.Date = DateTime.Today -> Current (CurrentDate(dt))
  | dt when dt.Date < DateTime.Today -> Past (PastDate(dt))
  | _ -> Future (FutureDate(dt))

let isToday = function
  | CurrentDate _ -> true
  | _ -> false

let isInFuture = function
  | FutureDate _ -> true
  | _ -> false

let isInPast = function
  | PastDate _ -> true
  | _ -> false  
