import * as React from 'react'
import {Release} from '../../models'
import Index from '../../components/changelog/Index'

async function fetchReleases(): Promise<Release[]> {
    const response = await fetch("/api/changelog")
    const releases = await response.json()
    return releases
}

interface OwnState {
    releases: Release[]
}

export class IndexContainer extends React.PureComponent<{}, OwnState> {
    componentWillMount() {
        this.setState({releases: []})

        fetchReleases().then(releases => {
            this.setState((state) => ({ ...state, releases }))
        })
    }

    render() {
        return <Index {...this.state} />
    }
}