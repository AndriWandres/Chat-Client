import { ChangeDetectionStrategy, Component, HostBinding, Input } from '@angular/core';
import { Contact } from 'src/models/contacts/contact';

@Component({
  selector: 'app-contacts-row',
  templateUrl: './contacts-row.component.html',
  styleUrls: ['./contacts-row.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ContactsRowComponent {
  @Input() contact: Contact;
  @Input() isActive = false;

  @HostBinding() tabindex = 0;
}
