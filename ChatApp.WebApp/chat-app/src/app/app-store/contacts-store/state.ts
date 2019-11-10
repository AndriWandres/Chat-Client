import { Contact } from 'src/models/contacts/contact';
import { EntityState, createEntityAdapter } from '@ngrx/entity';

export interface State extends EntityState<Contact> {
  activeContact: Contact | null;
  isLoading: boolean;
  error: any;
}

export const contactsAdapter = createEntityAdapter<Contact>({
  selectId: (contact) => contact.messageRecipientId,
  sortComparer: (a, b) => new Date(a.message.createdAt).getTime() - new Date(b.message.createdAt).getTime()
});

export const initialState: State = contactsAdapter.getInitialState({
  activeContact: null,
  isLoading: false,
  error: null,
});
