import * as Polyglot from 'node-polyglot'
import { Locale } from './models'
import { option } from './Option'
import phrases from './phrases'

const loadedLoc = option(localStorage.getItem('locale') as Locale)
let locale: Locale = loadedLoc.orElse('en-CA')

const pgs = {
    [locale]: new Polyglot({ locale, phrases: phrases[locale] })
}

export function setLocale(loc: Locale) {
    localStorage.setItem('locale', loc)
    locale = loc
    pgs[locale] = new Polyglot({ locale, phrases: phrases[locale] })
}

export function t(...args: any[]): string {
    return pgs[locale].t.apply(pgs[locale], args)
}

// Init current locale
setLocale(locale)
