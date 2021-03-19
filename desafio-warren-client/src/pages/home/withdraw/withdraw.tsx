import React, { useState } from 'react';
import NumberFormat from 'react-number-format';
import { createStyles, Theme, makeStyles } from '@material-ui/core/styles';
import Input from '@material-ui/core/Input';
import InputLabel from '@material-ui/core/InputLabel';
import TextField from '@material-ui/core/TextField';
import FormControl from '@material-ui/core/FormControl'
import { Button, Paper, Typography } from '@material-ui/core';
import { WithdrawButtonDiv, WithdrawDiv, WithdrawMoneyInputDiv, WithdrawTyphographyDiv } from './withdraw.styles';
import MoneyInput from '../common/money-input';
import { useSelector } from '../home.provider';
import { ITransactionResponse } from 'models/transaction';
import { putAsync } from 'api';
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


export const Withdraw: React.FC = () => {
    const [value, setValue] = useState('');
    const [show, setShow] = useState(false);
    const [failures, setFailures] =useState<string>('');
    const classes = useStyles();
    const { id } = useSelector(state => state.user)

    const handleWithdraw = () => {
        const numberValue = parseFloat(value);

        async function putWithdraw(){
            await putAsync<ITransactionResponse>(`/api/v1/accounts/${id.toString()}/withdraw`, { value: numberValue })
            .then(response => {
                if(!response.failures.length){
                    setValue('');
                }
            })
            .catch(err => {
                const failuresString = [...err.response.data.failures.map(failure => failure.errorMessage)].join(',\n');
                setFailures(failuresString);
                setShow(true)
            })

            
        }

        putWithdraw();

    }

    return <WithdrawDiv>
            <Paper className={classes.root}>
                <WithdrawTyphographyDiv>
                    <Typography variant='h5'>How much money do you want to withdraw?</Typography>
                </WithdrawTyphographyDiv>
                <WithdrawMoneyInputDiv>
                    <MoneyInput onChange={setValue} value={value} />
                </WithdrawMoneyInputDiv>
                <WithdrawButtonDiv>
                    <Button variant="contained" color="primary" fullWidth onClick={handleWithdraw}>Deposit</Button>
                </WithdrawButtonDiv>
            </Paper>
            <ErrorAlert show={show} setShow={setShow} message={failures}/>
        </WithdrawDiv>
}