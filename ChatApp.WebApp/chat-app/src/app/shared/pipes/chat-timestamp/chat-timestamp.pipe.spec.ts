import { ChatTimestampPipe } from './chat-timestamp.pipe';

describe('ChatTimestampPipe', () => {
  const pipe = new ChatTimestampPipe();
  const today = new Date(2019, 9, 17);

  // Mock current day
  beforeEach(() => {
    jasmine.clock().mockDate(today);
  });

  describe('transform(date)', () => {
    it('should display the time', () => {
      // On current day
      const date = new Date(2019, 9, 17, 12, 35);
      const result = pipe.transform(date);

      expect(result).toBe('12:35');
    });

    it('should display yesterdays time', () => {
      // 1 day prior to current date
      const date = new Date(2019, 9, 16, 12, 35);
      const result = pipe.transform(date);

      expect(result).toBe('Yesterday, 12:35');
    });

    it('should display the weekday only', () => {
      // 2 days prior to current date
      const date = new Date(2019, 9, 15, 12, 35);
      const result = pipe.transform(date);

      expect(result).toBe('Tuesday');
    });

    it('should display the weekday until 6 days after', () => {
      // 6 days prior to current date
      const date = new Date(2019, 9, 11, 11, 22);
      const result = pipe.transform(date);

      expect(result).toBe('Friday');
    });

    it('should display the full date', () => {
      // 7 days prior to current date
      const date = new Date(2019, 9, 10, 12, 35);
      const result = pipe.transform(date);

      expect(result).toBe('07.10.2019');
    });
  });

  describe('getDifferenceInDays(d1, d2)', () => {
    it('should calculate the difference between two dates in days', () => {
      const d1 = new Date(2019, 9, 17);
      const d2 = new Date(2019, 9, 16);

      const difference = pipe['getDifferenceInDays'](d1, d2);

      expect(difference).toBe(1);
    });
  });

  describe('datesOnSameDay(d1, d2)', () => {
    it('should return true for 2 dates on the same day', () => {
      const d1 = new Date(2019, 9, 17, 7, 11);
      const d2 = new Date(2019, 9, 17, 22, 58);

      const result = pipe['datesOnSameDay'](d1, d2);

      expect(result).toBe(true);
    });

    it('should return false for 2 dates that are not on the same day', () => {
      const d1 = new Date(2019, 9, 17);
      const d2 = new Date(2019, 9, 16);

      const result = pipe['datesOnSameDay'](d1, d2);

      expect(result).toBe(false);
    });
  });
});
