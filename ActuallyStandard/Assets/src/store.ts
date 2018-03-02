import { combineReducers, createStore, Reducer } from 'redux'

import * as alerts from './reducers/alerts'

export interface AppState {
    alerts: alerts.State
}

export type AppAction = alerts.Action

export type DispatchFn = (action: AppAction) => void

export function blankState(): AppState {
    return {
        alerts: alerts.blankState()
    }
}

export const store = createStore(
    combineReducers<AppState>({
        alerts: (state, action) => alerts.reducer(state as alerts.State, action as AppAction)
    }),
    blankState()
)
