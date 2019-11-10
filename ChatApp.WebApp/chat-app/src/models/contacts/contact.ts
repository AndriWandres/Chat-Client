
export interface Contact {
  messageRecipientId: number;
  isRead: boolean;
  unreadMessagesCount: number;

  message: LatestMessage;
  recipientUser?: ContactRecipientUser;
  recipientGroup?: ContactRecipientGroup;
}

interface LatestMessage {
  messageId: number;
  textContent: string;
  createdAt: Date;
  authorName: string;
}

interface ContactRecipientUser {
  userId: number;
  displayName: string;
}

interface ContactRecipientGroup {
  groupId: number;
  name: string;
}
