import React, { createContext, useContext, useState } from 'react';
import { AuthenticationActions, AzureAD } from 'react-aad-msal';
import { useDispatch } from 'react-redux';
import { authenticationReducer } from '../api/authentication/authentication.reducer';
import { authenticationProvider } from '../api/authentication/authenticationProvider';
import Routes from './routes';

type AppContextData = {
    onLogout(): void;
}


const AppContext = createContext<AppContextData>({} as AppContextData);

const AppContextProvider: React.FC = ({ children }) => {
    const dispatch = useDispatch();

    const handleLogout = () => {
        dispatch({ type: AuthenticationActions.LogoutSuccess, payload: null });
    }

    const handleAuthentication = (): JSX.Element => {
        return <Routes />
    }

    return <AppContext.Provider value={{ onLogout: handleLogout }}>
        <AzureAD provider={authenticationProvider} reduxStore={authenticationReducer} forceLogin={false} unauthenticatedFunction={handleAuthentication}>
            {children}
        </AzureAD>
    </AppContext.Provider>
}

const useAppContext = (): AppContextData => {
    const context = useContext(AppContext);

    return context;
}

export { AppContextProvider, useAppContext }