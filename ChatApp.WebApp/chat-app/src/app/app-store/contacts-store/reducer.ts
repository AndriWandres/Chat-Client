import { Action, createReducer, on } from '@ngrx/store';
import * as contactsActions from './actions';
import { contactsAdapter, initialState, State } from './state';

const reducer = createReducer(
  initialState,

  // Load Contacts
  on(contactsActions.loadContacts, (state) => {
    return {
      ...state,
      isLoading: true,
      error: null,
    };
  }),
  on(contactsActions.loadContactsSuccess, (state, { contacts }) => {
    return contactsAdapter.addAll(contacts, {
      ...state,
      isLoading: false,
      error: null,
    });
  }),
  on(contactsActions.loadContactsFailure, (state, { error }) => {
    return {
      ...state,
      isLoading: false,
      error
    };
  }),

  // Set active Contact
  on(contactsActions.setActiveContact, (state, { contact }) => {
    return {
      ...state,
      activeContact: contact,
    };
  })
);

export function contactsReducer(state: State | undefined, action: Action): State {
  return reducer(state, action);
}
