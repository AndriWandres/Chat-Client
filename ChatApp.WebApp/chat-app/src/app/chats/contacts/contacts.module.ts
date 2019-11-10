import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ContactsHeaderModule } from './contacts-header/contacts-header.module';
import { ContactsListModule } from './contacts-list/contacts-list.module';
import { ContactsComponent } from './contacts.component';

@NgModule({
  declarations: [ContactsComponent],
  imports: [
    CommonModule,
    ContactsHeaderModule,
    ContactsListModule,
  ],
  exports: [ContactsComponent]
})
export class ContactsModule { }
