import * as React from 'react'
import Index from '../../components/changelog/Index'
import { Release } from '../../models'

async function fetchReleases(): Promise<Release[]> {
    const response = await fetch('/api/v1/changelog')
    const releases = await response.json()
    return releases
}

interface OwnState {
    releases: Release[]
}

export default class IndexContainer extends React.PureComponent<{}, OwnState> {
    componentWillMount() {
        this.setState({ releases: [] })

        fetchReleases().then((releases) => {
            this.setState((state) => ({ ...state, releases }))
        })
    }

    render() {
        return <Index {...this.state} />
    }
}
