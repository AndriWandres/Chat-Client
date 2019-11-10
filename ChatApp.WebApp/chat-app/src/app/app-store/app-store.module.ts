import { NgModule } from '@angular/core';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from 'src/environments/environment';
import { AuthStoreModule } from './auth-store/auth-store.module';
import { ContactsStoreModule } from './contacts-store/contacts-store.module';

@NgModule({
  declarations: [],
  imports: [
    AuthStoreModule,
    ContactsStoreModule,
    StoreModule.forRoot({}),
    EffectsModule.forRoot([]),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production,
    }),
  ]
})
export class AppStoreModule { }
