import React, { createContext, useContext, useReducer } from 'react';
import { HomeActionTypes } from './home.actions';
import { reducer } from './home.reducer';
import { IHomeState, initialState } from './home.state';


interface IContextProps {
    state: IHomeState;
    dispatch: React.Dispatch<HomeActionTypes>;
}

const HomeContext = createContext({} as IContextProps);

export const useHomeContext = () => useContext(HomeContext);

export function useSelector<T>(selector: (state: IHomeState) => T) {

    const { state } = useHomeContext();

    return selector(state);
}

export const HomeProvider: React.FC = ({ children }) => {
    const [state, dispatch] = useReducer(reducer, initialState);

    const value = { state, dispatch };

    return <HomeContext.Provider value={value} children={children} />
}


