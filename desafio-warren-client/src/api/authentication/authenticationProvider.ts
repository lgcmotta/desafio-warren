import { Configuration, AuthenticationParameters } from 'msal';
import {
    MsalAuthProvider,
    LoginType,
    IMsalAuthProviderConfig,
} from 'react-aad-msal';

const config: Configuration = {
    auth: {
        authority: process.env.REACT_APP_AUTHORITY,
        clientId: process.env.REACT_APP_CLIENT_ID ?? '',
        postLogoutRedirectUri: window.location.origin,
        redirectUri: window.location.origin,
        validateAuthority: true,
        navigateToLoginRequestUrl: true,
    },
    cache: {
        cacheLocation: 'sessionStorage',
        storeAuthStateInCookie: true,
    },
};

const authenticationParameters: AuthenticationParameters = {
    scopes: [process.env.REACT_APP_SCOPES ?? ''],
};

const options: IMsalAuthProviderConfig = {
    loginType: LoginType.Redirect,
    tokenRefreshUri: `${window.location.origin}/`,
};

export const authenticationProvider = new MsalAuthProvider(
    config,
    authenticationParameters,
    options,
);
