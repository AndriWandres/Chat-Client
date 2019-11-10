import { AuthStoreState } from './auth-store';
import { ContactsStoreState } from './contacts-store';

export interface State {
  auth: AuthStoreState.State;
  contacts: ContactsStoreState.State;
}
