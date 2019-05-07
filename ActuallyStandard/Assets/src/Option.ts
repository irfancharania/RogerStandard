
export interface Option<T> {
    isEmpty(): boolean
    isDefined(): boolean
    map<U>(f: (v: T) => U): Option<U>
    bind<U>(f: (v: T) => Option<U>): Option<U>
    orElse(v: T): T
}

export function option<T>(x: T | null | undefined): Option<T> {
    if (x !== null && x !== undefined) {
        return new Some(x)
    }
    return new None()
}

export class Some<T> implements Option<T> {
    v: T

    constructor(x: T) {
        this.v = x
    }

    isEmpty() { return false }
    isDefined() { return true }
    map<U>(f: (v: T) => U): Option<U> {
        return option(f(this.v))
    }
    bind<U>(f: (v: T) => Option<U>): Option<U> {
        return f(this.v)
    }
    orElse() { return this.v }
}

export class None<T> implements Option<T> {
    isEmpty() { return true }
    isDefined() { return true }
    map<U>(f: (v: T) => U): Option<U> {
        return new None()
    }
    bind<U>(f: (v: T) => Option<U>): Option<U> {
        return new None()
    }
    orElse(x: T) { return x }
}
