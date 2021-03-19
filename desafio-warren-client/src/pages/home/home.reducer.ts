import { HomeActionType, HomeActionTypes } from "./home.actions";
import { IHomeState } from "./home.state";


export const reducer = (state: IHomeState, action: HomeActionTypes): IHomeState => {
    switch (action.type) {
        case HomeActionType.UserAcquired: {
            return { ...state, user: action.payload }
        }
        case HomeActionType.AccountBalanceChanged: {
            return {...state, user: {...state.user, balance: action.payload }}
        }
    }
}