import React from 'react';
import ExitToAppIcon from '@material-ui/icons/ExitToApp';
import AzureAD from 'react-aad-msal';
import IconButton from '@material-ui/core/IconButton';
import { authenticationProvider } from 'api/authentication/authenticationProvider';

export const LogoutButton: React.FC = () => {
    return <AzureAD provider={authenticationProvider} forceLogin={false}>
        {({ logout }) => <IconButton
            aria-label="account of current user"
            aria-controls="primary-search-account-menu"
            aria-haspopup="true"
            color="inherit"
            onClick={logout}>
            <ExitToAppIcon />
        </IconButton>}
    </AzureAD>
}