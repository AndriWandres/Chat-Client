import { Component, OnInit, OnDestroy } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { AppStoreState } from 'src/app/app-store';
import { Subject } from 'rxjs';
import { ContactsStoreActions, ContactsStoreSelectors } from 'src/app/app-store/contacts-store';
import { takeUntil } from 'rxjs/operators';
import { Contact } from 'src/models/contacts/contact';

@Component({
  selector: 'app-contacts-list',
  templateUrl: './contacts-list.component.html',
  styleUrls: ['./contacts-list.component.scss'],
})
export class ContactsListComponent implements OnInit, OnDestroy {
  private readonly destroy$ = new Subject<void>();

  readonly contacts$ = this.store$.pipe(
    select(ContactsStoreSelectors.selectAll),
    takeUntil(this.destroy$),
  );

  readonly activeContact$ = this.store$.pipe(
    select(ContactsStoreSelectors.selectActiveContact),
    takeUntil(this.destroy$),
  );

  constructor(private readonly store$: Store<AppStoreState.State>) { }

  ngOnInit(): void {
    this.store$.dispatch(ContactsStoreActions.loadContacts());
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  setActiveContact(contact: Contact): void {
    this.store$.dispatch(ContactsStoreActions.setActiveContact({ contact }));
  }

  trackByFn(contact: Contact, index: number): number {
    return contact.messageRecipientId;
  }
}
