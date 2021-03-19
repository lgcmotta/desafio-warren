import { createStore } from 'redux';
import { AuthenticationActions, AuthenticationState } from 'react-aad-msal';

import { AuthReducerState } from '../models/auth-state';

const initialState: AuthReducerState = {
    initializing: false,
    initialized: false,
    idToken: null,
    accessToken: null,
    state: AuthenticationState.Unauthenticated,
};

const rootReducer = (state = initialState, action: any): AuthReducerState => {
    switch (action.type) {
        case AuthenticationActions.Initializing:
            return {
                ...state,
                initializing: true,
                initialized: false,
            };
        case AuthenticationActions.Initialized:
            return {
                ...state,
                initializing: false,
                initialized: true,
            };
        case AuthenticationActions.AcquiredIdTokenSuccess:
            return {
                ...state,
                idToken: action.payload,
            };
        case AuthenticationActions.AcquiredAccessTokenSuccess:
            return {
                ...state,
                accessToken: action.payload,
            };
        case AuthenticationActions.AcquiredAccessTokenError:
            return {
                ...state,
                accessToken: null,
            };
        case AuthenticationActions.LoginSuccess:
            return {
                ...state,
                account: action.payload.account,
            };
        case AuthenticationActions.LoginError:
        case AuthenticationActions.AcquiredIdTokenError:
        case AuthenticationActions.LogoutSuccess:
            return { ...state, idToken: null, accessToken: null, account: null };
        case AuthenticationActions.AuthenticatedStateChanged:
            return {
                ...state,
                state: action.payload,
            };
        default:
            return state;
    }
};

export const authenticationReducer = createStore(rootReducer);
