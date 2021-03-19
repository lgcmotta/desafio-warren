import React, { useState } from 'react';
import NumberFormat from 'react-number-format';
import { createStyles, Theme, makeStyles } from '@material-ui/core/styles';
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
import TextField from '@material-ui/core/TextField';
import FormControl from '@material-ui/core/FormControl'
import { Button, Paper, Typography } from '@material-ui/core';
import { DepositButtonDiv, DepositDiv, DepositMoneyInputDiv, DepositTyphographyDiv } from './deposit.styles';
import MoneyInput from '../common/money-input';
import { useSelector } from '../home.provider';
import { ITransactionResponse } from 'models/transaction';
import { postAsync } from 'api';
import ErrorAlert from '../common/error-alert';



const useStyles = makeStyles((theme: Theme) => ({
    root: {
      flexGrow: 1,
      backgroundColor: theme.palette.background.paper,
      marginTop: '10px',
      height: '100%',
      width: '100%',
      minHeight:'500px',
      display: 'flex',
      flexDirection:'column',
      justifyContent: 'flex-start',
      alignItems: 'center'
    }
  }));


export const Deposit: React.FC = () => {
    const [value, setValue] = useState('');
    const classes = useStyles();
    const { id } = useSelector(state => state.user)
    const [show, setShow] = useState(false);
    const [failures, setFailures] =useState<string>('');
    const handleDeposit = () => {
        const numberValue = parseFloat(value);

        async function postDeposit(){
            await postAsync<ITransactionResponse>(`/api/v1/accounts/${id.toString()}/deposit`, { value: numberValue }).then(response => {
                if(!response.failures.length){
                    setValue('');
                    return;
                }
            }).catch(err => {
                const failuresString = [...err.response.data.failures.map(failure => failure.errorMessage)].join(',\n');
                setFailures(failuresString);
                setShow(true)
            });
            
        }

        postDeposit();

    }

    return <DepositDiv>
            <Paper className={classes.root}>
                <DepositTyphographyDiv>
                    <Typography variant='h5'>How much do you want to deposit?</Typography>
                </DepositTyphographyDiv>
                <DepositMoneyInputDiv>
                    <MoneyInput onChange={setValue} value={value} />
                </DepositMoneyInputDiv>
                <DepositButtonDiv>
                    <Button variant="contained" color="primary" fullWidth onClick={handleDeposit}>Deposit</Button>
                </DepositButtonDiv>
            </Paper>'
            <ErrorAlert show={show} setShow={setShow} message={failures}/>
        </DepositDiv>
}