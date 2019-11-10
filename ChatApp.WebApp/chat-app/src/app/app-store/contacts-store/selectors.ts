import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as appState from '../state';
import * as contactsState from './state';

export const contactsFeatureKey = 'contacts';
export const selectContactsFeature = createFeatureSelector<appState.State, contactsState.State>(contactsFeatureKey);

export const selectLoading = createSelector(
  selectContactsFeature,
  (state) => state.isLoading
);

export const selectError = createSelector(
  selectContactsFeature,
  (state) => state.error,
);

export const selectActiveContact = createSelector(
  selectContactsFeature,
  (state) => state.activeContact,
);

export const {
  selectAll,
  selectEntities,
  selectIds,
  selectTotal,
} = contactsState.contactsAdapter.getSelectors(selectContactsFeature);
