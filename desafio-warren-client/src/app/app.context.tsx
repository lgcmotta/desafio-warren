import React, { createContext, useContext } from 'react';

type AppContextData = {
}


const AppContext = createContext<AppContextData>({} as AppContextData);

const AppContextProvider: React.FC = ({ children }) => {
    return (
        <AppContext.Provider value={{ }}>
            {children}
        </AppContext.Provider>
    );
}

const useAppContext = (): AppContextData => {
    const context = useContext(AppContext);

    return context;
}

export { AppContextProvider, useAppContext }