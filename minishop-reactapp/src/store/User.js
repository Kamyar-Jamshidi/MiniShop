import { createStore } from "redux";

const initState = {
    firstName: '',
    lastName: '',
    isSuperAdmin: false
};

const rootReducer = (state = initState, action) => {
    if (action.type === 'SET_DATA')
        return {
            firstName: action.data.firstName,
            LastName: action.data.lastName,
            isSuperAdmin: action.data.isSuperAdmin
        };

    if (action.type === 'UNSET_DATA')
        return initState;

    return state;
}

const store = createStore(rootReducer);

export default store;