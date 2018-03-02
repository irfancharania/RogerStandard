import * as im from 'immutable'
import { AppAction } from '../store'

type MessageId = string

export interface Message {
    id: MessageId
    title: string
    body: string
    timeout: number
    type: 'success' | 'info' | 'warning' | 'danger'
}

export interface State {
    messages: im.Map<MessageId, Message>
}

export function blankState(): State {
    return {
        messages: im.Map()
    }
}

interface AddAlertAction {
    type: 'alerts/add'
    message: Message
}

interface RemoveAlertAction {
    type: 'alerts/remove'
    messageId: string
}

export type Action = AddAlertAction | RemoveAlertAction

export function reducer(state: State, action: AppAction): State {
    if (state === null || state === undefined) {
        state = blankState()
    }

    switch (action.type) {
        case 'alerts/add':
            const messageId = Math.random().toString()
            return {
                ...state,
                messages: state.messages.set(messageId, { ...action.message, id: messageId })
            }
        case 'alerts/remove':
            return {
                ...state,
                messages: state.messages.remove(action.messageId)
            }
    }

    return state
}
