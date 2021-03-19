import React from 'react';
import { Provider } from "react-redux";
import { authenticationReducer } from '../api/authentication/authentication.reducer';
import { AppContextProvider } from './app.context';


const AppProviders: React.FC = ({ children }) => {
    return <Provider store={authenticationReducer} >
        <AppContextProvider>{children}</AppContextProvider>
    </Provider>
}

export { AppProviders as default }