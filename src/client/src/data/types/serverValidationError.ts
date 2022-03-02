export interface ServerValidationError<T> {
  errors: Record<keyof T, string[]>;
  type: string;
  title: string;
  status: number;
  traceId: string;
}
