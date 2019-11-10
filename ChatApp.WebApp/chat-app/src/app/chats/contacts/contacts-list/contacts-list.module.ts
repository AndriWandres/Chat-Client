import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatDividerModule } from '@angular/material/divider';
import { ChatsRowModule } from '../contacts-row/contacts-row.module';
import { ContactsListComponent } from './contacts-list.component';

@NgModule({
  declarations: [ContactsListComponent],
  imports: [
    CommonModule,
    ChatsRowModule,
    MatDividerModule,
  ],
  exports: [ContactsListComponent],
})
export class ContactsListModule { }
