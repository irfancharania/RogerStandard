import * as React from 'react'
import { ChangeEvent } from 'react'
import { Locale } from '../models'

interface OwnProps {
    setLocale: (loc: Locale) => void
    currentLocale: Locale
}

export default class LangSwitcher extends React.PureComponent<OwnProps, {}> {
    render() {
        const onChange = (e: ChangeEvent<HTMLSelectElement>) => {
            this.props.setLocale(e.currentTarget.value as Locale)
        }
        return <div>
            <select value={this.props.currentLocale} onChange={onChange}>
                <option value="en-CA">en-CA</option>
                <option value="fr-CA">fr-CA</option>
            </select>
        </div>
    }
}
