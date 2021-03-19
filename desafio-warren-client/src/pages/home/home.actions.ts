import { IAccount } from "models/account";

export enum HomeActionType {
    UserAcquired = 'UserAcquired',
    AccountBalanceChanged = 'AccountBalanceChanged'
}

export interface ISetUserAccountInfo {
    type: HomeActionType.UserAcquired;
    payload: IAccount;
}

export interface IAccountBalanceChanged{
    type: HomeActionType.AccountBalanceChanged,
    payload: string;
}

export type HomeActionTypes = ISetUserAccountInfo | IAccountBalanceChanged;