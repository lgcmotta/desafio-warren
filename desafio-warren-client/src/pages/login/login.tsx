import { Button, Typography } from '@material-ui/core';
import { authenticationProvider } from 'api/authentication/authenticationProvider';
import React from 'react';
import AzureAD from 'react-aad-msal';
import { BackgroundDiv, LoginButtonDiv, LoginDiv,LoginTitleDiv } from './login.style';

export const Login: React.FC = () => {
    const imageUrl = `${process.env.PUBLIC_URL}/bg.svg`;

    return <BackgroundDiv style={{ backgroundImage: `url(${imageUrl})` }}>

        <LoginDiv>
            <LoginTitleDiv>
                <Typography variant="h1" style={{ fontFamily: 'Roboto', fontWeight: 100 }}>
                    Motta Bank
                </Typography>
            </LoginTitleDiv>
            <LoginButtonDiv>
                <Typography variant="h3" style={{ fontFamily: 'Roboto', fontWeight: 200 }}>
                    Welcome!
                </Typography>
                <AzureAD provider={authenticationProvider} forceLogin={false}>
                    {({ login }) => <Button variant="contained" color="primary" fullWidth onClick={login}>
                        Login
                    </Button>}
                </AzureAD>
            </LoginButtonDiv>
        </LoginDiv>
    </BackgroundDiv>
}
