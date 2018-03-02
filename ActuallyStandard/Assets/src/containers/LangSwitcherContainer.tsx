import * as React from 'react'
import LangSwitcher from '../components/LangSwitcher'
import { setLocale } from '../i18n'
import { Locale } from '../models'
import { option } from '../Option'

interface OwnState {
    currentLocale: Locale
}

export default class LangSwitcherContainer extends React.PureComponent<{}, OwnState> {
    constructor(props: {}) {
        // Retrieve current locale from form
        const currentLocale = option(document.querySelector('#selectLanguage select'))
            .bind((el) => option((el as HTMLSelectElement).value as Locale))
            .orElse('en-CA')

        super(props)
        this.state = { currentLocale }
    }

    setLocale(loc: Locale) {
        setLocale(loc)
        this.setState((state) => ({ ...state, currentLocale: loc }))
        option(
            document.getElementsByName('__RequestVerificationToken').item(0).getAttribute('value')
        ).map((tok) => {
            fetch('/home/setlanguage/?returnUrl=%2Fchangelog%2F', {
                credentials: 'same-origin',
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded;charset=UTF-8'
                },
                method: 'post',
                body: `culture=${loc}&__RequestVerificationToken=${encodeURIComponent(tok)}`
            }).then(() => window.location.reload())
        })
    }

    render() {
        return <LangSwitcher currentLocale={this.state.currentLocale} setLocale={this.setLocale.bind(this)} />
    }
}
