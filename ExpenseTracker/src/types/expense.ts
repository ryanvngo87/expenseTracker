export interface Expense {
  id: number;
  name: string;
  description: string | null;
  type: string;
  date: string; // ISO 8601 string from JSON serialization
}
