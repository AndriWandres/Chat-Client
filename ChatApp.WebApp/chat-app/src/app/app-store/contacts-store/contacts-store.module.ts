import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { ContactsEffects } from './effects';
import { contactsReducer } from './reducer';
import { contactsFeatureKey } from './selectors';

@NgModule({
  imports: [
    StoreModule.forFeature(contactsFeatureKey, contactsReducer),
    EffectsModule.forFeature([ContactsEffects]),
  ],
  providers: [ContactsEffects]
})
export class ContactsStoreModule { }
