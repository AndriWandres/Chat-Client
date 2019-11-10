import { createAction, props, union } from '@ngrx/store';
import { Contact } from 'src/models/contacts/contact';

// Type constants
export enum ActionTypes {
  LOAD_CONTACTS = '[Contacts] Load Contacts',
  LOAD_CONTACTS_SUCCESS = '[Contacts] Load Success',
  LOAD_CONTACTS_FAILURE = '[Contacts] Load Failure',

  SET_ACTIVE_CONTACT = '[Contact] Set Active Contact',
}

// Contacts Load Actions
export const loadContacts = createAction(ActionTypes.LOAD_CONTACTS);
export const loadContactsSuccess = createAction(ActionTypes.LOAD_CONTACTS_SUCCESS, props<{ contacts: Contact[] }>());
export const loadContactsFailure = createAction(ActionTypes.LOAD_CONTACTS_FAILURE, props<{ error: any }>());

// Set Active Actions
export const setActiveContact = createAction(ActionTypes.SET_ACTIVE_CONTACT, props<{ contact: Contact }>());


// Union for all actions
const allActions = union({
  loadContacts,
  loadContactsSuccess,
  loadContactsFailure,

  setActiveContact,
});

export type ContactsActionUnion = typeof allActions;
