import React, { useCallback } from 'react';
import { Button, Typography } from '@material-ui/core';
import { BackgroundDiv, LoginButtonDiv, LoginDiv,LoginTitleDiv } from './login.style';
import { useMsal } from '@azure/msal-react';
import { azureConfiguration } from 'api/authentication';

export const Login: React.FC = () => {
    const imageUrl = `${process.env.PUBLIC_URL}/bg.svg`;

    const { instance } = useMsal();

    const handleLogin = useCallback(() => {
        try{
            instance.loginRedirect({ scopes: [ azureConfiguration.getMSALScopes() ]});
        } catch(error){
            console.log(error);
        }

    }, [instance])

    return (
        <BackgroundDiv style={{ backgroundImage: `url(${imageUrl})` }}>
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
                    <Button variant="contained" color="primary" fullWidth onClick={handleLogin}>
                        Login
                    </Button>
                </LoginButtonDiv>
            </LoginDiv>
        </BackgroundDiv>
    );
}
