import { Typography } from '@material-ui/core';
import React from 'react';
import { AccountDetailsDiv } from './account-details.styles';

interface IAccountDetailsProps{
    cpf: string;
    number: string;
    name: string;
    currency: string;
    email: string
}

export const AccountDetails: React.FC<IAccountDetailsProps> = (props: IAccountDetailsProps) => {
    const { currency, cpf, number, name, email } = props;

    return <AccountDetailsDiv>
        <Typography variant="overline">Name: {name}</Typography>
        <Typography variant="overline">Email: {email}</Typography>
        <Typography variant="overline">CPF: {cpf}</Typography>
        <Typography variant="overline">Account Number: {number}</Typography>
        <Typography variant="overline">Currency: {currency}</Typography>
    </AccountDetailsDiv>
}