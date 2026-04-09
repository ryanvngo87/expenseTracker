import { useEffect, useState } from 'react';
import { Expense } from '../types/expense';
import './ExpenseGrid.css';

type FetchState =
  | { status: 'loading' }
  | { status: 'error'; message: string }
  | { status: 'success'; data: Expense[] };

function ExpenseGrid() {
  const [state, setState] = useState<FetchState>({ status: 'loading' });

  useEffect(() => {
    fetch('/api/expense')
      .then((res) => {
        if (!res.ok) throw new Error(`HTTP ${res.status}: ${res.statusText}`);
        return res.json() as Promise<Expense[]>;
      })
      .then((data) => setState({ status: 'success', data }))
      .catch((err: unknown) => {
        const message = err instanceof Error ? err.message : 'Unknown error';
        setState({ status: 'error', message });
      });
  }, []);

  if (state.status === 'loading') return <p className="grid-status">Loading expenses...</p>;
  if (state.status === 'error') return <p className="grid-status grid-error">Error: {state.message}</p>;

  const { data } = state;

  if (data.length === 0) return <p className="grid-status">No expenses found.</p>;

  return (
    <table className="expense-table">
      <thead>
        <tr>
          <th>ID</th>
          <th>Name</th>
          <th>Description</th>
          <th>Type</th>
          <th>Date</th>
        </tr>
      </thead>
      <tbody>
        {data.map((expense) => (
          <tr key={expense.id}>
            <td>{expense.id}</td>
            <td>{expense.name}</td>
            <td>{expense.description ?? '—'}</td>
            <td>{expense.type}</td>
            <td>{new Date(expense.date).toLocaleDateString()}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default ExpenseGrid;
