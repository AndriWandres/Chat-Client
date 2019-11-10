import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap } from 'rxjs/operators';
import { ContactsService } from 'src/app/core/contacts/contacts.service';
import * as contactsActions from './actions';

@Injectable()
export class ContactsEffects {
  readonly loadContacts$ = createEffect(() => this.actions$.pipe(
    ofType(contactsActions.loadContacts),
    switchMap(() => this.contactsService.getContacts().pipe(
      map(contacts => contactsActions.loadContactsSuccess({ contacts })),
      catchError(error => of(contactsActions.loadContactsFailure({ error })))
    ))
  ));

  constructor(
    private readonly contactsService: ContactsService,
    private readonly actions$: Actions<contactsActions.ContactsActionUnion>,
  ) {}
}
