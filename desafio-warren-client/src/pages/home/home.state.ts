import { IAccount } from "models/account";
import { ITransaction } from "models/transaction";

export interface IHomeState {
    user: IAccount;
    transactions: ITransaction[];
}

export const initialState: IHomeState = {
    user: {} as IAccount,
    transactions: []
}