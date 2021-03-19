import React, { Suspense } from 'react';
import { BrowserRouter as Router, HashRouter } from 'react-router-dom';
import { AuthenticationState, AzureAD } from 'react-aad-msal';
import { authenticationProvider } from '../../api/authentication/authenticationProvider';
import PublicRoutes from './routes.public';
import PrivateRoutes from './routes.private';

export const Routes: React.FC = () => {
    return <Router>

        <Suspense fallback={<div />}>
            <AzureAD provider={authenticationProvider} forceLogin={false}>
                {({ authenticationState }) => {
                    switch (authenticationState) {
                        case AuthenticationState.Authenticated:
                            return (
                                <PrivateRoutes />
                            );
                        default:
                            return <PublicRoutes />;
                    }
                }}
            </AzureAD>
        </Suspense>
    </Router>
}